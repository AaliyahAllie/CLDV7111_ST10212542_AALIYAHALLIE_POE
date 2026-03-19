using System.ComponentModel.DataAnnotations;

namespace EventEaseT.Models
{
    // Venue represents a physical location where events can be hosted.
    // It is linked to multiple events and includes capacity and optional image details.
    public class Venue
    {
        // Primary key for the Venue table
        public int VenueId { get; set; }

        // Name of the venue (required)
        [Required(ErrorMessage = "Venue name is required")]
        public string VenueName { get; set; }

        // Location of the venue (required)
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        // Maximum capacity of the venue (must be greater than 0)
        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        // Optional image URL for venue representation (e.g., photo of the venue)
        public string? ImageUrl { get; set; }

        // Navigation property: collection of events hosted at this venue
        public ICollection<Event>? Events { get; set; }
    }
}