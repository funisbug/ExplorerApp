namespace Explorer.Models
{
    public class FileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FileTypeId { get; set; }
        public int FolderId { get; set; }
        public string Content { get; set; }

        public FileTypeViewModel FileType { get; set; }
    }
}
