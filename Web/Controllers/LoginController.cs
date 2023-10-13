using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class LoginController : Controller
    {

        private readonly Context _context;
        private readonly IRepository _repository;

        public LoginController(IRepository repository, Context context)
        {
            _repository = repository;
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GirisYap(Users model)
        {
            if (ModelState.IsValid)
            {
                Users user = _repository.GetUserByUsername(model.Username);

                return RedirectToAction("Index", "ExpenseForm");

            }

            return View(model);
        }

    }
}
