namespace Explorer.Models
{
    public class FolderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentFolderId { get; set; }

        public List<FileViewModel>? Files { get; set; }
        public List<FolderViewModel>? Folders { get; set; }
    }
}
