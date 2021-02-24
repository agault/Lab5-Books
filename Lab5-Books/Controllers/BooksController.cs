using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;


namespace Lab5_Books.Controllers
{
    public class BooksController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            IList<Models.Book> bookList = new List<Models.Book>(); //list Book object
            //load books from books.xml
            string path = Request.PathBase + "App_Data/books.xml";
            XmlDocument doc = new XmlDocument();
            if (System.IO.File.Exists(path))
               
            {
                //only load if the file exists
                doc.Load(path); //exists, so load
                XmlNodeList books = doc.GetElementsByTagName("book"); //load book elements
                foreach (XmlElement b in books)
                {
                    //loadBook data from each book and bind to a model
                    Models.Book book = new Models.Book();
                    book.BookID = Int32.Parse(b.GetElementsByTagName("id")[0].InnerText);
                    book.BookTitle = b.GetElementsByTagName("title")[0].InnerText;
                    /*book.AuthorTitle = b.Attributes["title"].Value;*/
                    book.AuthorFName = b.GetElementsByTagName("firstname")[0].InnerText;
                    book.AuthorLName = b.GetElementsByTagName("lastname")[0].InnerText;
                    bookList.Add(book);
                }
            }
            return View(bookList); //pass the bookList to view
        }
        [HttpGet]
        //this will load when just loading the empty form
        public IActionResult Create()
        {
            var book = new Models.Book();
            return View(book);
        }
        //this will load when a form is submitted via post (form data passed as model)
        [HttpPost]
        public IActionResult Create(Models.Book b)
        {

            string path = Request.PathBase + "App_Data/books.xml";
            XmlDocument doc = new XmlDocument();

            // create file if it doesn't exist
            if (System.IO.File.Exists(path))
            {
                doc.Load(path); //exists, so load
                XmlNode root = doc.SelectSingleNode("books"); //get root
                XmlElement book = _CreateBookElement(doc, b);
                root.AppendChild(book); //append new
            }
            else
            {
                //doesn't exist so add stuff
                XmlNode dec = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                doc.AppendChild(dec);
                XmlNode root = doc.CreateElement("books");
                XmlNode book = _CreateBookElement(doc, b);
                root.AppendChild(book);
                doc.AppendChild(root);
            }
            doc.Save(path);
            return View();
        }
        private XmlElement _CreateBookElement(XmlDocument doc, Models.Book newBook)
        {
            XmlElement book = doc.CreateElement("book");
            XmlNode author = doc.CreateElement("author");

            XmlNode id = doc.CreateElement("id");
            id.InnerText = newBook.BookID.ToString();

            XmlNode title = doc.CreateElement("title");
            title.InnerText = newBook.BookTitle;
            
         /*   XmlAttribute titleA = doc.CreateAttribute("title");
            titleA.Value = newBook.AuthorTitle;
            author.Attributes.Append(titleA);*/


            XmlNode firstname = doc.CreateElement("firstname");
            firstname.InnerText = newBook.AuthorFName;

            XmlNode lastname = doc.CreateElement("lastname");
            lastname.InnerText = newBook.AuthorLName;

            book.AppendChild(id);
            book.AppendChild(title);
            author.AppendChild(firstname);
            author.AppendChild(lastname);
            book.AppendChild(author);
            

            return book;
        }

    }
}
