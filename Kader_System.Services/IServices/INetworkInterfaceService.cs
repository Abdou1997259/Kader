using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices
{
    public interface INetworkInterfaceService
    {
        public string GetDeviceMacAddress();
        public string GetIPAddress();
    }

}
