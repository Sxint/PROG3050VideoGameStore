namespace PROG3050VideoGameStore.Models
{
    public class ProfilePreferences
    {
        public string? Name { get; set; }

        public string? Gender { get; set; }

        public DateOnly? Birthday { get; set; }

        public DateOnly AccountCreationDate { get; set; }

        public bool? ReceiveEmails { get; set; }

        public string? Platforms { get; set; } //set this to list later

        public string? Genres { get; set; } //set this to list later

        public string? Language { get; set; } //set this to list later

    }
}
