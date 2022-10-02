using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassActivity.Models;

namespace ClassActivity.Controllers
{
    public class studentsController : Controller
    {
        private LibraryEntities db = new LibraryEntities();
        private string status;

        // GET: students
        public ActionResult Index(int ?id)
        {
            var items = db.students.ToList();
            var book = db.books.FirstOrDefault(x => x.bookId == id);
            var studentList = new List<StudentList>();
            int bookid = Convert.ToInt32(Session["temp"]);

            foreach (var item in db.students.ToList())
            {
                StudentList student = new StudentList();
                student.Student = item;
                List<borrow> borrow = db.borrows.Where(x => x.studentId == item.studentId && x.bookId == bookid).ToList();
                if (borrow.Count >0)
                {
                    List<borrow> enumerable = borrow.Where(x => x.broughtDate > DateTime.Now).ToList();
                    if (enumerable.Count > 0)
                    {
                        student.status = "*";
                        ViewBag.booked = student.Student.studentId;
                    }
                    else
                    {
                        student.status = "-";

                    }

                }
                else
                {
                    student.status = "-";
                }

                studentList.Add(student);
            }
       
            return View(studentList);
        }

        // GET: students/Details/5
        public ActionResult ReturnBook(int ?id)
        {
            borrow borrow = new borrow();
            int bookid = Convert.ToInt32(Session["temp"]);
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;

            db.borrows.Where(x => x.studentId == id && x.bookId == bookid).ToList().ForEach(c => { c.takenDate = dateTime; c.broughtDate = dateTime; });

            db.SaveChanges();
            

            return RedirectToAction("Index","books");

        }
        public ActionResult BorrowBook(int? id)
        {
            db.borrows.FirstOrDefault(x => x.studentId == id).broughtDate = DateTime.Now.AddDays(20);

            borrow borrow = new borrow();
            int bookid = Convert.ToInt32(Session["temp"]);
            borrow.book = db.books.FirstOrDefault(x=>x.bookId== bookid);
            borrow.student = db.students.FirstOrDefault(x => x.studentId == id);
            borrow.takenDate = DateTime.Now;
            borrow.broughtDate = DateTime.Now.AddDays(20);

            db.borrows.Add(borrow);
            db.SaveChanges();


            return RedirectToAction("Index","books");
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "studentId,name,surname,birthdate,gender,class,point")] student student)
        {
            if (ModelState.IsValid)
            {
                db.students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "studentId,name,surname,birthdate,gender,class,point")] student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            student student = db.students.Find(id);
            db.students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
