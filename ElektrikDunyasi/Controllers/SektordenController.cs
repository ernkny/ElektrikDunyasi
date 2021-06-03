using ElektrikDunyasi.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElektrikDunyasi.Controllers
{
    
        [Authorize]
        public class SektordenController : Controller
        {
            // GET: Haberler
            DergiElektrikEntities db = new DergiElektrikEntities();

            public ActionResult Index(int page = 1)
            {
                var result = db.Sektorden.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 15);
                return View(result);
            }

            [HttpGet]
            public ActionResult SektordenEkle()
            {
                return View();
            }

            [HttpPost]
            [ValidateInput(false)]
            public ActionResult SektordenEkle(Sektorden s, HttpPostedFileBase file)
            {

                try
                {
                    if (s != null)
                    {
                        if (file.ContentLength > 0)
                        {
                        if (s.Tarih == null)
                        {
                            s.Tarih = DateTime.Now;
                        }
                        var extensiton = Path.GetExtension(file.FileName);
                            if (extensiton == ".JPG" || extensiton == ".png" || extensiton == ".jpg" || extensiton == ".PNG")
                            {
                                string guid = Guid.NewGuid().ToString();
                                string pathway = Path.Combine(Server.MapPath("~/Images/SektordenImages/"), Path.GetFileName(guid + extensiton));
                                file.SaveAs(pathway);
                                s.DeleteUrl = pathway;
                                pathway = "../Images/SektordenImages/" + guid + extensiton;
                                s.ImageUrl = pathway;
                                db.Sektorden.Add(s);

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
            public ActionResult SektordenSil(int id)
            {
                var result = db.Sektorden.Find(id);
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
                    db.Sektorden.Remove(result);

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            public ActionResult SektordenGetir(int id)
            {

                var result = db.Sektorden.Find(id);

                return View("SektordenGetir", result);

            }

            [HttpPost]
            [ValidateInput(false)]
            public ActionResult SektordenGuncelle(Sektorden sektor)
            {

                var sektorgncl = db.Sektorden.Find(sektor.SektorId);
                sektorgncl.Baslik = sektor.Baslik;

                sektorgncl.Yazi = sektor.Yazi;
                sektorgncl.Keywords = sektor.Keywords;
                if (sektor.Tarih != null)
                {
                    sektorgncl.Tarih = sektor.Tarih;
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }

        }
    }
