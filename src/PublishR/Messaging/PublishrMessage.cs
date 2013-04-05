namespace PublishR.Messaging
{
    public abstract class PublishrMessage : IPublishrMessage
    {
        public string Raw { get; set; }
        public string HubName { get; set; }
        public string HubMethod { get; set; }
        public string HandleType { get; set; }
    }
}