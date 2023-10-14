using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        public async Task<IActionResult> GirisYap(Users model)
        {
            if (ModelState.IsValid)
            {
                Users user = _repository.GetUserByUP(model.Username, model.PasswordHash);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,model.Username)
                    };
                    var useridentity = new ClaimsIdentity(claims, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "ExpenseForm");
                }

            }

            return View(model);
        }

    }
}
