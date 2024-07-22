using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Interfaces
{
    public interface IFileServer
    {
        /// <summary>
        /// Upload File on Server on wwwroot ,then return new saved fileName
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="clinetName"></param>
        /// <param name="file"></param>
        /// <returns>new file name with GUID</returns>
        public Task<string> UploadFile(string rootPath, string clinetName, string moduleName, IFormFile file);
        /// <summary>
        /// Remove File From wwwroot by filename
        /// </summary>
        /// <param name="FolderPath"></param>
        /// <param name="filename"></param>
        public void RemoveFile(string FolderPath, string filename);


    }
}
