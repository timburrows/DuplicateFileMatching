using System.Drawing.Imaging;

namespace DuplicateFileMatching.Core
{
    public static class ColorMatrices
    {
        /// <summary>
        /// A 5x5 Black and White ColorMatrix transform
        /// </summary>
        /// <remarks>ref: https://docs.rainmeter.net/tips/colormatrix-guide/</remarks>
        public static ColorMatrix BlackAndWhiteMat5 =>
            new ColorMatrix(
                new[]
                {
                    new[] {1.5f, 1.5f, 1.5f, 0, 0},
                    new[] {1.5f, 1.5f, 1.5f, 0, 0},
                    new[] {1.5f, 1.5f, 1.5f, 0, 0},
                    new[] {0f, 0, 0, 1, 0},
                    new[] {-1f, -1, -1, 0, 1}
                }
            );
        
        /// <summary>
        /// A 5x5 Greyscale ColorMatrix transform
        /// </summary>
        /// <remarks>
        /// ref: https://docs.rainmeter.net/tips/colormatrix-guide/
        /// note: much less lossy then BlackAndWhite
        /// </remarks>
        public static ColorMatrix GreyscaleMat5 =>
            new ColorMatrix(
                new[]
                {
                    new[] {0.33f, 0.33f, 0.33f, 0, 0},
                    new[] {0.59f, 0.59f, 0.59f, 0, 0},
                    new[] {0.11f, 0.11f, 0.11f, 0, 0},
                    new[] {0f, 0, 0, 1, 0},
                    new[] {0f, 0, 0, 0, 1}
                }
            );
    }
}