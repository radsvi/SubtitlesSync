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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SubtitlesSync.View.Lists
{
    /// <summary>
    /// Interaction logic for DataGridOne.xaml
    /// </summary>
    public partial class DataGridOne : UserControl
    {
        public DataGridOne()
        {
            InitializeComponent();
        }
        //private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (!(e.Source is CheckBox))
        //    {
        //        DataGridRow row = sender as DataGridRow;
        //        YourDataClass dataObject = row.DataContext as YourDataClass;
        //        if (dataObject != null)
        //            dataObject.IsChecked = true;
        //    }
        //}
    }
}
