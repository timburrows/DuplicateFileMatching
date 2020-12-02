using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace DuplicateFileMatching.Core
{
    public class FileService : IFileService
    {
        public bool IsZipArchive(string path) => 
            new [] {".zip", ".tar", ".7z", ".rar"}.Contains(Path.GetExtension(path).ToLowerInvariant());
        
        public bool IsFileOfTypeImage(string path)
        {
            var ext = Path.GetExtension(path);
            if (string.IsNullOrEmpty(ext)) return false;
            
            var supportedImageCodecs = new List<string>();
            foreach (var imageEncoder in ImageCodecInfo.GetImageEncoders())
            {
                if (imageEncoder.FilenameExtension != null)
                {
                    supportedImageCodecs.AddRange(imageEncoder.FilenameExtension.Split(";"));
                }
            }

            return supportedImageCodecs.Contains($"*{ext.ToUpperInvariant()}");
        }
        
        public void UnZip(string path, string destinationPath)
        {
            if (string.IsNullOrEmpty(destinationPath))
            {
                destinationPath = path.Replace(Path.GetExtension(path), string.Empty);
            }

            var hasEntries = false;
            try
            {
                using (var zip = ZipFile.OpenRead(path))
                {
                    hasEntries = zip.Entries.Any();
                }
            }
            catch (InvalidDataException ex)
            {
                // todo: use ILogger
            }
            
            if (hasEntries)
            {
                ZipFile.ExtractToDirectory(path, destinationPath, true);
            }
        }
    }
}