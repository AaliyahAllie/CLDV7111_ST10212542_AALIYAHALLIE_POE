namespace EventEaseT.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int VenueId { get; set; }
        public Venue Venue { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string PersonName { get; set; }
        public string ContactDetails { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}