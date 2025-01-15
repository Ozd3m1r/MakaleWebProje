using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class MakaleData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MakaleDataId { get; set; }

        // Kullanıcı ve Makale ile ilişki
        public int UserId { get; set; }
        public virtual User User { get; set; }  // Navigation Property eklendi

        public int MakaleId { get; set; }
        public virtual Makale Makale { get; set; }

        // Beğeni ve Beğenmeme sayıları
        public int MakaleLike { get; set; }
        public int MakaleDislike { get; set; }

        // Yeni eklenen özellikler
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        // Yorumlar ile ilişki
        public virtual ICollection<MakaleComment> Comments { get; set; }
    }
} 