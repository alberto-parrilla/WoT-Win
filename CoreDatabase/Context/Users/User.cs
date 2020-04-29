using System;
using CoreDatabase.Abstractions;

namespace CoreDatabase.Context.Users
{
    public class User : BusinessEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
        public DateTime? RegisterDate { get; set; }
        public Guid ActivationCode { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? BlockDate { get; set; }
        public string BlockReason { get; set; }
    }
}
