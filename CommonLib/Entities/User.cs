﻿using CommonLib.Enums;

namespace CommonLib.Entities
{
    public class User
    { 
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
        public UserTypeEnum TypeId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public float Balance { get; set; }
    }
}
