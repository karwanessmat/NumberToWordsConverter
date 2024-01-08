namespace NumSpeaks;

public static class ConvertNumbersToArabicAlphabet
{
    public static string NumberWordsInArabic(this object val)
    {

        var isNumber = long.TryParse(val.ToString(), out var number);

        if (!isNumber)
        {
            return "وهو يدعم الأرقام فقط.";
        }

        if (number == 0) return "صفر";

        if (number < 0) return "minus " + Math.Abs(number).NumberWordsInArabic();

        string words = "";

        if (number / 1000000000 > 0)
        {
            words += ConvertBillions(number / 1000000000);
            number %= 1000000000;
            if (number > 0) words += " و ";
        }

        if (number / 1000000 > 0)
        {
            words += ConvertMillions(number / 1000000);
            number %= 1000000;
            if (number > 0) words += " و ";
        }

        if (number / 1000 > 0)
        {
            words += ConvertThousands(number / 1000);
            number %= 1000;
            if (number > 0) words += " و ";
        }

        if (number / 100 > 0)
        {
            words += ConvertHundreds(number / 100);
            number %= 100;
            if (number > 0) words += " و ";
        }

        if (number > 0)
        {
            words += ConvertTensAndUnits(number);
        }

        return words.Trim();
    }

    private static string ConvertBillions(long number)
    {
        if (number == 1) return "مليار";
        if (number == 2) return "ملياران";
        if (number > 2 && number < 11) return number.NumberWordsInArabic() + " مليارات";
        return number.NumberWordsInArabic() + " مليار";
    }

    private static string ConvertMillions(long number)
    {
        if (number == 1) return "مليون";
        if (number == 2) return "إثنان مليون";
        if (number > 2 && number < 11) return number.NumberWordsInArabic() + " ملايين";
        return number.NumberWordsInArabic() + " مليون";
    }

    private static string ConvertThousands(long number)
    {
        if (number == 1) return "ألف";
        if (number == 2) return "ألفان";
        if (number > 2 && number < 11) return number.NumberWordsInArabic() + " آلاف";
        return number.NumberWordsInArabic() + " ألف";
    }

    private static string ConvertHundreds(long number)
    {
        if (number == 1) return "مئة";
        if (number == 2) return "مئتان";
        if (number > 2) return number.NumberWordsInArabic() + " مائة";
        return "";
    }

    private static string ConvertTensAndUnits(long number)
    {
        var unitsMap = new[] { "صفر", "واحد", "اثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة" };
        var teensMap = new[] { "عشرة", "إحدى عشر", "اثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر" };
        var tensMap = new[] { "صفر", "عشرة", "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" };

        if (number < 10) return unitsMap[number];
        if (number >= 11 && number <= 19) return teensMap[number - 10];

        var tens = tensMap[number / 10];
        var units = number % 10 > 0 ? unitsMap[number % 10] + " و " : "";
        return units + tens;
    }



}