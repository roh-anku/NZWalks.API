using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadFile([FromForm] UploadImageRequestDto uploadImageRequestDto)
        {
            ValidateUploadedFile(uploadImageRequestDto);
            if (ModelState.IsValid)
            {
                //upload using repository
                //convert dto to domain
                var imageDomain = new Image
                {
                    File = uploadImageRequestDto.File,
                    FileName = uploadImageRequestDto.FileName,
                    FileDescription = uploadImageRequestDto.FileDescription,
                    FileExtension = Path.GetExtension(uploadImageRequestDto.File.FileName),
                    FileSizeInBytes = uploadImageRequestDto.File.Length
                };

                var imageUploadResponse = await _imageRepository.Upload(imageDomain);

                //convert domain to dto and send
               return Ok(imageUploadResponse);
            }

            return BadRequest(ModelState);
        }

        private void ValidateUploadedFile(UploadImageRequestDto request)
        {
            string[] extensions = new string[] {".png",".jpg",".jpeg" };

            if (!extensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "upload is more than 10 mb, lessen the size");
            }
        }
    }
}
