using Explorer.DAL;
using Explorer.Domain.Entities;
using Explorer.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using File = Explorer.Domain.Entities.File;

namespace Explorer.Application.Services
{
    public class FileService : IFileService
    {
        private readonly DatabaseContext databaseContext;

        public FileService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<File> GetFileAsync(int fileId)
        {
            var file = await databaseContext.Files.Include(f => f.FileType).FirstOrDefaultAsync(f => f.Id == fileId);
            return file;
        }

		public async Task<List<File>> GetChildAsync(int folderId)
		{
			var childrenFiles = await databaseContext.Files.Include(f => f.FileType).Where(f => f.FolderId == folderId).ToListAsync();

			return childrenFiles;
		}

		public async Task UploadAsync(string name, string description, int folderId, string content)
		{            
			var fileName = name.Substring(0, name.LastIndexOf('.'));
			var fileType = name.Split('.')[^1];
            var isNewFileTypes = await databaseContext.FileTypes.FirstOrDefaultAsync(f => f.Type == fileType);
            if (isNewFileTypes == null)
            {
                var newFileType = new FileType
                {
                    Type = fileType
                };
                await databaseContext.FileTypes.AddAsync(newFileType);
				await databaseContext.SaveChangesAsync();
			}
            var fileTypeId = await databaseContext.FileTypes.FirstOrDefaultAsync(f => f.Type == fileType);
			var newFile = new File
            {
                Name = fileName,
                Description = description,
				FolderId = folderId,
                FileTypeId = fileTypeId.Id,
				Content = content                
            };
            await databaseContext.Files.AddAsync(newFile);
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int fileId)
        {
            var file = await databaseContext.Files.FindAsync(fileId);
            if (file != null)
            {
                databaseContext.Files.Remove(file);
                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task RenameAsync(int fileId, string newName)
        {
            var file = await databaseContext.Files.FindAsync(fileId);
            if (file != null)
            {
                file.Name = newName;
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
