using System.Collections.Generic;
using Entities.Models;

public class User
{
    // Mevcut özellikler...

    // Navigation Properties
    public virtual ICollection<MakaleData> MakaleData { get; set; }
    public virtual ICollection<MakaleComment> Comments { get; set; }
} 