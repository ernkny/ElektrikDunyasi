using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElektrikDunyasi.Models.Entity;
namespace ElektrikDunyasi.Controllers
{
    [Authorize]
    public class EdergiController : Controller
    {
        DergiElektrikEntities db = new DergiElektrikEntities();
        // GET: Edergi
        public ActionResult Index()
        {
            var result = db.EDergi.Find(1);
            return View(result);
        }
        public ActionResult EDergiGuncelle(EDergi ed)
        {
            var result = db.EDergi.Find(1);
            result.EDegiUrl = ed.EDegiUrl;
            result.EDergiResimUrl = ed.EDergiResimUrl;
            db.SaveChanges();
            
            return RedirectToAction("Index");

        }
    }
}