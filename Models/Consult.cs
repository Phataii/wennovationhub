

namespace wennovation_hub.Models
{
    public class Consult
    {
        public int Id { get; set; }
        public string? ConsultId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Organization { get; set; }
        public string? Service { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } =  DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}