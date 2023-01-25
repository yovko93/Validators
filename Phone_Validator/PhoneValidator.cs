namespace Validators.Phone_Validator
{
    #region Usings
    using System.Text.RegularExpressions;
    #endregion

    public class PhoneValidator
    {
        public bool IsValid(string phoneNumber)
        {
            // 1. Remove whitespaces and letters
            phoneNumber = ReturnDigits(phoneNumber);

            // 2. Check PhoneNumber length
            if (phoneNumber.Length == 9)
            {
                return HandleValidation(phoneNumber);
            }

            return false;
        }

        private bool HandleValidation(string phoneNumber)
        {
            string PATTERN = @"^[8]{1}[7-9]{1}[2-9]{1}[0-9]{6}$";
            var isMatch = Regex.IsMatch(phoneNumber, PATTERN);
            return isMatch;
        }

        #region Helpers
        private string ReturnDigits(string str)
        {
            return Regex.Replace(str, @"\D+", string.Empty);
        }
        #endregion
    }
}
