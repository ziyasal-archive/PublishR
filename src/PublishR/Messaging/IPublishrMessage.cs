namespace PublishR.Messaging
{
    public interface IPublishrMessage
    {
        string Raw { get; set; }
        string HubName { get; set; }
        string HubMethod { get; set; }
        string HandleType { get; set; }
    }
}