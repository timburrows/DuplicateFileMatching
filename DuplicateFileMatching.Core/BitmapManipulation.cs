using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace DuplicateFileMatching.Core
{
    public class BitmapManipulation : IBitmapManipulation
    {
        public Bitmap ShrinkImage(string path)
        {
            var img = Image.FromFile(path) as Bitmap;
            return DoShrinkImage(img);
        }
        
        public Bitmap ShrinkImage(Bitmap bmp)
        {
            return DoShrinkImage(bmp);
        }

        private static Bitmap DoShrinkImage(Bitmap bmp)
        {
            const double min = 16;
            
            var minAspectRatio = Math.Min(bmp.Width / min, bmp.Height / min);
            var newWidth = (int) Math.Round(bmp.Width / minAspectRatio);
            var newHeight = (int) Math.Round(bmp.Height / minAspectRatio);
            
            var resizedImage = new Bitmap(bmp, new Size(newWidth, newHeight));

            using (var gfx = Graphics.FromImage(resizedImage))
            {
                gfx.DrawImage(bmp, 0, 0, newWidth, newHeight);
            }
            
            bmp.Dispose();
            return resizedImage;
        }

        public Bitmap ToGreyscale(Bitmap bmp)
        {
            var output = new Bitmap(bmp.Width, bmp.Height);

            using var imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(ColorMatrices.GreyscaleMat5);
            
            using (var gfx = Graphics.FromImage(output))
            {
                gfx.DrawImage(
                    bmp,
                    Rectangle.FromLTRB(0, 0, bmp.Width, bmp.Height),
                    0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel,
                    imgAttributes
                );
            }

            bmp.Dispose();
            return output;
        }
    }
}