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
    static BitmapImage imageSource;
    protected Canvas container;
    protected Point position;
    protected ImageBrush image;
    protected double width, height;
    protected Canvas imageContainer;
    protected TranslateTransform transform;
    protected ScaleTransform st;

    static Wall() {
      imageSource = new BitmapImage(new Uri("resources/img/wall.jpg", UriKind.Relative));
    }

    public Wall(Canvas container, Point position, double width, double height) {
      this.container = container;
      this.position = position;
      this.width = width;
      this.height = height;
      this.image = new ImageBrush();
      Debug.WriteLine("IMG width: " + imageSource.PixelWidth + ", height: " + imageSource.PixelHeight);
      this.image.ImageSource = imageSource;
      this.image.Stretch = Stretch.None;
      transform = new TranslateTransform();
      transform.X = -this.position.X;
      transform.Y = -this.position.Y;
      st = new ScaleTransform();
      st.ScaleX = 1.55;
      st.ScaleY = 2;
      this.image.RelativeTransform = st;
      this.image.Transform = transform;
      this.imageContainer = new Canvas();
      this.imageContainer.Width = width;
      this.imageContainer.Height = height;
      this.imageContainer.Background = this.image;
      this.container.Children.Add(this.imageContainer);
      Canvas.SetLeft(this.imageContainer, this.position.X);
      Canvas.SetTop(this.imageContainer, this.position.Y);
      Canvas.SetZIndex(this.imageContainer, 2);
    }

    public Point getPosition() { return this.position; }
    public double getWidth() { return this.width; }
    public double getHeight() { return this.height; }
  }
}