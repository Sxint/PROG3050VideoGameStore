namespace PROG3050VideoGameStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int GameId { get; set; }

        public int CartId { get; set; }

        public Game? Game { get; set; }

        public Cart? Cart { get; set; }
    }
}
