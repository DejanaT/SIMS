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

namespace Projekat.GuestPages
{
    /// <summary>
    /// Interaction logic for RejectReason.xaml
    /// </summary>
    public partial class RejectReason : Window
    {
        public RejectReason(string reason)
        {
            InitializeComponent();
            reasonTextBlock.Text = reason;
        }
    }
}
