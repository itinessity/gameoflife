using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Rules
{
    public class LifeRule
    {
        public byte CountNeibours { get; set; }

        public DeathRule RuleForDeath { get; set; }

        public BirthRule RuleForBirth { get; set; }

    }
}
