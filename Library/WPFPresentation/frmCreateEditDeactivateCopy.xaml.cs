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
    /// Interaction logic for frmCreateEditDeactivateCopy.xaml
    /// </summary>
    public partial class frmCreateEditDeactivateCopy : Window
    {
        private Copy _copy = null;
        private int id;
        private BookManager _bookManager = new BookManager();
        public frmCreateEditDeactivateCopy(int id)
        {
            InitializeComponent();
            btnDeactivateCopy.Visibility = Visibility.Hidden;
            this.id = id;
        }
        public frmCreateEditDeactivateCopy(Copy copy, int id)
        {
            InitializeComponent();
            _copy = copy;
            this.id = id;
            txtCondition.Text = copy.Condition;
            if(!copy.Active)
            {
                btnDeactivateCopy.Content = "Activate Copy";
            }
        }

        private void btnCopyConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCondition.Text != "")
                {
                    if (_copy is null)
                    {
                        _bookManager.addCopy(new Copy
                        {
                            BookId = id,
                            Condition = txtCondition.Text,
                            Active = true
                        });
                        this.Close();
                    }
                    else
                    {
                        _copy.Condition = txtCondition.Text;
                        Copy oldCopy = _bookManager.getCopyById(_copy.CopyID);
                        _bookManager.editCopy(_copy, oldCopy);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Enter a Condition", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeactivateCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var confirm = MessageBox.Show("Are You Sure?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (confirm == MessageBoxResult.Yes)
                {
                    if (_copy.Active)
                    {
                        _bookManager.deactivateCopy(_copy.CopyID);
                    }
                    else
                    {
                        _bookManager.activateCopy(_copy.CopyID);
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
