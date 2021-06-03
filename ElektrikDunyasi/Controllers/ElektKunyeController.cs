using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElektrikDunyasi.Models.Entity;


namespace ElektrikDunyasi.Controllers
{
    [Authorize]
    public class ElektKunyeController : Controller
    {
        // GET: ElektKunye
        
        DergiElektrikEntities db = new DergiElektrikEntities();
        public ActionResult Index()
        {
            var result = db.ElektKunye.Find(1);
            return View(result);
        }
        public ActionResult KunyeGuncelle(ElektKunye e)
        {
            var result = db.ElektKunye.Find(1);
            result.Imtyaz = e.Imtyaz;
            result.Sormlu = e.Sormlu;
            result.YayinDanisman = e.YayinDanisman;
            result.YayinKrl = e.YayinKrl;
            result.Reklam = e.Reklam;
            result.ReklamHalk = e.ReklamHalk;
            result.Guney = e.Guney;
            result.Dogu = e.Dogu;
            result.Kurye = e.Kurye;
            result.Ceviri = e.Ceviri;
            result.Iran = e.Iran;
            result.Misir = e.Misir;
            result.Holland = e.Holland;
            result.Poland = e.Poland;
            result.Hukuk = e.Hukuk;
            result.Dagitim = e.Dagitim;
            result.Baski = e.Baski;
            result.Yayin = e.Yayin;
            result.Type = e.Type;
            result.Year = e.Year;
            db.SaveChanges();
           return RedirectToAction("Index");
        }
    }
}