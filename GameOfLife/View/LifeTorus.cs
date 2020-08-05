using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.View
{
    public class LifeTorus
    {
		BitArray _array;
		int _width;
		int _height;

		public LifeTorus(Dimensions dim)
		{
			_width = dim.Width;
			_height = dim.Height;
			var size = _width * _height;
			_array = new BitArray(size);
		}

		public void Clear()
		{
			_array.SetAll(false);
		}

		public void CopyTo(LifeTorus dest)
		{
			dest._width = this._width;
			dest._height = this._height;
			var size = _width * _height;
			dest._array = new BitArray(size);

			for (int i = 0; i < this._array.Count; i++)
			{
				dest._array[i] = this._array[i];
			}

		}

		public bool this[int x, int y]
		{
			get
			{
				// the getter can index one outside the normal range to handle the torus folding
				if (x < -1 || x >= (_width + 1) ||
					y < -1 || y >= (_height + 1))
					throw new ArgumentOutOfRangeException();

				int x1 = (x < 0 ? x + _width : x) % _width;
				int y1 = (y < 0 ? y + _height : y) % _height;
				int index = y1 * _width + x1;
				return _array[index];
			}
			set
			{
				if (x < 0 || x >= _width ||
					y < 0 || y >= _height)
					throw new ArgumentOutOfRangeException();

				int x1 = x % _width;
				int y1 = y % _height;
				int index = y1 * _width + x1;
				_array[index] = value;
			}
		}

        public override bool Equals(object obj)
        {
            if (obj is LifeTorus)
            {
				var tor = (LifeTorus)obj;

				var eq = true;

				if (tor._array.Count != _array.Count)
				{
					eq = false;
				}
				else
                {
					for (int i = 0; i < tor._array.Count; i++)
                    {
						if (tor._array[i] != _array[i])
						{
							eq = false;
							break;
						}
					}

				}

				return eq;
			}
			else
            {
				return false;
            }
        }
    }
}
