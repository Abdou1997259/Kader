using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.HTTP;

namespace Kader_System.Services.Services.AppServices
{
    public class FileServer: IFileServer
    {
        private readonly string serverPath = string.Empty;
        private readonly IHttpContextService _httpContextService;
        public FileServer(IHttpContextService httpContextService)
        {
            _httpContextService = httpContextService;
            serverPath = _httpContextService.GetPhysicalServerPath();
        }
        //private readonly FileStream _fileStream;
        public bool FileExist(string moduleName, string fileName)
        {
            var filePath = Path.Combine(serverPath, moduleName, fileName);
            return File.Exists(filePath);
        }

        public string GetFilePath(params string[] paths)
        {
            return Path.Combine(paths);
        }
        public void RemoveFile(string moduleName,string fileName)
        {
            var filePath = Path.Combine(serverPath,moduleName, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
        public async Task<string> UploadFile(string moduleName, IFormFile file)
        {
            #region Directory_Validation
            var clientPath = Path.Combine(serverPath, moduleName);
            if (!Directory.Exists(clientPath))
                Directory.CreateDirectory(clientPath);

            #endregion

            #region FileCreation
            var fileEXE = Path.GetExtension(file.FileName);
            var newFileName = Guid.NewGuid().ToString() + fileEXE;
            var filefullPath = Path.Combine(clientPath, newFileName);
            using FileStream fileStream = new(filefullPath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return newFileName;
            #endregion
        }
    }
}
