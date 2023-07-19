using AutoMapper;
using Explorer.Domain.Entities;
using Explorer.Models;
using File = Explorer.Domain.Entities.File;

namespace Explorer.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Folder, FolderViewModel>();
            CreateMap<File, FileViewModel>();
            CreateMap<FileType, FileTypeViewModel>();
        }
    }
}