using System.Text.RegularExpressions;

namespace Validators.EGN_Validator
{
    public class EgnValidator
    {
        public record EGNInfo()
        {
            public string EGN { get; set; } = string.Empty;
            public string? Sex { get; set; }
            public DateTime? BirthDate { get; set; }
            public string? Region { get; set; }
        }

        private readonly int[] EGN_WEIGHTS = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };

        private readonly Dictionary<int, string> EGN_REGIONS = new()
        {
            /* Отделени номера */
            { 43, "Blagoevgrad" }, /* от 000 до 043 */
            { 93, "Burgas" },  /* от 044 до 093 */
            { 139, "Varna" }, /* от 094 до 139 */
            { 169, "Veliko Tarnovo" }, /* от 140 до 169 */
            { 183, "Vidin" }, /* от 170 до 183 */
            { 217, "Vraca" }, /* от 184 до 217 */
            { 233, "Gabrovo" }, /* от 218 до 233 */
            { 281, "Kardjali" }, /* от 234 до 281 */
            { 301, "Kustendil" }, /* от 282 до 301 */
            { 319, "Lovech" }, /* от 302 до 319 */
            { 341, "Montana" }, /* от 320 до 341 */
            { 377, "Pazardjik" }, /* от 342 до 377 */
            { 395, "Pernik" }, /* от 378 до 395 */
            { 435, "Pleven" }, /* от 396 до 435 */
            { 501, "Plovdiv" }, /* от 436 до 501 */
            { 527, "Razgrad" }, /* от 502 до 527 */
            { 555, "Ruse" }, /* от 528 до 555 */
            { 575, "Silistra" }, /* от 556 до 575 */
            { 601, "Sliven" }, /* от 576 до 601 */
            { 623, "Smolyan" }, /* от 602 до 623 */
            { 721, "Sofia - town" }, /* от 624 до 721 */
            { 751, "Sofia -region" }, /* от 722 до 751 */
            { 789, "Stara Zagora" }, /* от 752 до 789 */
            { 821, "Dobrich" },   /* от 790 до 821 */
            { 843, "Targovishte" }, /* от 822 до 843 */
            { 871, "Haskovo" }, /* от 844 до 871 */
            { 903, "Shumen" }, /* от 872 до 903 */
            { 925, "Jambol" }, /* от 904 до 925 */
            { 999, "Other/Unknown" }, /* от 926 до 999 - Такъв регион понякога се ползва при родени преди 1900, за родени в чужбина
                                                или ако в даден регион се родят повече деца от предвиденото. Доколкото ми е
                                                известно няма правило при ползването на 926 - 999 */
        };

        public bool IsValid(string egn)
        {
            if (!IsValidBirthDate(egn))
            {
                return false;
            }

            byte checksum = Convert.ToByte(egn.Substring(9, 1));
            int egnsum = 0;
            for (var i = 0; i < 9; i++)
            {
                egnsum += Convert.ToInt32(egn.Substring(i, 1)) * EGN_WEIGHTS[i];
            }

            var valid_checksum = egnsum % 11;
            if (valid_checksum == 10)
            { valid_checksum = 0; }
            if (checksum == valid_checksum)
            {
                return true;
            }
            return false;
        }

        public EGNInfo Parse(string egn)
        {
            if (!IsValid(egn))
            {
                throw new InvalidDataException($"{egn} invalid format");
            }
            var birthDate = GetValidDate(egn[..6]);

            var info = new EGNInfo { EGN = egn, BirthDate = birthDate };
            var region = Convert.ToInt32(egn.Substring(6, 3));

            if (Convert.ToInt32(egn.Substring(8, 1)) % 2 == 0)
            {
                info.Sex = "M";
            }
            else
            {
                region--;
                info.Sex = "F";
            }

            var first_region_num = 0;
            var keys = EGN_REGIONS.Keys.ToList();
            foreach (var key in keys)
            {
                if (region >= first_region_num && region <= key)
                {
                    info.Region = EGN_REGIONS[key];
                    break;
                }
                first_region_num = key + 1;
            }

            return info;
        }

        #region Helpers
        private static bool IsValidBirthDate(string egn)
        {
            if (string.IsNullOrWhiteSpace(egn) || egn.Length != 10)
            {
                return false;
            }
            if (!Regex.IsMatch(egn, "[0-9]{10}"))
            {
                return false;
            }
            try
            {
                GetValidDate(egn[..6]);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static DateTime GetValidDate(string subEgn)
        {
            int year = Convert.ToByte(subEgn[..2]);
            byte month = Convert.ToByte(subEgn.Substring(2, 2));
            byte day = Convert.ToByte(subEgn.Substring(4, 2));

            if (month > 40)
            {
                month -= 40;
                year += 2000;
            }
            else
           if (month > 20)
            {
                month -= 20;
                year += 1800;
            }
            else
            {
                year += 1900;
            }
            return DateTime.ParseExact($"{day:00}/{month:00}/{year}", "dd/MM/yyyy", null);
        }
        #endregion
    }
}
