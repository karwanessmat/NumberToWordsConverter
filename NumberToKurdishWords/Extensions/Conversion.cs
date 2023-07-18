using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NumberToKurdishWords.Extensions
{
    public static class Conversion
    {
        private static bool _isNumberOnBillion;
        private static bool _isNumberOnTrillion;


        public static string ToKurdishText(this object val)
        {
            var isNumber = long.TryParse(val.ToString(), out var number);

            if (!isNumber)
            {
                return "تەنها پشتگیری ژمارە دەکات.";
            }

            switch (number)
            {
                case 0:
                    return "سفر";
                case < 0:
                    return " - " + ToKurdishText(Math.Abs(number));
            }

            var words = " ";
            var isNumberBiggerThanTrillion = number > 999999999999999;
            if (isNumberBiggerThanTrillion)
            {
                return "پشتگیری ژمارەی بەرزتر لە تریلیۆن ناکات."; // doesn't support numbers above one trillion
            }


            if ((number / 1000000000000 > 0))
            {
                words += ToKurdishText(number / 1000000000000) + " تریلیۆن و";
                number %= 1000000000000;
                _isNumberOnTrillion = true;
            }

            if ((number / 1000000000 > 0))
            {
                words += ToKurdishText(number / 1000000000) + " ملیار و";
                number %= 1000000000;
                _isNumberOnBillion = true;

            }

            if ((number / 1000000) > 0)
            {
                words += ToKurdishText(number / 1000000) + " ملیۆن و";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                if (number % 1000 != 0)
                {

                    words += ToKurdishText(number / 1000) + " هەزار و";


                }
                else
                {
                    words += ToKurdishText(number / 1000) + " هەزار ";

                }

                number %= 1000;
            }


            if ((number / 100) > 0)
            {
                words += ToKurdishText(number / 100) + " سەد ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != " ")
                {
                    words += " و ";
                }

                var unitsMap = new[] { "سفر", "یەک", "دوو", "سێ", "چوار", "پێنج", "شەش", "حەوت", "هەشت", "نۆ", "دە‌", "یازدە‌", "دوازدە‌", "سێزدە‌", "چواردە‌", "پازدە‌", "شازدە‌", "حەڤدە‌", "هەژدە‌", "نۆزدە‌" };
                var tensMap = new[] { "سفر", "دە‌", "بیست", "سی", "چل", "پەنجا", "شەست", "حەفتا", "هەشتا", "نۆوەت" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " و " + unitsMap[number % 10];
                }


            }


            words = words.Replace("یەک سەد", "سەد");

            words = words.Replace("یەک ملیۆن", "ملیۆنێک");
            words = words.Replace("سەد و ملیۆنێک", "سەد و یەک ملیۆن");


            words = words.Replace("یەک ملیار", "ملیارێک");
            words = words.Replace("سەد و ملیارێک", "سەد و یەک ملیار");

            words = words.Replace("یەک تریلیۆن", "تریلیۆنێک");
            words = words.Replace("سەد و تریلیۆنێک", "سەد و یەک تریلیۆن");
            words = words.Replace("  ", " ");
            if (!_isNumberOnBillion) return words;

            words = words.Replace("سەد ملیۆن", "سەد و یەک ملیۆن");
            words = words.Replace("و ملیۆنێک", "و یەک ملیۆن");
            words = words.Replace("و ملیارێک", "و یەک ملیار");

            if (!_isNumberOnTrillion) return words;
            words = words.Replace("سەد ملیار", "سەد و یەک ملیار");
            words = words.Replace("و تریلیۆنێک", "و یەک تریلیۆن");


            return words;
        }
    }
}
