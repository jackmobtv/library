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
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : Window
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        public UserVM User { get; private set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtEmail.Focus();
            btnLogin.IsDefault = true;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UserManager _userManager = new UserManager();

            if (txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter An Email!", "", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (pwdPassword.Password.Trim() == "")
            {
                MessageBox.Show("Please Enter A Password!", "", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                User = _userManager.loginUser(txtEmail.Text, pwdPassword.Password);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Login Failed",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.Close();
        } 
    }
}
