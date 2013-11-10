using PublishR.Messaging;

namespace PublishR.Sample.MessageLibrary
{
    public class ProductUpdatedMessage : PublishrMessage
    {
        public int ProductId { get; set; }
    }
}