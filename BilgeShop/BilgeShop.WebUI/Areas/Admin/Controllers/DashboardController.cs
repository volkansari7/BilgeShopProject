using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BilgeShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")] // program.cs'teki area:exist kısmı ile eşleşir.
    [Authorize(Roles="Admin")] // Claimlerdeki claims.Add(New )
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
