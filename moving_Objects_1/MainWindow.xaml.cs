using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace moving_Objects_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random r = new Random();
        private List<IMoving> movingDots { get; set; } = new List<IMoving>();
        private bool animationActive { get; set; } = false;
        public MainWindow()
        {
            InitializeComponent();
            initUpdateThread();
        }
        /// <summary>
        /// Neuen thread deklarieren und eröffnen.
        /// </summary>
        private void initUpdateThread()
        {
            Thread childThread = new Thread(new ThreadStart(doUpdate));
            childThread.Start();
        }
        /// <summary>
        /// Festlegen, das in der Updatephase passieren soll.
        /// Hier: alles entfernen, alles neuzeichnen.
        /// anschließend schlafend für 10 Milisekunden.
        /// </summary>
        private void doUpdate()
        {
            while (true)
            {
                if (animationActive)
                {
                    Console.WriteLine("animating");
                    moveAll();
                    drawAll();
                }
                Thread.Sleep(10);
            }
        }
        /// <summary>
        /// Erstellung der Kreis-Objekte
        /// num als Anzahl der zu erstellenden Objecte.
        /// Es werden objecte von der Klasse MovingCircle erstellt und diese dann in einer Liste gespeichert.
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="num"></param>
        public void initDots(int radius, int num)
        {
            for(int i = 0; i<num; i++)
            {
                MovingCircle dot = new MovingCircle();
                dot.color = new SolidColorBrush(Color.FromRgb((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255)));
                dot.radius = radius;
                dot.X = r.Next(0, (int)GamingArea.ActualWidth);
                dot.Y = r.Next(0, (int)GamingArea.ActualHeight);

                dot.velocity = r.Next(1, 20);
                dot.directionup = r.Next(2) == 0;

                movingDots.Add(dot);
                
            }
        }
        /// <summary>
        /// Erstellung der Viereck-Objekte.
        /// Es werden Objekte der Klasse MovingRectangle erstellt und diese in einer Liste gespeichert.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="length"></param>
        /// <param name="num"></param>
        public void initRectangle(int height, int length, int num)
        {
            for(int i=0; i< num; i++)
            {
                MovingRectangle rec = new MovingRectangle();
                rec.color = new SolidColorBrush(Color.FromRgb((byte)r.Next(0, 255), (byte)r.Next(0, 255), (byte)r.Next(0, 255)));
                rec.height = height;
                rec.length = length;
                rec.X = r.Next(0, (int)GamingArea.ActualWidth);
                rec.Y = r.Next(0, (int)GamingArea.ActualHeight);

                rec.velocity = r.Next(1, 20);
                rec.directionup = r.Next(2) == 0;
                movingDots.Add(rec);

            }
        }
        /// <summary>
        /// Ruft einen Dispatcher auf. Dies ist notwendig, da das geschehen innerhalb von einem Thread stattfindet. Die änderung aber außerhalb des Threads stattfinden sollen.
        /// </summary>
        public void drawAll()
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    drawAllInvoke();
                });
            }
            catch(TaskCanceledException ignored)
            {
                
            }
        }


        public void drawAllInvoke()
        {
            GamingArea.Children.Clear();
            foreach (IMoving dot in movingDots)
            {
                dot.draw(GamingArea.Children);
            }
        }

        public void moveAll()
        {
            foreach(IMoving dot in movingDots)
            {
                dot.move(0, (int)GamingArea.ActualHeight);
            }
        }

        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            animationActive = !animationActive;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
           
           
            int num = int.Parse(AnzahlEingabe.Text);
            if (RadiusText.Visibility == Visibility.Visible)
            {
                int radius = int.Parse(RadiusEingabe.Text);
                initDots(radius, num); }
            else
            {
                int height = int.Parse(BreiteEingabe.Text);
                int length = int.Parse(LaengeEingabe.Text);
                initRectangle(length, height, num); 
            }
            StartStop.Visibility = Visibility.Visible;
            drawAll();
        }

        private void Step_Click(object sender, RoutedEventArgs e)
        {
            moveAll();
            drawAll();
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            RadiusText.Visibility = Visibility.Hidden;
            RadiusEingabe.Visibility = Visibility.Hidden;
            AnzahlText.Visibility = Visibility.Hidden;
            AnzahlEingabe.Visibility = Visibility.Hidden;
            BreiteEingabe.Visibility = Visibility.Hidden;
            LaengeEingabe.Visibility = Visibility.Hidden;
            BreiteText.Visibility = Visibility.Hidden;
            AnzahlEingabe.Text = "";
            BreiteEingabe.Text = "";
            LaengeEingabe.Text = "";
            RadiusEingabe.Text = "";
            RechteckBild.Visibility = Visibility.Visible;
            KreisBild.Visibility = Visibility.Visible;
            GamingArea.Children.Clear();
            movingDots.Clear();
        }


        private void Mouseclick_Circle(object sender, MouseButtonEventArgs e)
        {
            RadiusText.Visibility = Visibility.Visible;
            RadiusEingabe.Visibility = Visibility.Visible;
            AnzahlText.Visibility = Visibility.Visible;
            AnzahlEingabe.Visibility = Visibility.Visible;
            RechteckBild.Visibility = Visibility.Hidden;
            KreisBild.Visibility = Visibility.Hidden;
        }

        private void Rectangleclick(object sender, MouseButtonEventArgs e)
        {
            AnzahlText.Visibility = Visibility.Visible;
            BreiteEingabe.Visibility = Visibility.Visible;
            LaengeEingabe.Visibility = Visibility.Visible;
            AnzahlEingabe.Visibility = Visibility.Visible;
            BreiteText.Visibility = Visibility.Visible;
            RechteckBild.Visibility = Visibility.Hidden;
            KreisBild.Visibility = Visibility.Hidden;
        }
    }
}
