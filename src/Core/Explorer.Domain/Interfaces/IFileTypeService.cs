using Explorer.Domain.Entities;

namespace Explorer.Domain.Interfaces
{
    public interface IFileTypeService
    {
        Task<List<FileType>> GetAllAsync();
        Task CreateAsync(string type, byte[] icon);
        Task DeleteAsync(int fileTypeId);
    }
}
