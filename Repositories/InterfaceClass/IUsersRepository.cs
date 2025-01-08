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
        void UpdateUser(Users user);
        void DeleteUser(int userId);
    }
}
