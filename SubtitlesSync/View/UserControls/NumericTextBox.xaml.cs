﻿using SubtitlesSync.Model;
using SubtitlesSync.MVVM;
using SubtitlesSync.Services;
using SubtitlesSync.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
    public partial class NumericTextBox : UserControl //, INotifyPropertyChanged
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

        [Range(1,100)]
        public int NumTextBoxText
        {
            get { return (int)GetValue(NumTextBoxTextProperty); }
            set {
                if (value >= 1 && value <= 100)
                {
                    SetValue(NumTextBoxTextProperty, value);
                }
                else if (value < 1)
                {
                    SetValue(NumTextBoxTextProperty, 1);
                }
                else if (value > 100)
                {
                    SetValue(NumTextBoxTextProperty, 100);
                }
                else
                {
                    SetValue(NumTextBoxTextProperty, 1);
                }
            }
        }

        // Using a DependencyProperty as the backing store for NumTextBoxText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumTextBoxTextProperty =
            DependencyProperty.Register("NumTextBoxText", typeof(int), typeof(NumericTextBox), new PropertyMetadata(0));

        //public event PropertyChangedEventHandler PropertyChanged;
        //void SetValueDp(DependencyProperty property, object value,
        //    [System.Runtime.CompilerServices.CallerMemberName] String p = null)
        //{
        //    SetValue(property, value);
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(p));
        //}



        public RelayCommand NumericTextBoxUpInternalCommand => new RelayCommand(execute => NumTextBoxText++);
        public RelayCommand NumericTextBoxDownInternalCommand => new RelayCommand(execute => NumTextBoxText--);
    }
}
