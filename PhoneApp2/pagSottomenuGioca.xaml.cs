using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace PhoneApp2 {
  public partial class pagSottomenuGioca : PhoneApplicationPage{
    public pagSottomenuGioca(){
      InitializeComponent();
    }

    private void selezionaLivello_Click(object sender, RoutedEventArgs e){
      NavigationService.Navigate(new Uri("/menuSceltaLivello.xaml",UriKind.Relative));
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
      NavigationService.Navigate(new Uri("/Game.xaml?level=1", UriKind.Relative));
    }
  }
}