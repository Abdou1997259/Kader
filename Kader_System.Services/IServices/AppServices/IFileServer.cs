using Microsoft.AspNetCore.Mvc;
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
        /// Upload file on server on wwwroot ,then return new saved fileName
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="file"></param>
        /// <returns>new file name with GUID</returns>
        public Task<string> UploadFile(string moduleName, IFormFile file);
        /// <summary>
         /// Remove file from wwwroot
        /// </summary>
        /// <param name="paths"></param>
        public void RemoveFile(params string[] paths);

        /// <summary>
        /// Get File Path from wwwroot by filename
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public string GetFilePath(params string[] paths);
        /// <summary>
        /// Get full file path with server path  from wwwroot  
        /// </summary>
        /// <param name="paths"></param>
        /// <returns></returns>
        public string GetFilePathWithServerPath(params string[] paths);

        /// <summary>
        /// check if file is exist or not
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="fileName"></param>
        /// <returns>true if file exist</returns>
        public bool FileExist(string moduleName, string fileName);
        /// <summary>
        /// Download file from wwwroot by content type of request
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <returns>new FileStreamResult to download</returns>
        public Task<FileStreamResult> DownloadFile(string moduleName, string fileName, string contentType = "application/octet-stream");
        /// <summary>
        /// Get Content file of header request depand on file exentsion
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetContentType(string path);

    }
}
