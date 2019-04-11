using GameOfLife.Cells;
using GameOfLife.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class LifeSurround
    {
        private LifeContainer Cells { get; set; }

        private LifeRule Rule { get; set; }

        public LifeSurround(LifeContainer alicecells)
        {
            Cells = alicecells;

        }

        public IEnumerable<LifeCell> Get(LifeCell cell)
        {
            var area = Surroundigs(cell.Coordinate);

            var result = Cells.AliveCells.Where(x => area.Contains(x.Coordinate)).Take(Rule.CountNeibours);

            return result;
        }

        public IEnumerable<LifeCell> GetPotential(LifeCell cell)
        {
            var result = new List<LifeCell>();

            var area = Surroundigs(cell.Coordinate);
            var notlive = area.Where(x => !Cells.AliveCells.Select(y => y.Coordinate).Contains(x));

            foreach (var point in notlive)
            {
                result.Add(new LifeCell() { Coordinate = point});
            }

            return result;
        }

        private IEnumerable<Point> Surroundigs(Point point)
        {
            var result = new List<Point>
            {
                new Point(point.X, point.Y + 1),
                new Point(point.X, point.Y - 1),
                new Point(point.X-1, point.Y),
                new Point(point.X+1, point.Y),
                new Point(point.X+1, point.Y+1),
                new Point(point.X-1, point.Y-1),
                new Point(point.X+1, point.Y-1),
                new Point(point.X-1, point.Y+1),
            };

            return result;
        }

    }
}
