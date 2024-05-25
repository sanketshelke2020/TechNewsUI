namespace TechNewsUI.Models
{
    public class StoredFileDto
    {
        public string? StoredFileName { get; set; }
        public IFormFile? File { get; set; }
        public Byte[] StoredFilePath { get; set; }
        public string? FileType { get; set; }
    }
}
