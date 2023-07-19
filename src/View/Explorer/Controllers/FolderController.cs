using Explorer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.Controllers
{
    public class FolderController : Controller
    {
        private readonly IFolderService folderManager;

        public FolderController(IFolderService folderManager)
        {
            this.folderManager = folderManager;
        }		

		[HttpPost]
        public async Task<IActionResult> Create(int? parentFolderId, string folderName)
        {
            await folderManager.CreateAsync(parentFolderId, folderName);
            return RedirectToAction("Index", "Explorer");
        }

		[HttpPost]
		public async Task<IActionResult> Delete(int folderId)
        {
            await folderManager.DeleteAsync(folderId);
            return RedirectToAction("Index", "Explorer");
        }

        [HttpPost]
        public async Task<IActionResult> Rename(int folderId, string newName)
        {
            await folderManager.RenameAsync(folderId, newName);
            return RedirectToAction("Index", "Explorer");
        }
    }
}
