namespace Elia.Storage
{
    [Serializable]
    internal class ValuePath
    {
        public string Application { get; set; } = null!;
        public string Topic { get; set; } = null!;
        public string Key { get; set; } = null!;
    }
}
