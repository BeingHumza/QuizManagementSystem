using Microsoft.VisualStudio.Services.WebApi;
using QuizMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace QuizMS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home


        DBQuizEntities1 db = new DBQuizEntities1();
        // GET: Home

        [HttpGet]
        public ActionResult tlogin()
        {
            return View();
        }

        [HttpPost]

        public ActionResult tlogin(tbl_admin a)
        {
            tbl_admin ad = db.tbl_admin.Where(x => x.ad_name == a.ad_name && x.ad_password == a.ad_password).SingleOrDefault();
            if (ad != null)
            {
                Session["ad_id"] = ad.ad_id;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.msg = "Invalid Username or Password";

            }
            return View();
        }




        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index");
        }

        // GET: tbl_category/Edit/5
        public ActionResult Edit_Category(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category tbl_category = db.tbl_category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_fk_adid = new SelectList(db.tbl_admin, "ad_id", "ad_name", tbl_category.cat_fk_adid);
            return View(tbl_category);
        }

        // POST: tbl_category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Category([Bind(Include = "cat_id,cat_name,cat_fk_adid,cat_encryptedstring")] tbl_category tbl_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Add_Category");
            }
            ViewBag.cat_fk_adid = new SelectList(db.tbl_admin, "ad_id", "ad_name", tbl_category.cat_fk_adid);
            return View(tbl_category);
        }

        // GET: tbl_category/Delete/5
        public ActionResult Delete_Category(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category tbl_category = db.tbl_category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_category);
        }

        // POST: tbl_category/Delete/5
        [HttpPost, ActionName("Delete_Category")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Category(int id)
        {
            tbl_category tbl_category = db.tbl_category.Find(id);
            db.tbl_category.Remove(tbl_category);
            db.SaveChanges();
            return RedirectToAction("Add_Category");
        }



        // GET: tbl_chapter/Edit/5
        public ActionResult Edit_Chapter(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chapter tbl_chapter = db.chapters.Find(id);
            if (tbl_chapter == null)
            {
                return HttpNotFound();
            }
            ViewBag.ad_id = new SelectList(db.tbl_admin, "ad_id", "ad_name", tbl_chapter.ad_id);
            ViewBag.cat_id = new SelectList(db.tbl_category, "cat_id", "cat_name", tbl_chapter.cat_id);
            return View(tbl_chapter);
        }

        // POST: tbl_chapter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Chapter([Bind(Include = "ch_id,ch_name,ad_id,cat_id")] chapter chapter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chapter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Add_Category");
            }
            ViewBag.ad_id = new SelectList(db.tbl_admin, "ad_id", "ad_name", chapter.ad_id);
            ViewBag.cat_id = new SelectList(db.tbl_category, "cat_id", "cat_name", chapter.cat_id);
            return View(chapter);
        }

        // GET: tbl_chapter/Delete/5
        public ActionResult Delete_Chapter(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chapter tbl_chapter = db.chapters.Find(id);
            if (tbl_chapter == null)
            {
                return HttpNotFound();
            }
            return View(tbl_chapter);
        }

        // POST: tbl_chapter/Delete/5
        [HttpPost, ActionName("Delete_Chapter")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Chapter(int id)
        {
            chapter tbl_chapter = db.chapters.Find(id);
            db.chapters.Remove(tbl_chapter);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        // GET: tbl_students/Delete/5
        public ActionResult Delete_Student(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_students tbl_students = db.tbl_students.Find(id);
            if (tbl_students == null)
            {
                return HttpNotFound();
            }
            return View(tbl_students);
        }

        // POST: tbl_students/Delete/5
        [HttpPost, ActionName("Delete_Student")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_students tbl_students = db.tbl_students.Find(id);
            db.tbl_students.Remove(tbl_students);
            db.SaveChanges();
            return RedirectToAction("Student_Record");
        }









        public ActionResult Create_Question()
        {
            ViewBag.ch_id = new SelectList(db.chapters, "ch_id", "ch_name");
            ViewBag.cat_id = new SelectList(db.tbl_category, "cat_id", "cat_name");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_Question([Bind(Include = "question_id,q_txt,opa,opb,opc,opd,cop,ch_id,q_fk_ct_id")] tbl_questions tbl_questions)
        {
            if (ModelState.IsValid)
            {
                db.tbl_questions.Add(tbl_questions);
                db.SaveChanges();
                return RedirectToAction("Add_Category");
            }

            ViewBag.ch_id = new SelectList(db.chapters, "ch_id", "ch_name", tbl_questions.ch_id);
            ViewBag.q_fk_ct_id = new SelectList(db.tbl_category, "cat_id", "cat_name", tbl_questions.q_fk_ct_id);
            return View(tbl_questions);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit_Question(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_questions tbl_questions = db.tbl_questions.Find(id);
            if (tbl_questions == null)
            {
                return HttpNotFound();
            }
            ViewBag.ch_id = new SelectList(db.chapters, "ch_id", "ch_name", tbl_questions.ch_id);
            ViewBag.q_fk_ct_id = new SelectList(db.tbl_category, "cat_id", "cat_name", tbl_questions.q_fk_ct_id);
            return View(tbl_questions);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Question([Bind(Include = "question_id,q_txt,opa,opb,opc,opd,cop,ch_id,q_fk_ct_id")] tbl_questions tbl_questions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_questions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Add_Category");
            }
            ViewBag.ch_id = new SelectList(db.chapters, "ch_id", "ch_name", tbl_questions.ch_id);
            ViewBag.q_fk_ct_id = new SelectList(db.tbl_category, "cat_id", "cat_name", tbl_questions.q_fk_ct_id);
            return View(tbl_questions);
        }


        // GET: Questions/Delete/5
        public ActionResult Delete_Question(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_questions tbl_questions = db.tbl_questions.Find(id);
            if (tbl_questions == null)
            {
                return HttpNotFound();
            }
            return View(tbl_questions);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete_Question")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Question(int id)
        {
            tbl_questions tbl_questions = db.tbl_questions.Find(id);
            db.tbl_questions.Remove(tbl_questions);
            db.SaveChanges();
            return RedirectToAction("Add_Category");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }












        [HttpGet]
        public ActionResult register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult register(tbl_students svw)
        {
            tbl_students s = new tbl_students();
            try
            {

                s.std_name = svw.std_name;
                s.std_password = svw.std_password;

                db.tbl_students.Add(s);
                db.SaveChanges();
                return RedirectToAction("slogin");
            }
            catch (Exception)
            {
                ViewBag.msg = "Data could not be inserted.....";

            }


            return View();
        }

        public ActionResult slogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult slogin(tbl_students s)
        {
            tbl_students std = db.tbl_students.Where(x => x.std_name == s.std_name && x.std_password == s.std_password).SingleOrDefault();
            tbl_students std1 = db.tbl_students.Where(x => x.std_id == s.std_id).SingleOrDefault();
            if (std1 != null)
            {
                Session["std_id"] = std1.std_id;
                return RedirectToAction("Student_Record");
            }

            if (std == null)
            {
                ViewBag.msg = "Invalid or Password";

            }
            else
            {
                Session["std_id"] = std.std_id;
                return RedirectToAction("selectcategory");
            }
            return View();
        }

        public ActionResult Student_Record()
        {
            tbl_students std = new tbl_students();
            //if (Session["a_id"] == null)
            //{
            //    return RedirectToAction("Index");
            //}
            List<tbl_students> list = db.tbl_students.ToList();
            foreach (var item1 in list)
            {

                // Session["trainee_id"] = trainee;
                // int adid = Convert.ToInt32(Session["trainee_id"].ToString());

                List<tbl_students> li = db.tbl_students.Where(x => x.std_id == item1.std_id).OrderByDescending(x => x.std_id).ToList();
                List<tbl_students> li1 = db.tbl_students.OrderByDescending(x => std.std_id).ToList();

                ViewData["list"] = li1;


            }
            return View();
        }




        public ActionResult ViewAllQuestions(int? id, int? page)
        {

            if (Session["ad_id"] == null)
            {
                return RedirectToAction("tlogin");
            }
            if (id == null)
            {
                return RedirectToAction("Dashboard");
            }

            int pagesize = 8, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            List<tbl_questions> li = db.tbl_questions.Where(x => x.ch_id == id).ToList();
            PagedList.IPagedList<tbl_questions> stu = li.ToPagedList(pageindex, pagesize);


            return View(stu);
        }

        public ActionResult ViewAllChapters(int? id, int? page)
        {

            if (Session["ad_id"] == null)
            {
                return RedirectToAction("tlogin");
            }
            if (id == null)
            {
                return RedirectToAction("Dashboard");
            }

            int pagesize = 1, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            List<chapter> li = db.chapters.Where(x => x.cat_id == id).ToList();
            PagedList.IPagedList<chapter> stu = li.ToPagedList(pageindex, pagesize);


            return View(stu);
        }

        [HttpGet]
        public ActionResult selectcategory(int? id, int? page)
        {
            if (Session["std_id"] == null)
            {
                return RedirectToAction("slogin");
            }
            tbl_category c = new tbl_category();
            List<tbl_category> li1 = db.tbl_category.OrderByDescending(x => c.cat_id).ToList();
            //  ViewData["list"] = li1;
            int pagesize = 1, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_adid == id).ToList();
            PagedList.IPagedList<tbl_category> stu = li1.ToPagedList(pageindex, pagesize);


            return View(stu);

        }
        [HttpPost]
        public ActionResult selectcategory(tbl_category c, string room)
        {

            List<tbl_category> list1 = db.tbl_category.ToList();
            List<tbl_category> list = db.tbl_category.ToList();

            //foreach (var item1 in list1)
            //{
            //    List<tbl_category> li1 = db.tbl_category.OrderByDescending(x => c.cat_id).ToList();

            //    ViewData["list"] = li1;

            //}

            foreach (var item in list)
            {
                if (item.cat_name == room)
                {
                    List<tbl_category> li2 = db.tbl_category.Where(x => x.cat_name == item.cat_name).ToList();



                    foreach (var item2 in li2)
                    {


                        Session["cat_name"] = item.cat_name;




                        break;
                    }



                    List<chapter> li = db.chapters.Where(x => x.cat_id == item.cat_id).ToList();
                    TempData["list"] = li;
                    //TempData["chapter_id"] = item.cat_id;
                    return RedirectToAction("studentexam");

                }
                else
                {
                    ViewBag.error = "No Chapter Found.......";
                }





            }

            return View();
        }








        [HttpGet]
        public ActionResult studentexam()
        {
            string room1 = null;

            if (Session["std_id"] == null)
            {
                return RedirectToAction("slogin");
            }
            //   tbl_chapter c = new tbl_chapter();
            ////   int adid = Convert.ToInt32(Session["ad_id"].ToString());
            //   List<tbl_chapter> li1 = db.tbl_chapter.OrderByDescending(x => c.cha_id).ToList();

            //   ViewData["list"] = li1;
            List<tbl_category> list = db.tbl_category.ToList();
            foreach (var item in list)
            {
                //if (item.cat_name == room1)
                //{
                List<chapter> li = db.chapters.Where(x => x.cat_id == item.cat_id).ToList();
                ViewData["list"] = li;
                //TempData["chapter_id"] = item.cat_id;


                //}
                //else
                //{
                //    ViewBag.error = "No Chapter Found.......";
                //}
            }

            return View();
        }
        [HttpPost]
        public ActionResult studentexam(string room, chapter c, string room1)
        {

            List<chapter> list = db.chapters.ToList();
            List<tbl_category> list4 = db.tbl_category.ToList();
            tbl_category cat1 = new tbl_category();
            List<chapter> list1 = db.chapters.ToList();

            List<tbl_category> list5 = db.tbl_category.ToList();
            foreach (var item in list5)
            {
                //if (item.cat_name == room1)
                //{
                List<chapter> li = db.chapters.Where(x => x.cat_id == item.cat_id).ToList();
                ViewData["list"] = li;
                //TempData["chapter_id"] = item.cat_id;


                //}
                //else
                //{
                //    ViewBag.error = "No Chapter Found.......";
                //}

            }




            //foreach (var item1 in list1)
            //{
            //    // List<tbl_category> li1 = db.tbl_category.Where(x => x.cat_id == item1.cat_id).ToList();
            //    //ViewData["list"] = li1;

            //    // int adid = Convert.ToInt32(Session["ad_id"].ToString());
            //    List<tbl_category> li1 = db.tbl_category.OrderByDescending(x => c.cha_id).ToList();

            //    ViewData["list"] = li1;

            //}
            //foreach (var item3 in list1)
            //{

            //                List<tbl_category> li2 = db.tbl_category.Where(x => room == item4.cat_encryptedstring).ToList();

            //    foreach (var item2 in li2)
            //    {
            //        //   string t = Convert.ToInt32(room).ToString;


            //        Session["cat_encryptedstring"] = item2.cat_encryptedstring;
            //        break;
            //    }
            //    break;
            //}
            foreach (var item in list)
            {

                if (item.ch_name == room)
                {
                    //tbl_category s5 = new tbl_category();
                    // List<tbl_category> li2 = db.tbl_category.Where(x => s5.cat_name == item.cat_name).ToList();
                    List<chapter> li2 = db.chapters.Where(x => x.ch_name == item.ch_name).ToList();

                    //  List<tbl_questions> li3 = db.tbl_questions.Where(x => x.q_fk_ct_id == item.cat_id).ToList();
                    //  tbl_score s7 = new tbl_score();


                    foreach (var item2 in room)
                    {
                        //    //   string t = Convert.ToInt32(room).ToString;

                        Session["ch_name"] = item.ch_name;




                        break;
                    }


                    //       Session["cat_encryptedstring"] = item.cat_encryptedstring;

                    List<tbl_questions> li = db.tbl_questions.Where(x => x.ch_id == item.ch_id).ToList();
                    Queue<tbl_questions> queue = new Queue<tbl_questions>();
                    tbl_students s = new tbl_students();
                    li = li.OrderBy(x => Guid.NewGuid()).ToList();
                    foreach (var a in li)
                    {
                        queue.Enqueue(a);
                    }
                    TempData["e_id"] = item.ch_id;
                    TempData["std_id"] = s.std_id;
                    // TempData["std_name"] = s.std_name;
                    // TempData["exam_name"] = item.cat_name;
                    TempData["e_score"] = 0;
                    TempData["Questions"] = queue;
                    TempData["score"] = 0;
                    TempData.Keep();
                    return RedirectToAction("QuizStart");
                }
                else
                {
                    ViewBag.error = "No Room Found.......";
                }

            }
            //foreach (var item4 in list4)
            //{
            //    if (item4.cat_encryptedstring == room)
            //    {

            //        List<tbl_category> li2 = db.tbl_category.Where(x => room == item4.cat_encryptedstring).ToList();

            //        foreach (var item2 in li2)
            //        {
            //            //   string t = Convert.ToInt32(room).ToString;


            //            Session["cat_encryptedstring"] = item2.cat_encryptedstring;
            //            break;
            //        }
            //    }
            //    break;
            //}


            exam s1 = new exam();
            tbl_category cat = new tbl_category();

            //  db.tbl_score.Add(s1);
            //    db.SaveChanges();

            return View();
        }
        public ActionResult QuizStart()
        {
            if (Session["std_id"] == null)
            {
                return RedirectToAction("slogin");
            }
            tbl_questions q = null;
            if (TempData["Questions"] != null)
            {
                Queue<tbl_questions> qlist = (Queue<tbl_questions>)TempData["Questions"];
                if (qlist.Count > 0)
                {
                    q = qlist.Peek();
                    qlist.Dequeue();
                    TempData["Questions"] = qlist;
                    TempData.Keep();
                }
                else
                {
                    return RedirectToAction("End_Exam");
                }

            }
            else
            {
                return RedirectToAction("studentexam");
            }

            return View(q);

        }
        [HttpPost]
        public ActionResult QuizStart(tbl_questions q, tbl_students a, string room)
        {
            string correctans = null;
            if (q.opa != null)
            {
                correctans = "A";

            }
            else if (q.opb != null)
            {
                correctans = "B";
            }
            else if (q.opc != null)
            {
                correctans = "C";
            }
            else if (q.opd != null)
            {
                correctans = "D";
            }
            if (correctans.Equals(q.cop))
            {
                TempData["score"] = Convert.ToInt32(TempData["score"]) + 1;
                TempData["e_score"] = Convert.ToInt32(TempData["e_score"]) + 1;

            }

            // Session["std_id"] = 7;
            int stdid = Convert.ToInt32(Session["std_id"].ToString());
            List<tbl_students> li = db.tbl_students.Where(x => x.std_id == stdid).OrderByDescending(x => x.std_id).ToList();
            //  Session["cat_id"] = "1012";
            tbl_category f = new tbl_category();
            // int caw = Convert.ToInt32(Session["cat_name"].ToString());
            List<tbl_category> li1 = db.tbl_category.Where(x => x.cat_id == f.cat_id).OrderByDescending(x => x.cat_name).ToList();
            List<tbl_category> list4 = db.tbl_category.ToList();







            TempData.Keep();
            //tbl_score s = new tbl_score();

            //tbl_category cat = new tbl_category();
            //s.total_score = Convert.ToInt32(TempData["total_score"]);

            //s.std_id = Convert.ToInt32(Session["std_id"].ToString());
            //s.examname = Session["cat_encryptedstring"].ToString();

            //db.tbl_score.Add(s);
            //db.SaveChanges();




            return RedirectToAction("QuizStart");
        }
        public ActionResult End_Exam()
        {
            exam s = new exam();

            tbl_category cat = new tbl_category();
            s.e_score = Convert.ToInt32(TempData["e_score"]);

            s.std_id = Convert.ToInt32(Session["std_id"].ToString());
            s.e_name = Session["cat_name"].ToString();

            db.exams.Add(s);
            db.SaveChanges();
            return View();
        }
        [HttpPost]
        public ActionResult End_Exam(exam sa)
        {
            exam s = new exam();

            tbl_category cat = new tbl_category();
            s.e_score = Convert.ToInt32(TempData["e_score"]);

            s.std_id = Convert.ToInt32(Session["std_id"].ToString());
            s.e_name = Session["cat_name"].ToString();

            db.exams.Add(s);
            db.SaveChanges();
            return View();
        }





        public ActionResult Dashboard(int? page, int? id, exam c)
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Index");
            }
            List<exam> list = db.exams.ToList();
            foreach (var item in list)
            {


                exam r = new exam();
                tbl_students r1 = new tbl_students();


                List<exam> li = db.exams.Where(x => item.std_id == x.std_id).ToList();
                List<exam> li1 = db.exams.OrderByDescending(x => c.e_id).ToList();
                ViewData["list"] = li1;

                int pagesize = 10, pageindex = 1;
                pageindex = page.HasValue ? Convert.ToInt32(page) : 1;

                PagedList.IPagedList<exam> st = li1.ToPagedList(pageindex, pagesize);


                return View(st);










            }

            return RedirectToAction("Dashboard");





        }
        //[HttpPost]
        //public ActionResult Dashboard(tbl_score sc)
        //{
        //    List<tbl_score> list = db.tbl_score.ToList();
        //    foreach (var item in list)
        //    {

        //        List<tbl_score> li = db.tbl_score.Where(x => x.std_id == item.std_id).ToList();
        //        ViewData["list"] = li;




        //        //tbl_score s = new tbl_score();
        //        //tbl_students std = new tbl_students();
        //        //tbl_category cat = new tbl_category();
        //        //s.exam_id = cat.cat_id;
        //        //s.std_id = std.std_id;

        //        //db.tbl_score.Add(s);
        //        //db.SaveChanges();

        //        return View(li);
        //    }
        //    return View();
        //}
        [HttpGet]
        public ActionResult Add_Question()
        {
            int sid = Convert.ToInt32(Session["ad_id"]);
            List<tbl_category> li = db.tbl_category.Where(X => X.cat_fk_adid == sid).ToList();
            List<chapter> li1 = db.chapters.Where(X => X.ad_id == sid).ToList();
            ViewBag.list = new SelectList(li1, "ch_id", "ch_name");
            ViewBag.list1 = new SelectList(li, "cat_id", "cat_name");
            return View();
        }

        [HttpPost]
        public ActionResult Add_Question(tbl_questions q)
        {
            int sid = Convert.ToInt32(Session["ad_id"]);
            List<tbl_category> li = db.tbl_category.Where(X => X.cat_fk_adid == sid).ToList();
            List<chapter> li1 = db.chapters.Where(X => X.ad_id == sid).ToList();
            ViewBag.list1 = new SelectList(li, "cat_id", "cat_name");
            ViewBag.list = new SelectList(li1, "ch_id", "ch_name");
            tbl_questions qa = new tbl_questions();
            qa.q_txt = q.q_txt;
            qa.opa = q.opa;
            qa.opb = q.opb;
            qa.opc = q.opc;
            qa.opd = q.opd;
            qa.cop = q.cop;
            qa.q_fk_ct_id = q.q_fk_ct_id;
            qa.ch_id = q.ch_id;

            db.tbl_questions.Add(qa);
            db.SaveChanges();
            TempData["msg"] = "Question added successfully-----";
            TempData.Keep();
            return RedirectToAction("Add_Question");

        }




        [HttpGet]
        public ActionResult Add_Category()
        {
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Index");
            }

            //  Session["ad_id"] = 2;
            int adid = Convert.ToInt32(Session["ad_id"].ToString());
            List<tbl_category> li = db.tbl_category.Where(x => x.cat_fk_adid == adid).OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = li;
            return View();
        }
        [HttpPost]
        public ActionResult Add_Category(tbl_category cat)
        {

            List<tbl_category> li = db.tbl_category.OrderByDescending(x => x.cat_id).ToList();
            ViewData["list"] = li;

            tbl_category c = new tbl_category();
            c.cat_name = cat.cat_name;
            c.cat_fk_adid = Convert.ToInt32(Session["ad_id"].ToString());
            db.tbl_category.Add(c);
            db.SaveChanges();
            return RedirectToAction("Add_Category");
        }



        public ActionResult Index()
        {
            if (Session["ad_id"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }



        [HttpGet]
        public ActionResult Add_Chapter()
        {
            chapter c = new chapter();
            if (Session["ad_id"] == null)
            {
                return RedirectToAction("Index");
            }
            int adid = Convert.ToInt32(Session["ad_id"].ToString());
            List<tbl_category> list = db.tbl_category.ToList();
            foreach (var item in list)
            {


                List<chapter> li2 = db.chapters.Where(x => item.cat_id == x.cat_id).ToList();
                List<chapter> li1 = db.chapters.Where(x => x.ad_id == adid).OrderByDescending(x => x.ch_id).ToList();
                List<chapter> li3 = db.chapters.OrderByDescending(x => c.ch_id).ToList();
                ViewData["list"] = li3;
            }
            List<tbl_category> li = db.tbl_category.Where(X => X.cat_fk_adid == adid).ToList();
            ViewBag.list = new SelectList(li, "cat_id", "cat_name");
            return View();
        }

        [HttpPost]
        public ActionResult Add_Chapter(chapter q)
        {

            List<chapter> li1 = db.chapters.OrderByDescending(x => x.ch_id).ToList();
            ViewData["list"] = li1;

            int sid = Convert.ToInt32(Session["ad_id"]);
            List<tbl_category> li = db.tbl_category.Where(X => X.cat_fk_adid == sid).ToList();
            ViewBag.list = new SelectList(li, "cat_id", "cat_name");
            chapter qa = new chapter();
            qa.ch_name = q.ch_name;
            qa.ad_id = Convert.ToInt32(Session["ad_id"].ToString());
            qa.cat_id = q.cat_id;
            db.chapters.Add(qa);
            db.SaveChanges();
            TempData["msg"] = "Chapter added successfully-----";
            TempData.Keep();
            return RedirectToAction("Add_Chapter");

        }





    }
}