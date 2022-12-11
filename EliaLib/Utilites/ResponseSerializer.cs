using EliaLib.Entity;
using System.Text;
using System.Text.Json;

namespace EliaLib.Utilites
{
    public static class ResponseSerializer
    {
        public static byte[] Serialize(Response response)
        {
            var json = JsonSerializer.Serialize(response);

            return Encoding.UTF8.GetBytes(json);
        }

        public static Response Deserialize(byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<Response>(json)!;
        }
    }
}
