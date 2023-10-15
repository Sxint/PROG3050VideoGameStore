using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.ViewModels
{
    public class AddressListVM
    {
        public List<UserAddress>? AllAddresses { get; set; }

        public int? ProfileId { get; set; }
    }
}
