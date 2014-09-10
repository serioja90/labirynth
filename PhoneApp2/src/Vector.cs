using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace PhoneApp2.src {
  public class Vector {
    public double x;
    public double y;
    public Point origin;
    public Vector(double x, double y) {
      this.x = x;
      this.y = y;
      this.origin = new Point(0, 0);
    }

    public Vector(double x, double y, Point origin) {
      this.x = x;
      this.y = y;
      this.origin = origin;
    }

    public double dotProduct(Vector v) {
      return x * v.x + y * v.y;
    }
  }
}
