using AutoMapper;
using Explorer.Domain.Interfaces;
using Explorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.Controllers
{
    public class ExplorerController : Controller
    {
        private readonly IFolderService folderManager;
        private readonly IFileService fileManager;
        private readonly IMapper mapper;

        public ExplorerController(IFolderService folderManager, IFileService fileManager, IMapper mapper)
        {
            this.folderManager = folderManager;
            this.fileManager = fileManager;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var rootFolders = await folderManager.GetRootAsync();
            return View(mapper.Map<List<FolderViewModel>>(rootFolders));            
        }

		public async Task<IActionResult> GetChildren(int folderId)
		{
			var childFolders = await folderManager.GetChildAsync(folderId);
			var childFiles = await fileManager.GetChildAsync(folderId);
			var parentFolder = new FolderViewModel
			{
				Folders = mapper.Map<List<FolderViewModel>>(childFolders),
				Files = mapper.Map<List<FileViewModel>>(childFiles)
			};
			return PartialView("_ChildrenPartial", parentFolder);
		}

		public async Task<IActionResult> GetFile(int fileId)
		{
			var file = await fileManager.GetFileAsync(fileId);
			return PartialView("_FilePartial", mapper.Map<FileViewModel>(file));
		}
	}
}