using DataDomain;
using LogicLayer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserVM _accesskey = null;
        private BookManager _bookManager = new BookManager();
        private List<Copy> _cart = new List<Copy>();
        private bool _checkout = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hideAllTabs();
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnuLoginLogout_Click(object sender, RoutedEventArgs e)
        {
            if(mnuLoginLogout.Header.ToString() == "Login")
            {
                var loginWindow = new frmLogin();
                loginWindow.ShowDialog();
                _accesskey = loginWindow.User;

                if (_accesskey != null)
                {
                    foreach (var role in _accesskey.Roles)
                    {
                        switch (role)
                        {
                            case "User":
                                tabCheckedOut.Visibility = Visibility.Visible;
                                tabCheckoutList.Visibility = Visibility.Visible;
                                break;
                            case "Librarian":
                                tabMemberList.Visibility = Visibility.Visible;
                                break;
                            case "Admin":
                                tabCheckedOut.Visibility = Visibility.Visible;
                                tabCheckoutList.Visibility = Visibility.Visible;
                                tabMemberList.Visibility = Visibility.Visible;
                                tabAdmin.Visibility = Visibility.Visible;
                                break;
                            default:
                                break;
                        }
                    }
                    txtStatus.Content = "Welcome to the Library " + _accesskey.FirstName + " " + _accesskey.LastName;
                    mnuLoginLogout.Header = "Log out";
                    mnuCreateAccount.Header = "Update Account";
                }
            }
            else
            {
                logoutUser();
            }
        }

        private void mnuCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            frmCreateEditAccount frm;
            if(mnuCreateAccount.Header.ToString() == "Update Account")
            {
                frm = new frmCreateEditAccount(_accesskey);
            }
            else
            {
                frm = new frmCreateEditAccount();
            }

            frm.ShowDialog();
            if (frm.User != null)
            {
                _accesskey = frm.User;
                txtStatus.Content = "Welcome to the Library " + _accesskey.FirstName + " " + _accesskey.LastName;
            }
        }

        private void hideAllTabs()
        {
            tabBookList.IsSelected = true;
            tabCheckedOut.Visibility = Visibility.Hidden;
            tabCheckoutList.Visibility = Visibility.Hidden;
            tabMemberList.Visibility = Visibility.Hidden;
            tabAdmin.Visibility = Visibility.Hidden;
        }

        private void logoutUser()
        {
            _accesskey = null;

            mnuLoginLogout.Header = "Login";
            txtStatus.Content = "Welcome to the Library. Please Log In At The File Menu.";

            hideAllTabs();
        }

        private void tabBookList_Loaded(object sender, RoutedEventArgs e)
        {
            populateBookList();
        }

        private void populateBookList()
        {
            List<Book> books;
            try
            {
                books = _bookManager.getAllBooks();
                if (books == null)
                {
                    throw new Exception("Book List is Null.");
                }
                else
                {
                    grdBookList.ItemsSource = books;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdBookList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var book = grdBookList.SelectedItem as Book;
            List<Copy> copies;
            if (book != null)
            {
                try
                {
                    copies = _bookManager.getCopiesByBookId(book.BookId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var frm = new frmViewBook(book, copies, _accesskey);
                frm.ShowDialog();
                
                if(frm.selectedCopy != null)
                {
                    if (_cart.IsNullOrEmpty())
                    {
                        _cart.Add(frm.selectedCopy);
                    }
                    else
                    {
                        bool exists = false;
                        foreach (var copy in _cart)
                        {
                            if (frm.selectedCopy.CopyId == copy.CopyId)
                            {
                                MessageBox.Show("Copy is Already in Checkout List", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                                exists = true;
                            }
                        }
                        if (!exists)
                        {
                            _cart.Add(frm.selectedCopy);
                        }
                    }
                }
            }
            return;
        }

        private void populateCheckoutList()
        {
            List<CopyVM> cart = new List<CopyVM>();
            List<Book> books = null;

            try
            {
                books = _bookManager.getAllBooks();

                for (int i = 0; i < _cart.Count; i++)
                {
                    string title = "UNKNOWN";
                    foreach (var book in books)
                    {
                        if (book.BookId == _cart[i].BookId)
                        {
                            title = book.Name;
                            break;
                        }
                    }
                    cart.Add(new CopyVM
                    {
                        CopyId = _cart[i].CopyId,
                        Name = title,
                        Condition = _cart[i].Condition
                    });
                }

                grdCheckoutList.ItemsSource = null;
                grdCheckoutList.ItemsSource = cart;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tabCheckoutList_Loaded(object sender, RoutedEventArgs e)
        {
            populateCheckoutList();
        }

        private void tabsetMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            TabItem tab = tabControl.SelectedItem as TabItem;
            if(tab != null)
            {
                if (tab.Header.ToString() == "Checkout List")
                {
                    if (!_checkout)
                    {
                        populateCheckoutList();
                        _checkout = true;
                    }
                }
                else
                {
                    _checkout = false;
                }
            }
            else
            {
                return;
            }
        }

        private void grdCheckoutList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var copy = grdCheckoutList.SelectedItem as CopyVM;
            if (copy != null)
            {
                var confirm = MessageBox.Show("Remove Book From Checkout List?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (confirm == MessageBoxResult.Yes)
                {
                    for (int i = 0; i < _cart.Count(); i++)
                    {
                        if (copy.CopyId == _cart[i].CopyId)
                        {
                            _cart.RemoveAt(i);
                        }
                    }
                    populateCheckoutList();
                }
            }
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Check Out?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (confirm == MessageBoxResult.Yes)
            {
                _cart.Clear();
                populateCheckoutList();
            }
        }
    }
}