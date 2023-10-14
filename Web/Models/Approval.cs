using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Approval
    {
        public int ApprovalID { get; set; }

        
        public int ExpenseFormID { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }

        public DateTime? ApprovalDate { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(50)]
        public string? Comment { get; set; }

        [ForeignKey("ExpenseFormID")]
        public ExpenseForm ExpenseForm { get; set; }

    }
}
