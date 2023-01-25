namespace Validators.Eik_Validator
{
    #region Usings
    using System.Text.RegularExpressions;
    #endregion

    public class EikValidator
    {
        // BULSTAT - 153773988 or BG153773988
        // 131071587, 130408101, BG131129282
        public bool HandleValidation(string input)
        {
            // 1. Remove whitespaces and letters
            input = ReturnEIKDigits(input);

            // 2. Check BULSTAT length 9 OR 13
            if (input.Length == 9 || input.Length == 13)
            {
                return BulstatEIKValidator(input);
            }

            return false;
        }

        //BULSTAT - 153773988
        private bool BulstatEIKValidator(string numbers)
        {
            var match = Regex.Match(numbers, @"^((\d{9})(\d{4})?)$");

            if (!match.Success)
            {
                return false;
            }

            var a = ReturnDigitsArray(match.Groups[2].Value);

            var a9 = a[0] * 1 + a[1] * 2 + a[2] * 3 + a[3] * 4 + a[4] * 5 + a[5] * 6 + a[6] * 7 + a[7] * 8;
            a9 = a9 % 11;

            if (a9 == 10)
            {
                a9 = a[0] * 3 + a[1] * 4 + a[2] * 5 + a[3] * 6 + a[4] * 7 + a[5] * 8 + a[6] * 9 + a[7] * 10;
                a9 = a9 % 11;
            }

            a9 = a9 == 10 ? 0 : a9;

            if (a9 != a[8])
            {
                return false;
            }

            if (match.Groups[3].Value == string.Empty)
            {
                return true;
            }

            a = ReturnDigitsArray(match.Groups[3].Value);

            var a13 = a9 * 2 + a[0] * 7 + a[1] * 3 + a[2] * 5;
            a13 = a13 % 11;

            if (a13 == 10)
            {
                a13 = a9 * 4 + a[0] * 9 + a[1] * 5 + a[2] * 7;
                a13 = a13 % 11;
            }

            a13 = a13 == 10 ? 0 : a13;

            return a13 == a[3];
        }

        // Not in use
        // BULSTAT - 153773988 or BG153773988
        private bool BulstatValidator(string bulstat)
        {
            Match parts = Regex.Match(bulstat, @"^(BG)(\d{9,})");

            if (parts != null)
            {
                return BulstatEIKValidator(parts.Groups[2].Value);
            }

            return BulstatEIKValidator(bulstat);
        }

        #region Helpers
        private string RemoveWhiteSpaces(string str)
        {
            return Regex.Replace(str, @"\s+", string.Empty);
        }

        private string ReturnEIKDigits(string str)
        {
            return Regex.Replace(str, @"\D+", string.Empty);
        }

        private int[] ReturnDigitsArray(string str)
        {
            var a = new int[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                a[i] = str[i] - 48;
            }

            return a;
        }
        #endregion
    }
}
