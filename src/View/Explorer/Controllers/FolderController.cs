using Explorer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.Controllers
{
    public class FolderController : Controller
    {
        private readonly IFolderService folderService;

        public FolderController(IFolderService folderService)
        {
            this.folderService = folderService;
        }		

		[HttpPost]
        public async Task<IActionResult> Create(int? parentFolderId, string folderName)
        {
            await folderService.CreateAsync(parentFolderId, folderName);
            return RedirectToAction("Index", "Explorer");
        }

		[HttpPost]
		public async Task<IActionResult> Delete(int folderId)
        {
            await folderService.DeleteAsync(folderId);
            return RedirectToAction("Index", "Explorer");
        }

        [HttpPost]
        public async Task<IActionResult> Rename(int folderId, string newName)
        {
            await folderService.RenameAsync(folderId, newName);
            return RedirectToAction("Index", "Explorer");
        }
    }
}
