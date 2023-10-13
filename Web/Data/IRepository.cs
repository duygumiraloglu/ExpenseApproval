using Web.Models;

namespace Web.Data
{
    public interface IRepository
    {
        void AddExpenseForm(ExpenseForm model);
        void AddExpenseDetail(ExpenseDetail model);
        int AddExpenseFormRId(ExpenseForm model);
        void UpdateAmount(int expenseFormID, decimal newAmount);
        void AddApproval(Approval model);
        Users GetUserByUsername(string username);
        Users GetUsersById(int id);
    }

}
