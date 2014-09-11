using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PhoneApp2{
  public partial class menuSceltaLivello : PhoneApplicationPage{
    public menuSceltaLivello(){
        InitializeComponent();
    }
    private void StartLevel(object sender, RoutedEventArgs e) {
      String tag = (sender as Button).Tag.ToString();
      NavigationService.Navigate(new Uri("/Game.xaml?level=" + (tag == null ? "1" : tag), UriKind.Relative));
    }
  }
}