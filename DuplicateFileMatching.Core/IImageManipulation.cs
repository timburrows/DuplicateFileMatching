using System.Drawing;

namespace DuplicateFileMatching.Core
{
    public interface IImageManipulation
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
        /// <param name="img">An Image object with the loaded image file</param>
        /// <returns>A new Bitmap with the relevant size adjustments</returns>
        Bitmap ShrinkImage(Image img);
        
        /// <summary>
        /// Converts a Bitmap to it's Greyscale Equivalent
        /// </summary>
        /// <param name="img">The original Bitmap to execute the conversion on</param>
        Bitmap ToGreyscale(Image img);
    }
}