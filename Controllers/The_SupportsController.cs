using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using adminSection.Models;
using PagedList;

namespace adminSection.Controllers
{
    public class The_SupportsController : Controller
    {
        // GET: The_Supports
        [HttpGet]
        public ActionResult Rigester()
        {
            if(Session["ID"]==null)
            {
                return RedirectToAction("Login", "Minsitry");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Rigester(The_Supports supports)
        {
            if (supports == null)
            {
                return HttpNotFound();
            }
            else
            {
                string fileName = Path.GetFileNameWithoutExtension(supports.The_Supports_ImageFile.FileName);
                string exe = Path.GetExtension(supports.The_Supports_ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + exe;
                supports.Supports_Image_Path = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                supports.The_Supports_ImageFile.SaveAs(fileName);
                using (Context db = new Context())
                {
                    var voul = db.voulunter.Find(Session["VolID"]);
                    var ministry = db.ministry.Find(Session["ID"]);
                    var visi = db.visitor.Find(voul.Visitro_ID);

                    supports.User_Type = "Supports";
                    supports.visitor = visi;
                    supports.Supports_Id = visi.Visitro_ID;
                    supports.Num_Of_Child = 0;
                    supports.Accept_Voulnteer = 1;
                    visi.Support_Id = supports.Supports_Id;
                    visi.support = supports;
                    db.Entry(visi).State = System.Data.Entity.EntityState.Modified;


                    db.SaveChanges();

                    return RedirectToAction("Home", "Minsitry");
                }
            }
            
        }
        [HttpGet]
        public ActionResult Login()
        {
            if(Session["ID"]==null)
            {
                return View();
            }
            else
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        public ActionResult Login(The_Supports spo)
        {
            if(spo==null)
            {
                return HttpNotFound();
            }
            else
            {
                using (Context db = new Context())
                {
                    var support = db.sup.Where(x => x.Supports_Email == spo.Supports_Email && x.Supports_Password == spo.Supports_Password).FirstOrDefault();
                    if(support==null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        if (support.User_Type == "Supports")
                        {
                            Session["ID"] = support.Supports_Id;
                            Session["Name"] = support.Supports_FullName;
                            Session["ImagePath"] = support.Supports_Image_Path;
                            Session["UserType"] = "Supports";
                            return RedirectToAction("Home", "The_Supports");
                        }
                        else
                        {
                            return RedirectToAction("Login", "Home");
                        }
                    }

                }
            }

        }
        
        public ActionResult Home(int ?page)
        {
            if(Session["ID"]!=null)
            {
                using (Context db = new Context())
                {
                 
                    var The_Supports = db.sup.Find(Session["ID"]);
                    if(The_Supports!=null)
                    {
                        if(The_Supports.User_Type== "Supports")
                        {
                            int id = Convert.ToInt16( Session["ID"].ToString());
                            var Child = db.child.Include("home").Where(x => x.supports.Supports_Id == id).ToList().ToPagedList(page ?? 1, 6);
                            

                          
                            return View(Child);
                        }
                    }


                }
            }


            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult Add_volunteer()
       {
            if(Session["ID"]!=null)
            {
                if (Session["UserType"].ToString() == "Supports")
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
        public ActionResult Add_volunteer( volunteer vol)
        {
            if(vol!=null)
            {
                using (Context db = new Context())
                {
                    var Supports = db.sup.Find(Session["ID"]);
                    var visitor = db.visitor.Find(Session["ID"]);
                    vol.Volunteer_status = "under consideration";
                    vol.support = Supports;
                    
                    vol.Visitro_ID = Supports.Supports_Id;
                    vol.vistor = visitor;
                    db.voulunter.Add(vol);
                    db.SaveChanges();
                    Supports.vol = new List<volunteer>();
                    Supports.vol.Add(vol);
                    visitor.volunterr = new List<volunteer>();
                    visitor.volunterr.Add(vol);
                    db.Entry(Supports).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(visitor).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("volunteerList");
                }

            }
            else
            {
                return HttpNotFound();
            }
            
        }
        [HttpGet]
        public ActionResult volunteerList(int ?page)
        {
            if (Session["ID"] != null)
            {
                if(Session["UserType"].ToString()=="Supports")
                {
                    using (Context db = new Context())
                    {
                        int id = int.Parse(Session["ID"].ToString());
                        var vol = db.voulunter.Where(x => x.support.Supports_Id == id || x.Visitro_ID == id).ToList().ToPagedList(page ?? 1, 3);

                        return View(vol);
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




        [HttpGet]
        public ActionResult all_Home_Care(int? page)
        {
            if (Session["ID"] != null)
            {

                using (Context db = new Context())
                {
                    var home = db.home.ToList().ToPagedList(page ?? 1, 6);
                    return View(home);
                }


            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        public ActionResult More(int? id)
        {
            if (Session["ID"] != null)
            {
                if (id != null)
                {
                    using (Context db = new Context())
                    {
                        var Home = db.home.Find(id);
                        if(Home!=null)
                        {
                            return View(Home);
                        }
                        else
                        {
                            return HttpNotFound();
                        }
                    }
                }
                return HttpNotFound();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        
        public ActionResult Child_List(int? page, int ?id)
        {
            if (Session["ID"] != null)
            {
                if (id!=null)
                {
                    using (Context db = new Context())
                    {
                        var home = db.home.Find(id);
                        //var child=db.home.Include("parent").Include("supports").ToList().ToPagedList(page ?? 1, 6);
                        var child = home.child.ToList().ToPagedList(page?? 1,6);
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
        public ActionResult Child_Details(int? id)
        {
            if (Session["ID"] != null)
            {
                if (id != null)
                {
                    using (Context db = new Context())
                    {
                        var child = db.child.Include("supports").Where(x => x.Chi_Id == id).FirstOrDefault();
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
        public ActionResult Suporrting(int? id)
        {
            if (Session["ID"] != null)
            {
                if (id!=null)
                {
                    using (Context db = new Context())
                    {
                        var child = db.child.Find(id);
                        var supports = db.sup.Find(Session["ID"]);
                        if (child != null && supports != null)
                        {
                            if (supports.Num_Of_Child < supports.Accept_Voulnteer)
                            {
                                supports.child = new List<Child>();
                                supports.child.Add(child);
                                supports.Num_Of_Child++;
                                child.supports = supports;
                                child.The_SupportsID = supports.Supports_Id;
                                db.Entry(supports).State = System.Data.Entity.EntityState.Modified;
                                db.Entry(child).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                return RedirectToAction("Home");
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
                }
                else
                {
                    return HttpNotFound();
                }
            }
            
            return RedirectToAction("Login");
         }

        public ActionResult More_Child_Details(int? id)
        {
            if (Session["ID"] != null)
            {
                if (id!=null)
                {
                    using (Context db = new Context())
                    {

                        var child = db.child.Include("Home").FirstOrDefault();
                        
                        



                        return View(child);
                    }
                }
                return HttpNotFound();
            }
            else
            return RedirectToAction("Login");
        }
        public ActionResult Show_Note(int? id, int? page)
        {

           if(Session["ID"]!=null)
            {
                if(id!=null)
                {
                    using (Context db = new Context())
                    {
                        var note = db.note.Include("child").Include("admin").ToList();
                        var note1 = note.Where(x => x.Child_id == id).ToList().ToPagedList(page ?? 1, 6);
                        
                        return View(note1);
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

        [HttpGet]
        public ActionResult Add_Note(int ?id)
        {
            if(Session["ID"]!=null)
            {
                if(id!=null)
                {
                    using (Context db = new Context())
                    {
                        var child = db.child.Find(id);
                        if(child!=null)
                        {
                            Session["ChildID"] = child.Chi_Id;
                            return View();
                        }
                        else
                        {
                            return HttpNotFound();
                        }
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

        [HttpPost]
        public ActionResult Add_Note(Note note)
        {
            if(note!=null)
            {
                if(ModelState.IsValid)
                {
                    using (Context db = new Context())
                    {
                        try
                        {
                            var child = db.child.Find(Session["ChildID"]);
                            
                            if (child == null)
                            {
                                return HttpNotFound();
                            }
                            else
                            {
                                if (child.parent==null )
                                {
                                    var admin = db.admin.Find(child.Adm_ID);
                                    var min = db.ministry.Find(child.Min_id);
                                    var support = db.sup.Find(Session["ID"]);
                                    if (min != null && admin != null && min != null)
                                    {
                                        note.Posted = "Supports";
                                        note.child = child;
                                        int ChildID = int.Parse(Session["ChildID"].ToString());
                                        note.Child_id = ChildID;
                                        note.Note_Date = DateTime.Now;
                                        var note1 = note;
                                        db.SaveChanges();
                                        child.note = new List<Note>();
                                        child.note.Add(note);
                                        db.Entry(child).State = System.Data.Entity.EntityState.Modified;
                                        //child
                                        admin.note = new List<Note>();
                                        admin.note.Add(note);
                                        db.Entry(admin).State = System.Data.Entity.EntityState.Modified;
                                        //admin
                                        min.note = new List<Note>();
                                        child.note.Add(note);
                                        db.Entry(min).State = System.Data.Entity.EntityState.Modified;
                                        //min
                                        support.note = new List<Note>();
                                        support.note.Add(note);
                                        db.Entry(support).State = System.Data.Entity.EntityState.Modified;
                                        //suuport
                                        db.SaveChanges();
                                        return RedirectToAction("Show_Note", "The_Supports", new { id = child.Chi_Id });
                                    }
                                    else
                                    {
                                        return HttpNotFound();
                                    }
                                }
                                
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
                    }
                    return View();
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
        public ActionResult Edit_Note(int ? id)
        {
            if(Session["ID"]!=null)
            {
                if(id!=null)
                {
                    using (Context db = new Context())
                    {
                        var note = db.note.Find(id);
                        Session["NoteID"] = id;
                        return View();
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return RedirectToAction("Login");

        } 
        [HttpPost]
        public ActionResult Edit_Note(Note newnote)
        {
            if(newnote!=null)
            {
                using (Context db = new Context())
                {
                   if(ModelState.IsValid)
                    {
                        var oldnote = db.note.Find(Session["NoteID"]);
                        oldnote.note_content = newnote.note_content;
                        oldnote.Note_Date = DateTime.Now;
                        oldnote.Note_title = newnote.Note_title;
                        db.Entry(oldnote).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Show_Note", "The_Supports", new { id = oldnote.Child_id });
                    }
                }
            }
            return View();
        }
        public ActionResult Remove_Note(int? id)
        {
            if(Session["ID"]!=null)
            {
                if(id!=null)
                {
                    using (Context db = new Context())
                    {
                        var note = db.note.Find(id);
                        var child = db.child.Find(note.Child_id);
                        var admin = db.admin.Find(note.Admin_id);
                        var Support = db.sup.Find(Session["ID"]);
                        var min = db.ministry.Find(note.Min_ID);
                        db.note.Remove(db.note.FirstOrDefault(x => x.Note_Id == id));
                        db.SaveChanges();
                        
                        return RedirectToAction("Show_Note", "The_Supports",new { id = note.Child_id });


                                                    
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return RedirectToAction("Login");
        }

    }
}