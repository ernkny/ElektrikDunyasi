using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElektrikDunyasi.Models.Entity;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace ElektrikDunyasi.Controllers
{
    public class PanelController : Controller
    {
        // GET: Panel
        DergiElektrikEntities db = new DergiElektrikEntities();
        public ActionResult GirisYap()
        {
            return View();

        }
        [HttpPost]
        public ActionResult GirisYap(LoginAdmin admin)
        {
            proje.md5.ClassMD5 mD5 = new proje.md5.ClassMD5();
            string userN = mD5.md5sifreleme(admin.UserName);
            string userP = mD5.md5sifreleme(admin.UserPassword);

            var bilgiler = db.LoginAdmin.FirstOrDefault(a => a.UserName == userN &&
             a.UserPassword == userP);
            if (bilgiler !=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.UserName, false);
                return RedirectToAction("Index", "Haberler");
            }else
            return View();

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap");

        }
    }
}