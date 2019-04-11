using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Cells
{
    public class LifeCell
    {
      public Point Coordinate { get; set; }

        public virtual LifeCell Copy()
        {
            var newcell = new LifeCell
            {
                Coordinate = this.Coordinate
            };

            return newcell;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (obj is LifeCell cell)
            {
                if (cell.Coordinate.Equals(this.Coordinate))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
