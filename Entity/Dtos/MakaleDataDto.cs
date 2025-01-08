using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class MakaleDataDto
    {
        public int MakaleDataId { get; set; }

        // Kullanıcı ve Makale ile ilişki
        public int UserId { get; set; }   // Foreign Key User için
        public Users Users { get; set; }  // Navigation Property (User ile ilişki)

        public int MakaleId { get; set; }  // Foreign Key Makale için
        public Makale Makale { get; set; }  // Navigation Property (Makale ile ilişki)

        // Beğeni ve Beğenmeme sayıları
        public int MakaleLike { get; set; }
        public int MakaleDislike { get; set; }
    }
}
