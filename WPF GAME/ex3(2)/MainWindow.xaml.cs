using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ex3_2_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        System.Media.SoundPlayer playerstart = new System.Media.SoundPlayer();
        System.Media.SoundPlayer bah = new System.Media.SoundPlayer();
        int multiply = 1;
        public class Asteroid: UIElement
        {
            private static Random random = new Random();
            public int Width = random.Next(40, 70);
            public int Height = random.Next(30, 50);
            public double x;
            public double y;
            public double angle;
            public Rectangle rect;
            double speed = 80;
            const double speedmax = 100;
            const double speedmin = 50;
            
            double angularspeed;
            public Asteroid()
            {
                rect = new Rectangle();
                ImageBrush AsteroidSkin = new ImageBrush();
                switch (random.Next(3))
                {
                    case 0:
                        AsteroidSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/as1.png"));
                        //asteroids[asteroids.Count - 1].Width = random.Next(60, 65);
                       // asteroids[asteroids.Count - 1].Height = random.Next(60, 65);
                        break;
                    case 1:
                        AsteroidSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/as2.png"));
                        break;
                    case 2:
                        AsteroidSkin.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/as3.png"));
                        break;
                }
                rect.Fill = AsteroidSkin;
                rect.RenderTransformOrigin = new Point(0.5, 0.5);
                rect.Width = this.Width;
                rect.Height = this.Height;
                x = random.Next(1200);
               
                
                y = 0;
                angle = 0;
                speed = 80;
                angularspeed = 2;

            }
            public void Update(double deltaT, int multiply)
            {
                y += speed * deltaT/1000* multiply;
                angle += angularspeed * deltaT/1000;
                rect.RenderTransform = new RotateTransform(angle * 180 / Math.PI);
            }

        }
        DispatcherTimer timership = new DispatcherTimer();
        int directionX = 0, directionY = 0;
        double speedship = 7;
        //double x = 50, y = 70;
        bool flagmusic = false;
        bool flag = false;
        int i = 0;
        double n = 100;
        
        double k = 35;
        int tick = 0;

        List<Asteroid> asteroids = new List<Asteroid>();
        public MainWindow()
        {
            InitializeComponent();
          //  playerstart.Stop();
            player.SoundLocation = "game.wav";
            player.Load();
            player.Play();
            timership.Tick += new EventHandler(engine);
            timership.Interval = TimeSpan.FromMilliseconds(25);

        }

        private void GameArea_KeyDown(object sender, KeyEventArgs e)
        {
            directionX = (e.Key == Key.A) ? -1 : (e.Key == Key.D) ? 1 : 0;
            directionY = (e.Key == Key.W) ? -1 : (e.Key == Key.S) ? 1 : 0;
            flag = true;
        } 

        private void GameArea_KeyUp(object sender, KeyEventArgs e)
        {
            flag = false;
            switch (e.Key)
            {
                case Key.A:
                case Key.D:
                    directionX = 0;
                    break;
                case Key.S:
                case Key.W:
                    directionY = 0;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            player.Stop();
            playerstart.SoundLocation = "game_start.wav";
            playerstart.Load();
            playerstart.PlayLooping();
            multiply = 1;
            tick = 0;
            Score.Visibility = Visibility.Hidden;
            StartButton.Visibility = Visibility.Hidden;
            for (int i = 0; i <asteroids.Count; i++)
            {
                i = 0;
                GameArea.Children.Remove(asteroids[i].rect);
                asteroids.Remove(asteroids[i]);
            }
            timership.Start();
    
           
        }

        private void Run()
        {
            
           
                
        }

        private Double[] move(Double x0, Double y0, Double speed, Double deltaT)
        {
            Double[] output = new Double[3];
            
            
            Double x = x0 + speed * directionX * deltaT /1.2;
            Double y = y0 + speed * directionY * deltaT/2;
            
            output[0] = x;
            output[1] = y;
            return output;
        }

        int x = 0;
        private void engine(object sender, EventArgs e)
        {
            
            tick++;
            if (tick > 1000)
            { if (tick % 6 == 0)
                {
                    asteroids.Add(new Asteroid());
                    GameArea.Children.Add(asteroids[asteroids.Count - 1].rect);
                }
            }
            else
                if (tick % 25 == 0)
                {
                    asteroids.Add(new Asteroid());
                    GameArea.Children.Add(asteroids[asteroids.Count - 1].rect);
                }
            k -= 1;
            if (tick % 500 == 0)
                multiply++;
            for (int i = 0; i< asteroids.Count; i++)
            {
                if (Check(asteroids[i], Canvas.GetLeft(Spaceship), Canvas.GetBottom(Spaceship))) { timership.Stop();
                    
                    bah.SoundLocation = "bah.wav";
                    bah.Load();
                    bah.Play();
                    
                    StartButton.Content = "WASTED";
                    StartButton.Foreground = new SolidColorBrush(Colors.Red);
                    StartButton.Visibility = Visibility.Visible;  }
                 
                if (Canvas.GetTop(asteroids[i].rect) > 1000)
                {
                    GameArea.Children.Remove(asteroids[i].rect);
                    asteroids.Remove(asteroids[i]);
                    Score.Content = "Score: " + Convert.ToString(tick);
                    Score.Visibility = Visibility.Visible;
                }
                else
                {
                    Canvas.SetTop((asteroids[i].rect as Rectangle), asteroids[i].y);
                    Canvas.SetLeft((asteroids[i].rect as Rectangle), asteroids[i].x);

                }
                asteroids[i].Update(timership.Interval.Milliseconds, multiply);
            }
           /* tick++;
            if (tick < n)
                return;
            tick = 0;
            if (n < 5)
                n *= 0.9999;
            Random random = new Random();

            ImageBrush AsteroidSkin = new ImageBrush();
            asteroids.Add(new Rectangle());
            asteroids[asteroids.Count - 1].Width = ...
            asteroids[asteroids.Count - 1].Height = ...
            
            asteroids[asteroids.Count - 1].Fill = AsteroidSkin;
            GameArea.Children.Add(asteroids[asteroids.Count - 1]);
            Canvas.SetTop((asteroids[asteroids.Count - 1] as Rectangle), 0);
            Canvas.SetLeft((asteroids[asteroids.Count - 1] as Rectangle), random.Next(1200));
            // i++;

            i = 0;

            if (hasCollisionHappened) timership.Stop();*/
            if (flag)
            {
                try
                {

                    //foreach (var item in GameArea.Children)
                    //{
                    // if ((item as Rectangle).Name == "Spaceship")
                    //{
                    double[] coords = move(Canvas.GetLeft((Spaceship as Rectangle)), Canvas.GetTop((Spaceship as Rectangle)), 1, timership.Interval.Milliseconds);

                    if (coords[0] > -5 && coords[0] < 1220)
                        Canvas.SetLeft((Spaceship as Rectangle), coords[0]);
                    if (coords[1] > 435 && coords[1] < 710 - 100)
                        Canvas.SetTop((Spaceship as Rectangle), coords[1]);
                    //}
                    //}
                    // foreach(Rectangle asteroid in asteroids)
                    //           {
                    //                 double coordY = Canvas.GetTop(asteroid) * timership.Interval.Milliseconds / 1000 * 1.2;
                    //               Canvas.SetTop((asteroid), coordY);
                    //     }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "FAIL!");
                }
            }
           /* for (; i < asteroids.Count; i++)
            {
                if (Canvas.GetTop(asteroids[i]) == Canvas.GetBottom(Spaceship) ||
                    Canvas.GetBottom(asteroids[i]) == Canvas.GetTop(Spaceship) ||
                    Canvas.GetLeft(asteroids[i]) == Canvas.GetRight(Spaceship) ||
                    Canvas.GetRight(asteroids[i]) == Canvas.GetLeft(Spaceship))
                {
                    hasCollisionHappened = true;
                    MessageBox.Show("FAil!");
                }
                if (Canvas.GetTop(asteroids[i]) > 10000)
                {
                    GameArea.Children.Remove(asteroids[i]);
                    asteroids.RemoveAt(i);
                }
                else
                    Canvas.SetTop((asteroids[i] as Rectangle), Canvas.GetTop(asteroids[i])+ timership.Interval.Milliseconds * 1.2);
            }*/
            
            
        }
        private bool Check(Asteroid ast, double x, double y)
        {
            //double eps = 50;
            if (Canvas.GetTop(Spaceship) + Spaceship.Height < Canvas.GetTop(ast.rect))
                return false;
            else
                if (Canvas.GetTop(ast.rect) + ast.rect.Height > Canvas.GetTop(Spaceship) && ((Canvas.GetLeft(ast.rect) < Canvas.GetLeft(Spaceship) + Spaceship.Width && Canvas.GetLeft(ast.rect) > Canvas.GetLeft(Spaceship)) ||
                    (Canvas.GetLeft(ast.rect) + ast.Width > Canvas.GetLeft(Spaceship) && Canvas.GetLeft(ast.rect) + ast.Width < Canvas.GetLeft(Spaceship) + Spaceship.Width)))
            {
                
                return true; }
            else return false;
            
        }
    }
}
