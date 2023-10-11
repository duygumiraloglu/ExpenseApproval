using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Web.Data;
using Web.Models;
using Web.Bussiness;

namespace Web.Controllers
{
    public class ExpenseFormController : Controller
    {
        private readonly Context _context;
        private readonly IExpenseRepository _repository;
        
        public ExpenseFormController(IExpenseRepository repository, Context context)
        {
            _repository = repository;
            _context = context;
            
        }


        public IActionResult Index()
        {
            
            //GeneralFunctions generalFunctions = new GeneralFunctions();
            //generalFunctions.TotalAmount();
            var expenseForms = _context.ExpenseForms.ToList();

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

            ExpenseForm expenseModel = new ExpenseForm();
            expenseModel.ExpenseName = model.ExpenseName;
            expenseModel.Status = "Yeni Kayıt";
            expenseModel.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            decimal totalAmount = 0;
            //GeneralFunctions generalFunctions = new GeneralFunctions();
            //decimal totalAmount = generalFunctions.TotalAmount(model.ExpenseDetails);

            expenseModel.TotalAmount = totalAmount;

            expenseModel.ExpenseDetails = new List<ExpenseDetail>();
            int count = model.ExpenseDetails.Count; // ExpenseDetails sayısını al

            for (int i = 0; i < count; i++)
            {
                ExpenseDetail expenseDetail = new ExpenseDetail
                {
                    ExpenseType = model.ExpenseDetails[i].ExpenseType,
                    Amount = model.ExpenseDetails[i].Amount,
                    
                };

                expenseModel.ExpenseDetails.Add(expenseDetail); // ExpenseDetail'i ExpenseModel'e ekledik
            }

            int ExpenseId =  _repository.AddExpenseFormId(expenseModel);

            if (model.ExpenseDetails.Count>0)
            {
                foreach (ExpenseDetail expenseDetail in model.ExpenseDetails)
                {
                    _repository.AddExpenseDetail(expenseDetail);
                }
            }
            return RedirectToAction("Index"); // Ekleme işlemi tamamlandığında listeleme sayfasına yönlendirin
        }
    }

}
