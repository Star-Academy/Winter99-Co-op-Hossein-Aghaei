using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Word
    {
        [Key]
        public int WordId {get; set; }
        
        [Required]
        public string Term { get; set;}

        public List<Doc> DocsContainer { get; set; } = new List<Doc>();
    }
}