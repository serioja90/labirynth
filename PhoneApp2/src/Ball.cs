using Microsoft.Devices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PhoneApp2.src {
  public class Ball {
    const double MARGIN = 2;
    private double radius;
    private Point position;
    private Image image;
    private Canvas container;
    public Ball(Canvas container, double radius, Point position) {
      this.container = container;
      this.radius = radius;
      this.position = position;
      this.image = new Image();
      this.image.Source = new BitmapImage(new Uri("resources/img/ball.png", UriKind.Relative));
      this.image.Width = radius * 2;
      this.image.Height = radius * 2;
      this.container.Children.Add(this.image);
      this.setPosition(position);
      Canvas.SetZIndex(this.image, 1);
    }

    public void setRadius(double radius) {
      this.radius = radius;
      this.image.Width = radius * 2;
      this.image.Height = radius * 2;
    }

    public void setPosition(Point position) {
      this.position = position;
      Canvas.SetLeft(this.image, this.position.X - this.radius);
      Canvas.SetTop(this.image, this.position.Y - this.radius);
    }

    public double distanceFrom(Point point) {
      double dx = this.position.X - point.X;
      double dy = this.position.Y - point.Y;
      return Math.Sqrt(dx * dx + dy * dy);
    }

    public double getRadius() { return this.radius; }
    public Point getPosition() { return this.position; }
    public Image getImage() { return this.image; }
  }
}
