namespace EventEaseT.Models
{
    // Booking represents a reservation made by a customer for a specific event at a venue.
    // It links together User, Event, and Venue entities.
    public class Booking
    {
        // Primary key for the Booking table
        public int BookingId { get; set; }

        // Foreign key to the Event table
        public int EventId { get; set; }
        public Event Event { get; set; }   // Navigation property for related Event

        // Foreign key to the Venue table
        public int VenueId { get; set; }
        public Venue Venue { get; set; }   // Navigation property for related Venue

        // Foreign key to the User table
        public int UserId { get; set; }
        public User User { get; set; }     // Navigation property for related User

        // Name of the person making the booking (customer-facing field)
        public string PersonName { get; set; }

        // Contact details for the booking (e.g., phone or email)
        public string ContactDetails { get; set; }

        // Date and time when the booking was created
        // Defaults to current date/time if not explicitly set
        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}