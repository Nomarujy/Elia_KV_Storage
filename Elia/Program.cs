using Elia.Core;
using Elia.Handler;
using Elia.Handler.Midlewares;
using Elia.Network;
using Elia.Network.ReadingLoop;

public static class Program
{
    public static void Main()
    {
        EliaRequestHandlerProvider requestHandlerProvider = new();
        requestHandlerProvider.AddMidleware<StorageMidleware>();

        var handler = requestHandlerProvider.Create();

        ServerCore core = new(new ServerConfiguration(), new GenericLoop(handler));

        core.StartAsync();

        while (true)
        {
            Console.ReadLine();
        }
    }
}