using Elia.Handler.Context;
using Elia.Storage;

namespace Elia.Handler.Midlewares
{
    internal class StorageMidleware : Middleware
    {
        public StorageMidleware(Middleware next) : base(next)
        {

        }

        private readonly StorageCore storageCore = new();


        public override Task HandleAsync(HandlerContext context)
        {
            var path = context.Request.ValuePath;
            var entity = context.Request.ValueEntity;
            var response = context.Response;

            if (path is null)
            {
                response.Status = "Path Empty";
                return Task.CompletedTask;
            }

            if (entity is null)
            {
                var item = storageCore.Get(path);

                if (item is null)
                {
                    response.Status = "Not found";
                }
                else
                {
                    response.Status = "Found";
                    response.Value = item;
                }
            }
            else
            {
                response.Status = "Created";
                storageCore.Set(path, entity);
            }

            return Next(context);
        }
    }
}
