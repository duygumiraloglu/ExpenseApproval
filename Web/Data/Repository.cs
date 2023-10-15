using Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Web.Data
{
    public class Repository : IRepository
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public ExpenseForm GetExpenseForm(int expenseFormID)
        {
            return _context.ExpenseForms.FirstOrDefault(ef => ef.ExpenseFormID == expenseFormID);

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

        public void UpdateExpenseForm(ExpenseForm expenseForm)
        {    
            _context.Entry(expenseForm).State = EntityState.Modified;
            _context.Update(expenseForm);            
        }

        public void UpdateAmount(int expenseDetailID, decimal newAmount)
        {
            var expenseForm = _context.ExpenseDetails.FirstOrDefault(x => x.ExpenseDetailID == expenseDetailID);

            if (expenseForm != null)
            {
                expenseForm.Amount = newAmount;
                _context.SaveChanges();
            }
        }

        public void AddApproval(Approval model)
        {
            _context.Approvals.Add(model);
            _context.SaveChanges();
        }

        public Users GetUserByUP(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
        }

        public Users GetUsersById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserID == userId);
        }

        public Users GetUsersByName(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

    }

}
