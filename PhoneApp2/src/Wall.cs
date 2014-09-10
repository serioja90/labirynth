using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhoneApp2.src {
  public class Wall {
    protected Canvas container;
    protected Point position;
    protected Image image;
    protected double width, height;
    public Wall(Canvas container, Point position, double width, double height) {
      this.container = container;
      this.position = position;
      this.width = width;
      this.height = height;
      this.image = new Image();
      this.image.Source = new BitmapImage(new Uri("resources/img/wall.jpg", UriKind.Relative));
      this.image.Width = width; 
      this.image.Height = height;
      this.image.Stretch = Stretch.UniformToFill;
      this.container.Children.Add(this.image);
      Canvas.SetLeft(this.image, this.position.X);
      Canvas.SetTop(this.image, this.position.Y);
      Debug.WriteLine("Wall: (" + position.X + ", " + position.Y + ")");
    }

    public Point getPosition() { return this.position; }
    public double getWidth() { return this.width; }
    public double getHeight() { return this.height; }
  }
}