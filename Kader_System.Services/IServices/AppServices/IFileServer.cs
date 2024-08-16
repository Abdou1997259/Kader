using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.AppServices
{
    public interface IFileServer
    {
        /// <summary>
        /// Upload File on Server on wwwroot ,then return new saved fileName
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="file"></param>
        /// <returns>new file name with GUID</returns>
        public Task<string> UploadFile(string moduleName, IFormFile file);
        /// <summary>
        /// Remove File From wwwroot by filename
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="fileName"></param>
        public void RemoveFile(string moduleName, string fileName);

        /// <summary>
        /// Get File Path From wwwroot by filename
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public string GetFilePath(params string[] paths);

        /// <summary>
        /// check if file is exist or not
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="fileName"></param>
        /// <returns>true if file exist</returns>
        public bool FileExist(string moduleName, string fileName);

    }
}
