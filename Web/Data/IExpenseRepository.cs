using Web.Models;

namespace Web.Data
{
    public interface IExpenseRepository
    {
        void AddExpenseForm(ExpenseForm model);
        void AddExpenseDetail(ExpenseDetail model);
        int AddExpenseFormRId(ExpenseForm model);
        void UpdateAmount(int expenseFormID, decimal newAmount);
    }

}
