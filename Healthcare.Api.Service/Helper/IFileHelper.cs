namespace Healthcare.Api.Service.Helper
{
    public interface IFileHelper
    {
        string ReadAllText(string filePath);
        string GetExecutingDirectory();
        Task CopyFile(string sourceFileName, string destFileName);
        Stream OpenRead(string filePath);
        FileStream NewFileStreamOpenRead(string filePdf);
        void DeleteFile(string filePath);
    }
}
