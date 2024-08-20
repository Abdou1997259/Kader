using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.HTTP
{
    public interface IRequestService
    {
        /// <summary>
        /// Get Accept Language from Request Header
        /// </summary>
        public string GetRequestHeaderLanguage { get; }

        public string GetCurrentHost {  get; }

        public string client_id { get; }
        public string GetHeaderContentFile {  get; }    
    }
}
