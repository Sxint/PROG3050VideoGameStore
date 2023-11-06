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
        public DateTime DateOfEvent { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Time of Event")]
        public TimeSpan TimeOfEvent { get; set; }


        [Required]
        public string Description { get; set; }
    }
}
