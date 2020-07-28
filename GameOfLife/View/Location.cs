using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.View
{
	public struct Location
	{
		int _x;
		public int X { get { return _x; } }

		int _y;
		public int Y { get { return _y; } }

		bool _isValid;
		public bool IsValid { get { return _isValid; } }

		public Location(int x, int y, Dimensions dim)
			: this()
		{
			_x = x;
			_y = y;
			_isValid = _x >= 0 && _x < dim.Width &&
				_y >= 0 && _y < dim.Height;
		}

		public override string ToString()
		{
			return string.Format("({0}, {1})", X, Y);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Location))
				return false;
			Location loc = (Location)obj;
			return this.X == loc.X && this.Y == loc.Y;
		}

		public static bool operator !=(Location lhs, Location rhs)
		{
			return lhs.X != rhs.X || lhs.Y != rhs.Y;
		}

		public static bool operator ==(Location lhs, Location rhs)
		{
			return lhs.X == rhs.X && lhs.Y == rhs.Y;
		}

		public override int GetHashCode()
		{
			return _x ^ _y;
		}
	}
}
