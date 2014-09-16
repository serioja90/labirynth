using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PhoneApp2.src {
  public class Hole {
    protected Point position;
    protected Image image;
    protected double radius;
    protected Canvas container;
    public Hole(Canvas container, Point position, double radius) {
      this.container = container;
      this.position = position;
      this.radius = radius;
      this.image = new Image();
      this.image.Source = new BitmapImage(new Uri("resources/img/hole.png", UriKind.Relative));
      this.image.Width = radius * 2;
      this.image.Height = radius * 2;
      Canvas.SetZIndex(this.image, 0);
      this.container.Children.Add(this.image);
      this.setPosition(this.position);
    }

    public void setPosition(Point position) {
      this.position = position;
      Canvas.SetLeft(this.image, this.position.X - this.radius);
      Canvas.SetTop(this.image, this.position.Y - this.radius);
    }

    public Point getPosition() { return this.position; }
  }
}
