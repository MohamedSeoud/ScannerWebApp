using Microsoft.AspNetCore.Mvc;
using ScannerWebApp.Models;
using System.Diagnostics;

namespace ScannerWebApp.Controllers
{
    public class CameraController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public CameraController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImage([FromBody] ImageUploadModel imageData)
        {
            if (imageData == null || string.IsNullOrEmpty(imageData.Image))
                return BadRequest(new { message = "Invalid image data." });

            // Decode Base64 string to a byte array
            var base64Data = imageData.Image.Replace("data:image/jpeg;base64,", "");
            byte[] imageBytes = Convert.FromBase64String(base64Data);

            // Save the file (e.g., to disk or database)
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "captured_id.jpeg");
            System.IO.File.WriteAllBytes(filePath, imageBytes);

            // Perform dimension and card content analysis if needed
            // (Optional: use Emgu CV or another library)

            return Ok(new { message = "Image uploaded and processed successfully!" });
        }

        public class ImageUploadModel
        {
            public string Image { get; set; }
        }


    }
}
