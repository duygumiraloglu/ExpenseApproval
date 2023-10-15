using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Web.Data;
using Web.Models;
using Web.Bussiness;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlTypes;
using X.PagedList;
using X.PagedList.Mvc.Core;
using Microsoft.AspNetCore.Identity;

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
        public IActionResult Index(int page = 1)
        {
            var expenseForms = _context.ExpenseForms.OrderByDescending(ef => ef.ExpenseFormID).ToPagedList(page, 8);

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
                Users user = _repository.GetUsersByName(User.Identity.Name);

                model.Status = model.Status;
                model.CreatedDate = DateTime.Now;
                model.UserID = user.UserID;

                decimal totalAmount = CalculateTotalAmount(model.ExpenseDetails);
                model.TotalAmount = totalAmount;

                int expenseId = _repository.AddExpenseFormRId(model);

                Approval approval = new Approval();
                approval.Status = model.Status;
                approval.UserID = user.UserID;
                approval.ExpenseFormID = expenseId;

                _repository.AddApproval(approval);

                return RedirectToAction("Index", "ExpenseForm");
            }

            return View(model); // Aynı sayfayı tekrar göstermek için modeli geri döneme
        }

        private decimal CalculateTotalAmount(IEnumerable<ExpenseDetail> details)
        {
            decimal totalAmount = 0;

            foreach (var detail in details)
            {
                totalAmount += detail.Amount;
            }

            return totalAmount;
        }

        [Authorize]
        public IActionResult UpdateExpenseForm(int id)
        {
            // ID'ye göre ExpenseForm verisini al
            var expenseForm = _context.ExpenseForms
                .Include(ef => ef.ExpenseDetails)
                .FirstOrDefault(ef => ef.ExpenseFormID == id);

            if (expenseForm == null)
            {
                return RedirectToAction("Index", "ExpenseForm");
            }

            return View(expenseForm);
        }

        [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateExpenseForm(ExpenseForm updatedForm)
        {
            if (ModelState.IsValid)
            {
                var expenseForm = _repository.GetExpenseForm(updatedForm.ExpenseFormID);

                Users user = _repository.GetUsersByName(User.Identity.Name);
                expenseForm.UserID = user.UserID;
                decimal totalAmount = CalculateTotalAmount(updatedForm.ExpenseDetails);
                expenseForm.TotalAmount = totalAmount;
                expenseForm.Status = GeneralFunctions.GetExpenseFormStatusString(GeneralFunctions.ExpenseStatus.YenidenOnayaSunuldu);
                expenseForm.CreatedDate = DateTime.Now;
                // Mevcut ExpenseDetails verilerini güncelle veya ekle

                foreach (var detail in updatedForm.ExpenseDetails)
                {
                    if (detail.ExpenseDetailID == 0)
                    {
                        _repository.UpdateAmount(detail.ExpenseDetailID, detail.Amount);
                    }
                    else
                    {
                        _repository.UpdateAmount(detail.ExpenseDetailID, detail.Amount);
                    }
                }

                try
                {

                    _repository.UpdateExpenseForm(expenseForm);
                    return RedirectToAction("Index", "ExpenseForm"); // Başka bir sayfaya yönlendirin veya işlem sonuçlarını gösterilmesi
                }
                catch (DbUpdateException)
                {
                    // Hata işleme kodu eklenmesi
                    ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
                }
            }

            // Model doğrulama hatası varsa, sayfaynın tekrar gösterilmesi
            return View(updatedForm);
        }

        [Authorize]
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

        [Authorize]
        public IActionResult ApprovalForm(int page = 1)
        {

            var approvalForms = _context.Approvals.OrderByDescending(ef => ef.ApprovalID).ToPagedList(page, 8);
            foreach (Approval approvalForm in approvalForms)
            {
                var expenseForm = _context.ExpenseForms.FirstOrDefault(e => e.ExpenseFormID == approvalForm.ExpenseFormID);
                approvalForm.ExpenseForm = expenseForm;
            }
            return View(approvalForms);
        }
    }
}
