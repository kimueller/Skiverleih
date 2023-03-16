using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skiverleih.Models
{
    public class Article
    {
        [Key]
        public int ArticleID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "decimal(7, 2)")]
        public decimal PricePerDay { get; set; }

        [Required]
        public int LoanCount { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public int LoanStatusID { get; set; }
        public LoanStatus LoanStatus { get; set; }

        public virtual ICollection<OnLoan> OnLoan { get; set; }
    }
}
