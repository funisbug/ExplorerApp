using Explorer.DAL;
using Explorer.Domain.Entities;
using Explorer.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Application.Services
{
    public class FileTypeService : IFileTypeService
    {
        private readonly DatabaseContext databaseContext;

        public FileTypeService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<FileType>> GetAllAsync()
        {
            return await databaseContext.FileTypes.ToListAsync();
        }

        public async Task CreateAsync(string type, byte[] icon)
        {
            var fileType = new FileType 
            { 
                Type = type,
                Icon = icon
            };
            await databaseContext.FileTypes.AddAsync(fileType);
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int fileTypeId)
        {
            var fileType = await databaseContext.FileTypes.FindAsync(fileTypeId);
            if (fileType != null)
            {
                databaseContext.FileTypes.Remove(fileType);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
