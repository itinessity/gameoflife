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

        /// <summary>
        /// If near are N alive cell, then new cell can be created.
        /// </summary>
        /// <param name="alives">Count life sell for giving new cell</param>
        public BirthRule(byte alives)
        {
            CountAliveCells = alives;
        }
    }
}
