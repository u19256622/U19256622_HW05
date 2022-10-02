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
    public class booksController : Controller
    {
        private LibraryEntities db = new LibraryEntities();


        // GET: books
        public ActionResult Index()
        {
            BooksViewModel books1 = new BooksViewModel();
            books1.authors = new List<author>();
            books1.authors.AddRange(db.authors);
            books1.books = new List<book>();
            books1.books.AddRange(db.books);
            books1.types = new List<type>();
            books1.types.AddRange(db.types);

            return View(books1);
        }

        // GET: books/Details/5
        public ActionResult Details(int? id)
        {

            Session["temp"] = id;
            book book = new book();
            book = db.books.FirstOrDefault(x => x.bookId == id);
            BooksDetailsViewModel booksDetailsViewModel = new BooksDetailsViewModel();

            booksDetailsViewModel.Borrows = db.borrows.Where(x => x.bookId == id).ToList();
            booksDetailsViewModel.numborrows = booksDetailsViewModel.Borrows.Count();
            booksDetailsViewModel.name = db.books.FirstOrDefault(x => x.bookId == id).name;
            booksDetailsViewModel.id = (int)id;

           if (book.borrows.FirstOrDefault(x => x.broughtDate > DateTime.Now) != null)
           {
                        booksDetailsViewModel.status = "Booked";
           }
           else
           {
                        booksDetailsViewModel.status = "Available";

           }
            return View(booksDetailsViewModel);

        }

         public ActionResult Search(int? types, int ? author, string search)
        {

            BooksViewModel books1 = new BooksViewModel();
            books1.authors = new List<author>();
            books1.authors.AddRange(db.authors);
            books1.books = new List<book>();
            books1.books.AddRange(db.books);
            books1.types = new List<type>();
            books1.types.AddRange(db.types);

            if(author!=null)
                books1.books = books1.books.Where(x => x.authorId == author).ToList();
            if(types!=null)
                books1.books = books1.books.Where(x => x.typeId == types).ToList();
            if(search!="")
            {
                books1.books = books1.books.Where(x => x.name.ToLower().Contains(search.ToLower())).ToList();
            }
            return View("Index", books1);
        }
    
    }
}
