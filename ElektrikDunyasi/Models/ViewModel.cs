using ElektrikDunyasi.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;


namespace ElektrikDunyasi.Models
{
    public class ViewModel
    {
        public IEnumerable<Haberler> haberler { get; set; }
        public IEnumerable<Rportaj> rportaj { get; set; }
        public IEnumerable<Makaleler> makaleler { get; set; }
        public IEnumerable<Teknoloji> teknoloji { get; set; }
        public IEnumerable<LeftBanner> leftBanner { get; set; }
        public IEnumerable<RightBanner> rightBanner { get; set; }
        public IEnumerable<MainBanner> mainBanner { get; set; }
        public IEnumerable<Dergi> dergi { get; set; }
        public IEnumerable<EDergi> eDergi { get; set; }
        public IEnumerable<AboneInfo> aboneInfo { get; set; }
        public IEnumerable<ElektKunye> elekts { get; set; }
        public IEnumerable<Hakkimizda> hakkimiz { get; set; }
        public IPagedList<Haberler> habersss { get; set; }
        public IPagedList<Makaleler> makalelerss { get; set; }
        public IPagedList<Teknoloji> teknss { get; set; }
        public IPagedList<Rportaj> Rportajss { get; set; }
        public IEnumerable<Editor> Editors { get; set; }
        public IPagedList<Editor>  editorss { get; set; }
        public IEnumerable<Akedemisyen> Akademisyen { get; set; }
        public IPagedList<Akedemisyen> Akademisyens { get; set; }
        public IEnumerable<Sektorden> sektor { get; set; }
        public IEnumerable<stdImages> sdtimages { get; set; }
        public IPagedList<Sektorden> sektors { get; set; }
        public int id { get; set; }
        public string pdfUrl { get; set; }
        
    }
}