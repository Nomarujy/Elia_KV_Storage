namespace EliaLib.Entity
{
    public class Request
    {
        public StorageKey Key { get; set; } = null!;
        public StorageValue? Value { get; set; }
    }
}
