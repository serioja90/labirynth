using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;

namespace PhoneApp2{
  public partial class menuSceltaLivello : PhoneApplicationPage{
    SolidColorBrush activeColor;
    SolidColorBrush lockedColor;
    int greatestLevel;
    public menuSceltaLivello(){
      InitializeComponent();
      greatestLevel = 1;
      activeColor = new SolidColorBrush(new Color() { A = 0x99, R = 0x21, G = 0xE2, B = 0x18 });
      lockedColor = new SolidColorBrush(new Color() { A = 0x99, R = 0xEA, G = 0x00, B = 0x00 });
      foreach(Button button in ContentPanel.Children){
        if (Int16.Parse(button.Tag.ToString()) <= greatestLevel) {
          button.Background = activeColor;
          button.IsHitTestVisible = true;
        } else {
          button.Background = lockedColor;
          button.IsHitTestVisible = false;
        }
      }
    }
    private void StartLevel(object sender, RoutedEventArgs e) {
      String tag = (sender as Button).Tag.ToString();
      NavigationService.Navigate(new Uri("/Game.xaml?level=" + (tag == null ? "1" : tag), UriKind.Relative));
    }
  }
}