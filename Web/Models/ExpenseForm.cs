using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ExpenseForm
    {
        [Key]
        public int ExpenseFormID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(255)]
        public string Status { get; set; }

        public decimal TotalAmount { get; set; }

        // Navigation Property: ExpenseForm tablosu ile User tablosu arasındaki ilişkiyi temsil eder.
        public User User { get; set; }
        public List<ExpenseDetail> ExpenseDetails { get; set; }

        public decimal CalculateTotalAmount()
        {
            if (ExpenseDetails != null)
            {
                return ExpenseDetails.Sum(ed => ed.Amount);
            }

            return 0;
        }


    }
}
