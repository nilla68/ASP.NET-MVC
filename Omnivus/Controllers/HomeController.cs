using Microsoft.AspNetCore.Mvc;
using Omnivus.Helpers;
using Omnivus.Models;
using System.Diagnostics;

namespace Omnivus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailService _mailService;

        public HomeController(ILogger<HomeController> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMail(contact.Email, contact.Name, contact.Message);
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMail(contact.Email, contact.Name, contact.Message);
                ModelState.Clear();
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}