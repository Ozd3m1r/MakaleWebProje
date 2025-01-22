using AutoMapper;
using Entities.Dtos.UserDtos;
using Entities.Models;
using Repositories;
using Repositories.InterfaceClass;
using Services.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Services
{
    public class UserManager : IUsersServices
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public UserManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        // Kullanıcı oluşturma
        public void CreateUser(UserDtoInsertion userDto)
        {
            Users u = new Users();
            {
                u.Name = userDto.Name;
                u.UserName = userDto.UserName;
                u.SurName = userDto.SurName;
                u.Email = userDto.Email;
                u.UserName = userDto.UserName;

                // Şifreyi SHA-256 ile hash'liyoruz
                u.Password = userDto.Password;

                u.UserRoleId = userDto.UserRoleId;

                _manager.Users.AddUser(u);
                _manager.Save();
            }
        }

        // Kullanıcı silme
        public void DeleteUser(int id)
        {
            var user = _manager.Users.GetUserById(id, trackChanges: false);
            if (user == null)
                throw new Exception("Kullanıcı Bulunamadı");

            _manager.Users.DeleteUser(id);
            _manager.Save();
        }

        // Tüm kullanıcıları listeleme
        public IEnumerable<Users> GetAllUsers(bool trackChanges)
        {
            return _manager.Users.GetAllUsers(trackChanges);
        }

        // ID'ye göre kullanıcı getirme
        public Users? GetUserById(int id, bool trackChanges)
        {
            var user = _manager.Users.GetUserById(id, trackChanges);
            if (user == null)
                throw new Exception("Kullanıcı Bulunamadı");

            return user;
        }

        // Güncelleme için kullanıcı bilgilerini alma
        public UserDtoUpdate GetOneUserUpdate(int id, bool trackChanges)
        {
            var user = GetUserById(id, trackChanges);
            if (user == null)
                throw new Exception("Kullanıcı Bulunamadı");

            // Manuel dönüşüm
            var userDto = new UserDtoUpdate
            {
                Id = user.UserId,
                Name = user.Name,
                SurName = user.SurName,
                UserName = user.UserName,
                Email = user.Email,
                UserRoleId = user.UserRoleId
            };

            return userDto;
        }

        // Kullanıcı güncelleme
        public void UpdateUser(UserDtoUpdate userDto)
        {
            var user = _manager.Users.GetUserById(userDto.Id, trackChanges: true);
            if (user == null)
                throw new Exception("Kullanıcı Bulunamadı");

            user.Name = userDto.Name;
            user.SurName = userDto.SurName;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.UserRoleId = userDto.UserRoleId;

            // Şifreyi hash'liyoruz
            user.Password = userDto.Password;

            _manager.Users.UpdateUser(user);
            _manager.Save();
        }
        public void UpdateUserChangeProfile (UserDtoUpdate userDto)
        {
            var user = _manager.Users.GetUserById(userDto.Id, trackChanges: true);
            if (user == null)
                throw new Exception("Kullanıcı Bulunamadı");

            user.Name = userDto.Name;
            user.SurName = userDto.SurName;
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.UserRoleId = userDto.UserRoleId;
            user.Password = userDto.Password;

            _manager.Users.UpdateUserChangeProfile(user);
            _manager.Save();
        }

        // Kullanıcı rolü atama
        public void AssignRoleToUser(int userId, int roleId)
        {
            var user = _manager.Users.GetUserById(userId, trackChanges: true);
            if (user == null)
                throw new Exception("Kullanıcı Bulunamadı");

            var role = _manager.UserRole.GetUserRoleById(roleId);
            if (role == null)
                throw new Exception("Rol Bulunamadı");

            user.UserRole = role;
            _manager.Users.UpdateUser(user);
            _manager.Save();
        }

        // Şifre doğrulama işlemi
        public bool VerifyPassword(int userId, string enteredPassword)
        {
            var user = _manager.Users.GetUserById(userId, trackChanges: false);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            // Veritabanındaki hash'lenmiş şifre ile karşılaştırıyoruz
            return _manager.Users.VerifyPassword(user.Password, enteredPassword);
        }

        // Kullanıcıya ait yorumları silme
        public void DeleteCommentsByUserId(int userId)
        {
            _manager.MakaleComment.DeleteCommentsByUserId(userId);
        }
        public Users GetByUserName(string userName, bool trackChanges = false)
        {
            return _manager.Users.GetByUserName(userName, trackChanges);
        }

        public Users GetByEmail(string email, bool trackChanges = false)
        {
            return _manager.Users.GetByEmail(email, trackChanges);
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
    }
}
