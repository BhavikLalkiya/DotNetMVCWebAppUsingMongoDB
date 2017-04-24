using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using MongoDB.Driver;
using DotNetMVCWebAppUsingMongoDB.Models;
using DotNetMVCWebAppUsingMongoDB.Models.Account;
using System.Security.Claims;

namespace DotNetMVCWebAppUsingMongoDB.Services
{
    public class AccountServices
    {
        public static Task<User> LoginUsers(string Email, string Password)
        {
            var blogContext = new BlogContext();
            return blogContext.Users.Find(x => x.Email == Email && x.Password == Password).SingleOrDefaultAsync();
        }


        public static void RegisterUsers(string Name, string Email, string Password)
        {
            var blogContext = new BlogContext();
            var user = new User
            {
                Name = Name,
                Email = Email,
                Password = Password
            };

            blogContext.Users.InsertOneAsync(user);
        }
    }
}