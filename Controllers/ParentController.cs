using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using adminSection.Models;
using PagedList;

namespace adminSection.Controllers
{
    public class ParentController : Controller
    {
        // GET: Parent
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(Parent parent)
        {
            if (parent == null)
            {
                return HttpNotFound();
            }
            else
            {
                using (Context db = new Context())
                {

                    var par = db.parent.SingleOrDefault(x => x.Parent_Email == parent.Parent_Email && x.Parent_Password == parent.Parent_Password);
                    
                    if (par == null)
                    {
                        parent.loginErrorMessage = "Email or Password is Wrong";
                        return View();
                    }
                    else
                    {
                        Session["ID"] = par.Parent_Id;
                        Session["Name"] = par.Parent_Full_Name;
                        Session["ImagePath"] = par.Par_Image_Path;
                        Session["UserType"] = "Parent";
                        return RedirectToAction("Home", "Parent");
                    }
                }
            }


        }


        [HttpGet]
        public ActionResult Rigester()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Rigester(Parent parent, int id)
        {



            if (ModelState.IsValid)
            {

                using (Context db = new Context())
                {
                    try
                    {
                        // var child1 = db.child.Where(x => x.Chi_Id == id).FirstOrDefault();
                        var child1 = db.child.Find(id);
                        parent.Par_User_Type = "Parent";
                        parent.Par_Image_Path = "C:/Users/ABEDALRAHMAN/Downloads/download.png";
                        parent.child = new List<Child>();
                        parent.child.Add(child1);
                        db.parent.Add(parent);
                        db.SaveChanges();

                        var p = db.parent.Where(x => x.Parent_Id == parent.Parent_Id).FirstOrDefault();
                        child1.parent = p;
                        child1.Par_ID = p.Parent_Id;
                        db.Entry(child1).State = EntityState.Modified;
                        db.SaveChanges();




                        return RedirectToAction("ShowParent", "Admin");
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

                }
            }




            return View();

        }

        public void RemoveRecord(Child child, int id, Parent p)
        {
            Context db = new Context();
            var child1 = db.child.Where(x => x.Chi_Id == id).FirstOrDefault();
            child1.Chi_First_Name = child.Chi_First_Name;
            child1.Chi_Last_Name = child.Chi_Last_Name;
            child1.Chi_parent_status = child.Chi_parent_status;
            child1.Chi_School = child.Chi_School;
            child1.Chi_Image_Path = child.Chi_Image_Path;
            child1.Chi_Id = child.Chi_Id;
            child1.Adm_ID = child.Adm_ID;
            child1.Age = child.Age;
            child1.parent = p;

            child1.Gender = child.Gender;

            ///Remove Record
            var id_Add = db.child.Find(id);
            if (id_Add == null)
            {
                HttpNotFound();

            }
            else
            {
                db.child.Remove(db.child.FirstOrDefault(x => x.Chi_Id == id));
                db.SaveChanges();
            }

            db.child.Add(child1);
            db.SaveChanges();

        }
       
        [HttpGet]

        public ActionResult Details(int? id)
        {

            if(Session["ID"]!=null)
            {
                if(Session["UserType"].ToString()=="Parent")
                {
                    if(id!=null)
                    {
                        using (Context db = new Context())
                        {
                            var child = db.child.Include("admin").Where(x => x.Chi_Id == id).FirstOrDefault();
                            return View(child);
                        }
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }

            //if (Session["ID"] == null)
            //{
            //    return RedirectToAction("Login", "Parent");
            //}
            //else
            //{
            //    using (Context db = new Context())
            //    {


            //        var child = db.child.SingleOrDefault(x => x.Chi_Id == id);
            //        Session["AdminName"] = child.admin.Adm_Name;
            //        Session["ChildId"] = id;
            //        if (child == null)
            //        {
            //            return HttpNotFound();
            //        }
            //        else
            //        {
            //            return View(child);

            //        }
            //    }
            //}


        }

        public ActionResult Parent_Details()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Child_List(int? id,int? page)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                using (Context db = new Context())
                {
                    Session["Parent_Id"] = id;
                    int par_id = int.Parse(Session["ID"].ToString());
                    var child = db.child.Where(x => x.parent.Parent_Id == par_id ).ToList().ToPagedList(page ?? 1, 3);
                    return View(child);
                }
            }

        }




        public ActionResult Home(int ?page)
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Parent");
            }
            int id = int.Parse(Session["ID"].ToString());
            if (id == 0)
            {
                return HttpNotFound();
            }
            else
            {
                using (Context db = new Context())
                {
                    var child_list = db.child.ToList().ToPagedList(page ?? 1, 6);
                    // the each page have 3 row and defult page number when page=null one
                    return View(child_list);

                }
            }
        }

