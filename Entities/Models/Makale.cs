using System.Collections.Generic;

public class Makale
{
    public Makale()
    {
        Comments = new HashSet<MakaleComment>();
    }

    // Mevcut özellikler...

    // Navigation Properties
    public virtual ICollection<MakaleData> MakaleData { get; set; }
    public virtual ICollection<MakaleComment> Comments { get; set; }
} 