using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace DuplicateFileMatching.Core
{
    public class ImageComparison : IImageComparison
    {
        // This method replaces the .NET Bitmap.GetPixel method
        // which is far too slow for iterating over any sizeable Bitmap. 
        public unsafe IEnumerable<Color> GetPixels(Bitmap img)
        {
            var output = new List<Color>();

            var data = img.LockBits(
                Rectangle.FromLTRB(0, 0, img.Width, img.Height),
                ImageLockMode.ReadOnly, img.PixelFormat
            );

            var pixelByteSize = Image.GetPixelFormatSize(img.PixelFormat) / 8;
                
            for (var i = 0; i < data.Height; i++)
            {
                var row = (byte*) data.Scan0 + i * data.Stride;
                for (var j = 0; j < data.Width; j++)
                {
                    output.Add(Color.FromArgb(row[j * pixelByteSize]));
                }
            }

            img.UnlockBits(data);
            img.Dispose();

            return output;
        }
    }
}