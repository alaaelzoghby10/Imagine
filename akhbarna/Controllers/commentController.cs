using akhbarna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace akhbarna.Controllers
{
    
    public class commentController : Controller
    {
        akhbarnaEntities1 nh = new akhbarnaEntities1();

        #region Add comment
        [HttpGet]
        public ActionResult add(string owner,string mail,string details,int newsid)
        {
            comment comments=new comment();
            comments.date = DateTime.Now;
            comments.news_id = newsid;
            comments.owner = owner;
            comments.mail = mail;
            comments.details = details;
            nh.comments.Add(comments);
            nh.SaveChanges();
            return RedirectToAction("index","news");
        }
        #endregion


         #region Delete a comment
           public ActionResult delete(int id)
        {
            if(Session["admin"]==null)
            {
                return RedirectToAction("login" , "news");
            }

            else
            {
                comment mycomment = nh.comments.First(m => m.id == id);
                nh.comments.Remove(mycomment);
                nh.SaveChanges();
                return RedirectToAction("index","news");
            }
        }
         #endregion
        
      

    }
}
