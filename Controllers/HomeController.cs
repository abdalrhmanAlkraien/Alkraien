using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using adminSection.Models;

namespace adminSection.Controllers
{
    public class HomeController : Controller
    {
       // Context db = new Context();
        // GET: Home
        [HttpGet]
        public ActionResult Rigester(int? id)
        {
            if (Session["ID"] != null)
            {
                if (id != null)
                {
                    Session["HomeId"] = id;
                    return View();
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
                return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Rigester(Admin adm)
        {
            string fileName = Path.GetFileNameWithoutExtension(adm.ImageFile.FileName);
            string exe = Path.GetExtension(adm.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + exe;
            adm.Adm_Image_Path = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            adm.ImageFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                if (adm.Adm_Gender == Session["GenderHome1"].ToString())//form Minitry controll in Home_Details method
                {
                    using (Context db = new Context())
                    {
                        int id = int.Parse(Session["HomeId"].ToString());
                        var Home = db.home.Find(id);
                        var Min_Admin = db.ministry.Find(Home.Min_Id);
                        adm.home = Home;
                        adm.Home_ID = Home.Home_ID;
                        adm.min = Min_Admin;
                        adm.Min_id = Min_Admin.Min_Id;
                        adm.User_Type = "Admin";
                        db.admin.Add(adm);
                        Min_Admin.admin = new List<Admin>();
                        Min_Admin.admin.Add(adm);
                        Home.admin = new List<Admin>();
                        Home.admin.Add(adm);
                        db.Entry(Min_Admin).State = System.Data.Entity.EntityState.Modified;
                        db.Entry(Home).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Home", "Minsitry");
                    }
                }
                
                   
            }
            return View();
        }
        ////Login Section
        [HttpGet]
        public ActionResult login()
        {
            if (Session["ID"] != null)
            {
                return HttpNotFound();
            }
            else
            return View();
        }
        [HttpPost]
        public ActionResult login(Admin adm)
        {
            if (adm == null)
            {
                return HttpNotFound();
            }
                using (Context db = new Context())
                {
                    var i = db.admin.Where(x => x.Adm_Email == adm.Adm_Email && x.Adm_Password == adm.Adm_Password).FirstOrDefault();
                    if (i == null)
                    {
                        adm.loginErrorMessage = "Email or password is Wrong";
                        return View("login", adm);

                    }
                    else if(i.User_Type=="Admin")
                    {

                    Session["ID"] = i.Adm_ID;
                    Session["Name"] = i.Adm_Name;
                    Session["ImagePath"] = i.Adm_Image_Path;
                    Session["UserType"] = "Admin";
                    return RedirectToAction("Home", "Admin");

                    }
                else
                {
                    return View();
                }

                }
           

             
        }
        [HttpGet]
        public ActionResult Logout()
        {
            if (Session["ID"] != null)
            {

                Session.RemoveAll();
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return HttpNotFound();
            }


        }
        /// <summary>
        /// LogOut Section
        /// Details About Function: Logout From system
        /// success this method
        /// </summary>
        /// <returns></returns>
        /// 
    }
}