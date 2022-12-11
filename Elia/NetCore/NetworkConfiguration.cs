using System.Net;

namespace Elia.NetCore
{
    public class NetworkConfiguration
    {
        public IPAddress IP { get; set; } = IPAddress.Loopback;
        public int Port { get; set; } = 8558;
    }
}
