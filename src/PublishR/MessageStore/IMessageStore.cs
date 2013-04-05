namespace PublishR.MessageStore
{
    public interface IMessageStore
    {
        void Store<T>(string key, T message);
        T Get<T>(string key);
    }
}