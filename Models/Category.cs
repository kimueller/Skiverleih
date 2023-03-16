using System.ComponentModel.DataAnnotations;

namespace Skiverleih.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
