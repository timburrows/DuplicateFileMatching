using System;
using System.IO;
using Xunit;

namespace DuplicateFileMatching.Test.Unit
{
    public class ConsoleAppUnitTests
    {
        [Fact]
        public void Run_PrintsCorrectOutput()
        {
            // Arrange
            var sut = new ConsoleApp.AppHost();
            const string expectedOutput = "Hello World";
            string actualOutput;
            
            // Act
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                sut.Run();
                writer.Flush();

                actualOutput = writer.GetStringBuilder().ToString();
            }
            
            // Assert
            Assert.Contains(expectedOutput, actualOutput);
        }
    }
}