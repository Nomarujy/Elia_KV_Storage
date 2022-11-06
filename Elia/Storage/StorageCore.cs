namespace Elia.Storage
{
    internal class StorageCore
    {
        private readonly StorageRepository _storageRepository = new();

        public ValueEntity? Get(ValuePath path)
        {
            return _storageRepository.Get(path.Application)
                ?.Get(path.Topic)
                ?.GetValue(path.Key);
        }

        public void Set(ValuePath path, ValueEntity value)
        {
            _storageRepository
                .GetOrCreate(path.Application)
                .GetOrCreate(path.Topic)
                .CreateValue(path.Key, value);
        }
    }
}
