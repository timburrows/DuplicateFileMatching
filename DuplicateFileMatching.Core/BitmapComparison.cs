using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace DuplicateFileMatching.Core
{
    public class BitmapComparison : IBitmapComparison
    {
        // This method replaces the .NET Bitmap.GetPixel method
        // which is far too slow for iterating over any sizeable Bitmap. 
        public unsafe IEnumerable<Color> GetPixels(Bitmap bmp)
        {
            var output = new List<Color>();

            var data = bmp.LockBits(
                Rectangle.FromLTRB(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly, bmp.PixelFormat
            );

            var pixelByteSize = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;

            for (var i = 0; i < data.Height; i++)
            {
                var row = (byte*) data.Scan0 + i * data.Stride;
                for (var j = 0; j < data.Width; j++)
                {
                    output.Add(Color.FromArgb(row[j * pixelByteSize]));
                }
            }

            bmp.UnlockBits(data);
            return output;
        }

        public bool CompareBitmaps(Bitmap bmp1, Bitmap bmp2)
        {
            const int tolerancePct = 90;

            var pixels1 = GetPixels(bmp1);
            var pixels2 = GetPixels(bmp2);

            var maxPixels = Math.Max(bmp1.Width * bmp1.Height, bmp2.Width * bmp2.Height);

            var numMatchingPixels = pixels1
                .Zip(pixels2, (left, right) => left.ToArgb() == right.ToArgb())
                .Count(x => x);

            var pctMatchingPixels = (int) Math.Round(numMatchingPixels * 100.0 / maxPixels);
            return pctMatchingPixels >= tolerancePct;
        }
    }
}