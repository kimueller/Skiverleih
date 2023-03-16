using System.ComponentModel.DataAnnotations;

namespace Skiverleih.Models
{
    public class OnLoan
    {
        [Key]
        public int OnLoanID { get; set; }

        [Range(1,int.MaxValue, ErrorMessage = "No article selected!")]
        public int ArticleID { get; set; }
        public Article Article { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "No customer selected!")]
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public DateTime LoanDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Required]
        public bool Returned { get; set; }
    }
}
