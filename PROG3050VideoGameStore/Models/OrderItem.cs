namespace PROG3050VideoGameStore.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int OrderId { get; set; }

        public Game? Game { get; set; }

        public Order? Order { get; set; }
    }
}
