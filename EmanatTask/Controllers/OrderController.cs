using EmanatTask.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmanatTask.Controllers
{
    public class OrderController : Controller
    {

        private readonly IEmailService _emailService;
        public OrderController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Order(string imdbID)
        {
            ViewData["imdbID"] = imdbID;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Order(string email, string imdbId)
        {
            await _emailService.SendEmailAsync(email, imdbId);
            return RedirectToAction("Details","Home",new { imdbId });
        }
    }
}
