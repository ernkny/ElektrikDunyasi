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
    public class DergiController : Controller
    {
        DergiElektrikEntities db = new DergiElektrikEntities();
        // GET: Dergi
        public ActionResult Index(int page=1)
        {
            var result = db.Dergi.OrderByDescending(d => d.Tarih).ToList().ToPagedList(page, 15);
            return View(result);
        }
        [HttpGet]
        public ActionResult PdfEkle()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PdfEkle(Dergi d,HttpPostedFileBase file, HttpPostedFileBase fileImage)
        {
            try
            {
                if (d != null)
                {
                    if (file.ContentLength > 0 && fileImage.ContentLength>0)
                    {
                        var extensiton = Path.GetExtension(file.FileName);
                        var extensitonImage = Path.GetExtension(fileImage.FileName);
                        if (extensiton == ".PDF" || extensiton == ".pdf"  )
                        {
                             
                            
                            string pathway = Path.Combine(Server.MapPath("~/Dergipdf/"), Path.GetFileName(file.FileName));
                            file.SaveAs(pathway);
                            d.DergiDeleteUrl = pathway;
                            pathway = "../Dergipdf/" +file.FileName;
                            d.DergiUrl = pathway;
                            if (extensitonImage==".jpg" || extensitonImage==".png")
                            {
                                string guid = Guid.NewGuid().ToString();
                                string pathwayImage = Path.Combine(Server.MapPath("~/Images/KapakImages/"), Path.GetFileName(guid + extensitonImage));
                                fileImage.SaveAs(pathwayImage);
                                d.DeleteImageUrl = pathwayImage;
                                pathwayImage = "../Images/KapakImages/" + guid + extensitonImage;
                                d.ImageUrl = pathwayImage;
                                db.Dergi.Add(d);

                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
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


        public ActionResult DergiSil(int id)
        {
            var result = db.Dergi.Find(id);
            var path = result.DergiDeleteUrl;
            try
            {

                System.IO.File.Delete(path);
                result.DergiUrl = null;

            }
            catch
            {
                result.DergiUrl = null;
            }
            finally
            {
                db.Dergi.Remove(result);

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}