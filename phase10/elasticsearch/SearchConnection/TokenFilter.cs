namespace elasticsearch.SearchConnection
{
    public static class TokenFilter
    {
        public static string NGram => "My-Ngram-Filter";
        public static string EnglishStopWords => "stop";
        public static string WordDelimiter => "word_delimiter";
        public static string LowerCase => "lowercase";
        public static string Standard => "standard";
    }
}