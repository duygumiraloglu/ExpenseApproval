using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ExpenseForm
    {
        [Required]
        public int ExpenseFormID { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }

        [StringLength(100)]
        public string? ExpenseName { get; set; }

        public List<ExpenseDetail> ExpenseDetails { get; set; }

        public decimal CalculateTotalAmount()
        {
            if (ExpenseDetails != null)
            {
                return (decimal)ExpenseDetails.Sum(ed => ed.Amount);
            }

            return 0;
        }


    }
}
