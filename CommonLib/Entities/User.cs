using CommonLib.Enums;

namespace CommonLib.Entities;

public class User
{ 
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    public UserTypeEnum TypeId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public float Balance { get; set; }
    public int MessagingMethodId { get; set; } = 1;
    public string MessagingDestination { get; set; } = string.Empty;

    public override string ToString()
    {
        return $" {Id} | {FirstName} | {SecondName} | {DateOfBirth} | {Address} | {Phone} | {Email} | {RegistrationDate} | {TypeId} | {Login} | {Password} | {Balance} | {MessagingMethodId} | {MessagingDestination}";
    }
}
