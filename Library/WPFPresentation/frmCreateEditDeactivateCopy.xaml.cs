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
    /// Interaction logic for frmCreateEditDeactivateCopy.xaml
    /// </summary>
    public partial class frmCreateEditDeactivateCopy : Window
    {
        public frmCreateEditDeactivateCopy()
        {
            InitializeComponent();
            btnDeactivateCopy.Visibility = Visibility.Hidden;
        }
        public frmCreateEditDeactivateCopy(string condition)
        {
            InitializeComponent();
            txtCondition.Text = condition;
        }
    }
}
