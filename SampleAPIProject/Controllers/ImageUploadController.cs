using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MLMProject.Application.Interfaces;
using MLMProject.Application.IService;
using System.Net;
using MLMProject.Application.Common;

namespace MLMProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController() : ControllerBase
    {
        private readonly string ftpServer = "ftp://win8239.site4now.net";
        private readonly string URLS = "http://emscontent.workanthem.com";
        private readonly string ftpUser = "emscontent";
        private readonly string ftpPassword = "Anthem@nh22";
        private readonly string uploadsFolder = "/uploads/";

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            try
            {
                const string chars = "abcdefghijklmnopqrstuvwxyz";
                Random random = new Random();
                char[] stringChars = new char[50];
                for (int i = 0; i < 50; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var randomString = new string(stringChars);
                // Generate unique file name with timestamp
                var timestamp = DateTime.Now.ToString("ddMMyyyy_HHmmss_fff");
                var fileName = $"{randomString}{Path.GetExtension(file.FileName)}";

                //var fileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{timestamp}{Path.GetExtension(file.FileName)}";
                // Create the FTP URI with the 'uploads' folder
                var ftpUri = new Uri(new Uri(ftpServer), $"{uploadsFolder}{fileName}");
                var request = (FtpWebRequest)WebRequest.Create(ftpUri);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                using (var requestStream = await request.GetRequestStreamAsync())
                using (var fileStream = file.OpenReadStream())
                {
                    await fileStream.CopyToAsync(requestStream);
                }

                using (var response = (FtpWebResponse)await request.GetResponseAsync())
                {
                    // Construct the URL of the uploaded image
                    var imageUrl = $"{URLS}{uploadsFolder}{fileName}";

                    return Ok(new { Message = "File uploaded successfully.", ImageUrl = imageUrl });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
