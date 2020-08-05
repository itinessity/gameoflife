using GameOfLife.Cells;
using GameOfLife.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Point = GameOfLife.Cells.Point;

namespace GameOfLife
{
   public class MainViewModel: BindableBase
    {
        public LifeView View { get; set; }

        private Game LifeGame;

        public ICommand RunCommand => new RelayCommand(() => Run());
        public ICommand ClearCommand => new RelayCommand(() => Clear());

        public int StepCount
        {
            get => Get<int>("StepCount");
            set => Set<int>("StepCount",value);
        }

        public MainViewModel()
        {
            LifeGame = new Game();

            View = new LifeView(LifeGame);
        }

        private void Run()
        {
            var result = View.Next();
            StepCount = View.Generation;

            if (!result)
                MessageBox.Show("Game is over!");
        }

        private void Clear()
        {
            StepCount = 0;
            View.Clear();
        }

    }
}
