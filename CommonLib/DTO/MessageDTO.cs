namespace CommonLib.DTO;

public class MessageDTO
{
    public string MessagingMethod { get; set; }
    public string Destination { get; set; }
    public int ApplicationId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string TelephoneNumber { get; set; }
    public string ApplicantFullName { get; set; }
    public string ApplicantAddress { get; set; }
}
