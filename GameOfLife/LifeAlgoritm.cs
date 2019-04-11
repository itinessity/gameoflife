using GameOfLife.Cells;
using GameOfLife.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class LifeAlgoritm
    {
        public LifeContainer Now { get; private set; }

        private LifeContainer Previous { get; set; }

        private LifeRule Rule { get; set; }

        public LifeAlgoritm(LifeRule gamerule)
        {
            Rule = gamerule;
        }

        public bool Step()
        {
            Previous = Now.Copy();

            StepDeath();

            StepLife();

           return StepCheck();
        }

        public void StepDeath()
        {
            var surroundcheck = new LifeSurround(Now);

            foreach(var cell in Now.AliveCells)
            {
                var surroundings = surroundcheck.Get(cell);

                if (surroundings.Count() < Rule.RuleForDeath.CountMinLifeCells 
                    && surroundings.Count() > Rule.RuleForDeath.CountMaxLifeCells)
                {
                    Now.AliveCells.Remove(cell);
                }
            }
        }

        public void StepLife()
        {
            var NewLife = new List<LifeCell>();

            var surroundcheck = new LifeSurround(Now);

            foreach (var cell in Now.AliveCells)
            {
                var surroundings = surroundcheck.GetPotential(cell);

                foreach (var maybecell in surroundings)
                {
                    var surroundingspotential = surroundcheck.Get(maybecell);

                    if (surroundingspotential.Count() >= Rule.RuleForBirth.CountAliveCells)
                    {
                        NewLife.Add(maybecell);
                    }
                }
            }

            Now.AliveCells.AddRange(NewLife);
        }

        public bool StepCheck()
        {
            var result = true;

            if (Now.AliveCells.Count == 0)
                result = false;

            if (Now == Previous)
                result = false;

            return result;
        }
    }
}
