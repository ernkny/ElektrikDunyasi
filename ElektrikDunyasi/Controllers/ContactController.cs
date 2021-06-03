using ElektrikDunyasi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace ElektrikDunyasi.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        DergiElektrikEntities db = new DergiElektrikEntities();
        // GET: Contact


        public ActionResult Index()
        {
            var result = db.Contact.ToList();
            return View(result);
        }
        
        public ActionResult ContactGetir(int id)
        {
            var result = db.Contact.Find(id);
            return View("ContactGetir", result);
        }
        public ActionResult ContactGuncelle(Contact c)
        {
            var result = db.Contact.Find(1);
            result.Faks = c.Faks;
            result.Address = c.Address;
            result.Telefon = c.Telefon;
            result.Email = c.Email;
            db.SaveChanges();
            ViewBag.Message="Güncelleme başarılı";
            return RedirectToAction("Index");
        }
        public ActionResult Abone(int page=1)
        {

            var result = db.AboneInfo.OrderByDescending(d => d.Tarih).ToList().ToPagedList(page, 20);

            return View(result);

        }
        public ActionResult AboneSil(int id)
        {

            var result = db.AboneInfo.Find(id);
            db.AboneInfo.Remove(result);

            db.SaveChanges();

            return RedirectToAction("Abone");

        }
    }
}