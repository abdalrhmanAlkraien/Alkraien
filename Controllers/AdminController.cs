using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using adminSection.Models;
using PagedList;

namespace adminSection.Controllers
{
    public class AdminController : Controller
    {
        public Context db = new Context();
        // GET: Admin
        public ActionResult Home(int? page)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "Home");
            else
            {
                var admin = db.admin.Find(Session["ID"]);
                var Home = db.home.Find(admin.Home_ID);
                Session["HomeId"] = Home.Home_ID;
                var child_list = db.child.ToList().ToPagedList(page?? 1,6);
                return View(child_list);
            }
        }

        [HttpGet]
        public ActionResult Rigester()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "Home");
            else
            {
               
                return View();
            }
        }
        [HttpPost]
        public ActionResult Rigester(Child chi)
        {
            string fileName = Path.GetFileNameWithoutExtension(chi.ImageFile.FileName);
            string exe = Path.GetExtension(chi.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + exe;
            chi.Chi_Image_Path = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            chi.ImageFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                using (Context db = new Context())
                {
                    var HomeId = int.Parse(Session["HomeId"].ToString());
                    var home = db.home.Find(HomeId);
                    int home_age = home.Home_Age_Group;
                    if (home.Home_Child_Gender == chi.Gender && home.Home_Age_Group>=chi.Age)
                    {
                        var admin = db.admin.Find(Session["ID"]);
                        var min = db.ministry.Find(home.Min_Id);
                        chi.admin = admin;
                        chi.Adm_ID = admin.Adm_ID;
                        chi.min = min;
                        chi.Min_id = min.Min_Id;
                        chi.Home = home;
                        chi.Home_ID = home.Home_ID;

                        db.child.Add(chi);
                        admin.chi.Add(chi);
                        min.child.Add(chi);
                        home.child.Add(chi);
                        db.Entry(admin).State = EntityState.Modified;
                        db.Entry(min).State = EntityState.Modified;
                        db.Entry(home).State = EntityState.Modified;
                        db.SaveChanges();


                        if (chi.Chi_parent_status == "liver" || chi.Chi_parent_status == "Separated" || chi.Chi_parent_status == "Widower")
                        {
                            return RedirectToAction("List_Child", "Admin");
                        }
                        else
                            return RedirectToAction("Home", "Admin");

                    }
                    return View();

                }
            }
            else
                return View();



        }

        public ActionResult RigesterParent()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "Home");
            else
                return View();

        }

              

        [HttpGet]
        public ActionResult Remove(int id)
        {
            using (Context db = new Context())
            {
                if (id == 0)
                {
                    return HttpNotFound();
                }
                else
                {
                    var child = db.child.Find(id);
                    var admin = db.admin.Find(child.Adm_ID);
                    var parent = db.parent.Find(child.Par_ID);
                    var home = db.home.Find(child.Home_ID);
                    var min = db.ministry.Find(child.Min_id);
                    var note = db.note.RemoveRange(db.note.Where(x => x.Child_id == child.Chi_Id));
                    //db.Entry(note).State = EntityState.Modified;
                    db.SaveChanges();
                    if (child.parent != null)
                    {
                        parent.child.Remove(child);
                        db.Entry(parent).State = EntityState.Modified;
                        db.SaveChanges();
                    }


                    admin.chi.Remove(child);
                    db.Entry(admin).State = EntityState.Modified;
                    min.child.Remove(child);
                    db.Entry(min).State = EntityState.Modified;
                    home.child.Remove(child);
                    db.Entry(home).State = EntityState.Modified;




                    db.child.Remove(db.child.Find(id));
                    db.SaveChanges();
                    return RedirectToAction("Home", "Admin");



                }

            }

        }

       


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(Session["ID"]!=null)
            {
                if (id != null)
                {
                    Session["ChildID"] = id;
                    return View();
                }
                else
                {
                    return HttpNotFound();
                }

            }

            else
            {
                return RedirectToAction("Login", "Home");
            }

        }


        [HttpPost]
        public ActionResult Edit(Child child)
        {
            string fileName = Path.GetFileNameWithoutExtension(child.ImageFile.FileName);
            string exe = Path.GetExtension(child.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + exe;
            child.Chi_Image_Path = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            child.ImageFile.SaveAs(fileName);
            if (child != null)
            {
                if (ModelState.IsValid)
                {
                    using (Context db = new Context())
                    {
                        var oldchild = db.child.Find(Session["ChildID"]);
                        oldchild.Chi_First_Name = child.Chi_First_Name;
                        oldchild.Chi_Last_Name = child.Chi_Last_Name;
                        oldchild.Age = child.Age;
                        oldchild.Gender = child.Gender;
                        oldchild.Chi_parent_status = child.Chi_parent_status;
                        oldchild.Chi_Image_Path = child.Chi_Image_Path;
                        db.Entry(oldchild).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Home");
                    }


                }
                else
                {
                    return View();
                }
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        /// <summary>
        /// Edit Done 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
           
           

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "Home");
            else
            {

                Child obj = db.child.Single(x => x.Chi_Id == id);

                return View(obj);

            }
        }
        [HttpGet]
        
        
        public ActionResult List_Child()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "Home");

            else
            {

                var child_ok = db.child.Where(x => x.Chi_parent_status == "liver" || x.Chi_parent_status == "Separated" || x.Chi_parent_status == "Widower").ToList();

                var quary = from o in child_ok where o.Par_ID == 0 select o;
                
                return View(quary);
            }
        }
        [HttpGet]
        public ActionResult ShowParent (int? page)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "Home");
            else
            {
                var parent = db.parent.Include("child").ToList();
                var admin = db.admin.Find(Session["ID"]);
                var list = new List<Parent>();
                foreach(var i in parent)
                {
                    var child =i.child.ToList();
                    foreach(var x in child)
                    {
                        if(x.Home_ID==admin.Home_ID)
                        {
                            list.Add(i);
                        }
                    }
                }
                
                //var parent = db.parent.ToList().ToPagedList(page?? 1,6);
                return View(list.ToList().ToPagedList(page ?? 1, 6));
            }
        }

        [HttpGet]
        public ActionResult Add_Note(int? id)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "Home");
            else
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    using (Context db = new Context())
                    {
                        Child child = db.child.Find(id);
                        if (child == null)
                        {
                            return HttpNotFound();
                        }
                        else
                        {
                            Session["Childid"] = id;
                            return View();
                        }

                    }


                }
            }
        }
        [HttpPost]
        public ActionResult Add_Note(Note note)
        {
            var child = db.child.Find(Session["Childid"]);
            var min = db.ministry.Find(child.Min_id);
            var home = db.home.Find(child.Home_ID);
            var admin = db.admin.Find(Session["ID"]);
            try
            {
                if (child != null && admin!=null)
                {
                    if (ModelState.IsValid)
                    {
                        var parent = db.parent.Find(child.Par_ID);
                        if (parent == null)
                        {
                            note.child = child;
                            note.Child_id = child.Chi_Id;
                            note.home = home;
                            note.Home_ID = home.Home_ID;
                            note.min = min;
                            note.Min_ID = min.Min_Id;
                            note.admin = admin;
                            note.Note_Date = DateTime.Now;
                            note.Admin_id = admin.Adm_ID;
                            note.Posted = "Admin";
                            
                            db.note.Add(note);
                            
                            child.note = new List<Note>();
                            child.note.Add(note);
                            db.Entry(child).State = EntityState.Modified;
                            db.SaveChanges();

                            return RedirectToAction("Note_List", "Admin",new { id = child.Chi_Id });
                            
                        }
                        else
                        {
                            note.child = child;
                            note.admin = admin;
                            note.parent = child.parent;
                            note.home = home;
                            note.Home_ID = home.Home_ID;
                            note.min = min;
                            note.Min_ID = min.Min_Id;

                            note.Parent_Id = child.Par_ID;
                            

                            note.Note_Date = DateTime.Now;


                            note.Admin_id = admin.Adm_ID;
                            note.Child_id = child.Chi_Id;
                            note.Posted = "Admin";
                            db.note.Add(note);
                            db.SaveChanges();
                            child.note = new List<Note>();
                            child.note.Add(note);
                            db.Entry(child).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Note_List", "Admin",new { id = child.Chi_Id });
                        }
                        
                    }
                    return View();
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

            return View();

        }

        [HttpGet]
        public ActionResult Note_List(int? id,int?page)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "Home");
            else
            {
                if (id != null)
                {
                    
                    var obj = db.note.Where(x =>x.Child_id == id).ToList().ToPagedList(page?? 1,6);


                    return View(obj);
                }
                else
                    return HttpNotFound();

            }
        }

        [HttpGet]
        public ActionResult Edit_Note(int ? id)
        {
            if(Session["ID"]!=null)
            {
                if(id!=null)
                {
                    Session["NoteID"] = id;

                    return View();
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

           

        }
        [HttpPost]
        public ActionResult Edit_Note(Note note)
        {

           if(note!=null)
            {
                if(ModelState.IsValid)
                {
                    using (Context db = new Context())
                    {
                        var oldnote = db.note.Find(Session["NoteID"]);
                        oldnote.Note_title = note.Note_title;
                        oldnote.Note_Date = DateTime.Now;
                        oldnote.note_content = note.note_content;
                        db.Entry(oldnote).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Note_List","Admin",new { id = oldnote.Child_id });
                    }
                }
                else
                {
                    return View();
                }
            }
           else
            {
                return HttpNotFound();
            }

        }
        /// <summary>
        /// Sccess Edit Note
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public ActionResult All_Note_List(int?page)
        {
            if(Session["ID"]!=null)
            {
                using (Context db = new Context())
                {
                    int id = int.Parse(Session["ID"].ToString());
                    var admin = db.admin.Include("Home").Where(x => x.Adm_ID == id).FirstOrDefault();
                    var note = db.note.Include("child").Where(x => x.home.Home_ID == admin.home.Home_ID).ToList().ToPagedList(page ?? 1, 6);
                    return View(note);
                }
            }
            return RedirectToAction("Login", "Home");
        }
        /// <summary>
        /// Sccess All Note
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 


       public ActionResult moreNote_Info(int ? id)
        {
            if(Session["ID"]!=null)
            {
                if(id!=null)
                {
                    using (Context db = new Context())
                    {
                        var note = db.note.Include("child").Where(x => x.Note_Id == id).FirstOrDefault();
                        return View(note);
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Remove_Note(int ?id)
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    var note = db.note.Find(id);
                    var child = db.child.Find(note.Child_id);
                    var admin = db.admin.Find(note.Admin_id);
                    db.note.Remove(db.note.FirstOrDefault(x => x.Note_Id == id));
                    db.SaveChanges();
                    child.note = new List<Note>();
                    child.note.Remove(note);
                    admin.note = new List<Note>();
                    admin.note.Remove(note);
                        

                    return RedirectToAction("Note_List", "Admin",new { id = child.Chi_Id });
                }
            }
        }

        
        public ActionResult Admin_Details( int? id)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Parent");
            }
            else if (id==null)
            {
                return HttpNotFound();
            }
            else {
                var admin = db.admin.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(admin);
                }



            }
           // return View();
        }

        //Show Parent Details
        [HttpGet]
        public ActionResult Parent_Details(int? id)
        {
            if (Session["ID"] != null)
            {
                if (id == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    using (Context db = new Context())
                    {
                        var parent = db.parent.Find(id);
                        return View(parent);
                    }


                }
               

            
            }
            return RedirectToAction("Login","Home");
        }
    }






}

