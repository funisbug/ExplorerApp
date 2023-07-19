using Explorer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Explorer.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService fileManager;

        public FileController(IFileService fileManager)
        {
            this.fileManager = fileManager;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string description, int folderId)
        {
			if (file != null)
			{
                if (!IsTextFile(file))
                {
					TempData["ErrorMessage"] = "Неверный формат файла. Загрузка допустима только для текстовых файлов.";
					return RedirectToAction("Index", "Explorer");
                }
				using var reader = new StreamReader(file.OpenReadStream());
				var content = await reader.ReadToEndAsync();
				await fileManager.UploadAsync(file.FileName, description, folderId, content);
			}			
            return RedirectToAction("Index", "Explorer");
        }

        private bool IsTextFile(IFormFile file)
        {
            string[] allowedTextFormats = { ".txt", ".log", ".csv", ".xml", ".json" };
            var fileExtension = Path.GetExtension(file.FileName);
            return allowedTextFormats.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        }

        public async Task<IActionResult> Download(int fileId)
        {
            var file = await fileManager.GetFileAsync(fileId);
            var bytes = Encoding.UTF8.GetBytes(file.Content);
            var fileName = file.Name + "." + file.FileType.Type;
			return File(bytes, "text/plain", fileName);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int fileId)
        {
            await fileManager.DeleteAsync(fileId);
            return RedirectToAction("Index", "Explorer");
        }

        [HttpPost]
        public async Task<IActionResult> Rename(int fileId, string newName)
        {
            await fileManager.RenameAsync(fileId, newName);
            return RedirectToAction("Index", "Explorer");
        }
    }
}
