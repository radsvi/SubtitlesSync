using SubtitlesSync.MVVM;
using SubtitlesSync.Services;
using SubtitlesSync.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class NumericTextBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public NumericTextBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        //public int NumTextBoxText { get; set; }
        private int numTextBoxText;

        public int NumTextBoxText
        {
            get { return numTextBoxText; }
            set {
                numTextBoxText = value;
                OnPropertyChanged();
            }
        }


        internal RelayCommand NumericTextBoxUpCommand => new RelayCommand(execute => NumericTextBoxIncrease());

        private void NumericTextBoxIncrease()
        {
            //var NumTextBoxText = new NumericTextBox();
            //NumTextBoxText.NumTextBoxText++;

            NumTextBoxText = 20;
        }


    }
}
