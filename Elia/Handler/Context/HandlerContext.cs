namespace Elia.Handler.Context
{
    internal class HandlerContext
    {

        public Request Request { get; set; } = null!;
        public Response Response { get; set; } = new();
    }
}
