namespace NumberToKurdishWords.Extensions;



public static class ConvertNumbersToEnglishAlphabet
{
    private static readonly string[] Units = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    private static readonly string[] Teens = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
    private static readonly string[] Tens = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
    private static readonly string[] Thousands = { "", "thousand", "million", "billion" };

    public static string NumberWordsInEnglish(this object val)
    {
        var isNumber = long.TryParse(val.ToString(), out var number);

        if (!isNumber)
        {
            return "Just support number";
        }

        if (number == 0)
            return "zero";

        if (number < 0)
            return "- " + NumberWordsInEnglish(Math.Abs(number));

        var words = "";

        int thousandCounter = 0;

        while (number > 0)
        {
            if (number % 1000 != 0)
            {
                var prefix = ConvertLessThanOneThousand(number % 1000) + " " + Thousands[thousandCounter];
                words = prefix.Trim() + (string.IsNullOrEmpty(words) ? "" : " " + words);
            }

            number /= 1000;
            thousandCounter++;
        }

        return words.Trim();
    }

    private static string ConvertLessThanOneThousand(long number)
    {
        string words = "";

        if (number % 100 < 20)
        {
            words = number % 100 < 10 ? Units[number % 10] : Teens[number % 100 - 10];
            number /= 100;
        }
        else
        {
            words = Units[number % 10];
            number /= 10;

            words = (number % 10 > 0 ? Tens[number % 10] + (words != "zero" ? "-" : "") : "") + words;
            number /= 10;
        }

        if (number > 0)
        {
            words = Units[number] + " hundred" + (string.IsNullOrEmpty(words) ? "" : " " + words);
        }

        return words;
    }
}
