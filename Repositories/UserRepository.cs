using Entities.Models;
using Repositories.InterfaceClass;
using Repositories.RepositoryClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : RepositoryBase<Users>, IUsersRepository
    {
        private readonly IMakaleCommentRepository _makaleCommentRepository;

        public UserRepository(RepositoryContext context, IMakaleCommentRepository makaleCommentRepository) : base(context)
        {
            _makaleCommentRepository = makaleCommentRepository;
        }

        private readonly List<Users> _users = new List<Users>();
        private readonly List<UserRole> _userRoles = new List<UserRole>();

        // Kullanıcı ekleme işlemi
        public void AddUser(Users user)
        {
            user.Password = HashPassword(user.Password);
            _context.Add(user);
        }

        // Kullanıcı silme işlemi
        public void DeleteUser(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Önce kullanıcının tüm yorumlarını fiziksel olarak sil
                    _makaleCommentRepository.DeleteCommentsByUserId(id);

                    // Sonra kullanıcıyı sil
                    var user = FindByCondition(u => u.UserId.Equals(id), true);

                    if (user != null)
                    {
                        Delete(user);
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // Tüm kullanıcıları getirme
        public List<Users> GetAllUsers(bool trackChanges)
        {
            return FindAll(trackChanges).ToList();
        }

        // ID'ye göre kullanıcıyı getirme
        public Users? GetUserById(int id, bool trackChanges)
        {
            return FindByCondition(u => u.UserId == id, trackChanges);
        }

        // Kullanıcı güncelleme işlemi
        public void UpdateUser(Users user)
        {
            user.Password = HashPassword(user.Password);
            Update(user);
        }
        public void UpdateUserChangeProfile(Users user)
        {
            Update(user);
        }

        // Kullanıcı Yetkilendirme
        public void AssignRoleToUser(int userId, int roleId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            var role = _userRoles.FirstOrDefault(r => r.UserRoleId == roleId);
            if (user != null && role != null)
            {
                user.UserRole = role;
            }
        }

        // SHA-256 ile şifreyi hash'leme
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // Hexadecimal formatta yazdırıyoruz
                }
                return sb.ToString(); // Hashlenmiş şifreyi döndürüyoruz
            }
        }

        // Şifre doğrulama işlemi (hash'leri karşılaştırma)
        public bool VerifyPassword(string storedPassword, string enteredPassword)
        {
            // Kullanıcının girdiği şifreyi hash'liyoruz
            string enteredPasswordHash = HashPassword(enteredPassword);

            // Veritabanındaki hash ile karşılaştırıyoruz
            return enteredPasswordHash == storedPassword;
        }
        public Users GetByUserName(string userName, bool trackChanges = false)
        {
            return FindByCondition(u => u.UserName.ToLower() == userName.ToLower(), trackChanges);
        }

        public Users GetByEmail(string email, bool trackChanges = false)
        {
            return FindByCondition(u => u.Email.ToLower() == email.ToLower(), trackChanges);
        }
    }
}
