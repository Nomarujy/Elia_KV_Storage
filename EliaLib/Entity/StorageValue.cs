namespace EliaLib.Entity
{
    public class StorageValue
    {
        public StorageValue() { }

        public StorageValue(StorageValue value)
        {
            Value = value.Value;
            ExpireInterval = value.ExpireInterval;
        }

        public string Value { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public TimeSpan ExpireInterval { get; set; }
    }
}
