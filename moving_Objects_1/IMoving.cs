using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace moving_Objects_1
{
    interface IMoving
    {
        void draw(UIElementCollection drawArea);
        void move(int minY, int maxY);
    }
}
