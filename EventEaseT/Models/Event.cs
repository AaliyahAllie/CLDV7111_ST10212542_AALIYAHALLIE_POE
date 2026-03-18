namespace EventEaseT.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }

        // Foreign key
        public int VenueId { get; set; }

        // Navigation property
        public Venue Venue { get; set; }

        public string Status { get; set; } = "Pending";
    }
}