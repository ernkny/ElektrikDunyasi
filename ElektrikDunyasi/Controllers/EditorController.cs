using System;
using System.Collections.Generic;
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
    public class EditorController : Controller
    {
        // GET: Editor
        DergiElektrikEntities db = new DergiElektrikEntities();
        public ActionResult Index(int page=1)
        {
            var result = db.Editor.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 15);
            return View(result);
        }

        [HttpGet]
        public ActionResult EditorEkle()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditorEkle(Editor ed)
        {

            try
            {
                if (ed != null)
                {
                    if (ed.Tarih==null)
                    {
                        ed.Tarih = DateTime.Now;
                    }
                            db.Editor.Add(ed);

                            db.SaveChanges();
                       
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }



        }
        public ActionResult EditorSil(int id)
        {
            var result = db.Editor.Find(id);
         
                db.Editor.Remove(result);

                db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        public ActionResult EditorGetir(int id)
        {

            var result = db.Editor.Find(id);

            return View("EditorGetir", result);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditorGuncelle(Editor ed)
        {

            var editgncl = db.Editor.Find(ed.EditorId);
            editgncl.Baslik = ed.Baslik;
            editgncl.Icerik = ed.Icerik;

            editgncl.Keywords = ed.Keywords;
            if (ed.Tarih != null)
            {
                editgncl.Tarih = ed.Tarih;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ResimGetir(int id)
        {

            var result = db.stdImages.Find(id);

            return View("ResimGetir", result);

        }
        public ActionResult ResimGuncelle(stdImages std)
        {

            var gncl = db.stdImages.Find(std.Id);
            gncl.ImageUrl = std.ImageUrl;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
