using Elia.Handler;
using Elia.Handler.Midlewares;
using Elia.Network;

public static class Program
{
    public static void Main()
    {
        RequestHandlerBuilder requestHandlerBuilder = new();
        requestHandlerBuilder.AddMidleware<StorageMidleware>();

        var handler = requestHandlerBuilder.Build();

        NetworkCore core = new(8989, handler);

        core.Start();

        while (true)
        {
            Console.ReadLine();
        }
    }
}