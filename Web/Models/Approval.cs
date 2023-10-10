using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Approval
    {
        [Key]
        public int ApprovalID { get; set; }

        [Required]
        [ForeignKey("ExpenseForm")]
        public int ExpenseFormID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }

        public DateTime ApprovalDate { get; set; }

        [MaxLength(255)]
        public string Status { get; set; }

        [MaxLength(1000)] // Örnek bir maksimum yorum uzunluğu
        public string Comment { get; set; }

        // Navigation Property: Approval tablosu ile ExpenseForm ve User tabloları arasındaki ilişkileri temsil eder.
        public ExpenseForm ExpenseForm { get; set; }
        public User User { get; set; }
    }
}
