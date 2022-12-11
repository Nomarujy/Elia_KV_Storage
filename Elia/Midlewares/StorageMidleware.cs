using Elia.Midlewares.Base;
using Elia.Storage.Containers;

namespace Elia.Midlewares
{
    internal class StorageMidleware : Midleware
    {
        public StorageMidleware(Midleware? next) : base(next) { }


        public override Task HandleAsync(MidlewareContext context)
        {
            if (context.Request.Value is null)
            {
                Get(context);
            }
            else
            {
                Create(context);
            }

            return Next(context);
        }

        private readonly AppsContainer container = new();

        private void Create(MidlewareContext context)
        {
            var key = context.Request.Key;
            var value = context.Request.Value!;

            value.Created = DateTime.UtcNow;
            container.Add(key, value);

            context.Response.Status = "Created";
        }

        private void Get(MidlewareContext context)
        {
            var result = container.Get(context.Request.Key);

            var response = context.Response;

            if (result is null)
            {
                response.Status = "Not found";
            }
            else
            {
                response.Status = "Founded";
                response.Value = result;
            }
        }
    }
}
