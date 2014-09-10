using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PhoneApp2.src {
  public class Segment {
    public Point a;
    public Point b;
    public Segment(Point a, Point b) {
      this.a = a;
      this.b = b;
    }

    public double length() {
      double x = a.X - b.X;
      double y = a.Y - b.Y;
      return Math.Sqrt(x*x + y*y);
    }

    public double distanceFrom(Point c) {
      if (isPerpendicular(c)) return Math.Abs(Distance(a, b, c));
      return Math.Min(distance(a, c), distance(b, c));
    }

    private double cross(Point c) {
      Point ab = new Point(b.X - a.X, b.Y - a.Y);
      return ab.X * (c.Y - a.Y) - ab.Y * (c.X - a.X);
    }

    private double distance(Point a, Point b) {
      double dx = a.X - b.X;
      double dy = a.Y - b.Y;
      return Math.Sqrt(dx * dx + dy * dy);
    }

    private double Distance(Point a, Point b, Point c) {
      // normalize points
      Point cn = new Point(c.X - a.X, c.Y - a.Y);
      Point bn = new Point(b.X - a.X, b.Y - a.Y);

      double angle = Math.Atan2(bn.Y, bn.X) - Math.Atan2(cn.Y, cn.X);
      double abLength = Math.Sqrt(bn.X * bn.X + bn.Y * bn.Y);

      return Math.Sin(angle) * abLength;
    }

    public Boolean onTop(Point p) { return a.Y == b.Y && p.Y < a.Y; }
    public Boolean onBottom(Point p) { return a.Y == b.Y && p.Y > a.Y; }
    public Boolean onLeft(Point p) { return a.X == b.X && p.X < a.X; }
    public Boolean onRight(Point p) { return a.X == b.X && p.X > a.X; }
    public Boolean isVertical() { return a.X == b.X; }
    public Boolean isHorizontal() { return a.Y == b.Y; }

    public Segment clone() {
      return new Segment(new Point(a.X, a.Y), new Point(b.X, b.Y));
    }

    public Vector toVector() { return new Vector(b.X - a.X, b.Y - a.Y); }

    public void move(Point motion) {
      this.a.X += motion.X;
      this.a.Y += motion.Y;
      this.b.X += motion.X;
      this.b.Y += motion.Y;
    }

    public Boolean isPerpendicular(Point c) {
      Segment ac = new Segment(a, c);
      Segment bc = new Segment(b, c);
      return this.toVector().dotProduct(ac.toVector()) * this.toVector().dotProduct(bc.toVector()) < 0;
    }

    public Boolean hasIntersectionWith(Segment segment) {
      Point c = segment.a, d = segment.b;
      return isPerpendicular(c) && isPerpendicular(d) && segment.isPerpendicular(a);
    }

    public Point getDirection() {
      Point direction = new Point(0, 0);
      if (a.X == b.X) {
        direction.Y = 1;
      }
      else if (a.Y == b.Y) {
        direction.X = 1;
      }
      else {
        direction.X = 1 / Math.Abs(b.X - a.X);
        direction.Y = 1 / Math.Abs(b.Y - a.Y);
      }
      return direction;
    }

    private Boolean ccw(Point a, Point b, Point c) {
      return (c.Y - a.Y) * (b.X - a.X) > (b.Y - a.Y) * (c.X - a.X);
    }

    public String toString() {
      return "Segment: A(" + a.X + "," + a.Y + "), B(" + b.X + "," + b.Y + ")";
    }
  }
}
