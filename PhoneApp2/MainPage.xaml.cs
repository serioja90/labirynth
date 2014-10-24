using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace PhoneApp2
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Costruttore
        public MainPage()
        {
            InitializeComponent();
        }

        private void Info(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pagInfo.xaml", UriKind.Relative));
        }

        private void guida(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pagGuida.xaml", UriKind.Relative));
        }

        private void gioca_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pagSottomenuGioca.xaml", UriKind.Relative));
        }

        private void opzioni_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/pagOpzioni.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            while (NavigationService.RemoveBackEntry() != null)
            {
                NavigationService.RemoveBackEntry();
            }
        }


    }
}