

namespace wennovation_hub.Models
{
    public class Bookings
    {
        public int Id { get; set; }
        public string? BookingId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? Company { get; set; }

        public string? Location { get; set; }
        public string? SpaceType { get; set; }
        public string? Amount { get; set; }
        public DateTime? Date { get; set; }
        public string? Duration { get; set; }
        public string? Purpose { get; set; }
        public string? NoOfPeople { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}