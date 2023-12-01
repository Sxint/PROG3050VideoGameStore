using System.ComponentModel.DataAnnotations;

namespace PROG3050VideoGameStore.ViewModels
{
    public class ExpiryDateCheck:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string expiryDate)
            {
                // Parse the expiry date string to get the month and year
                if (DateTime.TryParseExact(expiryDate, "MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedExpiryDate))
                {
                    string year = parsedExpiryDate.Year.ToString();
                    if (year[0] == '1' && year[1]=='9')
                    {
                        char num1 = year[2];
                        char num2 = year[3];
                        string newYear =  "20"+num1.ToString()+num2.ToString();

                        int Year = int.Parse(newYear);

                        if (Year > DateTime.Now.Year)
                        {
                            return true;
                        }

                        else if (Year == DateTime.Now.Year)
                        {
                            return parsedExpiryDate.Month > DateTime.Now.Month;
                        }

                        else
                        {
                            return false;
                        }
                    }

                    else
                    {
                        if (parsedExpiryDate.Year > DateTime.Now.Year)
                        {
                            return true;
                        }

                        else if (parsedExpiryDate.Year == DateTime.Now.Year)
                        {
                            return parsedExpiryDate.Month > DateTime.Now.Month;
                        }

                        else
                        {
                            return false;
                        }
                    }
                }
            }

            // If the value is not a valid string or cannot be parsed, consider it as invalid
            return false;
        }
    }
}
