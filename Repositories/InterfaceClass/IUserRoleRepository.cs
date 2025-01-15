using Entities.Models;
using System.Collections.Generic;

namespace Repositories.InterfaceClass
{
    public interface IUserRoleRepository
    {
        IEnumerable<UserRole> GetAllUserRoles();
        UserRole GetUserRoleById(int userRoleId);
        void CreateUserRole(UserRole userRole);
        void UpdateUserRole(UserRole userRole);
        void DeleteUserRole(int userRoleId);
        string GetUserRoleNameById(int userRoleId);
    }
}