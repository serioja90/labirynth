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
    private double speedX;
    private double speedY;
    private VibrateController vibrate;
    private double friction;
    EllipseGeometry geometry;
    public Ball(Canvas container, double radius, Point position) {
      this.vibrate = VibrateController.Default;
      this.friction = 0.5;
      this.speedX = 0;
      this.speedY = 0;
      this.container = container;
      this.radius = radius;
      this.position = position;
      this.image = new Image();
      this.image.Source = new BitmapImage(new Uri("resources/img/ball.png", UriKind.Relative));
      this.image.Width = radius * 2;
      this.image.Height = radius * 2;
      this.container.Children.Add(this.image);
      this.setPosition(position);
      this.geometry = new EllipseGeometry();
      this.geometry.Center = position;
      this.geometry.RadiusX = radius;
      this.geometry.RadiusY = radius;
    }

    public void setRadius(double radius) {
      this.radius = radius;
      this.image.Width = radius * 2;
      this.image.Height = radius * 2;
      this.geometry.RadiusX = radius;
      this.geometry.RadiusY = radius;
    }

    public void setPosition(Point position) {
      this.position = position;
      Canvas.SetLeft(this.image, this.position.X - this.radius);
      Canvas.SetTop(this.image, this.position.Y - this.radius);
      Debug.WriteLine("Width: " + this.container.ActualWidth + ", Height: " + this.container.ActualHeight);
    }

    public void update(double elapsedMilliseconds, Microsoft.Xna.Framework.Vector3 acceleration, Wall[] walls) {
      double seconds = elapsedMilliseconds / 35;
      double accelX = acceleration.X * 9.8;
      double accelY = -acceleration.Y * 9.8;
      this.speedX += accelX * seconds;
      this.speedY += accelY * seconds;
      this.speedX -= Math.Abs(this.speedX) > this.friction * seconds ? Math.Sign(this.speedX) * this.friction * seconds : this.speedX;
      this.speedY -= Math.Abs(this.speedY) > this.friction * seconds ? Math.Sign(this.speedY) * this.friction * seconds : this.speedY;
    }

    public double getRadius() { return this.radius; }
    public Point getPosition() { return this.position; }
    public Image getImage() { return this.image; }
  }
}
