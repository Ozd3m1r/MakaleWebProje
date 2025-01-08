using Entities.Models;
using Repositories.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repositories
{
    public class UserRepository : RepositoryBase<Users>, IUsersRepository
    {
        public UserRepository(RepositoryContext context) : base(context)
        {
        }

        // Kullanıcı ekleme işlemi
        public void AddUser(Users user)
        {
            _context.Add(user);
            //Create(user);
        }

        // Kullanıcı silme işlemi
        public void DeleteUser(int userId)
        {
            var user = FindByCondition(u => u.UserId == userId, trackChanges: true);
            if (user != null)
            {
                Delete(user);
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
            Update(user);
        }
    }
}


