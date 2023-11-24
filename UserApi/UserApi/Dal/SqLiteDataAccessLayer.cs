using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MqLibrary.Context;
using UserApi.Models.Entity;

namespace UserApi.Dal
{
    public class SqLiteDataAccessLayer
    {
        private UserContext _userContext;
        public SqLiteDataAccessLayer(UserContext ctx)
        {
            _userContext = ctx;
        }

        public UserEntity? Update(UserEntity entity)
        {
            _userContext.Update(entity);
            _userContext.SaveChanges();
            return entity;
        }

        public UserEntity? Insert(UserEntity entity)
        {
            var res = _userContext.Add(entity);
            _userContext.SaveChanges();
            return entity;
        }

        public async Task<UserEntity?> GetOne(string email)
        {
            return await _userContext.Users.AsQueryable().FirstOrDefaultAsync(el => el.Email == email); ;

        }


    }
}
