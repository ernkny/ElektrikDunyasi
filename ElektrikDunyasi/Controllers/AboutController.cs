using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElektrikDunyasi.Models.Entity;

namespace ElektrikDunyasi.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        // GET: About
        DergiElektrikEntities db = new DergiElektrikEntities();
        public ActionResult Index()
        {
            var result = db.Hakkimizda.Find(1);
            return View(result);
            
        }
        [ValidateInput(false)]
        public ActionResult AboutGuncelle(Hakkimizda h)
        {
            var result = db.Hakkimizda.Find(1);
            result.HakkimizdaIcerik = h.HakkimizdaIcerik;
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        
    }
}