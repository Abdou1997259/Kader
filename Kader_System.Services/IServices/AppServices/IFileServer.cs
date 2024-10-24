﻿using Microsoft.AspNetCore.Mvc;

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
        /// Combine pathes together to get full path
        /// </summary>
        /// <param name="paths"></param>
        /// <returns>return new path except nullable paths</returns>
        public string CombinePath(params string[] paths);
        /// <summary>
        /// Combine path with server path  from wwwroot  
        /// </summary>
        /// <param name="paths"></param>
        /// <returns>return new path except nullable paths</returns>
        public string CombinePathWithServerPath(params string[] paths);
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
        public Task<byte[]> GetFileBytes(params string[] fileParts);
        /// <summary>
        /// Get File exetension
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>string empty if file name is null or empty</returns>
        public string GetFileEXE(string fileName);
        /// <summary>
        /// Download file from wwwroot by module name and filename
        /// </summary>
        /// <param name="module"></param>
        /// <param name="fileName"></param>
        /// <returns>File from wwwroot if exist</returns>
        public Task<FileContentResult> DownloadFileAsync(string module, string fileName);
        /// <summary>
        /// Copy file from source file to destination file 
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destFile"></param>
        public void CopyFile(string sourceFilePath, string destFilePath);
        /// <summary>
        /// Compress file using GZipStream to reduce file size
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="compressedFilePath"></param>
        public Task CompressFileAsync(string sourceFilePath);
        /// <summary>
        /// Create new file on wwwroot 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public void CreateNewFile(string fileName);
        /// <summary>
        /// Get file size by kilo bytes size (KB)
        /// </summary>
        /// <param name="file"></param>
        /// <returns>file size in (KB)</returns>
        public double GetFileSizeInKB(IFormFile file);
        /// <summary>
        /// Get file size by mega bytes size (MB)
        /// </summary>
        /// <param name="file"></param>
        /// <returns>file size in (MB)</returns>
        public double GetFileSizeInMB(IFormFile file);
        /// <summary>
        /// Get file size by Giga bytes size (GB)
        /// </summary>
        /// <param name="file"></param>
        /// <returns>file size in (GB)</returns>
        public double GetFileSizeInGB(IFormFile file);
    }
}
