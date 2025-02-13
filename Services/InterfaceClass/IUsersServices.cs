﻿using Entities.Dtos.UserDtos;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceClass
{
    public interface IUsersServices
    {
        // Tüm kullanıcıları listeleme
        IEnumerable<Users> GetAllUsers(bool trackChanges);

        // ID'ye göre kullanıcı getirme
        Users? GetUserById(int id, bool trackChanges);

        // Yeni kullanıcı ekleme
        void CreateUser(UserDtoInsertion userDto);

        // Kullanıcı güncelleme
        void UpdateUser(UserDtoUpdate userDto);
        void UpdateUserChangeProfile(UserDtoUpdate userDto);

        // Kullanıcı silme
        void DeleteUser(int id);

        // Kullanıcı güncelleme için gerekli bilgileri alma
        UserDtoUpdate GetOneUserUpdate(int id, bool trackChanges);

        void AssignRoleToUser(int userId, int roleId);
        public void DeleteCommentsByUserId(int userId);

        bool VerifyPassword(int userId, string enteredPassword);
        string HashPassword(string password);
    }
}
