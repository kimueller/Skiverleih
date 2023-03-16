using System.ComponentModel.DataAnnotations;

namespace Skiverleih.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }        
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public virtual ICollection<OnLoan> OnLoan { get; set; }

    }
}
