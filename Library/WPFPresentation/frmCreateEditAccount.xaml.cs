using DataDomain;
using LogicLayer;
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

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for frmCreateEditAccount.xaml
    /// </summary>
    public partial class frmCreateEditAccount : Window
    {
        public UserVM User { get; private set; } = null;
        public frmCreateEditAccount()
        {
            InitializeComponent();
            lblNewEmail.Visibility = Visibility.Hidden;
            lblNewPassword.Visibility = Visibility.Hidden;
            txtNewEmail.Visibility = Visibility.Hidden;
            pwdNewPassword.Visibility = Visibility.Hidden;
        }
        public frmCreateEditAccount(UserVM user)
        {
            InitializeComponent();
            User = user;
            txtFirstName.Text = User.FirstName;
            txtLastName.Text = User.LastName;
            txtEmail.Text = User.Email;
            lblOldEmail.Content = "Old Email";
            lblOldPassword.Content = "Old Password";
            txtEmail.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserManager _userManager = new UserManager();
            string email = null;
            string password = null;

            if (txtFirstName.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter a First Name.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtLastName.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter a Last Name.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtEmail.Text.Trim() == "" && txtEmail.IsEnabled)
            {
                MessageBox.Show("Please Enter an Email.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (pwdPassword.Password.Trim() == "")
            {
                MessageBox.Show("Please Enter a Password.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (txtNewEmail.Text.Trim() == "" && txtNewEmail.Visibility == Visibility.Visible)
            {
                email = User.Email;
            }
            else
            {
                email = txtNewEmail.Text;
            }
            if (pwdNewPassword.Password.Trim() != "")
            {
                password = pwdNewPassword.Password;
            }
            else
            {
                password = pwdPassword.Password;
            }

            try
            {
                if(User is null)
                {
                    _userManager.addUser(txtFirstName.Text, txtLastName.Text, txtEmail.Text, pwdPassword.Password);
                }
                else
                {
                    _userManager.authenticateUser(txtEmail.Text, pwdPassword.Password);
                    _userManager.editUser(txtFirstName.Text, txtLastName.Text, txtEmail.Text, email, pwdPassword.Password, password);
                }
            }
            catch (Exception ex)
            {
                if (User is null)
                {
                    MessageBox.Show(ex.Message, "User Creation Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, "User Update Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return;
            }

            if(User is null)
            {
                MessageBox.Show("User Successfully Created", "Success", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("User Successfully Updated", "Success", MessageBoxButton.OK, MessageBoxImage.None);
            }

            if (User != null)
            {
                User.FirstName = txtFirstName.Text;
                User.LastName = txtLastName.Text;
                User.Email = email;
            }

            this.Close();
        }
    }
}
