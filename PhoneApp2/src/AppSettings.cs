using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace PhoneApp2.src {
  public class AppSettings {
    protected int level, unlockedLevel, totalScore, games;
    private static IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
    protected AppSettings(){
      this.level = 1;
      this.unlockedLevel = 1;
    }

    public void setLevel(int level) { this.level = level; }
    public int getLevel() { return this.level; }
    public int getUnlockedLevel() { return this.unlockedLevel; }
    public double[] getBestTimes()
    {
        try
        {
            return ((IEnumerable<double>)appSettings["bestTimes"])
                                     .Cast<object>()
                                     .Select(x => (double)x)
                                     .ToArray();
        }
        catch (System.Collections.Generic.KeyNotFoundException ex) { return new double[7]; }
    }
    public int[] getScores()
    {
        try
        {
            return ((IEnumerable<int>)appSettings["scoresLv"])
                                     .Cast<object>()
                                     .Select(x => (int)x)
                                     .ToArray();
        }
        catch (System.Collections.Generic.KeyNotFoundException ex) { return new int[7]; }
    }
    public int[] getGames()
    {
        try
        {
            return ((IEnumerable<int>)appSettings["gamesLv"])
                                     .Cast<object>()
                                     .Select(x => (int)x)
                                     .ToArray();
        }
        catch (System.Collections.Generic.KeyNotFoundException ex) { return new int[7]; }
    }

    public static AppSettings loadSettings() {
      AppSettings settings = new AppSettings();
      String level = null;
      String unlockedLevel = null;
      try { level = (String)appSettings["level"]; } catch (System.Collections.Generic.KeyNotFoundException ex) { }
      try { unlockedLevel = (String)appSettings["unlockedLevel"]; } catch (System.Collections.Generic.KeyNotFoundException ex) { }
      
      if (!String.IsNullOrEmpty(level)) settings.setLevel(Int16.Parse(level));
      if (!String.IsNullOrEmpty(unlockedLevel)) settings.unlockLevel(Int16.Parse(unlockedLevel));
      return settings;
    }

    public static void saveSettings(AppSettings settings) {
      appSettings["level"] = settings.getLevel().ToString();
      appSettings["unlockedLevel"] = settings.getUnlockedLevel().ToString();
    }

    public void unlockLevel(int level) {
      if (level > unlockedLevel) this.unlockedLevel = level; ;
      this.level = level;
    }

    public void clearProgress() {
      appSettings["level"] = "1";
      appSettings["unlockedLevel"] = "1";
      appSettings["bestTimes"] = new double[7];
      appSettings["scoresLv"] = new int[7];
      appSettings["gamesLv"] = new int[7];
    }

    public void insertNewResults(double ms, int score, int level)
    {
        double[] times;
        int[] scores, gamesLv;
        try
        {
            times = ((IEnumerable<double>)appSettings["bestTimes"])
                                     .Cast<object>()
                                     .Select(x => (double)x)
                                     .ToArray();
        }
        catch (System.Collections.Generic.KeyNotFoundException ex) { times = new double[7]; }
        try
        {
            scores = ((IEnumerable<int>)appSettings["scoresLv"])
                                     .Cast<object>()
                                     .Select(x => (int)x)
                                     .ToArray();
        }
        catch (System.Collections.Generic.KeyNotFoundException ex) { scores = new int[7]; }
        try
        {
            gamesLv = ((IEnumerable<int>)appSettings["gamesLv"])
                                     .Cast<object>()
                                     .Select(x => (int)x)
                                     .ToArray();
        }
        catch (System.Collections.Generic.KeyNotFoundException ex) { gamesLv = new int[7]; }
        if (times[level] > ms || times[level] == 0.0) times[level] = ms;
        scores[level] += score;                         //punteggi per livello
        gamesLv[level]++;                               //giochi per livello
        scores[0] += score;                             //punteggi totali
        gamesLv[0]++;                                   //giochi totali
        appSettings["bestTimes"] = times;
        appSettings["scoresLv"] = scores;
        appSettings["gamesLv"] = gamesLv;
    }
  }
}
