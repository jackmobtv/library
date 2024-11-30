using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
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
using static System.Reflection.Metadata.BlobBuilder;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for frmViewBook.xaml
    /// </summary>
    public partial class frmViewBook : Window
    {
        private Book _Book;
        private List<Copy> _Copies;
        private UserVM _accesskey;
        public Copy? selectedCopy { get; private set; } = null;
        public frmViewBook(Book book, List<Copy> copies, UserVM _accesskey)
        {
            _Book = book;
            _Copies = copies;
            this._accesskey = _accesskey;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_Copies == null)
            {
                MessageBox.Show("Copies are Null");
            }
            else
            {
                grdCopyList.ItemsSource = _Copies;
            }

            txtTitle.Text = _Book.Name;
            txtGenre.Text = _Book.Genre;
            txtAuthor.Text = _Book.Author;
            txtPublisher.Text = _Book.Publisher;
            txtDescription.Text = _Book.Description;
        }

        private void grdCopyList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_accesskey != null)
            {
                var copy = grdCopyList.SelectedItem as Copy;
                if (copy != null)
                {
                    if (copy.Active)
                    {
                        var confirm = MessageBox.Show("Add Book To Checkout List?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);
                        if (confirm == MessageBoxResult.Yes)
                        {
                            selectedCopy = copy;
                            this.Close();
                        }
                    }
                }
                return;
            }
        }
    }
}
