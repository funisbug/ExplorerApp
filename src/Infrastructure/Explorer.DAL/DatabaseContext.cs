using Explorer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using File = Explorer.Domain.Entities.File;

namespace Explorer.DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Folder> Folders { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FileType> FileTypes { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }


    }
}
