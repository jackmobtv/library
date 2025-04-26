using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataDomain;
using LogicLayer;

namespace WebPresentation.Controllers
{
    public class BookController : Controller
    {
        private BookManager _bookManager = new BookManager();
        // GET: BookController
        public ActionResult Index()
        {
            var books = _bookManager.getAllBooks();

            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            Book book = _bookManager.getBookById(id);

            ViewBag.Copies = _bookManager.getCopiesByBookId(id);

            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    int[] data = CreateBookData(book);

                    _bookManager.addBook(book, data[0], data[1]);

                    foreach (var bookRow in _bookManager.getBookTable())
                    {
                        if (bookRow.Name == book.Name && bookRow.Description == book.Description)
                        {
                            bool yes = false;
                            foreach (var author in _bookManager.getAllAuthors())
                            {
                                if (author.Name == book.Author)
                                {
                                    _bookManager.addBookAuthor(author.AuthorID, book.BookID);
                                    yes = true;
                                }
                            }
                            if (!yes)
                            {
                                _bookManager.addBookAuthor(10001, bookRow.BookID);
                                _bookManager.addAuthor(book.Author, bookRow.BookID);
                            }
                        }
                    }

                    return RedirectToAction(nameof(Index));
                } 
                else
                {
                    throw new ArgumentException("Invalid Data");
                }
            }
            catch
            {
                return View(book);
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            Book book = _bookManager.getBookById(id);

            ViewBag.Copies = _bookManager.getCopiesByBookId(id);

            return View(book);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Book book)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    int[] data = CreateBookData(book);

                    book.BookID = id;

                    Book oldBook = _bookManager.getBookById(id);

                    bool yes = false;
                    foreach (var author in _bookManager.getAllAuthors())
                    {
                        if (!yes && author.Name == book.Author)
                        {
                            _bookManager.editAuthor(author.AuthorID, book.BookID);
                            yes = true;
                        }
                    }
                    if (!yes)
                    {
                        _bookManager.addAuthor(book.Author, book.BookID);
                    }

                    _bookManager.editBook(book, oldBook, data[0], data[1], oldBook.GenreId, oldBook.PublisherId);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new ArgumentException("Invalid Data");
                }
            }
            catch
            {
                return View(book);
            }
        }

        public int[] CreateBookData(Book book)
        {
            int GenreId = 0;
            int PublisherId = 0;
            List<Genre> genres = _bookManager.getAllGenres();
            List<Publisher> publishers = _bookManager.getAllPublishers();

            foreach (var genre in genres)
            {
                if (genre.Name == book.Genre)
                {
                    GenreId = genre.GenreID;
                    break;
                }
            }
            if (GenreId == 0)
            {
                Genre newGenre = new Genre()
                {
                    Name = book.Genre,
                    Description = ""
                };

                _bookManager.addGenre(newGenre);

                genres = _bookManager.getAllGenres();

                foreach (var genre in genres)
                {
                    if (genre.Name == book.Genre)
                    {
                        GenreId = genre.GenreID;
                        break;
                    }
                }
            }

            foreach (var publisher in publishers)
            {
                if (publisher.Name == book.Publisher)
                {
                    PublisherId = publisher.PublisherID;
                    break;
                }
            }
            if (PublisherId == 0)
            {
                _bookManager.addPublisher(book.Publisher);

                publishers = _bookManager.getAllPublishers();

                foreach (var publisher in publishers)
                {
                    if (publisher.Name == book.Publisher)
                    {
                        PublisherId = publisher.PublisherID;
                        break;
                    }
                }
            }

            int[] data = { GenreId, PublisherId };

            return data;
        }
    }
}
