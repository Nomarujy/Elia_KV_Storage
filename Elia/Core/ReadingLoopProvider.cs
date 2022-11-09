using System.Net.Sockets;

namespace Elia.Core
{
    public abstract class ReadingLoopProvider
    {
        public abstract Task CreateLoopAsync(TcpClient client);
    }
}
