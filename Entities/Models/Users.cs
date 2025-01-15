public class Users
{
    public Users()
    {
        Comments = new HashSet<MakaleComment>();
    }

    // Diğer özellikler...

    public virtual ICollection<MakaleComment> Comments { get; set; }
} 