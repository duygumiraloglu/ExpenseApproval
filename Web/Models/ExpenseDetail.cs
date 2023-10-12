using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ExpenseDetail
    {
        public int ExpenseDetailID { get; set; }
        public int ExpenseFormID { get; set; }

        [StringLength(50)]
        public string ExpenseType { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

    }
}
