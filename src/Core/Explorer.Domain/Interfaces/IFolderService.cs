using Explorer.Domain.Entities;

namespace Explorer.Domain.Interfaces
{
    public interface IFolderService
    {
        Task<List<Folder>> GetRootAsync();
        Task<List<Folder>> GetChildAsync(int folderId);
        Task CreateAsync(int? parentFolderId, string folderName);
        Task DeleteAsync(int folderId);
        Task RenameAsync(int folderId, string newName);
    }
}
