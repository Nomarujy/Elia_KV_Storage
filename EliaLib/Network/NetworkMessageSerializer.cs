using EliaLib.Entity;
using EliaLib.Utilites;
using System.Net.Sockets;

namespace EliaLib.Network
{
    public class NetworkMessageSerializer
    {
        public static NetworkMessage Serialize(Request request)
        {
            var data = RequestSerializer.Serialzie(request);
            return new NetworkMessage(data);
        }

        public static NetworkMessage Serialize(Response response)
        {
            var data = ResponseSerializer.Serialize(response);
            return new NetworkMessage(data);
        }

        public static async Task<NetworkMessage> DeserializeAsync(NetworkStream networkStream)
        {
            byte[] buffer = new byte[sizeof(int)];
            await networkStream.ReadAsync(buffer);

            int lenght = BitConverter.ToInt32(buffer);

            buffer = new byte[lenght];
            await networkStream.ReadAsync(buffer);

            return new NetworkMessage(buffer);
        }


    }
}
