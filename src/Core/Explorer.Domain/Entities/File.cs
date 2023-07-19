namespace Explorer.Domain.Entities
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FileTypeId { get; set; }
        public int FolderId { get; set; }
        public string Content { get; set; }

        public virtual FileType FileType { get; set; }
        public virtual Folder Folder { get; set; }
    }
}
