namespace DuplicateFileMatching.Core
{
    public interface IFileService
    {
        bool IsZipArchive(string path);
        bool IsFileOfTypeImage(string path);
        void UnZip(string path, string destinationPath);
    }
}