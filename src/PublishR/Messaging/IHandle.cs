namespace PublishR.Messaging
{
    public interface IHandle<in TMessage>
    {
        void Handle(TMessage message);
    }
}