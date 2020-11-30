using System.Drawing;

namespace DuplicateFileMatching.Core
{
    public static class ImageManipulationExtensions
    {
        public static Bitmap ShrinkImage(this IImageManipulation imageManipulation, string path) => 
            imageManipulation.ShrinkImage(path);

        public static Bitmap ShrinkImage(this IImageManipulation imageManipulation, Image img) => 
            imageManipulation.ShrinkImage(img);

        public static Bitmap ToGreyscale(this IImageManipulation imageManipulation, Image img) => 
            imageManipulation.ToGreyscale(img);
    }
}