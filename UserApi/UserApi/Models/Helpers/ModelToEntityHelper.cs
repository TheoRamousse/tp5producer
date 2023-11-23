using UserApi.Models.Entity;

namespace UserApi.Models.Helpers
{
    public static class ModelToEntityHelper
    {
        public static UserEntity ToEntity(this User user, bool accountStatus = false)
        {
            return new UserEntity
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AccountStatus = accountStatus
            };
        }

        public static User ToModel(this UserEntity entity)
        {
            return new User
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email
            };
        }
    }
}
