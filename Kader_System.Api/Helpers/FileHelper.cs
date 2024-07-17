namespace Kader_System.Api.Helpers
{
    public class FileHelper : IDisposable
    {
        private readonly FileStream _fileStream;
        public FileHelper(FileStream fileStream)
        {
            _fileStream = fileStream;
        }
        public string UploadFile(string clinetName, IFormFile file)
        {

        }

        public void Dispose() => _fileStream.Dispose();
    }
}
