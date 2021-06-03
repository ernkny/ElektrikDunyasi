using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElektrikDunyasi.Models.Entity;
using PagedList;

namespace ElektrikDunyasi.Controllers
{

    [Authorize]
    public class BannersController : Controller
    {
        // GET: Banners
        DergiElektrikEntities db = new DergiElektrikEntities();

        //Left banner
        public ActionResult LeftBanners(int page = 1)
        {
            var result = db.LeftBanner.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 15);
            return View(result);
        }
        [HttpGet]
        public ActionResult LeftBannerEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LeftBannerEkle(LeftBanner left,HttpPostedFileBase file)
        {
            try
            {
                if (left != null)
                {
                    if (file.ContentLength > 0)
                    {
                        var extensiton = Path.GetExtension(file.FileName);
                        if (extensiton == ".JPG" || extensiton == ".png" || extensiton == ".jpg" || extensiton == ".PNG")
                        {
                            string guid = Guid.NewGuid().ToString();
                            string pathway = Path.Combine(Server.MapPath("~/Images/Banners/LeftBanners/"), Path.GetFileName(guid + extensiton));
                            file.SaveAs(pathway);
                            left.DeleteUrl = pathway;
                            pathway = "../Images/Banners/LeftBanners/" + guid + extensiton;
                            left.ImageUrl = pathway;
                            db.LeftBanner.Add(left);

                            db.SaveChanges();
                            return RedirectToAction("LeftBanners");
                        }


                        return RedirectToAction("LeftBanners");
                    }
                    return RedirectToAction("LeftBanners");
                }
                return RedirectToAction("LeftBanners");

            }
            catch (Exception)
            {

                return RedirectToAction("LeftBanners");
            }
           
        }
        
        public ActionResult LeftBannerSil(int id)
        {
            var result = db.LeftBanner.Find(id);
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
                db.LeftBanner.Remove(result);

                db.SaveChanges();
            }
            
            return RedirectToAction("LeftBanners");
        }


        //Right Banner
        public ActionResult RightBanners(int page = 1)
        {
            var result = db.RightBanner.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 15);
            return View(result);
        }
        [HttpGet]
        public ActionResult RightBannerEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RightBannerEkle(RightBanner r, HttpPostedFileBase file)
        {
            try
            {
                if (r != null)
                {
                    if (file.ContentLength > 0)
                    {
                        var extensiton = Path.GetExtension(file.FileName);
                        if (extensiton == ".JPG" || extensiton == ".png" || extensiton == ".jpg" || extensiton == ".PNG")
                        {
                            string guid = Guid.NewGuid().ToString();
                            string pathway = Path.Combine(Server.MapPath("~/Images/Banners/RightBanners/"), Path.GetFileName(guid + extensiton));
                            file.SaveAs(pathway);
                            r.DeleteUrl = pathway;
                            pathway = "../Images/Banners/RightBanners/" + guid + extensiton;
                            r.ImageUrl = pathway;
                            db.RightBanner.Add(r);

                            db.SaveChanges();
                            return RedirectToAction("RightBanners");
                        }


                        return RedirectToAction("RightBanners");
                    }
                    return RedirectToAction("RightBanners");
                }
                return RedirectToAction("RightBanners");

            }
            catch (Exception)
            {

                return RedirectToAction("RightBanners");
            }
            
        }
        public ActionResult RightBannerSil(int id)
        {
            var result = db.RightBanner.Find(id);
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
                db.RightBanner.Remove(result);

                db.SaveChanges();
            }

            return RedirectToAction("RightBanners");
        }



        //Main Banner

        public ActionResult MainBanners(int page = 1)
        {
            var result = db.MainBanner.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 15);
            return View(result);
        }
        [HttpGet]
        public ActionResult MainBannerEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MainBannerEkle(MainBanner m,HttpPostedFileBase file)
        {
            try
            {
                if (m != null)
                {
                    if (file.ContentLength > 0)
                    {
                        var extensiton = Path.GetExtension(file.FileName);
                        if (extensiton == ".JPG" || extensiton == ".png" || extensiton == ".jpg" || extensiton == ".PNG")
                        {
                            string guid = Guid.NewGuid().ToString();
                            string pathway = Path.Combine(Server.MapPath("~/Images/Banners/MainBanners/"), Path.GetFileName(guid + extensiton));
                            file.SaveAs(pathway);
                            m.DeleteUrl = pathway;
                            pathway = "../Images/Banners/MainBanners/" + guid + extensiton;
                            m.ImageUrl = pathway;
                            db.MainBanner.Add(m);

                            db.SaveChanges();
                            return RedirectToAction("MainBanners");
                        }


                        return RedirectToAction("MainBanners");
                    }
                    return RedirectToAction("MainBanners");
                }
                return RedirectToAction("MainBanners");
            }
            catch (Exception)
            {

                return RedirectToAction("MainBanners");
            }

        }
        public ActionResult MainBannerSil(int id)
        {
            var result = db.MainBanner.Find(id);
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
                db.MainBanner.Remove(result);

                db.SaveChanges();
            }

            return RedirectToAction("MainBanners");
        }
          
        
    }
}