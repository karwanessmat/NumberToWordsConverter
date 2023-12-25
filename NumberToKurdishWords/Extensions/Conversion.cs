namespace NumberToKurdishWords.Extensions
{
    public static class Conversion
    {
        private static bool _isNumberOnBillion;
        private static bool _isNumberOnTrillion;
        

        public static string WordsInKurdish(this object val)
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
                    return " - " + WordsInKurdish(Math.Abs(number));
            }

            var words = " ";
            var isNumberBiggerThanTrillion = number > 999999999999999;
            if (isNumberBiggerThanTrillion)
            {
                return "پشتگیری ژمارەی بەرزتر لە تریلیۆن ناکات."; // doesn't support numbers above one trillion
            }


            if ((number / 1000000000000 > 0))
            {
                words += WordsInKurdish(number / 1000000000000) + " تریلیۆن و";
                number %= 1000000000000;
                _isNumberOnTrillion = true;
            }

            if ((number / 1000000000 > 0))
            {
                words += WordsInKurdish(number / 1000000000) + " ملیار و";
                number %= 1000000000;
                _isNumberOnBillion = true;

            }

            if ((number / 1000000) > 0)
            {
                words += WordsInKurdish(number / 1000000) + " ملیۆن و";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                if (number % 1000 != 0)
                {

                    words += WordsInKurdish(number / 1000) + " هەزار و";


                }
                else
                {
                    words += WordsInKurdish(number / 1000) + " هەزار ";

                }

                number %= 1000;
            }


            if ((number / 100) > 0)
            {
                words += WordsInKurdish(number / 100) + " سەد ";
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

            var replacements = new Dictionary<string, string>()
            {
                { "یەک سەد", "سەد" },
                { "یەک ملیۆن", "ملیۆنێک" },
                { "سەد و ملیۆنێک", "سەد و یەک ملیۆن" },
                { "یەک ملیار", "ملیارێک" },
                { "سەد و ملیارێک", "سەد و یەک ملیار" },
                { "یەک تریلیۆن", "تریلیۆنێک" },
                { "سەد و تریلیۆنێک", "سەد و یەک تریلیۆن" },
                { "  ", " " }
            };
            words = words.ReplaceString(replacements);
            if (!_isNumberOnBillion) return words;

            replacements.Add("سەد ملیۆن", "سەد و یەک ملیۆن");
            replacements.Add("و ملیۆنێک", "و یەک ملیۆن");
            replacements.Add("و ملیارێک", "و یەک ملیار");
            words = words.ReplaceString(replacements);

            if (!_isNumberOnTrillion) return words;

            replacements.Add("سەد ملیار", "سەد و یەک ملیار");
            replacements.Add("و تریلیۆنێک", "و یەک تریلیۆن");
            words = words.ReplaceString(replacements);
            return words;
        }
    }


    public static class ConvertNumbersToArabicAlphabet
    {
        public static string WordsInArabic(this object val)
        {
            var isNumber = long.TryParse(val.ToString(), out var number);

            if (!isNumber)
            {
                return "إنه يدعم الأرقام فقط.";
            }

            var strNumber= number.ToString();
            if (strNumber.Contains('.'))
            {
                if (strNumber.Split('.')[0].ToCharArray().Length > 6)
                {
                    return "No Number";
                }

                return strNumber.Split('.')[0].ToCharArray().Length switch
                {
                    1 => convertOneDigits(strNumber) + " و " + ConvertTwoDigits(strNumber.Split('.')[1]),
                    2 => ConvertTwoDigits(strNumber) + " و " + ConvertTwoDigits(strNumber.Split('.')[1]),
                    3 => ConvertThreeDigits(strNumber) + " و " + ConvertTwoDigits(strNumber.Split('.')[1]),
                    4 => ConvertFourDigits(strNumber) + " و " + ConvertTwoDigits(strNumber.Split('.')[1]),
                    5 => ConvertFiveDigits(strNumber) + " و " + ConvertTwoDigits(strNumber.Split('.')[1]),
                    6 => ConvertSixDigits(strNumber) + " و " + ConvertTwoDigits(strNumber.Split('.')[1]),
                    _ => ""
                };
            }

            if (strNumber.ToCharArray().Length > 6)
            {
                return "No Number";
            }

            return strNumber.ToCharArray().Length switch
            {
                1 => convertOneDigits(strNumber),
                2 => ConvertTwoDigits(strNumber),
                3 => ConvertThreeDigits(strNumber),
                4 => ConvertFourDigits(strNumber),
                5 => ConvertFiveDigits(strNumber),
                6 => ConvertSixDigits(strNumber),
                _ => ""
            };
        }
        private static string ConvertTwoDigits(string twoDigits)
        {
            var returnAlpha = "00";
            if (twoDigits.ToCharArray()[0] == '0' && twoDigits.ToCharArray()[1] != '0')
            {
                return convertOneDigits(twoDigits.ToCharArray()[1].ToString());
            }
            else
            {
                switch (int.Parse(twoDigits.ToCharArray()[0].ToString()))
                {
                    case 1:
                        {
                            switch (int.Parse(twoDigits.ToCharArray()[1].ToString()))
                            {
                                case 1:
                                    return "إحدى عشر";
                                case 2:
                                    return "إثنى عشر";
                                default:
                                    returnAlpha = "عشر";
                                    return convertOneDigits(twoDigits.ToCharArray()[1].ToString()) + " " + returnAlpha;
                            }
                        }
                    case 2: returnAlpha = "عشرون"; break;
                    case 3: returnAlpha = "ثلاثون"; break;
                    case 4: returnAlpha = "أريعون"; break;
                    case 5: returnAlpha = "خمسون"; break;
                    case 6: returnAlpha = "ستون"; break;
                    case 7: returnAlpha = "سبعون"; break;
                    case 8: returnAlpha = "ثمانون"; break;
                    case 9: returnAlpha = "تسعون"; break;
                    default: returnAlpha = ""; break;
                }
            }
            if (convertOneDigits(twoDigits.ToCharArray()[1].ToString()).Length == 0)
            { return returnAlpha; }
            else
            {
                return convertOneDigits(twoDigits.ToCharArray()[1].ToString()) + " و " + returnAlpha;
            }
        }
        private static string convertOneDigits(string oneDigits)
        {
            return int.Parse(oneDigits) switch
            {
                1 => "واحد",
                2 => "إثنان",
                3 => "ثلاثه",
                4 => "أربعه",
                5 => "خمسه",
                6 => "سته",
                7 => "سبعه",
                8 => "ثمانيه",
                9 => "تسعه",
                _ => ""
            };
        }
        private static string ConvertThreeDigits(string threeDigits)
        {
            switch (int.Parse(threeDigits.ToCharArray()[0].ToString()))
            {
                case 1:
                    {
                        if (int.Parse(threeDigits.ToCharArray()[1].ToString()) == 0)
                        {
                            if (int.Parse(threeDigits.ToCharArray()[2].ToString()) == 0)
                            {
                                return "مائه";
                            }
                            return "مائه" + " و " + convertOneDigits(threeDigits.ToCharArray()[2].ToString());
                        }
                        else
                        {
                            return "مائه" + " و " + ConvertTwoDigits(threeDigits.Substring(1, 2));
                        }
                    }
                case 2:
                    {
                        if (int.Parse(threeDigits.ToCharArray()[1].ToString()) == 0)
                        {
                            if (int.Parse(threeDigits.ToCharArray()[2].ToString()) == 0)
                            {
                                return "مائتين";
                            }
                            return "مائتين" + " و " + convertOneDigits(threeDigits.ToCharArray()[2].ToString());
                        }
                        else
                        {
                            return "مائتين" + " و " + ConvertTwoDigits(threeDigits.Substring(1, 2));
                        }
                    }
                case 3:
                    {
                        if (int.Parse(threeDigits.ToCharArray()[1].ToString()) == 0)
                        {
                            if (int.Parse(threeDigits.ToCharArray()[2].ToString()) == 0)
                            {
                                return convertOneDigits(threeDigits.ToCharArray()[0].ToString()).Split('ه')[0] + "مائه";
                            }
                            return convertOneDigits(threeDigits.ToCharArray()[0].ToString()).Split('ه')[0] + "مائه" + " و " + convertOneDigits(threeDigits.ToCharArray()[2].ToString());
                        }
                        else
                        {
                            return convertOneDigits(threeDigits.ToCharArray()[0].ToString()).Split('ه')[0] + "مائه" + " و " + ConvertTwoDigits(threeDigits.Substring(1, 2));
                        }
                    }
                case 4:
                    {
                        goto case 3;
                    }
                case 5:
                    {
                        goto case 3;
                    }
                case 6:
                    {
                        goto case 3;
                    }
                case 7:
                    {
                        goto case 3;
                    }
                case 8:
                    {
                        goto case 3;
                    }
                case 9:
                    {
                        goto case 3;
                    }
                case 0:
                    {
                        if (threeDigits.ToCharArray()[1] == '0')
                        {
                            return threeDigits.ToCharArray()[2] == '0' ? "" : convertOneDigits(threeDigits.ToCharArray()[2].ToString());
                        }
                        else
                        {
                            return ConvertTwoDigits(threeDigits.Substring(1, 2));
                        }
                    }
                default: return "";
            }
        }
        private static string ConvertFourDigits(string fourDigits)
        {
            switch (int.Parse(fourDigits.ToCharArray()[0].ToString()))
            {
                case 1:
                    {
                        if (int.Parse(fourDigits.ToCharArray()[1].ToString()) == 0)
                        {
                            if (int.Parse(fourDigits.ToCharArray()[2].ToString()) != 0)
                                return "ألف" + " و " + ConvertTwoDigits(fourDigits.Substring(2, 2));
                            if (int.Parse(fourDigits.ToCharArray()[3].ToString()) == 0)
                                return "ألف";
                            else
                            {
                                return "ألف" + " و " + convertOneDigits(fourDigits.ToCharArray()[3].ToString());
                            }
                        }
                        else
                        {
                            return "ألف" + " و " + ConvertThreeDigits(fourDigits.Substring(1, 3));
                        }
                    }
                case 2:
                    {
                        if (int.Parse(fourDigits.ToCharArray()[1].ToString()) == 0)
                        {
                            if (int.Parse(fourDigits.ToCharArray()[2].ToString()) != 0)
                                return "ألفين" + " و " + ConvertTwoDigits(fourDigits.Substring(2, 2));
                            if (int.Parse(fourDigits.ToCharArray()[3].ToString()) == 0)
                                return "ألفين";
                            else
                            {
                                return "ألفين" + " و " + convertOneDigits(fourDigits.ToCharArray()[3].ToString());
                            }
                        }
                        else
                        {
                            return "ألفين" + " و " + ConvertThreeDigits(fourDigits.Substring(1, 3));
                        }
                    }
                case 3:
                    {
                        if (int.Parse(fourDigits.ToCharArray()[1].ToString()) == 0)
                        {
                            if (int.Parse(fourDigits.ToCharArray()[2].ToString()) == 0)
                            {
                                if (int.Parse(fourDigits.ToCharArray()[3].ToString()) == 0)
                                    return convertOneDigits(fourDigits.ToCharArray()[0].ToString()) + " ألاف";
                                else
                                {
                                    return convertOneDigits(fourDigits.ToCharArray()[0].ToString()) + " ألاف" + " و " + convertOneDigits(fourDigits.ToCharArray()[3].ToString());
                                }
                            }
                            return convertOneDigits(fourDigits.ToCharArray()[0].ToString()) + " ألاف" + " و " + ConvertTwoDigits(fourDigits.Substring(2, 2));
                        }
                        else
                        {
                            return convertOneDigits(fourDigits.ToCharArray()[0].ToString()) + " ألاف" + " و " + ConvertThreeDigits(fourDigits.Substring(1, 3));
                        }
                    }
                case 4:
                    {
                        goto case 3;
                    }
                case 5:
                    {
                        goto case 3;
                    }
                case 6:
                    {
                        goto case 3;
                    }
                case 7:
                    {
                        goto case 3;
                    }
                case 8:
                    {
                        goto case 3;
                    }
                case 9:
                    {
                        goto case 3;
                    }
                default: return "";
            }
        }
        private static string ConvertFiveDigits(string fiveDigits)
        {
            if (ConvertThreeDigits(fiveDigits.Substring(2, 3)).Length == 0)
            {
                return ConvertTwoDigits(fiveDigits.Substring(0, 2)) + " ألف ";
            }
            else
            {
                return ConvertTwoDigits(fiveDigits.Substring(0, 2)) + " ألفا " + " و " + ConvertThreeDigits(fiveDigits.Substring(2, 3));
            }
        }
        private static string ConvertSixDigits(string sixDigits)
        {
            if (ConvertThreeDigits(sixDigits.Substring(2, 3)).Length == 0)
            {
                return ConvertThreeDigits(sixDigits.Substring(0, 3)) + " ألف ";
            }
            else
            {
                return ConvertThreeDigits(sixDigits.Substring(0, 3)) + " ألفا " + " و " + ConvertThreeDigits(sixDigits.Substring(3, 3));
            }
        }




    }


}
