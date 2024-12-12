using SubtitlesSync.MVVM;
using SubtitlesSync.Services;
using SubtitlesSync.ViewModel;
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

namespace SubtitlesSync.View.UserControls
{
    /// <summary>
    /// Interaction logic for NumericTextBox.xaml
    /// </summary>
    public partial class NumericTextBox : UserControl
    {
        public NumericTextBox()
        {
            InitializeComponent();

            //var windowService = new WindowService();
            //DataContext = new MainWindowViewModel(windowService);
        }

        public int NumTextBoxText { get; set; }

        


    }
}
