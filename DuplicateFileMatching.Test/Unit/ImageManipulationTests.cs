using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DuplicateFileMatching.Core;
using Xunit;
// ReSharper disable xUnit1026

namespace DuplicateFileMatching.Test.Unit
{
    public class ImageManipulationTests
    {
        private readonly IImageManipulation _sut = new ImageManipulation();
        
        [Theory]
        [InlineData("AspectRatioDisproportionate", "/Dec 2016/scary bear.JPG", 24, 16)]
        [InlineData("AspectRatioProportionate", "/random cats I saw/afraid i can't do that dave.jpg", 16, 16)]
        private void ShrinkImage_RetainsCorrectAspectRatio(string when, string imagePath, int expectedHeight, int expectedWidth)
        {
            // Arrange
            var testImage = $"{Directory.GetCurrentDirectory()}/TestImages/{imagePath}";

            // Act
            var resizedImage = _sut.ShrinkImage(testImage);

            // Assert
            Assert.Equal(expectedHeight, resizedImage.Height);
            Assert.Equal(expectedWidth, resizedImage.Width);
        }
        
        [Theory]
        [InlineData("/Dec 2016/scary bear.JPG", true, true)]
        [InlineData("/Dec 2016/scary bear.JPG", false, false)]
        [InlineData("/germany/staring contest.jpg", true, true)]
        private void ToBlackAndWhite_PixelsAreCorrectColor(string imagePath, bool isImageConverted, bool expectedResult)
        {
            // Arrange
            var testBmp = Image.FromFile($"{Directory.GetCurrentDirectory()}/TestImages/{imagePath}") as Bitmap;
            var actualResult = true;

            // Act
            var image = isImageConverted
                ? _sut.ToGreyscale(testBmp)
                : testBmp;

            unsafe
            {
                var data = image.LockBits(
                    Rectangle.FromLTRB(0, 0, image.Width, image.Height),
                    ImageLockMode.ReadOnly, image.PixelFormat
                );
            
                var pixels = (int*)data.Scan0;
                for (var i = 0; i < data.Height * data.Width; i++)
                {
                    var color = Color.FromArgb(pixels[i]);
                    if (color.A != 0 && (color.R != color.G || color.G != color.B))
                    {
                        actualResult = false;
                        break;
                    }
                }
                
                image.UnlockBits(data);
            }
            
            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}