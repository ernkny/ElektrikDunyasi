using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElektrikDunyasi.Models;
using ElektrikDunyasi.Models.Entity;

namespace ElektrikDunyasi.Models
{
    public class NewsBests
    {
        public int id;
        public string baslik;
        public string icerik;
        public string imageurl;
        public string detailUrl;
        //public NewsBests(int id,
        //string baslik, string icerik,
        //string imageurl,
        //string detailUrl)
        //{
        //    this.id = id;
        //    this.baslik = baslik;
        //    this.icerik = icerik;
        //    this.imageurl = imageurl;
        //    this.detailUrl = detailUrl;
        //}

        //public List<NewsBests> NewsBests(){

        //    DergiElektrikEntities db = new DergiElektrikEntities();
        //    NewsBests news= db.Haberler.OrderByDescending(d => Guid.NewGuid()).ToList().Take(20);
        //    return 
        //}

    }
}