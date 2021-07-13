using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace moving_Objects_1
{
    public class MovingCircle : IMoving
    {
        public double Y { get; set; }
        public double X { get; set; }
        public int radius { get; set; }
        public bool directionup { get; set; }

        public double velocity { get; set; }

        public SolidColorBrush color { get; set; }

        public void move(int minY, int maxY)
        {
            this.Y += (directionup ? -1 : 1) * velocity;

            if(this.Y > maxY || this.Y < minY)
            {
                directionup = !directionup;
            }
        }
        public void draw(UIElementCollection drawArea)
        {
            Ellipse circle = new Ellipse();
            circle.Width = radius;
            circle.Height = radius;
            circle.Fill = color;

            Canvas.SetLeft(circle, X);
            Canvas.SetTop(circle, Y);

            drawArea.Add(circle);
        }
    }
}
