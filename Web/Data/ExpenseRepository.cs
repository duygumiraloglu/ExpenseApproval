using Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly Context _context;

        public ExpenseRepository(Context context)
        {
            _context = context;
        }
        public void AddExpenseForm(ExpenseForm model)
        {
            _context.ExpenseForms.Add(model);
            _context.SaveChanges();
        }

        public int AddExpenseFormRId(ExpenseForm model)
        {

            _context.ExpenseForms.Add(model);
            _context.SaveChanges();
            return model.ExpenseFormID;

        }


        public void AddExpenseDetail(ExpenseDetail model)
        {
            _context.ExpenseDetails.Add(model);
            _context.SaveChanges();
        }

        public void UpdateAmount(int expenseFormID, decimal newAmount)
        {
            var expenseForm = _context.ExpenseForms.FirstOrDefault(ef => ef.ExpenseFormID == expenseFormID);

            if (expenseForm != null)
            {
                expenseForm.TotalAmount = newAmount;
                _context.SaveChanges();
            }
        }

    }

}
