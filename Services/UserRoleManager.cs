using Entities.Models;
using Repositories.InterfaceClass;
using Services.InterfaceClass;
using System.Collections.Generic;

namespace Services
{
    public class UserRoleManager : IUserRoleServices
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleManager(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        // Tüm kullanıcı rollerini getirir
        public IEnumerable<UserRole> GetAllUserRoles()
        {
            return _userRoleRepository.GetAllUserRoles();
        }

        // UserRoleId'ye göre bir kullanıcı rolünü getirir
        public UserRole GetUserRoleById(int userRoleId)
        {
            return _userRoleRepository.GetUserRoleById(userRoleId);
        }

        // Yeni bir kullanıcı rolü ekler
        public void CreateUserRole(UserRole userRole)
        {
            _userRoleRepository.CreateUserRole(userRole);
        }

        // Kullanıcı rolünü günceller
        public void UpdateUserRole(UserRole userRole)
        {
            _userRoleRepository.UpdateUserRole(userRole);
        }

        // Kullanıcı rolünü siler
        public void DeleteUserRole(int userRoleId)
        {
            _userRoleRepository.DeleteUserRole(userRoleId);
        }

        // UserRoleId'ye göre kullanıcı rol ismini döner
        public string GetUserRoleNameById(int userRoleId)
        {
            // Repository'den userRoleName almak için fonksiyonu çağırıyoruz
            return _userRoleRepository.GetUserRoleNameById(userRoleId);
        }
    }
}
