namespace PROG3050VideoGameStore.Models
{
    public class EventParticipation
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int UserProfileId { get; set; }

        public UserProfile? Profile { get; set; } // nav property

        public Event? Event { get; set; } // nav property


    }
}
