using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ExpenseDetail
    {
        [Key]
        public int ExpenseDetailID { get; set; }

        [Required]
        [ForeignKey("ExpenseForm")]
        public int ExpenseFormID { get; set; }

        [MaxLength(255)]
        public string ExpenseType { get; set; }

        public decimal Amount { get; set; }

        // Navigation Property: ExpenseDetail tablosu ile ExpenseForm tablosu arasındaki ilişkiyi temsil eder.
        public ExpenseForm ExpenseForm { get; set; }
    }
}
