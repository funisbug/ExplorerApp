namespace Explorer.Domain.Entities
{
    public class FileType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public byte[]? Icon { get; set; }

        public virtual ICollection<File>? Files { get; set; }
    }
}
