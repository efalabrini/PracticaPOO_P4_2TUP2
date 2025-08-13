using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class BankAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
