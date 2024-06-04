using System.IO;

namespace Kader_System.Domain.Extensions;

public static class ManageFilesHelper
{
    public static GetFileNameAndExtension UploadFile(IFormFile file, string path)
    {
        string fileName = Guid.NewGuid() + "_" + file.FileName;
        string finalFilePath = Path.Combine(Directory.GetCurrentDirectory() + path, fileName);

        using (var Stream = new FileStream(finalFilePath, FileMode.Create))
        {
            file.CopyTo(Stream);
        };
        return new GetFileNameAndExtension
        {
            FileName = fileName,
            FileExtension = Path.GetExtension(file.FileName)
        };
    }

    public static List<GetFileNameAndExtension> UploadFiles(IFormFileCollection files, string path)
    {
        List<GetFileNameAndExtension> list = [];
        foreach (var file in files)
        {
            string fileName = Guid.NewGuid() + "_" + file.FileName;
            string finalFilePath = Path.Combine(Directory.GetCurrentDirectory() + path, fileName);
            using (var Stream = new FileStream(finalFilePath, FileMode.Create))
            {
                file.CopyTo(Stream);
            };
            list.Add(new GetFileNameAndExtension
            {
                FileName = fileName,
                FileExtension = Path.GetExtension(file.FileName)
            });
        }
        return list;
    }



    public static GetFileNameAndExtension SaveBase64StringToFile(string base64String, string filePath,string fileName)
    {
        try
        {
           
            // Convert Base64 string to byte array
            byte[] fileBytes = Convert.FromBase64String(base64String);
            var extension= GetFileExtension(fileBytes);
            if (fileName.Contains('.'))
            {
                extension = "";
            }
            string createdFileName = Guid.NewGuid() + "_" + fileName + extension;
            string finalFilePath = Path.Combine(Directory.GetCurrentDirectory() + filePath, createdFileName);
            if (Directory.Exists(Directory.GetCurrentDirectory()+filePath))
            {
                // Save byte array to a file
                File.WriteAllBytes(finalFilePath, fileBytes);

                return new GetFileNameAndExtension
                {
                    FileName = createdFileName,
                    FileExtension = extension
                };
            }

            return null;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
            throw new Exception($"Error saving file: {ex.Message}");
        }
    }

    static string GetFileExtension(byte[] bytes)
    {
        // Check if the file has enough bytes to identify its signature
        if (bytes.Length < 8)
        {
            return "Unknown";
        }

        // Extract the first 8 bytes (file signature) as a hexadecimal string
        string hexSignature = BitConverter.ToString(bytes.Take(8).ToArray()).Replace("-", "");

        // Map file signatures to file extensions
        switch (hexSignature)
        {
            case "89504E470D0A1A0A": // PNG
                return ".png";
            case "474946383961": // GIF
            case "474946383761": // GIF
                return ".gif";
            case "25504446": // PDF
                return ".pdf";
            case "FFD8FFE0": // JPEG
            case "FFD8FFE1": // JPEG
                return ".jpeg";
            case "504B0304": // ZIP
                return ".zip";
            case "FFD8FFE000104A46":
                return ".jpg";
            case "504B030414000600": // Microsoft Office Open XML Format (DOCX)
                return ".docx";
            case "504B030414000800": // Microsoft Office Open XML Format (XLSX)
                return ".xlsx";
            case "504B030414000200": // Microsoft Office Open XML Format (PPTX)
                return ".pptx";
            case "1F8B08": // GZIP
                return ".gz";
            // Add more cases for other file types as needed
            // ...

            default:
                return "Unknown";
        }
    }
    public static void RemoveFile(string file)
    {
        file = Directory.GetCurrentDirectory() + file;
        if (File.Exists(file))
            File.Delete(file);
    }

    public static void RemoveFiles(List<string> files)
    {
        foreach (string file in files)
        {
            string fullPath = Directory.GetCurrentDirectory() + file;

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
    public static string ConvertFileToBase64(string file)
    {
        try
        {
            string fullPath = Directory.GetCurrentDirectory()+ file;
            if (File.Exists(fullPath))
            {

                // Read the file content as a byte array
                byte[] fileBytes = File.ReadAllBytes(fullPath);

                // Convert the byte array to a Base64 string
                string base64String = Convert.ToBase64String(fileBytes);

                return base64String;
            }

            return "";

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error converting file to Base64: {ex.Message}");
            return null;
        }
    }
    //public static List<GetFileNameAndExtension> UploadFiles(IFormFileCollection files, string path)
    //{
    //    List<GetFileNameAndExtension> list = new();
    //    foreach (var file in files)
    //    {
    //        string fileName = Guid.NewGuid() + "_" + file.FileName;
    //        string finalFilePath = Path.Combine(Directory.GetCurrentDirectory() + path, fileName);
    //        using (var Stream = new FileStream(finalFilePath, FileMode.Create))
    //        {
    //            file.CopyTo(Stream);
    //        };
    //        list.Add(new GetFileNameAndExtension
    //        {
    //            FileName = fileName,
    //            FileExtension = Path.GetExtension(file.FileName)
    //        });
    //    }
    //    return list;
    //}

}
