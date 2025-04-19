using DataDomain;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for frmCreateEditBook.xaml
    /// </summary>
    public partial class frmCreateEditBook : Window
    {
        private Book _book;
        private List<Copy> _copies;
        private BookManager _bookManager = new BookManager();
        public frmCreateEditBook()
        {
            InitializeComponent();
            _book = null;
        }
        public frmCreateEditBook(Book book)
        {
            InitializeComponent();
            _book = book;
            txtTitle.Text = _book.Name;
            txtGenre.Text = _book.Genre;
            txtAuthor.Text = _book.Author;
            txtPublisher.Text = _book.Publisher;
            txtDescription.Text = _book.Description;
            try
            {
                _copies = _bookManager.getCopiesByBookId(book.BookID);
                grdCopyList.ItemsSource = _copies;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddCopy_Click(object sender, RoutedEventArgs e)
        {
            if(_book != null)
            {
                new frmCreateEditDeactivateCopy(_book.BookID).ShowDialog();
                _copies = _bookManager.getCopiesByBookId(_book.BookID);
                grdCopyList.ItemsSource = _copies;
            }
            else
            {
                MessageBox.Show("Please Create The Book Before Creating Copies", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnUpdateCopy_Click(object sender, RoutedEventArgs e)
        {
            if(grdCopyList.SelectedItem != null)
            {
                var copy = grdCopyList.SelectedItem as Copy;
                if (copy != null)
                {
                    new frmCreateEditDeactivateCopy(copy, _book.BookID).ShowDialog();
                    _copies = _bookManager.getCopiesByBookId(_book.BookID);
                    grdCopyList.ItemsSource = _copies;
                }
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtTitle.Text.Trim() == "")
                {
                    MessageBox.Show("Title is Empty", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtAuthor.Text.Trim() == "")
                {
                    MessageBox.Show("Author is Empty", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtDescription.Text.Trim() == "")
                {
                    MessageBox.Show("Description is Empty", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtGenre.Text.Trim() == "")
                {
                    MessageBox.Show("Genre is Empty", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (txtPublisher.Text.Trim() == "")
                {
                    MessageBox.Show("Publisher is Empty", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                int GenreId = 0;
                int PublisherId = 0;
                List<Genre> genres = _bookManager.getAllGenres();
                List<Publisher> publishers = _bookManager.getAllPublishers();

                foreach (var genre in genres)
                {
                    if (genre.Name == txtGenre.Text)
                    {
                        GenreId = genre.GenreID;
                        break;
                    }
                }
                if (GenreId == 0)
                {
                    
                    var frm = new frmCreateGenre();
                    frm.ShowDialog();
                    if (frm.Description is null)
                    {
                        MessageBox.Show("A Description is Required", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    Genre newGenre = new Genre()
                    {
                        Name = txtGenre.Text,
                        Description = frm.Description
                    };
                    
                    _bookManager.addGenre(newGenre);

                    genres = _bookManager.getAllGenres();

                    foreach (var genre in genres)
                    {
                        if (genre.Name == txtGenre.Text)
                        {
                            GenreId = genre.GenreID;
                            break;
                        }
                    }
                }

                foreach (var publisher in publishers)
                {
                    if (publisher.Name == txtPublisher.Text)
                    {
                        PublisherId = publisher.PublisherID;
                        break;
                    }
                }
                if (PublisherId == 0)
                {
                    _bookManager.addPublisher(txtPublisher.Text);

                    publishers = _bookManager.getAllPublishers();

                    foreach (var publisher in publishers)
                    {
                        if (publisher.Name == txtPublisher.Text)
                        {
                            PublisherId = publisher.PublisherID;
                            break;
                        }
                    }
                }

                if (_book != null)
                {
                    Book oldBook = _bookManager.getBookById(_book.BookID);
                    int oldGenre = 0;
                    int oldPublisher = 0;

                    foreach (var genre in _bookManager.getAllGenres())
                    {
                        if (genre.Name == _book.Genre)
                        {
                            oldGenre = genre.GenreID;
                            break;
                        }
                    }
                    foreach (var publisher in _bookManager.getAllPublishers())
                    {
                        if (publisher.Name == _book.Publisher)
                        {
                            oldPublisher = publisher.PublisherID;
                            break;
                        }
                    }

                    if (oldGenre == 0 || oldPublisher == 0)
                    {
                        MessageBox.Show("ERROR", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    _book.Name = txtTitle.Text;
                    _book.Description = txtDescription.Text;
                    _book.Author = txtAuthor.Text;

                    bool yes = false;
                    foreach (var author in _bookManager.getAllAuthors())
                    {
                        if (!yes && author.Name == _book.Author)
                        {
                            _bookManager.editAuthor(author.AuthorID, _book.BookID);
                            yes = true;
                        }
                    }
                    if (!yes)
                    {
                        _bookManager.addAuthor(_book.Author, _book.BookID);
                    }

                    _bookManager.editBook(_book, oldBook, GenreId, PublisherId, oldGenre, oldPublisher);
                    this.Close();
                }
                else
                {
                    _book = new Book()
                    {
                        Name = txtTitle.Text,
                        Author = txtAuthor.Text,
                        Description = txtDescription.Text
                    };

                    _bookManager.addBook(_book, GenreId, PublisherId);

                    foreach (var book in _bookManager.getBookTable())
                    {
                        if(book.Name == _book.Name && book.Description == _book.Description)
                        {
                            bool yes = false;
                            foreach (var author in _bookManager.getAllAuthors())
                            {
                                if (author.Name == _book.Author)
                                {
                                    _bookManager.addBookAuthor(author.AuthorID, _book.BookID);
                                    yes = true;
                                }
                            }
                            if (!yes)
                            {
                                _bookManager.addBookAuthor(10001, book.BookID);
                                _bookManager.addAuthor(_book.Author, book.BookID);
                            }
                        }
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
