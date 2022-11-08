namespace Elia.Core.Handler
{
    public abstract class RequestHandler
    {
        public abstract Task<byte[]> HandleAsync(byte[] recivedBytes);
    }
}
