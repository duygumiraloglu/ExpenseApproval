using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Bussiness
{
    public class GeneralFunctions
    {
        //genel metodların tanımlanması
        private readonly Context _context;

        public GeneralFunctions(Context context)
        {
            _context = context;
        }
        public void TotalAmount()
        {
            var expenseForms = _context.ExpenseForms.ToList();

            foreach (ExpenseForm expenseForm in expenseForms)
            {
                var _expenseForm = _context.ExpenseForms
                .Include(ef => ef.ExpenseDetails)
                .FirstOrDefault(ef => ef.ExpenseFormID == expenseForm.ExpenseFormID);

                if (expenseForm != null)
                {
                    // totalAmount, ExpenseForm içindeki toplam Amount değeri

                    decimal totalAmount = expenseForm.CalculateTotalAmount();

                    if (_expenseForm.TotalAmount != totalAmount)
                    {
                        expenseForm.TotalAmount = expenseForm.CalculateTotalAmount();
                        _context.ExpenseForms.Update(expenseForm);
                    }
                }
            }
        }

        public decimal TotalAmount(List<ExpenseDetail> expenseDetails)
        {
            decimal totalAmount = 0;

            if (expenseDetails != null)
            {
                totalAmount = (decimal)expenseDetails.Sum(detail => detail.Amount);                
            }

            return totalAmount;
        }
    }
}
