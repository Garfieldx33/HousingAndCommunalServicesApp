namespace CommonLib.Entities;

public class Message
{
    public int Id { get; set; }
    public int MessagingMethodId { get; set; }
    public string Destination { get; set; }
    public int ApplicationId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
