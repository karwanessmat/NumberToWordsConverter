namespace NumberToWordsConverter.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceString(this string str, Dictionary<string, string> replacement)
        {
            return replacement
                .Aggregate(str, (current, pair) => 
                    current.Replace(pair.Key, pair.Value));
        }
    }
}
