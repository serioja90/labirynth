using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using PhoneApp2.src;
using Microsoft.Devices.Sensors;
using System.Diagnostics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Common;
using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework;

namespace PhoneApp2 {
  public partial class Game : PhoneApplicationPage {
    private Accelerometer accelerometer;
    private int currentLevel;
    private Ball ball;
    private Level level;
    private int updateRate = 20; //ms
    DispatcherTimer timer;
    World world;
    Body ballBody;
    Microsoft.Xna.Framework.Vector3 currentAcceleration;
    public Game() {
      InitializeComponent();
      accelerometer = new Microsoft.Devices.Sensors.Accelerometer();
      accelerometer.TimeBetweenUpdates += TimeSpan.FromMilliseconds(updateRate);
      accelerometer.CurrentValueChanged += OnCurrentValueChanged;
      accelerometer.Start();
      ConvertUnits.SetDisplayUnitToSimUnitRatio(50f);
      world = new World(Vector2.Zero);
      LayoutRoot.SizeChanged += onGridSizeChanged;
      timer = new DispatcherTimer();
      timer.Interval = TimeSpan.FromMilliseconds(updateRate);
      timer.Tick += onTick;
      timer.Start();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e) {
      String levelParam = NavigationContext.QueryString["level"].ToString();
      Debug.WriteLine("LEVEL: " + levelParam);
      currentLevel = levelParam == null ? 1 : Int16.Parse(levelParam);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e) {
      if (timer != null) {
        timer.Stop();
      }
      if (accelerometer != null && accelerometer.State == SensorState.Ready) {
        accelerometer.Stop();
      }
    }


    private void onGridSizeChanged(object sender, SizeChangedEventArgs e) {
      Debug.WriteLine("width: " + LayoutRoot.ActualWidth + " height: " + LayoutRoot.ActualHeight);
      if (ball == null) {
        float width = (float)LayoutRoot.ActualWidth;
        float height = (float)LayoutRoot.ActualHeight;
        ballBody = BodyFactory.CreateBody(world, new Vector2(ConvertUnits.ToSimUnits(width / 2f), ConvertUnits.ToSimUnits(height / 2f)));
        ballBody.BodyType = BodyType.Dynamic;
        ballBody.LinearDamping = 1f;
        CircleShape circleShape = new CircleShape(ConvertUnits.ToSimUnits(8f), 1f);
        Fixture fixture = ballBody.CreateFixture(circleShape);
        fixture.OnCollision += OnBalCollision;
        ball = new Ball(canvas, 8, new System.Windows.Point(width / 2f, height / 2f));
      }
      if (level == null) {
        level = new Level(currentLevel, canvas);
        Body wallBody;
        System.Windows.Point position;
        foreach (Wall wall in level.getWalls()) {
          position = wall.getPosition();
          wallBody = BodyFactory.CreateRectangle(
            world, 
            ConvertUnits.ToSimUnits(wall.getWidth()),
            ConvertUnits.ToSimUnits(wall.getHeight()),
            0.001f,
            new Vector2(ConvertUnits.ToSimUnits(position.X + wall.getWidth() / 2f), ConvertUnits.ToSimUnits(position.Y + wall.getHeight() / 2f))
          );
          wallBody.Restitution = 0.25f;
        }
        ballBody.Position = new Vector2(ConvertUnits.ToSimUnits(level.getStart().X), ConvertUnits.ToSimUnits(level.getStart().Y));
        ball.setPosition(new System.Windows.Point(ConvertUnits.ToDisplayUnits(ballBody.Position.X), ConvertUnits.ToDisplayUnits(ballBody.Position.Y)));
      }
    }

    private bool OnBalCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact) {
      return true;
    }

    private void onTick(object sender, System.EventArgs e) {
      if (ball != null && level != null && currentAcceleration != null) {
        ballBody.ApplyForce(new Vector2(currentAcceleration.X * 9.8f, -currentAcceleration.Y * 9.8f));
        world.Step(updateRate / 1000.0f);
        Dispatcher.BeginInvoke(() => {
          ball.setPosition(new System.Windows.Point(ConvertUnits.ToDisplayUnits(ballBody.Position.X), ConvertUnits.ToDisplayUnits(ballBody.Position.Y)));
        });
        if (ball.distanceFrom(level.getFinish()) <= ball.getRadius()/2.0f) {
          // level completed, go to next level
          timer.Stop();
          accelerometer.Stop();
          Debug.WriteLine("LEVEL COMPLETED");
          MessageBox.Show("Livello completato!", "Cogratulazioni!", MessageBoxButton.OK);
          AppSettings settings = AppSettings.loadSettings();
          settings.unlockLevel(level.getLevel() + 1);
          NavigationService.Navigate(new Uri("/Game.xaml?level=" + (settings.getLevel()), UriKind.Relative));
          AppSettings.saveSettings(settings);
          NavigationService.RemoveBackEntry();
        }
      }
    }


    private void OnCurrentValueChanged(object sender, Microsoft.Devices.Sensors.SensorReadingEventArgs<Microsoft.Devices.Sensors.AccelerometerReading> e) {
      AccelerometerReading reading = e.SensorReading;
      currentAcceleration = reading.Acceleration;
    }
  }
}