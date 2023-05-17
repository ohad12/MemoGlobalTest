using System.Text.RegularExpressions;

namespace MemoGlobalTest.Services
{
    public class Validation
    {
        public static bool EmailIsValid(string email)
        {
            bool isValid = Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

            return isValid;
        }
    }
}
