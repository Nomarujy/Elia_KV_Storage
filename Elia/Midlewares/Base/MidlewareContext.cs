using EliaLib.Entity;

namespace Elia.Midlewares.Base
{
    internal class MidlewareContext
    {
        public Request Request { get; set; } = null!;
        public Response Response { get; set; } = null!;
    }
}
