using GameOfLife.Cells;
using GameOfLife.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameOfLife
{
   public class MainViewModel
    {
        public Game LifeGame;

        public ObservableCollection<LifeCell> ItemsToDraw { get; set; }

        public ICommand RunCommand => new RelayCommand(() => Run());

        public MainViewModel()
        {
            LifeGame = new Game();

            ItemsToDraw = new ObservableCollection<LifeCell>();
            //cross

            var cross = new List<LifeCell>();
            cross.Add(new LifeCell() { Coordinate = new Point(0, 1) });
            cross.Add(new LifeCell() { Coordinate = new Point(-1, 0) });
            cross.Add(new LifeCell() { Coordinate = new Point(1, 0) });
            cross.Add(new LifeCell() { Coordinate = new Point(0, -1) });

            LifeGame.SetItems(cross);
        }


        public void Run()
        {
            ItemsToDraw = new ObservableCollection<LifeCell>(LifeGame.GetItems());
            LifeGame.Run();
        }
    }
}
