using UserApi.Dal;
using UserApi.Models;
using UserApi.Models.Helpers;

namespace UserApi.Services
{
    public class UserService
    {
        private string _tableName = "pixelwar";
        private SqLiteDataAccessLayer _dal;

        public int Insert(User el)
        {
            var newEntity = el.ToEntity();
            newEntity.UrlApproveProfile = Guid.NewGuid().ToString();
            var statusCode = _dal.Insert(newEntity);
            return statusCode;
        }

        public int Update(User el)
        {
            var statusCode = _dal.Update(el.ToEntity());
            return statusCode;
        }

        public int ValidateAccount(string email, string urlValidation)
        {
            var res = _dal.GetOne(email);
            if (res != null && urlValidation == res.UrlApproveProfile)
            {
                res.AccountStatus = true;
                return _dal.Update(res);
            }
            return 0;
        }

        public User GetUser(string email)
        {
            var entity = _dal.GetOne(email);
            if (entity != null)
                return entity.ToModel();
            return null;
        }
    }
}
