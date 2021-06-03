using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElektrikDunyasi.Models.Entity;
using Microsoft.AspNetCore.Http;
using PagedList;
using PagedList.Mvc;

namespace ElektrikDunyasi.Controllers
{
    [Authorize]
    public class AkademisyenController : Controller
    {
        // GET: Akademisyen

        DergiElektrikEntities db = new DergiElektrikEntities();
        public ActionResult Index(int page = 1)
        {
            var result = db.Akedemisyen.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 15);
            return View(result);
        }

        [HttpGet]
        public ActionResult AkademisyenEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AkademisyenEkle(Akedemisyen akedemisyen)
        {
            try
            {
                if (akedemisyen != null)
                {
                    if (akedemisyen.Tarih == null)
                    {
                        akedemisyen.Tarih = DateTime.Now;
                    }
                    db.Akedemisyen.Add(akedemisyen);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }





        public ActionResult AkademisyenSil(int id)
        {
            var result = db.Akedemisyen.Find(id);
            
           
           
           
                db.Akedemisyen.Remove(result);

                db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        public ActionResult AkademisyenGetir(int id)
        {

            var result = db.Akedemisyen.Find(id);

            return View("AkademisyenGetir", result);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AkademisyenGuncelle(Akedemisyen akedemisyen)
        {

            var akademigncl = db.Akedemisyen.Find(akedemisyen.AkedemiId);
            akademigncl.Baslik = akedemisyen.Baslik;
            akademigncl.Ekleyen = akedemisyen.Ekleyen;
            akademigncl.Yazi = akedemisyen.Yazi;
            akademigncl.Keywords = akedemisyen.Keywords;

            if (akedemisyen.Tarih != null)
            {
                akademigncl.Tarih = akedemisyen.Tarih;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult ResimGetir(int id)
        {

            var result = db.stdImages.Find(id);

            return View("ResimGetir", result);

        }
        public ActionResult ResimGuncelle(stdImages std)
        {

            var gncl = db.stdImages.Find(std.Id);
            gncl.ImageUrl = std.ImageUrl;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}