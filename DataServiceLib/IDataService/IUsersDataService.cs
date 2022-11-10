using DataServiceLib.DBObjects;

namespace DataServiceLib.IDataService
{
    public interface IUsersDataService
    {
        Users GetUser(int userId); // used after user is logged in
        Users GetUser(int userId,string password); // only used for loging in
        void CreateUser(Users user);
        bool UpdateUser(int userId, Users user);
        bool DeleteUser(int userId);

        //For Authentication
        UsersForAuth AuthenticationGetUser(int userId);
        UsersForAuth AuthenticationGetUser(string username);
        UsersForAuth AuthenticationCreateUser(string name, string username, string password = null, string salt = null);
    }
}