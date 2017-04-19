using System;
using System.Configuration;
using System.Web.Mvc;
using EncryptedText.Models;

namespace EncryptedText.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var text = ConfigurationManager.AppSettings.Get("TextToEncrypt");
            var encryptedText = Helpers.Encryption.Encrypt(text);

            return View(new HomeViewModel() { EncryptedText = encryptedText });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Decrypt(HomeViewModel vm)
        {
            try
            {
                vm.DecryptedText = Helpers.Encryption.Decrypt(vm.EncryptedText, vm.EntropyText);
            }
            catch (Exception)
            {
                return Json(new {success = false, error = "Incorrect. Please try again!", vm});
            }

            return Json(new {success = true, vm});
        }
    }
}