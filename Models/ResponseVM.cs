namespace TechNewsUI.Models
{
    public class ResponseVM
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } 

        public List<string> Errors { get; set; }

        public dynamic Data { get; set; }
    }
}
