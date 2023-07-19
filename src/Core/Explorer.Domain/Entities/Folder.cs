namespace Explorer.Domain.Entities
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentFolderId { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
