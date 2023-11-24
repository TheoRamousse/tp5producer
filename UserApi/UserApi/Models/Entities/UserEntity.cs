using Microsoft.EntityFrameworkCore;

namespace UserApi.Models.Entity
{
    [PrimaryKey(nameof(Email))]
    public class UserEntity
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public string Email { get; set; }
        public bool AccountStatus { get; set; }
        public string UrlApproveProfile { get; set; }
    }
}
