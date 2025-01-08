using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Repositories.InterfaceClass;
using Services.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            /*Users u = _mapper.Map<Users>(userDto);
            _manager.Users.AddUser(u);
            //_manager.Users.Create(u);
            _manager.Save();*/
             Users u= new Users();
             {
                     u.Name = userDto.Name;
                     u.UserName = userDto.UserName;
                     u.SurName = userDto.SurName;
                     u.Email = userDto.Email;
                     u.UserName = userDto.UserName;
                     u.Password = userDto.Password;
                     _manager.Users.AddUser(u);
                     _manager.Save();
                     /*var user = _mapper.Map<Users>(userDto);
                     _manager.Users.AddUser(user);
                     _manager.Save();
            _manager.Users.AddUser(u);
                _manager.Save();*/
            }
        }

        // Kullanıcı silme
        public void DeleteUser(int id)
        {
            var user = _manager.Users.GetUserById(id,trackChanges: false);
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
            var user = _manager.Users.GetUserById(id,trackChanges);
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

            var userDto = _mapper.Map<UserDtoUpdate>(user);
            return userDto;
        }

        // Kullanıcı güncelleme
        public void UpdateUser(UserDtoUpdate userDto)
        {
            var user = _manager.Users.GetUserById(userDto.Id, trackChanges: true);
            if (user == null)
                throw new Exception("Kullanıcı Bulunamadı");

            _mapper.Map(userDto, user);
            _manager.Users.UpdateUser(user);
            _manager.Save();
        }
    }
}
