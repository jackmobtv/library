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
using DataDomain;
using LogicLayer;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for frmViewUserHistory.xaml
    /// </summary>
    public partial class frmViewUserHistory : Window
    {
        User _user;
        TransactionManager _transactionManager;
        UserManager _userManager;
        public frmViewUserHistory(User user)
        {
            try
            {
                InitializeComponent();
                _user = user;
                _transactionManager = new TransactionManager();
                _userManager = new UserManager();
                grdCheckoutList.ItemsSource = _transactionManager.getCheckedOutCopies(_user.UserId);
                grdTransactionList.ItemsSource = _transactionManager.getTransactionsByUserId(_user.UserId);
                if (!_user.Active)
                {
                    btnDeactivate.Content = "Activate";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void grdTransactionList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var transaction = grdTransactionList.SelectedItem as Transaction;
            if (transaction != null)
            {
                new frmViewTransaction(transaction.TransactionId).ShowDialog();
            }
        }

        private void btnDeactivate_Click(object sender, RoutedEventArgs e)
        {
            var confirm = MessageBox.Show("Are You Sure?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (confirm == MessageBoxResult.Yes)
            {
                try
                {
                    if ((string)btnDeactivate.Content == "Deactivate")
                    {
                        _userManager.deactivateUser(_user.UserId);
                    }
                    else
                    {
                        _userManager.activateUser(_user.UserId);
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
}
