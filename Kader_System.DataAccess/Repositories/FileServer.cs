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
        public async Task<string> UploadFile(string rootPath, string clinetName, string moduleName, IFormFile file)
        {
            #region Directory_Validation
            var clientPath = Path.Combine(rootPath, SysFileServer.UploadFolderNamder, clinetName, moduleName);
            if (!Directory.Exists(clientPath))
                Directory.CreateDirectory(clientPath);

            #endregion

            #region FileCreation
            var fileEXE = Path.GetExtension(file.FileName);
            var newFileName = Guid.NewGuid().ToString() + fileEXE;
            var filefullPath = Path.Combine(clientPath, newFileName);
            using (FileStream fileStream = new FileStream(filefullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
                return newFileName;
            } 
            #endregion




        }

    }
}
