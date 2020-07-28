using GameOfLife.Cells;
using System;
using System.Collections.Generic;

namespace GameOfLife
{
    public class LifeContainer
    {
        public List<LifeCell> AliveCells { get; set; }

        public LifeContainer()
        {
            AliveCells = new List<LifeCell>();
        }

        public LifeContainer Copy()
        {
            var copy = new LifeContainer();

            foreach (var cell in AliveCells)
            {
                copy.AliveCells.Add(cell.Copy());
            }

            return copy;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is LifeContainer lifecontainer)
            {
                if (lifecontainer.AliveCells.Count != this.AliveCells.Count)
                    return false;

                foreach (var cell in this.AliveCells)
                {
                    var check = !lifecontainer.AliveCells.Contains(cell);

                    if (check)
                        return false;
                }

                return true;
            }
            else
                return false;
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
