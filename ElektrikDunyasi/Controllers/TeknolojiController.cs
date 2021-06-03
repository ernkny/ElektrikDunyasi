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
    public class TeknolojiController : Controller
    {
        DergiElektrikEntities db = new DergiElektrikEntities();
        // GET: Teknoloji
        
        public ActionResult Index(int page=1)
        {
            var result = db.Teknoloji.OrderByDescending(t => t.Tarih).ToList().ToPagedList(page, 15);
            return View(result);
        }
        [HttpGet]
        public ActionResult TeknolojiEkle()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TeknolojiEkle(Teknoloji t, HttpPostedFileBase file)
        {
            try
            {
                if (t != null)
                {
                    if (file.ContentLength > 0)
                    {
                        if (t.Tarih == null)
                        {
                            t.Tarih = DateTime.Now;
                        }
                        var extensiton = Path.GetExtension(file.FileName);
                        if (extensiton == ".JPG" || extensiton == ".png" || extensiton == ".jpg" || extensiton == ".PNG")
                        {
                            string guid = Guid.NewGuid().ToString();
                            string pathway = Path.Combine(Server.MapPath("~/Images/TeknolojiImages/"), Path.GetFileName(guid + extensiton));
                            file.SaveAs(pathway);
                            t.DeleteUrl = pathway;
                            pathway = "../Images/TeknolojiImages/" + guid + extensiton;
                            t.ImageUrl = pathway;
                            db.Teknoloji.Add(t);

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
        public ActionResult TeknoSil(int id)
        {
            var result = db.Teknoloji.Find(id);
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
                db.Teknoloji.Remove(result);

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        public ActionResult TeknoGetir(int id)
        {

            var result = db.Teknoloji.Find(id);

            return View("TeknoGetir", result);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Guncelle(Teknoloji tek)
        {

            var teknogncl = db.Teknoloji.Find(tek.TeknoId);
            teknogncl.TeknoBaslik = tek.TeknoBaslik;

            teknogncl.TeknoYazi = tek.TeknoYazi;
            teknogncl.Keywords = tek.Keywords;
            if (tek.Tarih != null)
            {
                teknogncl.Tarih = tek.Tarih;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}