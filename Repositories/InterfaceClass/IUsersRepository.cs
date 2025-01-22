using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceClass
{
    public interface IUsersRepository
    {
        void AddUser(Users user);
        List<Users> GetAllUsers(bool trackChanges);
        Users? GetUserById(int id, bool trackChanges);
        void AssignRoleToUser(int userId, int roleId);
        void UpdateUser(Users user);
        void UpdateUserChangeProfile(Users user);
        void DeleteUser(int userId);

        bool VerifyPassword(string storedPassword, string enteredPassword);

        Users GetByUserName(string userName, bool trackChanges = false);
        Users GetByEmail(string email, bool trackChanges = false);
    }
}
