using Web.Models;

namespace Web.Data
{
    public interface IRepository
    {
        ExpenseForm GetExpenseForm(int expenseFormID);
        void AddExpenseForm(ExpenseForm model);
        void AddExpenseDetail(ExpenseDetail model);
        int AddExpenseFormRId(ExpenseForm model);
        void UpdateExpenseForm(ExpenseForm model);
        void UpdateAmount(int expenseFormID, decimal newAmount);
        void AddApproval(Approval model);
        Users GetUserByUP(string username, string password);
        Users GetUsersById(int id);
        Users GetUsersByName(string username);
    }

}
