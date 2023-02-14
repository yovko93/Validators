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
            string PATTERN = @"^(875|876|877|878|879|882|883|884|885|886|887|888|889|890|892|893|894|895|896|897|898|899|988|989|999){1}[0-9]{6}$";
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
