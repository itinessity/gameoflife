using GameOfLife.Cells;
using GameOfLife.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Game
    {
        private LifeAlgoritm Algoritm { get; set; }

        public int CountSteps { get; set; }

        public Game()
        {
            var birthrule = new BirthRule(3);
            var deathrule = new DeathRule(0,5);
            var rule = new LifeRule()
            {
                CountNeibours = 8,
                RuleForDeath = deathrule,
                RuleForBirth = birthrule
            };

            Algoritm = new LifeAlgoritm(rule);
        }


        public bool Step()
        {
            CountSteps++;
            return Algoritm.Step();
        }

        public void Run()
        {
            var CountSteps = 0;

            while (Algoritm.Step())
            {
                CountSteps++;
            }
        }

        public List<LifeCell> GetItems()
        {
            return Algoritm.Now.AliveCells;
        }

        public void SetItems(List<LifeCell> items)
        {
            Algoritm.Now.AliveCells.AddRange(items);
        }

        public void AddItem(int x, int y)
        {
            var newcell = new LifeCell()
            {
                Coordinate = new Point(x, y)
            };

            Algoritm.Now.AliveCells.Add(newcell);
        }

    }
}
