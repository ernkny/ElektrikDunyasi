using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElektrikDunyasi.Models;
using ElektrikDunyasi.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace ElektrikDunyasi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            
            vm.rportaj = db.Rportaj.OrderByDescending(d => Guid.NewGuid()).ToList().Take(20);
            vm.teknoloji = db.Teknoloji.OrderByDescending(d => Guid.NewGuid()).ToList().Take(20);
            vm.makaleler = db.Makaleler.OrderByDescending(d => Guid.NewGuid()).ToList().Take(20);
            vm.leftBanner = db.LeftBanner.OrderByDescending(d => d.Tarih).ToList();
            vm.rightBanner = db.RightBanner.OrderByDescending(d => d.Tarih).ToList();
            vm.mainBanner = db.MainBanner.OrderByDescending(d => d.Tarih).ToList();
            
            vm.eDergi = db.EDergi.ToList();
            return View(vm);
        }


        public ActionResult Dergi()
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            var result = db.EDergi.ToList();
            return View(result);
        }

        [HttpPost]
        public ActionResult AboneEkle(AboneInfo abone)
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            abone.Tarih = DateTime.Now;
            db.AboneInfo.Add(abone);
            db.SaveChanges();
           
            return Redirect("Index");
        }

        public ActionResult Kunye()
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.elekts = db.ElektKunye.ToList();
            vm.eDergi = db.EDergi.ToList();
            return View(vm);
        }
        public ActionResult Hakkimizda()
        {

            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.hakkimiz = db.Hakkimizda.ToList();
            vm.eDergi = db.EDergi.ToList();
            return View(vm);

        }
        public ActionResult Haberler(int page = 1)
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.habersss = db.Haberler.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 10);
            vm.haberler=db.Haberler.OrderByDescending(d => Guid.NewGuid()).ToList().Take(30);
            vm.eDergi = db.EDergi.ToList();
            return View(vm);
        }
        public ActionResult Haber(int id)
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.haberler = db.Haberler.ToList();
            vm.haberler= db.Haberler.OrderByDescending(d => Guid.NewGuid()).ToList().Take(30);
            vm.id = id;
            vm.eDergi = db.EDergi.ToList();
            
            return View(vm);
        }
        public ActionResult Makaleler(int page = 1)
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.makalelerss= db.Makaleler.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 10);
            vm.makaleler = db.Makaleler.OrderByDescending(d => Guid.NewGuid()).ToList().Take(30);
            vm.eDergi = db.EDergi.ToList();
            return View(vm);
        }

        public ActionResult Makale(int id)
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.makaleler = db.Makaleler.ToList();
            vm.makaleler = db.Makaleler.OrderByDescending(d => Guid.NewGuid()).ToList().Take(30);
            vm.id = id;
            vm.eDergi = db.EDergi.ToList();

            return View(vm);
        }
        public ActionResult Teknolojiler(int page = 1)
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.teknss = db.Teknoloji.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 10);
            vm.teknoloji = db.Teknoloji.OrderByDescending(d => Guid.NewGuid()).ToList().Take(30);
            vm.eDergi = db.EDergi.ToList();
            return View(vm);
        }

        public ActionResult Teknoji(int id)
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.teknoloji = db.Teknoloji.ToList();
            vm.teknoloji = db.Teknoloji.OrderByDescending(d => Guid.NewGuid()).ToList().Take(30);
            vm.id = id;
            vm.eDergi = db.EDergi.ToList();

            return View(vm);
        }
        public ActionResult Roportajlar(int page = 1)
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.Rportajss = db.Rportaj.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 10);
            vm.rportaj = db.Rportaj.OrderByDescending(d => Guid.NewGuid()).ToList().Take(30);
            vm.eDergi = db.EDergi.ToList();
            return View(vm);
        }

        public ActionResult Roportaj(int id)
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.rportaj = db.Rportaj.ToList();
            vm.rportaj = db.Rportaj.OrderByDescending(d => Guid.NewGuid()).ToList().Take(30);
            vm.id = id;
            vm.eDergi = db.EDergi.ToList();

            return View(vm);
        }
        public ActionResult Arsiv(int page = 1)
        {

            DergiElektrikEntities db = new DergiElektrikEntities();
            
            var result = db.Dergi.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 12);
            return View(result);
        }
        public ActionResult DergiPdf(int id)
        {

            DergiElektrikEntities db = new DergiElektrikEntities();

            
            ViewModel vm = new ViewModel();
            vm.dergi = db.Dergi.ToList();
            vm.id = id;
            return View(vm);
        }
        public ActionResult Editorden(int page = 1)
        {

            DergiElektrikEntities db = new DergiElektrikEntities();
            
            
            ViewModel vm = new ViewModel();
            vm.sdtimages = db.stdImages.ToList();
            vm.editorss = db.Editor.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 10);
            vm.eDergi = db.EDergi.ToList();
            return View(vm);
        }
        public ActionResult Editor(int id)
        {
            
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.sdtimages = db.stdImages.ToList();
            vm.Editors = db.Editor.ToList();
           
            vm.id = id;
            vm.eDergi = db.EDergi.ToList();

            return View(vm);
        }
        public ActionResult Akademisyenden(int page = 1)
        {

            DergiElektrikEntities db = new DergiElektrikEntities();


            ViewModel vm = new ViewModel();
            vm.sdtimages = db.stdImages.ToList();
            vm.Akademisyens = db.Akedemisyen.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 10);
            vm.eDergi = db.EDergi.ToList();
            return View(vm);
        }
        public ActionResult Akademisyen(int id)
        {

            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.sdtimages = db.stdImages.ToList();
            vm.Akademisyen = db.Akedemisyen.ToList();
           
            vm.id = id;
            vm.eDergi = db.EDergi.ToList();

            return View(vm);
        }
        public ActionResult Sektorden(int page = 1)
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.sektors = db.Sektorden.OrderByDescending(h => h.Tarih).ToList().ToPagedList(page, 10);
            vm.sektor = db.Sektorden.OrderByDescending(d => Guid.NewGuid()).ToList().Take(30);
            vm.eDergi = db.EDergi.ToList();
            return View(vm);
        }

        public ActionResult Sektor(int id)
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            ViewModel vm = new ViewModel();
            vm.sektor = db.Sektorden.ToList();
            vm.sektor = db.Sektorden.OrderByDescending(d => Guid.NewGuid()).ToList().Take(30);
            vm.id = id;
            vm.eDergi = db.EDergi.ToList();

            return View(vm);
        }
        public ActionResult İletisim()
        {
            DergiElektrikEntities db = new DergiElektrikEntities();
            
            var result = db.Contact.ToList();
            
            return View(result);
        }

    }
}   