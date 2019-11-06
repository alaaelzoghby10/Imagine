using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using akhbarna.Models;
using System.Collections;
using System.IO;

namespace akhbarna.Controllers
{
    
    public class newsController : Controller
    {
    
        akhbarnaEntities1 nh = new akhbarnaEntities1();
       

        #region index
         public ActionResult Index(string id)
        {
           
            if (id == null)
            {
                var news = nh.news.OrderByDescending(m => m.date).Take(15);
                return View(news);
            }

            else
            {
                //var news = from b in nh.news.OrderBy(m=>m.date)
                //           where b.category == id
                //           select b;
                var news=nh.news.Where(m=>m.category==id).OrderByDescending(m=>m.date).Take(30);
                Session["news"] = nh.news.Select(m => m.title).Take(5);
                return View(news);
            }

            

        }
        #endregion

        #region details

        public ActionResult details(int id)
         {
             var searchednew = nh.news.First(m => m.id==id);
             searchednew.views = searchednew.views + 1;
             nh.Configuration.ValidateOnSaveEnabled = false;
             nh.SaveChanges();
             ViewBag.id = searchednew.id;
             return View(searchednew);
         }

        #endregion


        #region add news
        [HttpGet]
        public ActionResult add()
        {
            if (Session["admin"] != null)
            {
                return View();
            }

            else
            {
                return RedirectToAction("login");
            }
        }

        [HttpPost]

        public ActionResult add(news mynew)
        {
            HttpPostedFileBase file = mynew.imagelink;
            string imagename="";
            string imagepath = "";
            if (file.ContentLength > 0)
            {
                imagename = Path.GetFileName(file.FileName);
                imagepath = Path.Combine(Server.MapPath("~/images/" + imagename));
                file.SaveAs(imagepath);
            }
            mynew.views = 0;
            mynew.date = DateTime.Now;
            mynew.image = imagename;
            nh.news.Add(mynew);
            nh.Configuration.ValidateOnSaveEnabled = false;
            nh.SaveChanges();
            return RedirectToAction("index");
        }

        #endregion


        #region logIn
          [HttpGet]

        public ActionResult login()
        {
            if (Session["admin"] == null)
            {
                return View();
            }

            else
            {
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult login(string male,string password)
        {
            try
            {
                var user = nh.users.First(m => (m.male == male) && (m.password == password));
                Session["admin"] = male;
                return RedirectToAction("add");
            }
            catch
            {

                ViewBag.message = "البريد الإلكتروني او اسم المستخدم غير صحيح";
                return View();
            }
        }
       

        #endregion

        #region Delete
        public ActionResult delete(int id)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("login", "news");
            }

            else
            {
                news mynew = nh.news.First(m => m.id == id);
                bool check = true;
                while (check==true)
                { 
                    
                    try
                    {
                        comment mycomment= nh.comments.Remove(nh.comments.First(m=>m.news_id==id));
                        nh.comments.Remove(mycomment);
                        nh.SaveChanges();
                    }

                catch 
                    {
                    check = false;
                    }
                }
                
               
                nh.news.Remove(mynew);

                //comment mycomment = nh.comments.Find(mynew.id);
                //nh.comments.Remove(mycomment);
                
                System.IO.File.Delete(Server.MapPath("~/images/" +mynew.image));
                nh.SaveChanges();
                return RedirectToAction("index", "news");
            }
        }
        #endregion


        #region contact
        public ActionResult contact()
        {
            return View();
        } 
        #endregion


        //news slider 
     
    }
}
