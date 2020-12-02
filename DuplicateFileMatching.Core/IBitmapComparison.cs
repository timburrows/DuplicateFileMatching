using System.Collections.Generic;
using System.Drawing;

namespace DuplicateFileMatching.Core
{
    public interface IBitmapComparison
    {
        /// <summary>
        /// Gets a collection of individual pixels representing the image of a Bitmap.
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns>An enumerable collection of type <see cref="Color"/></returns>
        IEnumerable<Color> GetPixels(Bitmap bmp);

        /// <summary>
        /// Compare two Bitmaps for equality.
        /// </summary>
        /// <param name="bmp1"></param>
        /// <param name="bmp2"></param>
        /// <returns></returns>
        bool CompareBitmaps(Bitmap bmp1, Bitmap bmp2);
    }
}