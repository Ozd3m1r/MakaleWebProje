using Entities.Models;
using System.Collections.Generic;

namespace Services.InterfaceClass
{
    public interface IUserRoleServices
    {
        IEnumerable<UserRole> GetAllUserRoles();
        UserRole GetUserRoleById(int userRoleId);
        void CreateUserRole(UserRole userRole);
        void UpdateUserRole(UserRole userRole);
        void DeleteUserRole(int userRoleId);
        string GetUserRoleNameById(int userRoleId);
    }
}