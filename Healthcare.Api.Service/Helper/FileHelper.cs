using Healthcare.Api.Core.ServiceInterfaces;
using System.Reflection;

namespace Healthcare.Api.Service.Helper
{
    public class FileHelper : IFileHelper
    {
        public string GetExecutingDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        public string ReadAllText(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public async Task CopyFile(string sourceFileName, string destFileName)
        {
            File.Copy(sourceFileName, destFileName);
        }

        public Stream OpenRead(string filePath)
        {
            return File.OpenRead(filePath);
        }

        public FileStream NewFileStreamOpenRead(string filePdf)
        {
            return new FileStream(filePdf, FileMode.Create);
        }

        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }
    }
}
