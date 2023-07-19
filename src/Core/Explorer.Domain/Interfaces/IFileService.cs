using File = Explorer.Domain.Entities.File;

namespace Explorer.Domain.Interfaces
{
    public interface IFileService
    {
        Task<File> GetFileAsync(int fileId);
        Task<List<File>> GetChildAsync(int folderId);
		Task UploadAsync(string name, string description, int folderId, string content);
        Task DeleteAsync(int fileId);
        Task RenameAsync(int fileId, string newName);
    }
}
