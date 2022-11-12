using EliaLib.Entity;
using System.Text;
using System.Text.Json;

namespace EliaLib.Utilites
{
    public static class RequestSerializer
    {
        public static byte[] Serialzie(Request request)
        {
            var json = JsonSerializer.Serialize(request);

            return Encoding.UTF8.GetBytes(json);
        }

        public static Request? Deserialize(byte[] data)
        {
            var json = Encoding.UTF8.GetString(data);
            return JsonSerializer.Deserialize<Request>(json);
        }
    }
}
