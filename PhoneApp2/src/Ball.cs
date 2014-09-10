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
      if (this.position.X - this.radius + MARGIN < 0 && this.speedX < 0) {
        if (this.speedY <= -10) {
          vibrate.Start(TimeSpan.FromMilliseconds(100));
        }
        this.speedX = -this.speedX / 3;
        this.position.X = this.radius - MARGIN;
      }
      else if (this.position.X + this.radius - MARGIN > this.container.ActualWidth && this.speedX > 0) {
        if (this.speedX >= 10) {
          vibrate.Start(TimeSpan.FromMilliseconds(100));
        }
        this.speedX = -this.speedX / 3;
        this.position.X = this.container.ActualWidth - this.radius + MARGIN;
      }
      if (this.position.Y - radius + MARGIN < 0 && this.speedY < 0) {
        if (this.speedY <= -10) {
          vibrate.Start(TimeSpan.FromMilliseconds(100));
        }
        this.speedY = -this.speedY / 3;
        this.position.Y = this.radius - MARGIN;
      }
      else if (this.position.Y + this.radius - MARGIN > this.container.ActualHeight && this.speedY > 0) {
        if (this.speedY >= 10) {
          vibrate.Start(TimeSpan.FromMilliseconds(100));
        }
        this.speedY = -this.speedY / 3;
        this.position.Y = this.container.ActualHeight - this.radius + MARGIN;
      }
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
      Debug.WriteLine("speedX: " + this.speedX + " speedY: " + this.speedY);
      //detectCollision(walls, this.speedX * seconds, this.speedY * seconds);
      //simulateMovementSimple(walls, this.speedX * seconds, this.speedY * seconds);
      //this.collideWith(walls, new Point(this.position.X + this.speedX * seconds, this.position.Y + this.speedY * seconds));
    }


   /* private void simulateMovement(Wall[] walls, double dX, double dY) {
      double max = Math.Max(Math.Abs(dX), Math.Abs(dY));
      double xStep = dX / max * Math.Min(radius,Math.Abs(dX));
      double yStep = dY / max * Math.Min(radius,Math.Abs(dY));
      double xDistance = Math.Abs(dX);
      double yDistance = Math.Abs(dY);
      Point lastPosition = position;
      Debug.WriteLine("Sim movement start (" + position.X + "," + position.Y + ")");
      Debug.WriteLine("max: " + max + ", radius: " + radius + ", xStep: " + xStep + ", yStep: " + yStep);
      while (xDistance > 0 || yDistance > 0) {
        xDistance -= Math.Abs(xStep);
        yDistance -= Math.Abs(yStep);
        position.X += Math.Sign(xStep) * Math.Min(Math.Max(xDistance,0), Math.Abs(xStep));
        position.Y += Math.Sign(yStep) * Math.Min(Math.Max(yDistance,0), Math.Abs(yStep));
        foreach (Wall wall in walls) {
          foreach (Segment border in wall.getBorders()) {
            if (border.distanceFrom(position) <= radius) {
              if(border.onTop(lastPosition) || border.onBottom(lastPosition)){
                position.Y = border.a.Y - Math.Sign(speedY) * radius;
                speedY = -speedY / 3;
                yStep = -yStep / 3;
              }
              if(border.onLeft(lastPosition) || border.onRight(lastPosition)){
                position.X = border.a.X - Math.Sign(speedX) * radius;
                speedX = -speedX / 3;
                xStep = -xStep / 3;
              }
            }
          }
        }
        lastPosition = position;
      }
      setPosition(this.position);
      Debug.WriteLine("Sim movement end(" + position.X + "," + position.Y + ")");
    }

    private void simulateMovementSimple(Wall[] walls, double dX, double dY) {
      Point last = new Point(position.X, position.Y);
      Segment horizontal = null, vertical = null;
      Segment trajectory;
      double nearestHorizontal = Double.MaxValue, nearestVertical = Double.MaxValue;
      double distance;
      position.X += dX;
      position.Y += dY;
      trajectory = new Segment(last, position);
      foreach (Wall wall in walls) {
        foreach (Segment border in wall.getBorders()) {
          distance = border.distanceFrom(position);
          if (border.hasIntersectionWith(trajectory)) {
            Debug.WriteLine("Collision: border((" + border.a.X + "," + border.a.Y + "),(" + border.b.X + "," + border.b.Y + ")), position(" + position.X + "," + position.Y + "), distance: " + border.distanceFrom(position));
            if (border.isHorizontal() && (border.onTop(last) || border.onBottom(last)) && distance < nearestHorizontal) {
              nearestHorizontal = distance;
              horizontal = border;
            }
            if( border.isVertical() &&  (border.onLeft(last) || border.onRight(last)) && distance < nearestVertical) {
              nearestVertical = distance;
              vertical = border;
            }
          }
        }
      }
      if (horizontal != null) {
        position.Y = horizontal.a.Y - Math.Sign(speedY) * radius;
        speedY = -speedY / 3;
      }
      if (vertical != null) {
        position.X = vertical.a.X - Math.Sign(speedX) * radius;
        speedX = -speedX / 3;
      }
      setPosition(this.position);
    }


    private void collideWith(List<Wall> walls, Point newPosition) {
      Point direction = new Point(0, 0);
      Point tmp;
      Point a = new Point(this.position.X, this.position.Y);
      Point b = new Point(newPosition.X + Math.Sign(speedX) * radius, newPosition.Y + Math.Sign(speedY) * radius);
      Segment trajectory = new Segment(a,b);
      this.position = newPosition;
      Debug.WriteLine("================Detecting collisions=============");
      foreach(Wall wall in walls){
        foreach (Segment border in wall.getBorders()) {
          if (trajectory.hasIntersectionWith(border)) {
            tmp = bounceFrom(border);
            direction.X += tmp.X;
            direction.Y += tmp.Y;
          }
        }
      }
      if (direction.Y != 0) {
        speedX = -speedX / 3;
      }
      if (direction.X != 0) {
        speedY = -speedY / 3;
      }
      setPosition(this.position);
      Debug.WriteLine("================END=============");
    }

    private Point bounceFrom(Segment border) {
      Point direction = border.getDirection();
      Debug.WriteLine("Collision detected!");
      Debug.WriteLine("Ball position: (" + position.X + "," + position.Y + ")");
      Debug.WriteLine("Direction: (" + direction.X + "," + direction.Y + ")");
      Debug.WriteLine(border.toString());
      if (direction.X != 0 && border.distanceFrom(position) > border.distanceFrom(new Point(position.X, border.a.Y - Math.Sign(speedY) * radius))) {
        position.Y = border.a.Y - Math.Sign(speedY) * radius;
      }
      if (direction.Y != 0 && border.distanceFrom(position) > border.distanceFrom(new Point(border.a.X - Math.Sign(speedX) * radius, position.Y))) {
        position.X = border.a.X - Math.Sign(speedX) * radius;
      }
      return direction;
    }*/

    public double getRadius() { return this.radius; }
    public Point getPosition() { return this.position; }
    public Image getImage() { return this.image; }
  }
}
