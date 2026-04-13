using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationLayer.Base;
using MediatR;
using ApplicationLayer.Features.GenralSettings.Admin.Dtos;
using ApplicationLayer.Features.GenralSettings.Admin.Queries.Models;
using ApplicationLayer.Features.GenralSettings.Admin.Commands.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace API_ERP_Layer.Controllers.Genral_Settings
{
    [ApiController]
    [Route("api/genralsettings/admin/[controller]")]
    public class AdminPanelSettingController : ERP_System.Controllers.AppControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public AdminPanelSettingController(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// رفع صورة الشعار (إضافة أو تحديث). يُرجع المسار النسبي لحفظه في الحقل Photo.
        /// </summary>
        [HttpPost("upload-photo")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { Message = "لم يتم اختيار ملف." });

            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
            var contentType = file.ContentType?.ToLowerInvariant() ?? "";
            if (!allowedTypes.Contains(contentType))
                return BadRequest(new { Message = "نوع الملف غير مدعوم. يُسمح بـ: JPEG, PNG, GIF, WebP فقط." });

            const long maxBytes = 5 * 1024 * 1024; // 5 MB
            if (file.Length > maxBytes)
                return BadRequest(new { Message = "حجم الملف يتجاوز 5 ميجابايت." });

            var webRoot = string.IsNullOrEmpty(_env.WebRootPath)
                ? Path.Combine(_env.ContentRootPath, "wwwroot")
                : _env.WebRootPath;
            var uploadsDir = Path.Combine(webRoot, "uploads", "admin-panel");
            Directory.CreateDirectory(uploadsDir);

            var ext = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(ext)) ext = ".jpg";
            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(uploadsDir, fileName);

            await using (var stream = new FileStream(fullPath, FileMode.Create))
                await file.CopyToAsync(stream);

            var relativePath = $"/uploads/admin-panel/{fileName}";
            return Ok(new { path = relativePath, message = "تم رفع الصورة بنجاح." });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 0) return BadRequest("Parameter Are Wrong");

            var response = await Mediator.Send(new GetAdminPanelSettingQuery { Id = id });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdminPanelSettingDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await Mediator.Send(new CreateAdminPanelSettingCommand { Dto = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AdminPanelSettingDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await Mediator.Send(new UpdateAdminPanelSettingCommand { Dto = dto });
            if (response != null && response.GetType().IsGenericType && response.GetType().GetGenericTypeDefinition() == typeof(Response<>))
                return NewResult((dynamic)response);

            return Ok(response);
        }
    }
}
