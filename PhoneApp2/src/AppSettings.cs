using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace PhoneApp2.src {
  public class AppSettings {
    protected int level;
    protected int unlockedLevel;
    private static IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;
    protected AppSettings(){
      this.level = 1;
      this.unlockedLevel = 1;
    }

    public void setLevel(int level) { this.level = level; }
    public int getLevel() { return this.level; }
    public int getUnlockedLevel(){ return this.unlockedLevel; }

    public static AppSettings loadSettings() {
      AppSettings settings = new AppSettings();
      String level = null;
      String unlockedLevel = null;
      try { level = (String)appSettings["level"]; } catch (System.Collections.Generic.KeyNotFoundException ex) {}
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
    }
  }
}
