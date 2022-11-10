using System.Collections.Generic;
using DataServiceLib.DBObjects;

namespace DataServiceLib
{ 
    public class UserData
    { 
        public static List<UsersForAuth> Users { get; set; } 
        static UserData()
        { 
            Users = new List<UsersForAuth> 
            {
                new UsersForAuth{UserId = 1, Name = "Test User", Username = "testing"}
            };
        }
    }
}