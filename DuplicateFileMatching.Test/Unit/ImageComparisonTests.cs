using System;
using System.Drawing;
using System.IO;
using System.Linq;
using DuplicateFileMatching.Core;
using Xunit;

namespace DuplicateFileMatching.Test.Unit
{
    public class ImageComparisonTests
    {
        private readonly IImageComparison _sut = new ImageComparison();

        [Theory]
        [InlineData("Dec 2016/scary bear.JPG", "Dec 2016/scary bear.JPG", true)]
        [InlineData("camping DELETE ME/tent.jpg", "camping DELETE ME/likehome_away_from_home.jpg", false)]
        public void GetPixels_WhenPixelsCompared_ResultWithinTolerance(string image1, string image2, bool expectedMatch)
        {
            // Arrange
            const int tolerancePct = 90;

            var img1 = new Bitmap($"{Directory.GetCurrentDirectory()}/TestImages/{image1}");
            var img2 = new Bitmap($"{Directory.GetCurrentDirectory()}/TestImages/{image2}");
            
            var maxPixels = img1.Width * img1.Height;
            
            // Act
            var pixels1 = _sut.GetPixels(img1);
            var pixels2 = _sut.GetPixels(img2);

            var numMatchingPixels = pixels1.Zip(pixels2, (i, j) => i.ToArgb() == j.ToArgb()).Count(eq => eq);
            var pctMatchingPixels = (int)Math.Round(numMatchingPixels * 100.0 / maxPixels);
            
            // Assert
            Assert.True(expectedMatch == pctMatchingPixels >= tolerancePct);
        }
    }
}