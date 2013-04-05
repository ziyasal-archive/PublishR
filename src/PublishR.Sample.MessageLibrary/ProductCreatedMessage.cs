using PublishR.Messaging;

namespace PublishR.Sample.MessageLibrary
{
    public class ProductCreatedMessage : PublishrMessage
    {
        public string Message { get; set; }
        public int ProductId { get; set; }
    }
}