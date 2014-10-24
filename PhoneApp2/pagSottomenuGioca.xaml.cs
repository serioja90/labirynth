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

namespace PhoneApp2 {
  public partial class pagSottomenuGioca : PhoneApplicationPage{
    public pagSottomenuGioca(){
      InitializeComponent();
    }

    private void selezionaLivello_Click(object sender, RoutedEventArgs e){
      NavigationService.Navigate(new Uri("/menuSceltaLivello.xaml", UriKind.Relative));
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
        AppSettings settings = AppSettings.loadSettings();
        if (settings.getLevel() > 5)
        {
            MessageBox.Show("Hai già terminato tutti i livelli!", "Verrai reindirizzato al menù dei livelli.", MessageBoxButton.OK);
            NavigationService.Navigate(new Uri("/menuSceltaLivello.xaml", UriKind.Relative));
            NavigationService.RemoveBackEntry();
        }
        else
        {
            NavigationService.Navigate(new Uri("/Game.xaml?level=" + settings.getUnlockedLevel(), UriKind.Relative));
        }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new Uri("/pagStatistiche.xaml", UriKind.Relative));
    }

    private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
    {
        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
    }
  }
}