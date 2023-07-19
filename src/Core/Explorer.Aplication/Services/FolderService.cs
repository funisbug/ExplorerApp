using Explorer.DAL;
using Explorer.Domain.Entities;
using Explorer.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Application.Services
{
    public class FolderService : IFolderService
    {
        private readonly DatabaseContext databaseContext;

        public FolderService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<List<Folder>> GetRootAsync()
        {
            var rootFolders = await databaseContext.Folders.Where(f => f.ParentFolderId == null).ToListAsync();
            return rootFolders;
        }

        public async Task<List<Folder>> GetChildAsync(int folderId)
        {
            var childreFolders = await databaseContext.Folders.Where(f => f.ParentFolderId == folderId).ToListAsync();
            return childreFolders;
        }

        public async Task CreateAsync(int? parentFolderId, string folderName)
        {
            Folder newFolder = new Folder
            {
                Name = folderName,
                ParentFolderId = parentFolderId
            };
            await databaseContext.Folders.AddAsync(newFolder);
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int folderId)
        {
            var folderToDelete = await databaseContext.Folders.FindAsync(folderId);
            if (folderToDelete != null)
            {
                await DeleteFolderAndContentsAsync(folderToDelete);
                databaseContext.SaveChanges();
            }
        }

        private async Task DeleteFolderAndContentsAsync(Folder folder)
        {
            var subfolders = await databaseContext.Folders.Where(f => f.ParentFolderId == folder.Id).ToListAsync();
            if (folder.Files != null)
            {
                foreach (var file in folder.Files.ToList())
                {
                    databaseContext.Files.Remove(file);
                }
            }

            foreach (var subfolder in subfolders)
            {
                await DeleteFolderAndContentsAsync(subfolder);
            }
            databaseContext.Folders.Remove(folder);
        }

        public async Task RenameAsync(int folderId, string newName)
        {
            var folder = await databaseContext.Folders.FirstOrDefaultAsync(f => f.Id == folderId);
            if (folder != null)
            {
                folder.Name = newName;
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
