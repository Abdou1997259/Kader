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
        public Task<string> UploadFileAsync(string moduleName, IFormFile file);
        /// <summary>
        /// Upload collection files on server on wwwroot ,then return new saved fileNames
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="files"></param>
        /// <returns>new list of file names with GUID</returns>
        public Task<List<GetFileNameAndExtension>> UploadFilesAsync(string moduleName, IFormFileCollection files);
        /// <summary>
        /// Remove file from wwwroot
        /// </summary>
        /// <param name="paths"></param>
        public void RemoveFile(params string[] paths);
        /// <summary>
        /// Remove files in Directory by filename
        /// </summary>
        /// <param name="ModuleName"></param>
        /// <param name="fileNames"></param>
        /// <returns>number of removed files</returns>
        public int RemoveFiles(string ModuleName, List<string> fileNames);

        /// <summary>
        /// Remove all files in Directory from wwwroot
        /// </summary>
        /// <param name="paths"></param>
        public void RemoveDirectory(string folderName);

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
        /// Get filename with exetnesion from specific path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>return filename in dirctory if exist ,otherwise return empty string</returns>
        public string GetFilenameFromPath(params string[] paths);

        /// <summary>
        /// check if file is exist or not
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="fileName"></param>
        /// <returns>true if file exist</returns>
        public bool FileExist(string moduleName, string fileName);
        /// <summary>
        /// Get Content file of header request depand on file exentsion
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetContentType(string path);
        public  Task<byte[]> GetFileBytes(params string[] fileParts);
        /// <summary>
        /// Get File exetension
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>string empty if file name is null or empty</returns>
        public string GetFileEXE(string fileName);


    }
}
