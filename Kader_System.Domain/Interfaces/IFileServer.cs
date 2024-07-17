using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Interfaces
{
    public interface IFileServer
    {
        public string UploadFile(string clinetName, IFormFile file);
    }
}
