using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElektrikDunyasi.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace ElektrikDunyasi.Controllers
{
    [Authorize]
    public class RoportajController : Controller
    {
        // GET: Roportaj
        DergiElektrikEntities db = new DergiElektrikEntities();
        public ActionResult Index(int page=1)
        {
            var result = db.Rportaj.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page,15) ;
            return View(result);
        }
        [HttpGet]
        public ActionResult RoportajEkle()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RoportajEkle(Rportaj r, HttpPostedFileBase file)
        {

            try
            {
                if (r != null)
                {
                    if (file.ContentLength > 0)
                    {
                        if (r.Tarih == null)
                        {
                            r.Tarih = DateTime.Now;
                        }
                        var extensiton = Path.GetExtension(file.FileName);
                        if (extensiton == ".JPG" || extensiton == ".png" || extensiton == ".jpg" || extensiton == ".PNG")
                        {
                            string guid = Guid.NewGuid().ToString();
                            string pathway = Path.Combine(Server.MapPath("~/Images/RoportajImages/"), Path.GetFileName(guid + extensiton));
                            file.SaveAs(pathway);
                            r.DeleteUrl = pathway;
                            pathway = "../Images/RoportajImages/" + guid + extensiton;
                            r.ImageUrl = pathway;
                            db.Rportaj.Add(r);

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
        public ActionResult RoportajSil(int id)
        {
            var result = db.Rportaj.Find(id);
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
                db.Rportaj.Remove(result);

                db.SaveChanges();
            }
            return RedirectToAction("Index");


        }


        public ActionResult RoportajGetir(int id)
        {

            var result = db.Rportaj.Find(id);

            return View("RoportajGetir", result);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Guncelle(Rportaj rp)
        {

            var rprtajgncl = db.Rportaj.Find(rp.RportajId);
            rprtajgncl.RportajBaslik = rp.RportajBaslik;
            
            rprtajgncl.RportajYazi = rp.RportajYazi;
            rprtajgncl.Keywords = rp.Keywords;
            if (rp.Tarih != null)
            {
                rprtajgncl.Tarih = rp.Tarih;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}