using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Rules
{
    public sealed class DeathRule
    {
        public byte CountMaxLifeCells { get; private set; }

        public byte CountMinLifeCells { get; private set; }

        /// <summary>
        /// If alive cells are more then N and less then M then cell is dying
        /// </summary>
        /// <param name="min">Minimum alive count</param>
        /// <param name="max">Maximum alive count</param>
        public DeathRule(byte min, byte max)
        {
            CountMinLifeCells = min;
            CountMaxLifeCells = max;
        }
    }
}