        //Note Section
        [HttpGet]
        public ActionResult Note_List(int? id, int? page)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login", "Parent");
            else
            {
                if (id != null)
                {
                    using (Context db = new Context())
                    {

                        var obj = db.note.Include(c => c.child).ToList();
                        var note = obj.Where(x => x.Child_id == id).ToList().ToPagedList(page ?? 1, 6); ;


                        return View(note);
                    }
                }
                else
                    return HttpNotFound();

            }
        }

        [HttpGet]
        public ActionResult Add_Note(int? id)
        {
            if(Session["ID"]!=null)
            {
                if (Session["UserType"].ToString()=="Parent")
                {
                    if(id!=null)
                    {

                        Session["ChildId"] = id;
                        return View();
                    }
                    else
                    {
                        return HttpNotFound();
                    }

                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }


        [HttpPost]
        public ActionResult Add_Note(Note note)
        {
            if (note != null)
            {
               if(ModelState.IsValid)
                {
                    using (Context db = new Context())
                    {

                        var child = db.child.Find(Session["ChildId"]);
                        var parent = db.parent.Find(Session["ID"]);
                        var home = db.home.Find(child.Home_ID);
                        var admin = db.admin.Find(child.Adm_ID);
                        note.child = child;
                        note.Child_id = child.Chi_Id;
                        note.home = home;
                        note.Home_ID = home.Home_ID;
                        note.min = child.min;
                        note.Min_ID = child.Min_id;
                        note.admin = admin;
                        note.Admin_id = int.Parse(child.Adm_ID.ToString());
                        note.Note_Date = DateTime.Now;
                        note.Admin_id = admin.Adm_ID;
                        note.Posted = "Parant";

                        db.note.Add(note);

                        child.note = new List<Note>();
                        child.note.Add(note);
                        db.Entry(child).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Note_List","Parent",new { id = child.Chi_Id });



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
        [HttpGet]
        public ActionResult Edit_Note(int ?id)
        {
            if (Session["ID"] != null)
            {
                if (Session["UserType"].ToString() == "Parent")
                {
                    if (id != null)
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
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        public ActionResult Edit_Note(Note note)
        {
            if(note !=null)
            {
                using (Context db = new Context())
                {
                    int id = int.Parse(Session["NoteID"].ToString());
                    var oldnote = db.note.Find(id);
                    oldnote.Note_title = note.Note_title;
                    oldnote.note_content = note.note_content;
                    oldnote.Note_Date = DateTime.Now;

                    db.Entry(oldnote).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Note_List", "Parent", new { id = oldnote.Child_id });

                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult Remove_Note(int? id)
        {
            if(Session["ID"]!=null)
            {
                if(Session["UserType"].ToString()=="Parent")
                {
                    if(id!=null)
                    {
                        using (Context db = new Context())
                        {
                            var note = db.note.Find(id);
                            var child = db.child.Find(note.child.Chi_Id);
                            var admin = db.admin.Find(note.admin.Adm_ID);
                            var parent = db.parent.Find(Session["ID"]);
                            db.note.Remove(note);
                            child.note = new List<Note>();
                            admin.note = new List<Note>();
                            parent.note = new List<Note>();

                            child.note.Remove(note);
                            admin.note.Remove(note);
                            parent.note.Remove(note);
                            db.Entry(child).State = EntityState.Modified;
                            db.Entry(admin).State = EntityState.Modified;
                            db.Entry(parent).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Note_List","Parent",new { id = note.Child_id });
                        }
                                
                    }
                    else
                    {
                        return HttpNotFound();
                    }

                    
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        //edit Parent Profile
        [HttpGet]
        public ActionResult Edit_Profile()
        {
            if (Session["ID"] != null)
            {
                if (Session["UserType"].ToString() == "Parent")
                {
                    
                        return View();
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }


        }
        [HttpPost]
        public ActionResult Edit_Profile(Parent par)
        {
            if(par!=null)
            {
                
                    string fileName = Path.GetFileNameWithoutExtension(par.ImageFile.FileName);
                    string exe = Path.GetExtension(par.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + exe;
                    par.Par_Image_Path = "~/Image/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                    par.ImageFile.SaveAs(fileName);
                    using (Context db = new Context())
                    {
                        var parent = db.parent.Find(Session["ID"]);
                        parent.Parent_Full_Name = par.Parent_Full_Name;
                        parent.Parent_Email = par.Parent_Email;
                        parent.Parent_Password = par.Parent_Password;
                        parent.Parent_Location = par.Parent_Location;
                        parent.Parent_Phone = par.Parent_Phone;
                        parent.Parent_status = par.Parent_status;
                        parent.Par_Image_Path = par.Par_Image_Path;
                        db.Entry(parent).State = EntityState.Modified;
                        db.SaveChanges();
                        Session["ImagePath"] = parent.Par_Image_Path;
                        return RedirectToAction("Home");
                    }

               
            }
            return View();
        }

        // show admin
        

    }
}