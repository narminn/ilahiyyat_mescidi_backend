using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Ilahiyyat.Models;
using Ilahiyyat.ViewModel;
using Newtonsoft.Json.Linq;
using PagedList;
using PagedList.Mvc;

namespace Ilahiyyat.Controllers
{
    public class HomeController : Controller
    {
        ilahiyyetEntities db = new ilahiyyetEntities();
        IndexModel im = new IndexModel();
        AdvertModel am = new AdvertModel();
        BlogModel bm = new BlogModel();
        GaleryModel gm = new GaleryModel();
        LibraryModel lm = new LibraryModel();
        QuestionModel qm = new QuestionModel();
        SermonModel sm = new SermonModel();
        SingleAdvertModel sam = new SingleAdvertModel();
        SingleBookModel sbm = new SingleBookModel();
        SingleNewsModel snm = new SingleNewsModel();
        SinglePillarModel spm = new SinglePillarModel();
        SingleSermonModel ssm = new SingleSermonModel();
       
        public ActionResult Index()
        {

            //return Content(parseJson() + "");

            im._advert = db.Advertisements.Take(3).OrderByDescending(a => a.elanin_başlanqic_günü).ToList();
            im._news = db.News.Take(3).OrderByDescending(n => n.xəberin_əlavə_edilən_gün).ToList();
            im._sermon = db.Sermons.Take(6).ToList();
            im._slider = db.Sliders.Take(5).ToList();
            return View(im);
        }
        
        


        public ActionResult About()
        {
           

            return View();
        }
        public ActionResult Aphabite()
        {
            return View();
        }

        public ActionResult ContactPage()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ContactPage(FormCollection frm)
        {
            Contact con = new Contact();
            con.user_adi = frm["name"];
            con.user_email = frm["email"];
            con.user_nomresi = frm["phone"];
            con.user_mesaji = frm["message"];
            db.Contacts.Add(con);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Advertisement(int ? page)
        {
            int pageSize = 4;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var _adver = db.Advertisements.OrderByDescending(d => d.elanin_başlanqic_günü).ToList();
            IEnumerable<Advertisement> _adv_list = db.Advertisements.ToList();
           // am._advertis = db.Advertisements.OrderByDescending(d => d.elanin_başlanqic_günü).ToList();
            return View(_adver.ToPagedList(pageIndex,pageSize));
        }
        public ActionResult Blog(int ? page)
        {
            
            //return Content(news_count.ToString());
            int pageSize = 6;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            
            var _news = db.News.OrderByDescending(w => w.xəberin_əlavə_edilən_gün).ToList();
            bm._news = _news.ToPagedList(pageIndex, pageSize);
            IEnumerable<News> _news_list = db.News.ToList();
            return View(_news.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult Galery()
        {
            gm._galery = db.Galleries.ToList();
            return View(gm);
        }
        public ActionResult Library()
        {
            lm._books = db.Books.ToList();
            return View(lm);
        }
        public ActionResult QuestionPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult QuestionPage(FormCollection frm)
        {
            Question quest = new Question();
            quest.sual_user_adi = frm["u_name"];
            quest.sual_user_email = frm["u_email"];
            quest.sual = frm["u_question"];
            quest.sual_əlavə_edilən_gün = DateTime.Now.ToShortDateString();
            db.Questions.Add(quest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Question_answer(int ? page)
        {
            int pageSize = 6;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var _quest = db.Questions.ToList();
            IEnumerable<Question> _quest_list = db.Questions.ToList();
           // qm._question = db.Questions.ToList();
            return View(_quest.ToPagedList(pageIndex,pageSize));
        }
        public ActionResult Sermons(int ? page)
        {
            int pageSize = 6;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            // sm._sermons = db.Sermons.ToList();
            var _sermons = db.Sermons.ToList();
            IEnumerable<Sermon> _srmn_list = db.Sermons.ToList();
            return View(_sermons.ToPagedList(pageIndex,pageSize));
        }
        public ActionResult Single_advert(int id)
        {
            sam._single_advert = db.Advertisements.Where(v => v.id == id).FirstOrDefault();
            sam._advert = db.Advertisements.Take(5).OrderByDescending(d => d.elanin_başlanqic_günü).ToList();
            return View(sam);
        }
        public ActionResult Single_book(int id)
        {
            sbm._single_book = db.Books.Where(b => b.id == id).FirstOrDefault();
            return View(sbm);
        }
        public ActionResult Single_news(int id)
        {
            snm._single_news = db.News.Where(s => s.id == id).FirstOrDefault();
            snm._news = db.News.Take(5).OrderByDescending(w => w.xəberin_əlavə_edilən_gün).ToList();
            snm._single_news.xəberin_baxiş_sayi++;
            db.SaveChanges();
            return View(snm);
        }
        public ActionResult Single_pillar(int id)
        {
            spm._pillar = db.Pillars.Where(p => p.id == id).FirstOrDefault();
            spm._pillars = db.Pillars.ToList();
            return View(spm);
        }
        public ActionResult Single_sermon(int id)
        {
            ssm._sermon = db.Sermons.Where(s => s.id == id).FirstOrDefault();
            ssm._sermons = db.Sermons.Take(5).ToList();
            return View(ssm);
        }
        public ActionResult NewsSearch(string search)
        {
            IEnumerable<News> news_list = db.News.Where(e => e.xəberin_adi.Contains(search) || e.xəberin_məzmunu.Contains(search)).ToList();
            return View(news_list);
        }
        public ActionResult AdvertSearch(string search)
        {
            IEnumerable<Advertisement> adv_list = db.Advertisements.Where(e => e.elanin_adi.Contains(search) || e.elanin_məzmunu.Contains(search)).ToList();
            return View(adv_list);
        }
        public ActionResult BookSearch(string search)
        {
            IEnumerable<Book> book_list = db.Books.Where(e => e.kitabin_adi.Contains(search)).ToList();
            return View(book_list);
        }
        public ActionResult SermonSearch(string search)
        {
            IEnumerable<Sermon> srm_list = db.Sermons.Where(e => e.xutbə_adi.Contains(search) || e.xutbə_məzmunu.Contains(search)).ToList();
            return View(srm_list);
        }
        
    }
}