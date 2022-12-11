using EliaLib.Entity;

namespace Elia.Storage.Containers.Base
{
    internal interface IContainer
    {
        public void Add(StorageKey key, StorageValue value);
        public StorageValue? Get(StorageKey key);
    }
}
