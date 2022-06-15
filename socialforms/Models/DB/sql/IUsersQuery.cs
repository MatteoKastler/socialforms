using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB
{

    // TODO: asynchrone Programmierung 
   interface IUsersquery
    {
        // zur DB verbinden
        void Connect();
        // Verbindung zur DB beenden
        void Disconnect();

        bool Insert(User user);
        bool Delete(int userId);
        User GetUser(int userId);
        Task<bool> CheckUsernameAsync(string unsername);
        List<User> GetAllUsers();
        bool Update(int userId, User newUserData);

        User Login(string username, string password);

        //weitere Methoden
    }
}
