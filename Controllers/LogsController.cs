using Microsoft.AspNetCore.Mvc;

namespace TempMailX.Controllers
{
    public class LogsController : Controller
    {
        public IActionResult Index()
        {
            var logDir = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

            if (!Directory.Exists(logDir))
                return Content("Logs folder not found");

            var file = Directory.GetFiles(logDir)
                                .OrderByDescending(f => f)
                                .FirstOrDefault();

            if (file == null)
                return Content("No log file found");

            var lines = System.IO.File.ReadAllLines(file)
                                      .Reverse()
                                      .Take(100)
                                      .ToList();

            return View(lines);
        }

    }

}
