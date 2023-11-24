using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserApi.Models.Entity;

namespace MqLibrary.Context
{
    public class UserContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public string DbPath { get; }

        public UserContext()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DbPath = System.IO.Path.Join(path, "users.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
    }
}
