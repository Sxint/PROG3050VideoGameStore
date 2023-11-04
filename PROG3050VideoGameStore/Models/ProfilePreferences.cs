﻿using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace PROG3050VideoGameStore.Models
{
    public class ProfilePreferences
    {
        public int Id { get; set; }

        [Required]
        public string FavCategory { get; set; } = "None";
        [Required]
        public string FavPlatform { get; set; } = "None";
        [Required]
        public string Language { get; set; } = "None";

        public int UserProfileId { get; set; } // Required foreign key property
        public UserProfile? Profile { get; set; } // Required reference navigation to principal

    }
}
