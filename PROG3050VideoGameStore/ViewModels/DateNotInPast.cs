using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.ViewModels
{
    public class DateNotInPast:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                // Check if the date is in the past
                return date >= DateTime.Now.Date;
            }

            // If the value is not a DateTime, consider it as valid
            return true;
        }
    }
}
