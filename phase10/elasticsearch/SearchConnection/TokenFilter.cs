namespace elasticsearch
{
    public static class TokenFilter
    {
        public static string NGram => "my-ngram-filter";
        public static string EnglishStopWords => "stop";
        public static string WordDelimiter => "word_delimiter";
        public static string LowerCase => "lowercase";
        public static string Standard => "standard";
    }
}