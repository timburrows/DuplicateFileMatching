using System.Collections.Generic;
using System.Drawing;

namespace DuplicateFileMatching.Core
{
    public interface IImageComparison
    {
        /// <summary>
        /// Gets a collection of individual pixels representing the image of a Bitmap.
        /// </summary>
        /// <param name="img"></param>
        /// <returns>An enumerable collection of type <see cref="Color"/></returns>
        IEnumerable<Color> GetPixels(Bitmap img);
    }
}