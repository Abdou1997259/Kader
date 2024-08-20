using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.HTTP;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Kader_System.Services.Services.AppServices
{
    public class FileServer : IFileServer
    {
        private readonly string serverPath = string.Empty;
        private readonly IHttpContextService _httpContextService;
        private readonly IRequestService _requestService;
        public FileServer(IHttpContextService httpContextService, IRequestService requestService)
        {
            _httpContextService = httpContextService;
            serverPath = _httpContextService.GetPhysicalServerPath();
            _requestService = requestService;
        }

        public async Task<FileStreamResult> DownloadFileAsync(params string [] fileParts)
        {
            var filePath = Path.Combine(serverPath, Path.Combine(fileParts));
            var fileName = Path.GetFileName(filePath);
            var memory = new MemoryStream();
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            if (stream != null)
            {
                await stream.CopyToAsync(memory);
                memory.Position = 0;
                var contentType = GetContentType(filePath);
                return new FileStreamResult(memory, contentType)
                {
                    FileDownloadName = fileName,
                };
            }
            return null;
        }

        //private readonly FileStream _fileStream;
        public bool FileExist(string moduleName, string fileName)
        {
            var filePath = Path.Combine(serverPath, moduleName, fileName);
            return File.Exists(filePath);
        }

        public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }

        public string GetFilePath(params string[] paths)
        {
            if (paths.Any(p => p == null))
                return null;
            return Path.Combine(paths);
        }

        public string GetFilePathWithServerPath(params string[] paths)
        {
            return Path.Combine(serverPath, Path.Combine(paths));
        }

        public void RemoveFile(params string[] paths)
        {
            var filePath = Path.Combine(serverPath, Path.Combine(paths));
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


        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
        {
            {".txt", "text/plain"},
            {".pdf", "application/pdf"},
            {".doc", "application/vnd.ms-word"},
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif", "image/gif"},
            {".csv", "text/csv"}
            };
        }
    }
}
