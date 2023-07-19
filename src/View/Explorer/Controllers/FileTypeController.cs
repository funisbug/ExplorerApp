using AutoMapper;
using Explorer.Domain.Interfaces;
using Explorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.Controllers
{
    public class FileTypeController : Controller
    {
        private readonly IFileTypeService fileTypeService;
        private readonly IMapper mapper;

        public FileTypeController(IFileTypeService fileTypeService, IMapper mapper)
        {
            this.fileTypeService = fileTypeService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var fileTypes = await fileTypeService.GetAllAsync();
            return View(mapper.Map<List<FileTypeViewModel>>(fileTypes));
        }

        [HttpPost]
        public async Task<IActionResult> Create(string type, IFormFile icon)
        {
            if (type != null && icon != null)
            {
				var ms = new MemoryStream();
				icon.CopyTo(ms);
				var iconData = ms.ToArray();
				await fileTypeService.CreateAsync(type, iconData);
			}            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int fileTypeId)
        {
            await fileTypeService.DeleteAsync(fileTypeId);
            return RedirectToAction("Index");
        }
    }
}
