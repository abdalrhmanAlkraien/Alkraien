using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using adminSection.Models;
using PagedList;

namespace adminSection.Controllers
{
    public class VisitorController : Controller
    {
        
        
        //GET: Visitor
           [HttpGet]
            public ActionResult Rigester()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Rigester(Visitor visitor)
        {
            if (visitor == null)
            {
                return HttpNotFound();
            }
            else
            {
                string fileName = Path.GetFileNameWithoutExtension(visitor.ImageFile.FileName);
                string exe = Path.GetExtension(visitor.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + exe;
                visitor.Visitor_Image_Path = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                visitor.ImageFile.SaveAs(fileName);
                using (Context db = new Context())
                {
                    visitor.User_Type = "Visitor";
                    db.visitor.Add(visitor);
                    db.SaveChanges();


                    return RedirectToAction("Login", "Visitor");
                }

            }
        }
        /// <summary>
        /// the Register Section Function
        /// Details About Function:Register New Visitor 
        /// this Method is success
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["ID"] == null)
            {
                return View();
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Login(Visitor visitor)
        {
            if (visitor == null)
            {
                return HttpNotFound();
            }
            else
            {
                using (Context db = new Context())
                {
                    var i = db.visitor.Where(x => x.Visitor_Email == visitor.Visitor_Email
                    && x.Visitor_Password == visitor.Visitor_Password).FirstOrDefault();
                    if (i != null)
                    {
                        if (i.User_Type == "Visitor")
                        {

                            Session["ID"] = i.Visitro_ID;
                            Session["Name"] = i.Visitor_FullName;
                            Session["ImagePath"] = i.Visitor_Image_Path;
                            Session["UserType"] = "Visitor";
                            return RedirectToAction("Home", "Visitor");
                        }
                    }
                    else
                    {

                        visitor.loginErrorMessage = "Email or password is Wrong";
                        return View("login", visitor);
                    }
                    return View();
                }
            }
        }
        /// <summary>
        /// Login Function
        /// Details About Function:Login The Visitor
        /// this Method is success
        /// </summary>
        /// <returns></returns>
        public ActionResult Home(int ?page)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                using (Context db = new Context())
                {
                    int id = int.Parse(Session["ID"].ToString());
                    var vou = db.voulunter.Where(x => x.Visitro_ID == id).ToList().ToPagedList(page ?? 1, 3);
                    return View(vou);
                }
            }
        }

        [HttpGet]
        public ActionResult Add_volunteer(int? id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Visitor");
            }
            else
            {
                // Session["ID"]
                return View();

            }
        }
        [HttpPost]
        public ActionResult Add_volunteer(volunteer voluter)
        {
            if (voluter != null)
            {
                using (Context db = new Context())
                {
                    var i = db.visitor.Find(Session["ID"]);
                    voluter.Visitro_ID = int.Parse(Session["ID"].ToString());
                    voluter.Volunteer_status = "under consideration";
                    voluter.vistor = i;
                    db.voulunter.Add(voluter);
                    db.SaveChanges();
                    i.volunterr = new List<volunteer>();
                    i.volunterr.Add(voluter);
                    db.Entry(i).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("volunteerList");
                }
            }
            else
            {
                return HttpNotFound();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult Remove_volunteer()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit_volunteer()
        {
            return View();
        }
        public ActionResult volunteerList()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Visitor");
            }
            else
            {
                using (Context db = new Context())
                {
                    var visitor = db.visitor.Find(Session["ID"]);
                    if (visitor == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {

                        return View(visitor.volunterr.ToList());
                    }
                }
            }

        }
        [HttpGet]
        public ActionResult More_Details( int ? id)
        {
            if(Session["ID"]!=null)
            {
                if(Session["UserType"].ToString() =="Visitor")
                {
                    if(id!=null)
                    {
                        using (Context db = new Context())
                        {
                            var vol = db.voulunter.Find(id);
                            return View(vol);
                        }
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
            }
            
            return RedirectToAction("Login");
        }


    }
}
