using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities.Models
{
    public class Makale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MakaleId { get; set; }
        public string MakaleName { get; set; } 
        public string MakaleSummary { get; set; }
        public string MakaleContent { get; set; }
        public string MakaleDate {  get; set; }
        public bool MakaleIsShow {  get; set; }
        public string? MakaleImagesUrl { get; set; }
        public int KategoriId {  get; set; }
        public Kategori? Kategori { get; set; }
        public bool MakaleCarousel { get; set; }
        public ICollection<MakaleData> MakaleDatas { get; set; }
        public bool MakaleIsShowHome { get; set; }
        public ICollection<MakaleComment> MakaleComments { get; set; }

        public Makale()
        {

            MakaleComments = new HashSet<MakaleComment>();

        }

    }
}
