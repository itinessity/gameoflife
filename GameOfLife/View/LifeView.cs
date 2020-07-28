using GameOfLife.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLife
{
    public class LifeView: FrameworkElement
    {
		#region Fields
		Pen _greyPen = new Pen(Brushes.Gray, 1);

		Point _offset = new Point(0.0, 0.0);
		LifeModel _model;
		Location _last;
		Game _game;

		List<Visual> _visuals = new List<Visual>();
		DrawingVisual _gridVisual = new DrawingVisual();
		DrawingVisual _cellsVisual = new DrawingVisual();
		DrawingVisual _adornerVisual = new DrawingVisual();
		#endregion

		#region Dependency properties

		public int CellSize
		{
			get { return (int)GetValue(CellSizeProperty); }
			set { SetValue(CellSizeProperty, value); }
		}

		public static readonly DependencyProperty CellSizeProperty =
			DependencyProperty.Register("CellSize", typeof(int), typeof(LifeView), new FrameworkPropertyMetadata(12, FrameworkPropertyMetadataOptions.AffectsMeasure));

		public Dimensions Dimensions
		{
			get { return (Dimensions)GetValue(DimensionsProperty); }
			set { SetValue(DimensionsProperty, value); }
		}

		public static readonly DependencyProperty DimensionsProperty =
			DependencyProperty.Register("Dimensions", typeof(Dimensions), typeof(LifeView),
				new FrameworkPropertyMetadata(new Dimensions(64, 64), FrameworkPropertyMetadataOptions.AffectsMeasure, DimensionsChangedCallback));

		static void DimensionsChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((LifeView)d).UpdateDimensions();
		}

		public int Generation
		{
			get { return (int)GetValue(GenerationProperty); }
			set { SetValue(GenerationProperty, value); }
		}

		public static readonly DependencyProperty GenerationProperty =
			DependencyProperty.Register("Generation", typeof(int), typeof(LifeView), new FrameworkPropertyMetadata(0));

		public static readonly DependencyProperty BackgroundProperty;
		public Brush Background
		{
			get { return (Brush)GetValue(BackgroundProperty); }
			set { SetValue(BackgroundProperty, value); }
		}

		public static readonly DependencyProperty ForegroundProperty;
		public Brush Foreground
		{
			get { return (Brush)GetValue(ForegroundProperty); }
			set { SetValue(ForegroundProperty, value); }
		}

		public static readonly DependencyProperty PaddingProperty;
		public Thickness Padding
		{
			get { return (Thickness)GetValue(PaddingProperty); }
			set { SetValue(PaddingProperty, value); }
		}

		#endregion

		#region Ctors
		static LifeView()
		{
			BackgroundProperty = Control.BackgroundProperty.AddOwner(typeof(LifeView),
				new FrameworkPropertyMetadata(Brushes.LightGoldenrodYellow, FrameworkPropertyMetadataOptions.AffectsRender));
			ForegroundProperty = Control.ForegroundProperty.AddOwner(typeof(LifeView), new FrameworkPropertyMetadata(Brushes.LightCoral, FrameworkPropertyMetadataOptions.AffectsRender));
			PaddingProperty = Control.PaddingProperty.AddOwner(typeof(LifeView), new FrameworkPropertyMetadata(new Thickness(10), FrameworkPropertyMetadataOptions.AffectsRender));
		}

		public LifeView(Game life)
		{
			_game = life;


			AddVisual(_gridVisual);
			AddVisual(_cellsVisual);
			AddVisual(_adornerVisual);// top of Z layer
		}
		#endregion

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);

			UpdateDimensions();
		}

		protected void UpdateDimensions()
		{
			if (_model == null)
				_model = new LifeModel(this.Dimensions, _game);
			else
				_model.Dimension = this.Dimensions;

			DrawGrid();
			DrawModel();
		}

		void DrawGrid()
		{
			using (DrawingContext dc = _gridVisual.RenderOpen())
			{
				Dimensions dim = this.Dimensions;
				Thickness padding = this.Padding;
				int cellSize = this.CellSize;
				Rect grid = new Rect(
					padding.Left,
					padding.Top,
					dim.Width * cellSize,
					dim.Height * cellSize
					);
				dc.DrawRectangle(this.Background, null, grid);//Brushes.Beige

				// Draw vertical lines
				double x = padding.Left;
				for (int i = 0; i <= dim.Width; i++)
				{
					dc.DrawLine(_greyPen, new Point(x, padding.Top), new Point(x, padding.Top + dim.Height * cellSize));
					x += cellSize;
				}

				// Draw horizontal lines
				double y = padding.Top;
				for (int i = 0; i <= dim.Height; i++)
				{
					dc.DrawLine(_greyPen, new Point(padding.Left, y), new Point(padding.Left + dim.Width * cellSize, y));
					y += cellSize;
				}
			}
		}


		public void DrawModel()
		{
			using (DrawingContext dc = _cellsVisual.RenderOpen())
			{
				Dimensions dim = this.Dimensions;
				Thickness padding = this.Padding;
				int cellSize = this.CellSize;
				Brush cellBrush = this.Foreground;
				for (int x = 0; x < dim.Width; x++)
					for (int y = 0; y < dim.Height; y++)
					{
						if (_model[x, y])
						{
							Rect rect = new Rect(padding.Left + x * cellSize + 1, padding.Top + y * cellSize + 1, cellSize - 1, cellSize - 1);
							dc.DrawRectangle(cellBrush, null, rect);
						}
					}
			}
		}

		protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);
			Point pt = e.GetPosition(this);
			Location loc = GetLocationFromPt(pt);
			if (loc.IsValid)
			{
				_model[loc.X, loc.Y] = !_model[loc.X, loc.Y];
				DrawModel();
			}
		}

		protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
		{
			base.OnMouseMove(e);

			Point pt = e.GetPosition(this);
			Location loc = GetLocationFromPt(pt);
			if (!loc.IsValid || loc == _last)
				return;

			_last = loc;
			if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
			{
				_model[loc.X, loc.Y] = true;
				DrawModel();
			}
			else
			{
				// Draw hover cell in our own private 'adorner' layer
				Thickness padding = this.Padding;
				int cellSize = this.CellSize;
				Rect rect = new Rect(padding.Left + loc.X * cellSize + 1, padding.Top + loc.Y * cellSize + 1, cellSize - 1, cellSize - 1);
				using (DrawingContext dc = _adornerVisual.RenderOpen())
					dc.DrawRectangle(Brushes.HotPink, null, rect);
			}
		}

		protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
		{
			base.OnMouseLeave(e);
			using (_adornerVisual.RenderOpen())// clears any previous drawing
				;
		}

		public void Clear()
		{
			_model.Clear();
			this.Generation = 0;
			DrawModel();
		}

		public bool Next()// returns true if changed
		{
			try
			{
				bool ret = _model.Next();
				this.Generation++;
				DrawModel();
				return ret;
			}
			catch
			{
				return false;
			}
		}

		public Location GetLocationFromPt(Point pt)
		{
			Thickness padding = this.Padding;
			Dimensions dim = this.Dimensions;
			int cellSize = this.CellSize;
			return new Location((int)((pt.X - padding.Left) / cellSize), (int)((pt.Y - padding.Top) / cellSize), dim);
		}

		void AddVisual(Visual child)
		{
			this.AddLogicalChild(child);
			this.AddVisualChild(child);
			_visuals.Add(child);
		}

		protected override Visual GetVisualChild(int index)
		{
			return _visuals[index];
		}

		protected override int VisualChildrenCount
		{
			get
			{
				return _visuals.Count;
			}
		}

	}
}
