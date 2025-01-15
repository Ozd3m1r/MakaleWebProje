using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.InterfaceClass;
using Repositories.RepositoryClass;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly RepositoryContext _context;
        public UserRoleRepository(RepositoryContext context)
        {
            _context = context;
        }

        // Tüm kullanıcı rollerini getirir
        public IEnumerable<UserRole> GetAllUserRoles()
        {
            return _context.Role.ToList(); // Veritabanından tüm roller çekiliyor
        }

        // UserRoleId'ye göre rolü getirir
        public UserRole GetUserRoleById(int userRoleId)
        {
            return _context.Role.FirstOrDefault(ur => ur.UserRoleId == userRoleId);
        }

        // Yeni bir kullanıcı rolü ekler
        public void CreateUserRole(UserRole userRole)
        {
            _context.Role.Add(userRole);
            _context.SaveChanges(); // Veritabanına kaydediyoruz
        }

        // Kullanıcı rolünü günceller
        public void UpdateUserRole(UserRole userRole)
        {
            var existingUserRole = _context.Role.FirstOrDefault(ur => ur.UserRoleId == userRole.UserRoleId);
            if (existingUserRole != null)
            {
                existingUserRole.UserRoleName = userRole.UserRoleName;
                _context.SaveChanges(); // Veritabanında değişiklikleri kaydediyoruz
            }
        }

        // Kullanıcı rolünü siler
        public void DeleteUserRole(int userRoleId)
        {
            
            var userRole = _context.Role.FirstOrDefault(ur => ur.UserRoleId == userRoleId);
            if (userRole != null)
            {
                _context.Role.Remove(userRole);
                _context.SaveChanges(); // Silme işlemini veritabanında kaydediyoruz
            }
        }

        // UserRoleId'ye göre rol ismini döndüren fonksiyon
        public string GetUserRoleNameById(int userRoleId)
        {
            // Veritabanından UserRole'ü getiriyoruz
            var userRole = _context.Role.FirstOrDefault(ur => ur.UserRoleId == userRoleId);

            // Eğer rol varsa ismini döndürüyoruz, yoksa "Rol Bulunamadı" döndürüyoruz
            return userRole?.UserRoleName ?? "Rol Bulunamadı";
        }
    }
}
