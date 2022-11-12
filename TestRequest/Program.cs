using EliaLib;
using EliaLib.Entity;
using System.Net;

namespace Elia
{
    public static class Program
    {
        public static void Main()
        {
            EliaClient client = new(new IPEndPoint(IPAddress.Loopback, 8989));

            SaveValue(client).Wait();

            ReadValue(client).Wait();

            Console.ReadLine();
        }

        private readonly static ValuePath path = new()
        {
            Application = "Elia",
            Topic = "SuperTopic",
            Key = "SecretKey",
        };

        private static async Task SaveValue(EliaClient client)
        {
            ValueEntity entity = new()
            {
                Timestamp= DateTime.Now,
                Value = "Hello world",
            };
            var response = await client.SaveValue(path, entity);

            if (response is not null)
            {
                Console.WriteLine(response.Status);
            }
        }

        private static async Task ReadValue(EliaClient client)
        {
            var response = await client.ReadValue(path);

            if (response is not null)
            {
                Console.WriteLine(response.Status);
                Console.WriteLine(response.Value!.Value);
                Console.WriteLine(response.Value!.Timestamp);
            }
        }
    }
}