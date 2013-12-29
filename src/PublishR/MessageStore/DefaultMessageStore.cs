using System;

namespace PublishR.MessageStore
{
    public class DefaultMessageStore : IMessageStore
    {
        /*TODO: MemoryCacheT*/

        public void Store<T>(string key, T message)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }
    }
}