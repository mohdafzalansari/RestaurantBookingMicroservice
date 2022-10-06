using System;

namespace Identity.Domain
{
    public class User: BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
