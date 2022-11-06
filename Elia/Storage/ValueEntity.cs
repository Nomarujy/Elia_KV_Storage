namespace Elia.Storage
{
    [Serializable]
    internal class ValueEntity
    {
        public string Value { get; set; } = null!;
        public DateTime Timestamp { get; set; } 
    }
}
