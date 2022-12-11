namespace EliaLib.Entity
{
    public class StorageKey
    {
        public StorageKey() { }

        public StorageKey(StorageKey key)
        {
            App = key.App;
            Topic = key.Topic;
            Name = key.Name;
        }

        public string App { get; set; } = null!;
        public string Topic { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
