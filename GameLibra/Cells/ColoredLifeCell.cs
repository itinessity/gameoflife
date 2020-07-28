using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameOfLife.Cells
{
    public class ColoredLifeCell: LifeCell
    {
        public Color CellColor { get; set; }

        public override bool Equals(object obj)
        {
            var result = base.Equals(obj);

            if (obj is ColoredLifeCell cell)
            {
                if (cell.CellColor == this.CellColor)
                    result = result && true;
                else
                    result = false;
            }

            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override LifeCell Copy()
        {
            var newcell = new ColoredLifeCell
            {
                Coordinate = this.Coordinate,
                CellColor = this.CellColor
            };

            return newcell;
        }

    }
}
