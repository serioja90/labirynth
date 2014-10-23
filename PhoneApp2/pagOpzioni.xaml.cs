using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp2.src;

namespace PhoneApp2
{
    public partial class pagOpzioni : PhoneApplicationPage
    {
        public pagOpzioni()
        {
            InitializeComponent();
        }

        private void Clear_Progress(object sender, RoutedEventArgs e) {
          MessageBoxResult result = MessageBox.Show("Sei sicuro di voler cancellare i progressi?", "Cancella Progressi", MessageBoxButton.OKCancel);
          if ( result == MessageBoxResult.OK) {
            AppSettings settings = AppSettings.loadSettings();
            settings.clearProgress();
          }
        }
    }
}