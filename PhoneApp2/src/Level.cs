using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PhoneApp2.src {
  public class Level {
    const String VALID_CHARS = " ▀▄█●SF";
    protected int level;
    protected List<Wall> walls;
    protected Canvas container;
    protected String content;
    protected Point start;
    protected Hole finish;
    public Level(int level, Canvas container){
      this.level = level;
      this.container = container;
      this.content = ReadFile("resources/levels/" + level.ToString() + ".txt");
      Debug.WriteLine(content);
      this.start = new Point(Canvas.GetLeft(container), Canvas.GetTop(container));
      this.finish = new Hole(container, new Point(0, 0), 8);
      this.GenerateWalls();
    }

    public Wall[] getWalls() { return this.walls.ToArray(); }
    public Point getStart() { return this.start; }
    public Point getFinish() { return this.finish.getPosition(); }
    public int getLevel() { return this.level; }

    private void GenerateWalls() {
      double wallSize = 20;
      double x = 0;
      double y = -wallSize;
      char chr;
      walls = new List<Wall>();
      for (int i = 0; i < content.Length; i++) {
        chr = content[i];
        switch (chr) {
          case ' ':
            x += wallSize;
            break;
          case 'S':
            this.start.X = x + wallSize / 2;
            this.start.Y = y + wallSize / 2;
            x += wallSize;
            break;
          case 'F':
            Point newFinish = this.finish.getPosition();
            newFinish.X = x + wallSize / 2;
            newFinish.Y = y + wallSize / 2;
            this.finish.setPosition(newFinish);
            x += wallSize;
            break;
          case '\n':
            x = 0;
            y += wallSize * 2;
            break;
          case '▀':
            walls.Add(new Wall(this.container, new Point(x, y), wallSize, wallSize));
            x += wallSize;
            break;
          case '▄':
            walls.Add(new Wall(this.container, new Point(x, y + wallSize), wallSize, wallSize));
            x += wallSize;
            break;
          case '█':
            walls.Add(new Wall(this.container, new Point(x, y), wallSize, wallSize * 2));
            x += wallSize;
            break;
        }
      }
    }

    private string ReadFile(string filePath) {
      var ResrouceStream = Application.GetResourceStream(new Uri(filePath, UriKind.Relative));
      if (ResrouceStream != null) {
        Stream myFileStream = ResrouceStream.Stream;
        if (myFileStream.CanRead) {
          StreamReader myStreamReader = new StreamReader(myFileStream);
          return myStreamReader.ReadToEnd();
        }
      }
      return "NULL";
    }
  }
}
