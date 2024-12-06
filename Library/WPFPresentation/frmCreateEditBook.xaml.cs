using DataDomain;
using LogicLayer;
using System;
using System.Collections.Generic;
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
        public bool Success { get; private set; } = false;
        public frmCreateEditBook()
        {
            InitializeComponent();
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
                _copies = _bookManager.getCopiesByBookId(book.BookId);
                grdCopyList.ItemsSource = _copies;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddCopy_Click(object sender, RoutedEventArgs e)
        {
            new frmCreateEditDeactivateCopy().ShowDialog();
        }

        private void btnUpdateCopy_Click(object sender, RoutedEventArgs e)
        {
            if(grdCopyList.SelectedItem != null)
            {
                var copy = grdCopyList.SelectedItem as Copy;
                if (copy != null)
                {
                    new frmCreateEditDeactivateCopy(copy.Condition).ShowDialog();
                }
            }
        }

        private void btnDeactiveCopy_Click(object sender, RoutedEventArgs e)
        {
            if (grdCopyList.SelectedItem != null)
            {
                var copy = grdCopyList.SelectedItem as Copy;
                if (copy != null)
                {
                    if (copy.Active)
                    {
                        var confirm = MessageBox.Show("Deactivate Copy?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);
                        if (confirm == MessageBoxResult.Yes)
                        {
                            return;
                        }
                    }
                    else
                    {
                        var confirm = MessageBox.Show("Reactivate Copy?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);
                        if (confirm == MessageBoxResult.Yes)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}
