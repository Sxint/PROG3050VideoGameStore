﻿using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateOfEvent { get; set; } = DateTime.Now;

        [Required]
        public string Description { get; set; }
    }
}
