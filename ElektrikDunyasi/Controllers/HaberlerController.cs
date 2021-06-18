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
    public class HaberlerController : Controller
    {
        // GET: Haberler
        DergiElektrikEntities db = new DergiElektrikEntities();
        
        public ActionResult Index(int page=1)
        {
            var result = db.Haberler.OrderByDescending(h=>h.Tarih).ToList().ToPagedList(page, 15);
            return View(result);
        }

        [HttpGet]
        public ActionResult HaberEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HaberEkle(Haberler h,HttpPostedFileBase file)
        {

            try
            {
                if (h != null)
                {
                    if (file.ContentLength > 0)
                    {
                        if (h.Tarih==null)
                        {
                            h.Tarih = DateTime.Now;
                        }
                        var extensiton = Path.GetExtension(file.FileName);
                        if (extensiton == ".JPG" || extensiton == ".png" || extensiton == ".jpg" || extensiton == ".PNG")
                        {
                            string guid = Guid.NewGuid().ToString();
                            
                            string pathway = Path.Combine(Server.MapPath("~/Images/HaberImages/"), Path.GetFileName(guid + extensiton));
                            file.SaveAs(pathway);
                            h.DeleteUrl = pathway;
                            pathway = "../Images/HaberImages/" + guid + extensiton;
                            h.ImageUrl = pathway;
                            db.Haberler.Add(h);

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
        public ActionResult  HaberSil(int id)
        {
            var result = db.Haberler.Find(id);
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
                db.Haberler.Remove(result);

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult  HaberGetir(int id)
        {

            var result = db.Haberler.Find(id);
         
            return View("HaberGetir", result);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HaberGuncelle(Haberler haber)
        {

            var habrgncl = db.Haberler.Find(haber.HaberId);
            habrgncl.HaberBaslik = haber.HaberBaslik;
            habrgncl.HaberEkleyen = haber.HaberEkleyen;
            habrgncl.HaberYazi = haber.HaberYazi;
            habrgncl.Keywords = haber.Keywords;
            if (haber.Tarih!=null)
            {
                habrgncl.Tarih = haber.Tarih;
            }
            
            db.SaveChanges();

            return RedirectToAction("Index");
        }
       
    }
}
