﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SurName { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Password { get; set; }= string.Empty;
        public string UserName { get; set; }= string.Empty;
        public ICollection<MakaleData> MakaleData { get; set; }
        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }

        public ICollection<MakaleComment> MakaleComments { get; set; }
        public Users()
        {
            MakaleComments = new HashSet<MakaleComment>();
        }


    }
}
