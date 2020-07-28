using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.View
{
    public class LifeModel
    {
		LifeTorus _current;
		Dimensions _dim;
		Game _life;
		
		public bool CanGoNext { get; set; }

		public LifeModel(Dimensions dim, Game life)
		{
			_dim = dim;
			_current = new LifeTorus(dim);
			_life = life;
			SetLife();
		}

		private void SetLife()
        {
			_current.Clear();
			foreach(var cell in _life.GetItems())
            {
				if (cell.Coordinate.X <= _dim.Width && cell.Coordinate.Y <= _dim.Height)
					_current[cell.Coordinate.X, cell.Coordinate.Y] = true;
			}
        }

		private void IputLife(int x, int y)
        {
			var alive = _life.GetItems();
			
			if (alive.Where(c => c.Coordinate.X == x && c.Coordinate.Y == y).Count() == 0)
            {
				_life.AddItem(x, y);
            }

		}

		public Dimensions Dimension
		{
			get { return _dim; }
			set
			{
				if (value == _dim)
					return;
				LifeTorus lt = new LifeTorus(value);
				_current.CopyTo(lt);
				_current = lt;
				_dim = value;
			}
		}

		public void Clear()
		{
			_current.Clear();
		}

		public bool this[int x, int y]
		{
			get
			{
				return _current[x, y];
			}
			set
			{
				_current[x, y] = value;

				if (value)
					IputLife(x, y);
			}
		}


		public bool Next()
		{
			bool changed = false;
			var prev = new LifeTorus(Dimension);
			_current.CopyTo(prev);

			CanGoNext = _life.Step();

			SetLife();

			if (!prev.Equals(_current))
				changed = true;

			return changed;
		}
	}

}
