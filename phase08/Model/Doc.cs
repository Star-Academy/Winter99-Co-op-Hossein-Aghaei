using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Doc
    {
        [Key]
        public int DocId { get; set;}
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        public ICollection<Word> WordsOfDoc { get; set; }
    }
}