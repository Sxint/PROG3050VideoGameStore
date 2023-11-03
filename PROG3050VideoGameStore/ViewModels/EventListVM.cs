using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.ViewModels
{
    public class EventListVM
    {
        public int ProfileId { get; set; }

        public List<Event> Events { get; set; }
    }
}
