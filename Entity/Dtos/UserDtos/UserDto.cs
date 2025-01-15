using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.UserDtos
{
    public record UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? CurrentPassword { get; set; }
        public string UserName { get; set; }
        public int UserRoleId { get; set; }
    }
}
