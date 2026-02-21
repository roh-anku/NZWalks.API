namespace NZWalks.API.Models.DTOs
{
    public class UploadImageRequestDto
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
