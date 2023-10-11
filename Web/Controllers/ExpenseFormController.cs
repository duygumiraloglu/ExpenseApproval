using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Web.Models;

namespace Web.Controllers
{
    public class ExpenseFormController : Controller
    {
        Context context = new Context();
        public IActionResult Index()
        {
            var expenseForms = context.ExpenseForms.ToList();

            foreach (ExpenseForm expenseForm in expenseForms)
            {
                var _expenseForm = context.ExpenseForms
                .Include(ef => ef.ExpenseDetails)
                .FirstOrDefault(ef => ef.ExpenseFormID == expenseForm.ExpenseFormID);

                if (expenseForm != null)
                {
                    // totalAmount, ExpenseForm içindeki toplam Amount değerini içerecektir.

                    decimal totalAmount = expenseForm.CalculateTotalAmount();

                    if (_expenseForm.TotalAmount != totalAmount)
                    {
                        expenseForm.TotalAmount = expenseForm.CalculateTotalAmount();
                        context.ExpenseForms.Update(expenseForm);
                    }

                }
            }

            return View(expenseForms);
        }

        public IActionResult AddExpenseForm()
        {
            return View();
        }


        [HttpPost]

        public IActionResult AddExpenseForm(ExpenseForm model)
        {

            // ExpenseForm verilerini kaydedin
            context.ExpenseForms.Add(model);

            if (model.ExpenseDetails.Count>0)
            {
                foreach (ExpenseDetail expenseDetail in model.ExpenseDetails)
                {
                    context.ExpenseDetails.Add(expenseDetail);
                }
            }
            return RedirectToAction("Index"); // Ekleme işlemi tamamlandığında listeleme sayfasına yönlendirin
        }
    }

}
