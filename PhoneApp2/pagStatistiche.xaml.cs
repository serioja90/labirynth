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
    public partial class pagStatistiche : PhoneApplicationPage
    {
        public pagStatistiche()
        {
            InitializeComponent();
            AppSettings settings = AppSettings.loadSettings();
            double[] tempo = settings.getBestTimes();
            int[] partite = settings.getGames();
            int[] punteggi = settings.getScores();
            string testo = "Partite giocate:\t\t\t\t\t\t" + partite[0] + "\n" +
                           "Totale punteggio:\t\t\t\t\t" + punteggi[0] + "\n" +
                           "Media punteggio:\t\t\t\t\t" + (partite[0] != 0 ? (double)punteggi[0] / (double)partite[0] : 0.0) + "\n\n";
            for (int i = 1; i < punteggi.Length; i++)
            {
                int milliseconds = (int)((tempo[i] % 1000) / 10);
                int seconds = (int)(tempo[i] % 60000 / 1000);
                int minutes = (int)(tempo[i] % 3600000 / 60000);
                testo += "Livello " + i + ":\n\tPunteggio massimo:\t\t\t" + ((tempo[i] / 60000) < 5 && tempo[i] != 0 ? (int)Math.Round(-206.61157 * (tempo[i] / 60000) + 1033) : 0) + 
                         "\n\tTempo migliore:\t\t\t\t" + String.Format("{0:00}", minutes) + String.Format(":{0:00}", seconds) + String.Format(".{0:00}", milliseconds) + 
                         "\n\tPunteggio medio:\t\t\t\t" + (partite[i] != 0 ? (double)punteggi[i] / (double)partite[i] : 0.0) + "\n";
            }
            stats.Text = testo;
        }
    }
}