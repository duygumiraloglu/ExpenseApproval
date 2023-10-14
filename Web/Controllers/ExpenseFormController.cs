using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Web.Data;
using Web.Models;
using Web.Bussiness;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlTypes;

namespace Web.Controllers
{
    public class ExpenseFormController : Controller
    {
        private readonly Context _context;
        private readonly IRepository _repository;

        public ExpenseFormController(IRepository repository, Context context)
        {
            _repository = repository;
            _context = context;

        }
        [Authorize]
        public IActionResult Index()
        {

            GeneralFunctions generalFunctions = new GeneralFunctions(_context);
            generalFunctions.TotalAmount();
            var expenseForms = _context.ExpenseForms.ToList();

            return View(expenseForms);
        }

        [Authorize]
        public IActionResult AddExpenseForm()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddExpenseForm(ExpenseForm model)
        {
            if (ModelState.IsValid)
            {
                model.Status = model.Status;
                model.CreatedDate = DateTime.Now;
                model.UserID = 1;

                decimal totalAmount = CalculateTotalAmount(model.ExpenseDetails);
                model.TotalAmount = totalAmount;

                int expenseId = _repository.AddExpenseFormRId(model);

                Approval approval = new Approval();
                approval.Status = model.Status;
                approval.UserID = 1;
                approval.ExpenseFormID = expenseId;

                _repository.AddApproval(approval);

                return RedirectToAction("Index", "ExpenseForm");
            }

            return View(model); // Aynı sayfayı tekrar göstermek için modeli geri döneme
        }

        private decimal CalculateTotalAmount(IEnumerable<ExpenseDetail> details)
        {
            decimal totalAmount = 0;

            foreach (var detail in details) { 
                totalAmount += detail.Amount;
            }

            return totalAmount;
        }

        public IActionResult DeleteExpenseForm(int id)
        {
            try
            {
                // Null değeri işle

                var approvals = _context.Approvals.Where(ed => ed.ExpenseFormID == id).ToList();

                if (approvals != null)
                {
                    foreach (Approval approval in approvals)
                    {
                        _context.Approvals.Remove(approval);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata : " + ex.Message);
            }
            try
            {               
                var expenseDetails = _context.ExpenseDetails.Where(ed => ed.ExpenseFormID == id).ToList();

                if (expenseDetails != null)
                {
                    foreach (ExpenseDetail expenseDetail in expenseDetails)
                    {
                        _context.ExpenseDetails.Remove(expenseDetail);
                        _context.SaveChanges();
                    }
                }

                var expenseForm = _context.ExpenseForms.FirstOrDefault(ef => ef.ExpenseFormID == id);
                if (expenseForm != null)
                {
                    _context.ExpenseForms.Remove(expenseForm);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Genel hata yakalama

                Console.WriteLine("Hata: " + ex.Message);
            }
            finally
            {
                // Her durumda çalışacak
                Console.WriteLine("İşlem tamamlandı.");
            }



            return RedirectToAction("Index", "ExpenseForm"); // Silme işlemi tamamlandıktan sonra yonlendirme
        }
    }

}
