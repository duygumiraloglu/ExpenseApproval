using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ExpenseForm
    {
        public int ExpenseFormID { get; set; }


        [Required]
        [ForeignKey("UserID")]
        public int UserID { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? TotalAmount { get; set; }

        [StringLength(100)]
        public string? ExpenseName { get; set; }

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
