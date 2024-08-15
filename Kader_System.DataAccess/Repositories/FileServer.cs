namespace Kader_System.DataAccess.Repositories
{
    public class FileServer : IFileServer
    {
        //private readonly FileStream _fileStream;
        public FileServer()
        {
        }

        public bool FileExist(string appPath, string moduleName, string fileName)
        {
            var filePath = Path.Combine(appPath, moduleName, fileName);
            return File.Exists(filePath);
        }

        public string GetFilePath(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public void RemoveFile(string FolderPath, string filename)
        {
            var fullPath = Path.Combine(FolderPath, filename);
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
        public void RemoveFile(string appPath, string moduleName, string fileName)
        {
            var filePath = Path.Combine(appPath, moduleName, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
        public async Task<string> UploadFile(string appPath, string moduleName, IFormFile file)
        {
            #region Directory_Validation
            var clientPath = Path.Combine(appPath, moduleName);
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
