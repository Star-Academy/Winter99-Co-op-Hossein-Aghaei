using System;

namespace elasticsearch.model
{
    public class Doc
    {
        public string Name { get; set;}
        public string Content { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Doc doc && Name == doc.Name && Content == doc.Content;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Content);
        }
    }
}