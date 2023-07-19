using AutoMapper;
using Explorer.Domain.Interfaces;
using Explorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.Controllers
{
    public class ExplorerController : Controller
    {
        private readonly IFolderService folderService;
        private readonly IFileService fileService;
        private readonly IMapper mapper;

        public ExplorerController(IFolderService folderService, IFileService fileService, IMapper mapper)
        {
            this.folderService = folderService;
            this.fileService = fileService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var rootFolders = await folderService.GetRootAsync();
            return View(mapper.Map<List<FolderViewModel>>(rootFolders));            
        }

		public async Task<IActionResult> GetChildren(int folderId)
		{
			var childFolders = await folderService.GetChildAsync(folderId);
			var childFiles = await fileService.GetChildAsync(folderId);
			var parentFolder = new FolderViewModel
			{
				Folders = mapper.Map<List<FolderViewModel>>(childFolders),
				Files = mapper.Map<List<FileViewModel>>(childFiles)
			};
			return PartialView("_ChildrenPartial", parentFolder);
		}

		public async Task<IActionResult> GetFile(int fileId)
		{
			var file = await fileService.GetFileAsync(fileId);
			return PartialView("_FilePartial", mapper.Map<FileViewModel>(file));
		}
	}
}