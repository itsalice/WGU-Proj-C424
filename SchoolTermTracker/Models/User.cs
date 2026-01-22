using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolTermTracker.Services;
using SQLite;

namespace SchoolTermTracker.Models
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("userName")]
        public string UserName { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("isLoggedIn")]
        public bool IsLoggedIn { get; set; }

        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
            {
                return;
            }

            db = new DataService().GetConnection();

            await db.CreateTableAsync<User>();
        }

        public static async Task AddUserLogin(string userName, string password, bool isLoggedIn)
        {
            await Init();

            var user = new User
            {
                UserName = userName,
                Password = password,
                IsLoggedIn = false
            };

            await db.InsertAsync(user);
        }

        public static async Task<List<User>> GetUsersAsync()
        {
            await Init();

            return await db.Table<User>().ToListAsync();
        }

        public static async Task<bool> CheckUser(string userName, string password, bool isLoggedIn)
        {
            await Init();

            var user = await db.Table<User>().Where(i => i.UserName == userName && i.Password == password).FirstOrDefaultAsync();

            if (user != null)
            {
                user.UserName = userName;
                user.Password = password;
                user.IsLoggedIn = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> IsUserLoggedIn()
        {
            await Init();

            var userIsLoggedIn = await db.Table<User>().Where(i => i.IsLoggedIn).FirstOrDefaultAsync();

            if (userIsLoggedIn != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
