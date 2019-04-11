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

        public DeathRule(byte min, byte max)
        {
            CountMinLifeCells = min;
            CountMinLifeCells = max;
        }
    }
}
