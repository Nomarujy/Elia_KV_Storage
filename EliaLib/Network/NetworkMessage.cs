using System.Net.Sockets;

namespace EliaLib.Network
{
    public class NetworkMessage
    {
        public byte[] Lenght => BitConverter.GetBytes(Body.Length);
        public byte[] Body { get; private set; }

        public NetworkMessage(byte[] body)
        {
            Body = body;
        }
    }
}
