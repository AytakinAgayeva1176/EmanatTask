using EmanatTask.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmanatTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _movieService.GetAll();
            return View(result);
        }

        public async Task<IActionResult> Details(string imdbId)
        {
            var result = await _movieService.GetByImdbID(imdbId);
            return View(result);
        }
    }
}
