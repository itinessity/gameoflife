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

        public void Run()
        {
            var countsteps = 0;

            while (Algoritm.Step())
            {
                countsteps++;
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
    }
}
