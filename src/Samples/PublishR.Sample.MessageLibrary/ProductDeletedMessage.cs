using PublishR.Messaging;

namespace PublishR.Sample.MessageLibrary
{
    public class ProductDeletedMessage : PublishrMessage
    {
        public int ProductId { get; set; }
        public string Message { get; set; }
    }
}