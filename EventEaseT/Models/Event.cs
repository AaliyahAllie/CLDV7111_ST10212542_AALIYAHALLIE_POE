namespace EventEaseT.Models
{
    // Event represents a scheduled activity that takes place at a specific venue.
    // It is linked to a Venue and can be booked by customers.
    public class Event
    {
        // Primary key for the Event table
        public int EventId { get; set; }

        // Name of the event (e.g., "Music Festival")
        public string EventName { get; set; }

        // Date and time when the event will occur
        public DateTime EventDate { get; set; }

        // Optional description providing more details about the event
        public string Description { get; set; }

        // Foreign key linking the event to a Venue
        public int VenueId { get; set; }

        // Navigation property for the related Venue
        public Venue Venue { get; set; }

        // Status of the event (Pending, Approved, Denied)
        // Defaults to "Pending" when created
        public string Status { get; set; } = "Pending";
    }
}