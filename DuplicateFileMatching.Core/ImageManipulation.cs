using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace DuplicateFileMatching.Core
{
    public class ImageManipulation : IImageManipulation
    {
        public Bitmap ShrinkImage(string path)
        {
            var img = Image.FromFile(path);
            return DoShrinkImage(img);
        }
        
        public Bitmap ShrinkImage(Image img)
        {
            return DoShrinkImage(img);
        }

        private static Bitmap DoShrinkImage(Image img)
        {
            const double min = 16;
            
            var minAspectRatio = Math.Min(img.Width / min, img.Height / min);
            var newWidth = (int) Math.Round(img.Width / minAspectRatio);
            var newHeight = (int) Math.Round(img.Height / minAspectRatio);
            
            var resizedImage = new Bitmap(img, new Size(newWidth, newHeight));

            using (var gfx = Graphics.FromImage(resizedImage))
            {
                gfx.DrawImage(img, 0, 0, newWidth, newHeight);
            }
            
            img.Dispose();
            return resizedImage;
        }

        public Bitmap ToGreyscale(Image img)
        {
            var output = new Bitmap(img.Width, img.Height);

            using var imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(ColorMatrices.GreyscaleMat5);
            
            using (var gfx = Graphics.FromImage(output))
            {
                gfx.DrawImage(
                    img,
                    Rectangle.FromLTRB(0, 0, img.Width, img.Height),
                    0, 0, img.Width, img.Height, GraphicsUnit.Pixel,
                    imgAttributes
                );
            }

            img.Dispose();
            return output;
        }
    }
}