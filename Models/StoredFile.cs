namespace TechNewsUI.Models
{
    public class StoredFile
    {
        public int StoredFileId { get; set; }
        public string StoredFileName { get; set; }
        public Byte[] StoredFilePath { get; set; }
        public string FileType { get; set; }
        //public int FieldType { get; set; }

        public int? SectionMasterId { get; set; }
    }
}
