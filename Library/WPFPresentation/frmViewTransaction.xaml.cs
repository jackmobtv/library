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
using LogicLayer;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for frmViewTransaction.xaml
    /// </summary>
    public partial class frmViewTransaction : Window
    {
        public frmViewTransaction(int transactionId)
        {
            InitializeComponent();
            try
            {
                grdCheckoutList.ItemsSource = new TransactionManager().getCopiesByTransactionId(transactionId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
