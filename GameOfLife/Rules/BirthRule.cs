using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Rules
{
    public class BirthRule
    {
        public byte CountAliveCells { get; private set; }

        public BirthRule(byte alives)
        {
            CountAliveCells = alives;
        }
    }
}
