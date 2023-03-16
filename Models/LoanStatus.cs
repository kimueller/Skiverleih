using System.ComponentModel.DataAnnotations;

namespace Skiverleih.Models
{
    public class LoanStatus
    {
        [Key]
        public int LoanStatusID { get; set; }

        [Required]
        public bool Borrowed { get; set; }

    }
}
