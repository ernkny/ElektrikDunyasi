using ElektrikDunyasi.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace ElektrikDunyasi.Controllers
{
    [Authorize]
    public class MakalelerController : Controller
    {
        // GET: Makaleler
        DergiElektrikEntities db = new DergiElektrikEntities();
        public ActionResult Index(int page=1)
        {
            var result = db.Makaleler.OrderByDescending(t => t.Tarih).ToList().ToPagedList(page, 15);
            return View(result);
        }
        [HttpGet]
        public ActionResult MakaleEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MakaleEkle(Makaleler m, HttpPostedFileBase file)
        {
            try
            {
                if (m != null)
                {
                    if (file.ContentLength > 0)
                    {
                        if (m.Tarih == null)
                        {
                            m.Tarih = DateTime.Now;
                        }
                        var extensiton = Path.GetExtension(file.FileName);
                        if (extensiton == ".JPG" || extensiton == ".png" || extensiton == ".jpg" || extensiton == ".PNG")
                        {
                            string guid = Guid.NewGuid().ToString();
                            string pathway = Path.Combine(Server.MapPath("~/Images/MakaleImages/"), Path.GetFileName(guid + extensiton));
                            file.SaveAs(pathway);
                            m.DeleteUrl = pathway;
                            pathway = "../Images/MakaleImages/" + guid + extensiton;
                            m.ImageUrl = pathway;
                            db.Makaleler.Add(m);

                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }


                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }

        }
        public ActionResult MakaleSil(int id)
        {
            var result = db.Makaleler.Find(id);
            var path = result.DeleteUrl;
            try
            {

                System.IO.File.Delete(path);
                result.ImageUrl = null;

            }
            catch
            {
                result.ImageUrl = null;
            }
            finally
            {
                db.Makaleler.Remove(result);

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult MakaleGetir(int id)
        {

            var result = db.Makaleler.Find(id);

            return View("MakaleGetir", result);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MakaleGuncelle(Makaleler makale)
        {

            var makalegncl = db.Makaleler.Find(makale.MakaleId);
            makalegncl.MakaleBaslik = makale.MakaleBaslik;
            makalegncl.Ekleyen = makale.Ekleyen;
            makalegncl.MakaleYazi = makale.MakaleYazi;
            makalegncl.Keywords = makale.Keywords;
            if (makale.Tarih != null)
            {
                makalegncl.Tarih = makale.Tarih;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}