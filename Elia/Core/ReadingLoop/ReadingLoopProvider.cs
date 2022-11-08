using System.Net.Sockets;

namespace Elia.Network.ReadingLoop
{
    public abstract class ReadingLoopProvider
    {
        public abstract Task CreateLoopAsync(TcpClient client);
    }
}
