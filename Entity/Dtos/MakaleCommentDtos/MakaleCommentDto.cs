using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.MakaleCommentDtos
{
    public class MakaleCommentDto
    {
        public int MakaleCommentId { get; set; }
        public int MakaleId { get; set; }
        public string MakaleName { get; set; }  
        public int UserId { get; set; }
        public string UserName { get; set; }    
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
