using Elia.Midlewares;
using Elia.Midlewares.Builder;

namespace Elia
{
    public static class Program
    {
        public static void Main()
        {
            MidlewareBuilder builder = new MidlewareBuilder();
            builder.Add<StorageMidleware>();
            var midleware = builder.Build();

            ServerCore serverCore = new(midleware);
            serverCore.Start()
                .Wait();
        }
    }
}