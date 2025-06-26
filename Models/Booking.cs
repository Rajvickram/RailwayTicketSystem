using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RailwayTicketSystem.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "From Location is required")]
        public string FromLocation { get; set; }

        [Required(ErrorMessage = "To Location is required")]
        public string ToLocation { get; set; }

        [Required(ErrorMessage = "Date of Journey is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfJourney { get; set; }

        [Required(ErrorMessage = "Seats are required")]
        [Range(1, 10, ErrorMessage = "You can book 1 to 10 seats only.")]
        public int Seats { get; set; }

        public int UserId { get; set; }  // Foreign key reference to User
    }
}
