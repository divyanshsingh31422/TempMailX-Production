using Microsoft.AspNetCore.Mvc;
using TempMailX.Data;
using TempMailX.Services;

namespace TempMailX.Controllers
{
    public class MailController : Controller
    {
        private readonly TempEmailDAL _dal;
        private readonly EmailGeneratorService _gen;

        //Dependency Injection
        public MailController(TempEmailDAL dal, EmailGeneratorService gen)
        {
            _dal = dal;
            _gen = gen;
        }

        public IActionResult Create()
        {
            string email = _gen.Generate();
            _dal.SaveEmail(email);

            ViewBag.Email = email;
            return View();
        }

        public IActionResult Inbox()
        {
            var emails = _dal.GetActiveEmails();
            return View(emails);
        }

        public IActionResult Deactivate(int id)
        {
            _dal.DeactivateEmail(id);
            return RedirectToAction("Inbox");
        }
    }
}
