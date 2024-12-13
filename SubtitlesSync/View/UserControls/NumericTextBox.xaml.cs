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
    public partial class NumericTextBox : UserControl , INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler? PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}



        public NumericTextBox()
        {
            InitializeComponent();
            //DataContext = this;
            (this.Content as FrameworkElement).DataContext = this;
        }

        //public int NumTextBoxText { get; set; }
        //private int numTextBoxText;

        //public int NumTextBoxText
        //{
        //    get { return numTextBoxText; }
        //    set {
        //        numTextBoxText = value;
        //        //OnPropertyChanged();
        //    }
        //}


        public int NumTextBoxText
        {
            get { return (int)GetValue(NumTextBoxTextProperty); }
            set {
                SetValue(NumTextBoxTextProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumTextBoxText"));
            }
        }

        // Using a DependencyProperty as the backing store for NumTextBoxText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumTextBoxTextProperty =
            DependencyProperty.Register("NumTextBoxText", typeof(int), typeof(NumericTextBox), new PropertyMetadata(0));

        public event PropertyChangedEventHandler PropertyChanged;
        void SetValueDp(DependencyProperty property, object value,
            [System.Runtime.CompilerServices.CallerMemberName] String p = null)
        {
            SetValue(property, value);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }



        //internal RelayCommand NumericTextBoxUpCommand => new RelayCommand(execute => NumericTextBoxIncrease());

        //private void NumericTextBoxIncrease()
        //{
        //    //var NumTextBoxText = new NumericTextBox();
        //    //NumTextBoxText.NumTextBoxText++;

        //    NumTextBoxText = 20;
        //}


    }
}
