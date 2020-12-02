using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DuplicateFileMatching.Core;

namespace DuplicateFileMatching.ConsoleApp
{
    public class AppHost : IAppHost
    {
        private readonly IFileService _fileService;
        private readonly IBitmapManipulation _bitmapManipulation;
        private readonly IBitmapComparison _bitmapComparison;

        public AppHost(IFileService fileService, IBitmapComparison bitmapComparison, IBitmapManipulation bitmapManipulation)
        {
            _fileService = fileService;
            _bitmapComparison = bitmapComparison;
            _bitmapManipulation = bitmapManipulation;
        }

        public void Run(string[] args)
        {
            // Note that for a published CLI I would prefer to rely on args over interactive user input.
            // I would also likely utilize a library such as CommandLineParser
            Console.WriteLine("DuplicateFileMatching is an interactive command line tool\n" +
                              "for recursively searching a directory or zipped archive for identical,\n" + 
                              "or almost identical images. To use the tool, please provide a path.\n\n" +
                              "Example usage:\n - C:\\My Documents\\Images.zip\n\n - C:\\My Documents\\ImageFolder\n");


            var inputPath = PromptForImagesPath();
            
            var imagesDir = string.Empty;
            Console.WriteLine("Determining if path is archive or directory...");
            imagesDir = _fileService.IsZipArchive(inputPath) 
                ? UnzipArchive(inputPath) 
                : inputPath;

            var matchesFound = FindMatchingImages(BuildImageList(imagesDir));
            foreach (var match in matchesFound)
            {
                Console.WriteLine($"Found duplicate images:\n {match.ImagePath1}\n{match.ImagePath2}\n");
            }

            Console.WriteLine($"Total duplicates found: {matchesFound.Count}");
        }

        private IList<(string ImagePath1, string ImagePath2)> FindMatchingImages(IReadOnlyList<(Bitmap Bitmap, string Path)> images)
        {
            var matchingImages = new List<(string, string)>();
            for (var i = 0; i < images.Count - 1; i++)
            {
                for (var j = i + 1; j < images.Count; j++)
                {
                    if (matchingImages.Any(x => x.Item1 == images[j].Path || x.Item2 == images[j].Path)) continue;
                    if (!_bitmapComparison.CompareBitmaps(images[i].Bitmap, images[j].Bitmap)) continue;
                    
                    matchingImages.Add((images[i].Path, images[j].Path));
                }
            }

            return matchingImages;
        }

        private List<(Bitmap Bitmap, string Path)> BuildImageList(string imagesDir)
        {
            var imageFiles = Directory.GetFiles(imagesDir, string.Empty, SearchOption.AllDirectories);
            var images = new List<(Bitmap Bitmap, string Path)>();
            foreach (var path in imageFiles)
            {
                if (!_fileService.IsFileOfTypeImage(path)) continue;
                
                var bitmap = new Bitmap(path);
                bitmap = _bitmapManipulation.ShrinkImage(bitmap);
                bitmap = _bitmapManipulation.ToGreyscale(bitmap);
                images.Add((bitmap, path));
            }

            return images;
        }

        private static string PromptForImagesPath()
        {
            var tryCount = 0;

            while (true)
            {
                if (tryCount > 5)
                {
                    Console.WriteLine("5 attempts have already failed..bailing out.");
                    Environment.Exit(1);
                }
                
                Console.Write("Path: ");
                var path = Console.ReadLine();
                
                if (path == "q") Environment.Exit(0);
                
                if (string.IsNullOrEmpty(path) || !Directory.Exists(path) && !File.Exists(path))
                {
                    Console.WriteLine("The path you provided was invalid." +
                                      "Please check and try again, or type 'q' to quit.\n");
                }

                tryCount++;
                return path;
            }
        }
        
        private string UnzipArchive(string path)
        {
            Console.WriteLine("Compressed archive detected, press enter to unzip here, or specify a preferred" +
                              " directory. Both options will create a new folder.");

            Console.Write("Destination path: ");
            var destPath = Console.ReadLine();
            _fileService.UnZip(path, destPath);

            path = !string.IsNullOrEmpty(destPath)
                ? destPath
                : path.Replace(Path.GetExtension(path), string.Empty);

            Console.WriteLine("Finished unpacking zip archive");

            return path;
        }
    }
}