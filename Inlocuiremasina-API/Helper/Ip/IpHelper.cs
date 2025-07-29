using System.Net;
using System.Net.Sockets;

namespace Helper.Ip
{
    public static class IpHelper
    {
        public static string GetIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            var ip = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

            if (ip != null) { return ip.ToString(); }

            return string.Empty;
        }
    }
}
