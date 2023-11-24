using Microsoft.Data.Sqlite;
using MqLibrary.Models;
using MqLibrary.Services;
using System.Reflection;
using UserApi.Dal;
using UserApi.Models;
using UserApi.Models.Helpers;

namespace UserApi.Services
{
    public class UserService : IUserService
    {
        private string _tableName = "users";
        private SqLiteDataAccessLayer _dal;
        private IProducerService _producerService;

        public UserService(IProducerService service, SqLiteDataAccessLayer dal)
        {
            _producerService = service;
            _dal = dal;
        }

        public User Insert(User el)
        {
            var newEntity = el.ToEntity();
            newEntity.UrlApproveProfile = Guid.NewGuid().ToString();
            var res = _dal.Insert(newEntity);
            _producerService.SendMessage(new MqUserObject
            {
                Email = newEntity.Email,
                UrlApproveProfile = newEntity.UrlApproveProfile,
            });
            return res.ToModel();
        }

        public User? Update(User el)
        {
            var res = _dal.Update(el.ToEntity());
            return res.ToModel();
        }

        public async Task<User?> ValidateAccount(string email, string urlValidation)
        {
            var res = await _dal.GetOne(email);
            if (res != null && urlValidation == res.UrlApproveProfile)
            {
                res.AccountStatus = true;
                return _dal.Update(res).ToModel();
            }
            return null;
        }

        public async Task<User> GetUser(string email)
        {
            var entity = await _dal.GetOne(email);
            if (entity != null)
                return entity.ToModel();
            return null;
        }
    }
}
