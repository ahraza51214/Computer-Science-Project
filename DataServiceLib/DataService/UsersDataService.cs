using System;
using System.Collections.Generic;
using System.Linq;
using DataServiceLib.DBObjects;
using DataServiceLib.IDataService;

namespace DataServiceLib.DataService
{
    public class UsersDataService : IUsersDataService
    {
        private readonly Raw11Context _db;
        
        public UsersDataService(string connStr)
        {
            _db = new Raw11Context(connStr);
        }

        public Users GetUser(int userId)  
        {
            return _db.Users.FirstOrDefault(x => x.UserId == userId);
        }

        public Users GetUser(int userId,string password) // used only for authentication
        {
            return _db.Users.FirstOrDefault(x => x.UserId == userId && x.Password == password);
        }


        public void CreateUser(Users user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
        

        public bool UpdateUser(int userId, Users user)
        {
            var dbUser = GetUser(userId);
            if (dbUser == null)
            {
                return false;
            }
            dbUser.Name = user.Name;
            dbUser.Age = user.Age;
            dbUser.Language = user.Language;
            dbUser.Username = user.Username;
            dbUser.Password = user.Password;
            _db.SaveChanges();
            return true;
        }

        public bool DeleteUser(int userId)
        {
            var dbUser = GetUser(userId);
            if (dbUser == null)
            {
                return false;
            }
            _db.Users.Remove(dbUser);
            _db.SaveChanges();
            return true;
        }
        
        //For Authentication
        private List<UsersForAuth> _usersListForAuth = UserData.Users;

        public UsersForAuth AuthenticationGetUser(int userId)
        {
            return _usersListForAuth.FirstOrDefault(x => x.UserId == userId);
        }
        
        public UsersForAuth AuthenticationGetUser(string username)
        {
            return _usersListForAuth.FirstOrDefault(x => x.Username == username);
        }
        
        public UsersForAuth AuthenticationCreateUser(string name, string username, string password = null, string salt = null)
        {
            var user = new UsersForAuth()
            {
                UserId = _usersListForAuth.Max(x => x.UserId) + 1,
                Name = name,
                Username = username,
                Password = password,
                Salt = salt
            };
            _usersListForAuth.Add(user);
            return user;
        }
    }
}