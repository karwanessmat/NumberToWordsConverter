namespace NumberToKurdishWords.Extensions
{
 public static  class Conversion
    {
        public static string NumberToWords(this long number)
        {
            var realNumber = number;
            switch (number)
            {
                case 0:
                    return "سفر";
                case < 0:
                    return " - " + NumberToWords(Math.Abs(number));
            }

            var words = " ";
            if (realNumber> 999999999999999)
            {
                return "پشتگیری ژماره‌ بەرزتر لە تریلیۆن ناكات"; // doesn't support numbers above one trillion
            }


            if ((number / 1000000000000 > 0))
            {
                words += NumberToWords(number / 1000000000000) + " تریلیۆن و ";
                number %= 1000000000000;
            }

            if ((number / 1000000000 > 0))
            {
                words += NumberToWords(number / 1000000000) + " ملیار و ";
                number %= 1000000000;
            }

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " ملیۆن و ";
                number %= 1000000;
            }
            var getNumber = number % 1000;

            if ((number / 1000) > 0)
            {
                if (realNumber % 1000 != 0)
                {

                    words += NumberToWords(number / 1000) + " هه‌زار و ";


                }
                else
                {
                    words += NumberToWords(number / 1000) + " هه‌زار  ";

                }
                number %= 1000;
            }


            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " سه‌د ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != " ")
                    words += " و ";

                var unitsMap = new[] { "سفر", "یه‌ك", "دوو", "سێ", "چوار", "پێنج", "شه‌ش", "حه‌وت", "هه‌شت", "نۆ", "ده‌", "یازده‌", "دوازده‌", "سێزده‌", "چوارده‌", "پازده‌", "شازده‌", "حه‌ڤده‌", "هه‌ژده‌", "نۆزده‌" };
                var tensMap = new[] { "سفر", "ده‌", "بیست", "سی", "چل", "په‌نجا", "شه‌ست", "حه‌فتا", "هه‌شتا", "نۆوه‌ت" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " و " + unitsMap[number % 10];
                }


            }


            words = words.Replace("یه‌ك سه‌د", "سه‌د");

            words = words.Replace("یه‌ك هه‌زار و", "هه‌زار");


            words = words.Replace("یه‌ك ملیۆن", "ملیۆنێك");
            words = words.Replace("سه‌د و ملیۆنێك", "سه‌د و یه‌ك ملیۆن");


            words = words.Replace("یه‌ك ملیار", "ملیارێك");
            words = words.Replace("سه‌د و ملیارێك", "سه‌د و یه‌ك ملیار");

            words = words.Replace("یه‌ك تریلیۆن", "تریلیۆنێك");
            words = words.Replace("سه‌د و تریلیۆنێك", "سه‌د و یه‌ك تریلیۆن");
            words = words.Replace("  ", " ");
            return words;

        }

    }

}
