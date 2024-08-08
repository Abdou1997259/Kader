using Kader_System.Services.IServices;
using System.Net.NetworkInformation;

namespace Kader_System.Services.Services
{
    public class NetworkInterfaceService : INetworkInterfaceService
    {
        public string GetDeviceMacAddress()
        {
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
                    return "MAC Address: " + string.Join(":", physicalAddress.GetAddressBytes().Select(b => b.ToString("X2")));
                }
            }
            return string.Empty;
        }

        public string GetIPAddress()
        {
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in networkInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) // IPv4
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
