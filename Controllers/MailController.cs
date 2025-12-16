using Microsoft.AspNetCore.Mvc;
using Serilog;
using TempMailX.Data;
using TempMailX.Services;

namespace TempMailX.Controllers
{
    public class MailController : Controller
    {
        EmailGeneratorService gen = new EmailGeneratorService();
        TempEmailDAL dal = new TempEmailDAL();

        public IActionResult Create()
        {
            string email = gen.Generate();
            dal.SaveEmail(email);
            Log.Information("Temp email created: {Email}", email);
            ViewBag.Email = email;
            return View();
        }

        public IActionResult Inbox()
        {
            var emails = dal.GetActiveEmails();
            return View(emails);
        }
        public IActionResult Deactivate(int id)
        {
            dal.DeactivateEmail(id);
            return RedirectToAction("Inbox");
        }

    }
}
