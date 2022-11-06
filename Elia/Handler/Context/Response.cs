using Elia.Storage;

namespace Elia.Handler.Context
{
    [Serializable]
    internal class Response
    {
        public string Status { get; set; } = null!;

        public ValueEntity? Value { get; set; }
    }
}
