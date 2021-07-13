using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace moving_Objects_1
{
    public class MovingRectangle : IMoving
    {
        public double Y { get; set; }
        public double X { get; set; }
        public int length { get; set; }
        public int height { get; set; }
        public bool directionUp { get; set; }
        public double velocity { get; set; }
        public SolidColorBrush color { get; set; }

        public void draw(UIElementCollection drawArea)
        {

            Rectangle rec = new Rectangle();
            rec.Width = length;
            rec.Height = height;
            rec.Fill = color;

            Canvas.SetLeft(rec, X);
            Canvas.SetTop(rec, Y);
            drawArea.Add(rec);
        }

        public void move(int minY, int maxY)
        {
            throw new NotImplementedException();
        }
    }
}
