using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.ViewModels
{
    public class DateNotInFuture:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;

            // Check if the date is in the future
            if (date > DateTime.Now)
            {
                return false;
            }

            return true;
        }
        }
}
