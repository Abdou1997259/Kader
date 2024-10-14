using Kader_System.Services.IServices.AppServices;
using Kader_System.Services.IServices.HTTP;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace Kader_System.Services.Services.AppServices
{
    public class FileServer : IFileServer
    {
        private readonly string serverPath = string.Empty;
        private readonly IHttpContextService _httpContextService;
        private readonly IRequestService _requestService;
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        public FileServer(IHttpContextService httpContextService, IRequestService requestService, IStringLocalizer<SharedResource> stringLocalizer)
        {
            _httpContextService = httpContextService;
            serverPath = _httpContextService.GetPhysicalServerPath();
            _requestService = requestService;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<byte[]> GetFileBytes(params string[] fileParts)
        {
            if (fileParts.Any(string.IsNullOrEmpty) || fileParts is null)
                return null;

            var filePath = Path.Combine(serverPath, Path.Combine(fileParts));
            var fileBytes = await File.ReadAllBytesAsync(filePath);
            return fileBytes;
        }
        //private readonly FileStream _fileStream;
        public bool FileExist(string moduleName, string fileName)
        {
            var filePath = Path.Combine(serverPath, moduleName, string.IsNullOrEmpty(fileName) ? "" : fileName);
            return File.Exists(filePath);
        }

        public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }

        public string CombinePath(params string[] paths)
        {
            var leftPath = string.Empty;
            if (paths.Any(string.IsNullOrEmpty) || paths is null)
            {
                var leftPathArr = paths?.Where(x => !string.IsNullOrEmpty(x)).Select(x => x).ToArray();
                leftPath = Path.Combine(leftPathArr);
                #region DefaultPath
                if (leftPath.Contains("User"))
                    return Path.Combine(leftPath, "Doctor.png");
                else if (leftPath.Contains("News"))
                    return Path.Combine(leftPath, "News.jpg");
                else if (leftPath.Contains("Blog"))
                    return Path.Combine(leftPath, "Blog.jpg");
                else if (leftPath.Contains("Course"))
                    return Path.Combine(leftPath, "Course.jpg");
                else
                    return Path.Combine("default.png");

                #endregion
            }

            leftPath = Path.Combine(paths);
            return leftPath;

        }

        public string CombinePathWithServerPath(params string[] paths)
        {
            if (paths.Any(string.IsNullOrEmpty) || paths is null)
                return _stringLocalizer[Localization.PathNotFound];
            return Path.Combine(serverPath, Path.Combine(paths));
        }

        public void RemoveFile(params string[] paths)
        {
            if (paths.Any(string.IsNullOrEmpty) || paths is null)
                return;

            var filePath = Path.Combine(serverPath, Path.Combine(paths));
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
        public int RemoveFiles(string ModuleName, List<string> fileNames)
        {
            var fileCount = 0;
            if (fileNames is null)
                return 0;

            foreach (var file in fileNames)
            {
                if (file is null)
                    continue;
                var filePath = Path.Combine(serverPath, ModuleName, file);
                if (File.Exists(filePath))
                    File.Delete(filePath);

                fileCount++;
            }
            return fileCount;
        }
        public void RemoveDirectory(string folderName)
        {
            var directoryPath = Path.Combine(serverPath, folderName);
            if (Directory.Exists(directoryPath))
                Directory.Delete(directoryPath, true);

        }

        public async Task<string> UploadFileAsync(string moduleName, IFormFile file)
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
        public string GetFileEXE(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;
            return Path.GetExtension(fileName);
        }
        public async Task<List<GetFileNameAndExtension>> UploadFilesAsync(string moduleName, IFormFileCollection files)
        {
            List<GetFileNameAndExtension> fileNames = new();
            for (int i = 0; i < files.Count; i++)
            {
                if (files[i] is null)
                    fileNames.Add(new GetFileNameAndExtension { FileName = null, FileExtension = null });

                var fileName = await UploadFileAsync(moduleName, files[i]);
                fileNames.Add(new GetFileNameAndExtension { FileName = fileName, FileExtension = Path.GetExtension(fileName) });
            }
            return fileNames;
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

        public string GetFilenameFromPath(params string[] paths)
        {
            var fullPath = CombinePath(paths);
            if (File.Exists(fullPath))
                return Path.GetFileName(fullPath);
            else
                return string.Empty;
        }

        public async Task<FileContentResult> DownloadFileAsync(string module, string fileName)
        {
            var filePath = CombinePath(serverPath, module, fileName);
            var fileBytes = await GetFileBytes(filePath);
            var contentType = GetContentType(fileName);
            return new FileContentResult(fileBytes, contentType)
            {
                FileDownloadName = fileName
            };
        }

        public void CopyFile(string sourceFilePath, string destFilePath)
        {
            var desDir = Path.GetDirectoryName(destFilePath);
            if (!Directory.Exists(desDir))
                Directory.CreateDirectory(desDir);

            if (File.Exists(sourceFilePath))
            {
                File.Copy(sourceFilePath, destFilePath);
            }
        }

        public async Task CompressFileAsync(string sourceFilePath)
        {
            try
            {
                var full_path = CombinePathWithServerPath($"Compress_{GetFilenameFromPath(sourceFilePath)}" + ".gz");
                using FileStream sourceStream = new(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.None);
                using FileStream compressedStream = new(full_path, FileMode.Create, FileAccess.Write, FileShare.None);
                using GZipStream compressionStream = new(compressedStream, CompressionMode.Compress);
                await sourceStream.CopyToAsync(compressionStream);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void CreateNewFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }

        }
        public double GetFileSizeInKB(IFormFile file)
        {
            var size = 0.0;
            var Size_IN_KB = 1024;
            if (file is not null)
            {
                var fileSize = (file.Length) / Size_IN_KB;
                return fileSize;
            }
            return size;
        }
        public double GetFileSizeInMB(IFormFile file)
        {
            var size = 0.0;
            var Size_IN_MB = 1024 * 1024;
            if (file is not null)
            {
                var fileSize = (file.Length) / Size_IN_MB;
                return fileSize;
            }
            return size;
        }
        public double GetFileSizeInGB(IFormFile file)
        {
            var size = 0.0;
            var Size_IN_GB = 1024 * 1024 * 1024;
            if (file is not null)
            {
                var fileSize = (file.Length) / Size_IN_GB;
                return fileSize;
            }
            return size;
        }
    }
}
