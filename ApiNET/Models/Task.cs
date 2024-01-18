namespace ApiRestNet.Models
{
    public class Task
    {
        public int id { get; set; }
        public string? description { get; set; }
        public DateTime dateCreated { get; set; }
        public bool isCompleted { get; set; }
        public DateTime dateCompleted { get; set; }
    }
}