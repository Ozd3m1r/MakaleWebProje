using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Card
    {
        public List<Cardline> Lines { get; set; }
        public Card()
        {
            Lines = new List<Cardline>();
        }

        public virtual void AddItem(Makale makale, int Sayı)
        {
            Cardline? Line = Lines.Where(m => m.makale.MakaleId == makale.MakaleId).FirstOrDefault();
            if (Line == null)
            {
                Lines.Add(new Cardline()
                {
                    makale = makale,
                    Sayı = Sayı
                });
            }
            else
            {
                Line.Sayı = +Sayı;
            }
        }
        public virtual void RemoveLine(Makale makale)=>Lines.RemoveAll(l=>l.makale.MakaleId.Equals(makale.MakaleId));
        
        public virtual void Clear()=>Lines.Clear();
    }
}
