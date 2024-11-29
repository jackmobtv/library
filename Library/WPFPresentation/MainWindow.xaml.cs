using DataDomain;
using LogicLayer;
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
            tabCheckedOut.Visibility = Visibility.Hidden;
            tabCheckoutList.Visibility = Visibility.Hidden;
            tabMemberList.Visibility = Visibility.Hidden;
        }

        private void logoutUser()
        {
            _accesskey = null;

            mnuLoginLogout.Header = "Login";
            txtStatus.Content = "Welcome to the Library. Please Log In At The File Menu.";

            hideAllTabs();
        }
    }
}