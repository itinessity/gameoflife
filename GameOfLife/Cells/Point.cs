using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Cells
{
   public sealed class Point
    {
        /// <summary>
        /// Координата по горизонтали
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Координата по вертикали
        /// </summary>
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Point)
            {
                if (this.X == X && this.Y == Y)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return X * Y;
        }
    }
}
