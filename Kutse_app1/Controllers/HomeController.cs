using Kutse_app1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kutse_app1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            int hour = DateTime.Now.Hour;
            ViewBag.Message = "Ootan sind minu peole! Palun tule!!!";
            if (hour < 12)
            {
                ViewData["Greeting"] = "Tere hommikust!";
            }
            else if (hour < 17)
            {
                ViewData["Greeting"] = "Tere päevast!";
            }
            else if (hour < 21)
            {
                ViewData["Greeting"] = "Tere õhtust!";
            }
            else
            {
                ViewData["Greeting"] = "Hea--d ööd!";
            };
            return View();

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
        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            if (ModelState.IsValid)
            { return View("Thanks", guest); }
            else
            { return View(); }
        }
        public void E_mail(Guest guest )
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "alex2002tyn@gmail.com";
                WebMail.Password = "Alexzx200231";
                WebMail.From = "alex2002tyn@gmail.com";
                WebMail.Send("marina.oleinik@tthk.ee", "Vastus kutsele", guest.Name + "vastas" + ((guest.WillAttend ?? false) ? "tuleb peole " : "ei tule peole"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch(Exception)
            {
                ViewBag.Message = "Mul on kahju! Ei saa kirja saada!!!";
            }

        }
       public void NotifyMe(Guest guest)
        {
            WebMail.Send(guest.Email, "Meeldetuletus", guest.Name + ", ara unusta. Pidu toimub 12.03.20! Sind ootavad väga!",
                    null, "marina.oleinik@tthk.ee",
                    filesToAttach: new String[] { Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName("kutse.png")) }
                   );
        }
    }
}