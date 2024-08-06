using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.DataAccess.Repositories
{
    public class FileServer : IFileServer
    {
        //private readonly FileStream _fileStream;
        public FileServer()
        {
        }

        public void RemoveFile(string FolderPath, string filename)
        {
            var fullPath = Path.Combine(FolderPath, filename);
            if (File.Exists(fullPath))
                File.Delete(fullPath);
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
