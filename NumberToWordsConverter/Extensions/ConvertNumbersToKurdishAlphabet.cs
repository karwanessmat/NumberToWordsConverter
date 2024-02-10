using System.Text;

namespace NumberToKurdishWords.Extensions;

public static class ConvertNumbersToKurdishAlphabet
{
    public static string NumberWordsInKurdish(this object val)
    {
        var isNumber = long.TryParse(val.ToString(), out var number);

        if (!isNumber)
        {
            return "تەنها پشتگیری ژمارە دەکات.";
        }


        if (number == 0)
            return "سفر";

        if (number < 0)
            return "- " + NumberWordsInKurdish(Math.Abs(number));

        if (number > 999999999999999)
            return "پشتگیری ژمارەی بەرزتر لە تریلیۆن ناکات.";

        var words = new StringBuilder();
        words.Append(ConvertNumberToKurdish(number));
        return words.ToString().Trim();
    }

    private static string ConvertNumberToKurdish(long number)
    {
        var words = new StringBuilder();

        // Trillions
        if ((number / 1000000000000) > 0)
        {
            words.Append(ConvertTrillions(number / 1000000000000));
            number %= 1000000000000;
            if (number > 0) words.Append(" و ");
        }

        // Billions
        if ((number / 1000000000) > 0)
        {
            words.Append(ConvertBillions(number / 1000000000));
            number %= 1000000000;
            if (number > 0) words.Append(" و ");
        }

        // Millions
        if ((number / 1000000) > 0)
        {
            words.Append(ConvertMillions(number / 1000000));
            number %= 1000000;
            if (number > 0) words.Append(" و ");
        }

        // Thousands
        if ((number / 1000) > 0)
        {
            words.Append(ConvertThousands(number / 1000));
            number %= 1000;
            if (number > 0) words.Append(" و ");
        }

        // Hundreds
        if ((number / 100) > 0)
        {
            words.Append(ConvertHundreds(number / 100));
            number %= 100;
            if (number > 0) words.Append(" و ");
        }

        // Tens and Units
        if (number > 0)
        {
            words.Append(ConvertTensAndUnits(number));
        }

        return words.ToString();
    }
    private static string ConvertTrillions(long number)
    {
        if (number == 1)
            return "تریلیۆنێك";  // Special case for one trillion

        return NumberWordsInKurdish(number) + " تریلیۆن";
    }

    private static string ConvertBillions(long number)
    {
        if (number == 1)
            return "ملیارێك";  // Special case for one billion

        return NumberWordsInKurdish(number) + " ملیار";
    }

    private static string ConvertMillions(long number)
    {
        if (number == 1)
            return "ملیۆنێك";  // Special case for one million

        return NumberWordsInKurdish(number) + " ملیۆن";
    }
    private static string ConvertThousands(long number)
    {
        if (number == 1)
            return "هەزار";  // Just "هەزار" for 1000

        return NumberWordsInKurdish(number) + " هەزار";
    }

    private static string ConvertHundreds(long number)
    {
        if (number == 1)
            return "سەد";  // Just "سەد" for 100

        return NumberWordsInKurdish(number) + " سەد";
    }

    private static string ConvertTensAndUnits(long number)
    {
        var unitsMap = GetUnitsMap();
        var tensMap = GetTensMap();

        if (number < 20)
            return unitsMap[number];

        var tens = tensMap[number / 10];
        var units = (number % 10) > 0 ? " و " + unitsMap[number % 10] : "";
        return tens + units;
    }

    private static string[] GetUnitsMap()
    {
        return new[] { "سفر", "یەک", "دوو", "سێ", "چوار", "پێنج", "شەش", "حەوت", "هەشت", "نۆ", "دە‌", "یازدە‌", "دوازدە‌", "سێزدە‌", "چواردە‌", "پازدە‌", "شازدە‌", "حەڤدە‌", "هەژدە‌", "نۆزدە‌" };
    }

    private static string[] GetTensMap()
    {
        return new[] { "سفر", "دە‌", "بیست", "سی", "چل", "پەنجا", "شەست", "حەفتا", "هەشتا", "نۆوەت" };
    }
}