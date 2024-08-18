using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.HTTP
{
    public interface IHttpContextService
    {
        /// <summary>
        /// Get Physical Server Path like path of Operating System like  (C:\\..\\..\\..)
        /// </summary>
        /// <returns>Physical Server Path On Operating System</returns>
        public string GetPhysicalServerPath();
        /// <summary>
        /// Get Relative Server Path like path of file like a link or URL
        /// </summary>
        /// <returns>Physical Server Path On Operating System</returns>
        public string GetRelativeServerPath(string moduleName,string fileName);

    }
}
