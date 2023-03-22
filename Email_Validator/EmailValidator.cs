namespace Validators.Email_Validator
{
    #region Usings
    using System.Text.RegularExpressions;
    #endregion

    public class EmailValidator
    {
        public bool IsValid(string email)
        {
            return HandleValidation(email);
        }

        private bool HandleValidation(string email)
        {
            string PATTERN = @"^[a-zA-Z]+[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var isMatch = Regex.IsMatch(email, PATTERN);
            return isMatch;
        }
    }
}
