using PROG3050VideoGameStore.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of Event")]
        [DateNotInPast(ErrorMessage = "Birth Date cannot be in the past")]
        public DateTime DateOfEvent { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Time of Event")]
        public TimeSpan TimeOfEvent { get; set; }


        [Required]
        public string Description { get; set; }
    }
}
