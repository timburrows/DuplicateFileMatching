using System.Drawing;

namespace DuplicateFileMatching.Core
{
    public interface IBitmapManipulation
    {
        /// <summary>
        /// Shrinks an Image to the smallest possible aspect ratio. (Min width/height = 16)
        /// </summary>
        /// <param name="path">Absolute path to the image file</param>
        /// <returns>A new Bitmap with the relevant size adjustments</returns>
        Bitmap ShrinkImage(string path);

        /// <summary>
        /// Shrinks an Image to the smallest possible aspect ratio. (Min width/height = 16)
        /// </summary>
        /// <param name="bmp">An Image object with the loaded image file</param>
        /// <returns>A new Bitmap with the relevant size adjustments</returns>
        Bitmap ShrinkImage(Bitmap bmp);

        /// <summary>
        /// Converts a Bitmap to it's Greyscale Equivalent
        /// </summary>
        /// <param name="bmp">The original Bitmap to execute the conversion on</param>
        Bitmap ToGreyscale(Bitmap bmp);
    }
}