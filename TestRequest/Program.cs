using EliaLib;
using EliaLib.Entity;

namespace TestRequest
{
    public static class Program
    {
        public static void Main()
        {
            Test().Wait();

            Console.ReadLine();
        }

        private async static Task Test()
        {
            string host = "127.0.0.1";
            EliaClient client = new(host, 8558);

            StorageKey key = new()
            {
                App = "First",
                Topic = "Nomar",
                Name = "Secret",
            };
            StorageValue value = new()
            {
                Value = "Hello"
            };

            var response = await client.GetAsync(key);
            ShowResponse(response);

            response = await client.SaveAsync(key, value);
            ShowResponse(response);

            while (true)
            {
                Console.ReadLine();

                response = await client.GetAsync(key);
                ShowResponse(response);
            }
        }

        private static void ShowResponse(Response response)
        {
            Console.WriteLine(response.Status);
            if (response.Value is not null)
            {
                Console.WriteLine(response.Value.Value);
            }
        }
    }
}
