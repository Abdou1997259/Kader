using System.Net.NetworkInformation;

namespace NetworkService.Network
{
    internal class NetworkInterface
    {

        public string IPAdress
        {
            get
            {
                foreach (System.Net.NetworkInformation.NetworkInterface networkInterface in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
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
        public string MacAddress
        {
            get
            {
                var mac = string.Empty;
                foreach (System.Net.NetworkInformation.NetworkInterface networkInterface in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        networkInterface.OperationalStatus == OperationalStatus.Up)
                    {
                        PhysicalAddress physicalAddress = networkInterface.GetPhysicalAddress();
                        mac = "MAC Address: " + string.Join(":", physicalAddress.GetAddressBytes().Select(b => b.ToString("X2")));
                    }
                }
                return mac;
            }
        }

    }
}
