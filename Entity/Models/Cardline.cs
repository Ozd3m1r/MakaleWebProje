using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Cardline
    {
        public int CardLineId { get; set; }
        public Makale makale { get; set; }
        public int Sayı { get; set; }
    }
}
