using System;
using System.Runtime.Serialization;

namespace ZedGraph
{
    public class FilledLineItem : LineItem, ICloneable, ISerializable
    {
        private FilledLine FilledLine { get {return Line as FilledLine; } }

        public IPointList LowerPoints { get; private set; }

        public FilledLineItem(string label) : base (label)
        {
            _line = new FilledLine();
        }

		/// <summary>
		/// Create a new <see cref="FilledLineItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The _label that will appear in the legend.</param>
		/// <param name="x">An array of double precision values that define
		/// the independent (X axis) values for this curve</param>
		/// <param name="high">An array of double precision values that define
		/// the upper dependent (Y axis) values for this curve</param>
		/// <param name="low">An array of double precision values that define
		/// the lower dependent (Y axis) values for this curve</param>
		/// <param name="color">A <see cref="System.Drawing.Color"/> value that will be applied to
		/// the <see cref="Line"/> and <see cref="Symbol"/> properties.
		/// </param>
		/// <param name="symbolType">A <see cref="SymbolType"/> enum specifying the
		/// type of symbol to use for this <see cref="LineItem"/>.  Use <see cref="SymbolType.None"/>
		/// to hide the symbols.</param>
		/// <param name="lineWidth">The width (in points) to be used for the <see cref="Line"/>.  This
		/// width is scaled based on <see cref="PaneBase.CalcScaleFactor"/>.  Use a value of zero to
		/// hide the line (see <see cref="ZedGraph.LineBase.IsVisible"/>).</param>
		public FilledLineItem( string label, double[] x, double[] high, double[] low, System.Drawing.Color color, SymbolType symbolType, float lineWidth )
			: this( label, new PointPairList( x, high ), new PointPairList( x, low ), color, symbolType, lineWidth )
		{
		}

		/// <summary>
		/// Create a new <see cref="FilledLineItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The _label that will appear in the legend.</param>
		/// <param name="x">An array of double precision values that define
		/// the independent (X axis) values for this curve</param>
		/// <param name="high">An array of double precision values that define
		/// the upper dependent (Y axis) values for this curve</param>
		/// <param name="low">An array of double precision values that define
		/// the lower dependent (Y axis) values for this curve</param>
        /// <param name="color">A <see cref="System.Drawing.Color"/> value that will be applied to
		/// the <see cref="Line"/> and <see cref="Symbol"/> properties.
		/// </param>
		/// <param name="symbolType">A <see cref="SymbolType"/> enum specifying the
		/// type of symbol to use for this <see cref="LineItem"/>.  Use <see cref="SymbolType.None"/>
		/// to hide the symbols.</param>
		public FilledLineItem( string label, double[] x, double[] high, double[] low, System.Drawing.Color color, SymbolType symbolType )
			: this( label, new PointPairList( x, high ), new PointPairList( x, low ), color, symbolType )
		{
		}

		/// <summary>
		/// Create a new <see cref="FilledLineItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The _label that will appear in the legend.</param>
		/// <param name="upperPoints">A <see cref="IPointList"/> of double precision value pairs that define
		/// the X and upper Y values for this curve</param>
		/// <param name="lowerPoints">A <see cref="IPointList"/> of double precision value pairs that define
		/// the X and lower Y values for this curve</param>
		/// <param name="color">A <see cref="System.Drawing.Color"/> value that will be applied to
		/// the <see cref="Line"/> and <see cref="Symbol"/> properties.
		/// </param>
		/// <param name="symbolType">A <see cref="SymbolType"/> enum specifying the
		/// type of symbol to use for this <see cref="LineItem"/>.  Use <see cref="SymbolType.None"/>
		/// to hide the symbols.</param>
		/// <param name="lineWidth">The width (in points) to be used for the <see cref="Line"/>.  This
		/// width is scaled based on <see cref="PaneBase.CalcScaleFactor"/>.  Use a value of zero to
		/// hide the line (see <see cref="ZedGraph.LineBase.IsVisible"/>).</param>
		public FilledLineItem( string label, IPointList upperPoints, IPointList lowerPoints, System.Drawing.Color color, SymbolType symbolType, float lineWidth )
			: base( label )
		{
		    Points = upperPoints ?? new PointPairList();
		    LowerPoints = lowerPoints ?? new PointPairList();

			_line = new FilledLine( color );
			if ( lineWidth == 0 )
				_line.IsVisible = false;
			else
				_line.Width = lineWidth;

			_symbol = new Symbol( symbolType, color );
		}

		/// <summary>
		/// Create a new <see cref="FilledLineItem"/> using the specified properties.
		/// </summary>
		/// <param name="label">The _label that will appear in the legend.</param>
		/// <param name="upperPoints">A <see cref="IPointList"/> of double precision value pairs that define
		/// the X and upper Y values for this curve</param>
		/// <param name="lowerPoints">A <see cref="IPointList"/> of double precision value pairs that define
		/// the X and lower Y values for this curve</param>
		/// <param name="color">A <see cref="System.Drawing.Color"/> value that will be applied to
		/// the <see cref="Line"/> and <see cref="Symbol"/> properties.
		/// </param>
		/// <param name="symbolType">A <see cref="SymbolType"/> enum specifying the
		/// type of symbol to use for this <see cref="LineItem"/>.  Use <see cref="SymbolType.None"/>
		/// to hide the symbols.</param>
		public FilledLineItem( string label, IPointList upperPoints, IPointList lowerPoints, System.Drawing.Color color, SymbolType symbolType )
			: this( label, upperPoints, lowerPoints, color, symbolType, ZedGraph.LineBase.Default.Width )
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="LineItem"/> object from which to copy</param>
        public FilledLineItem(FilledLineItem rhs)
            : base(rhs)
		{
			_symbol = new Symbol( rhs.Symbol );
			_line = new FilledLine( rhs.FilledLine );
		    LowerPoints = rhs.LowerPoints;
		}

		/// <summary>
		/// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
		/// calling the typed version of <see cref="Clone" />
		/// </summary>
		/// <returns>A deep copy of this object</returns>
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Typesafe, deep-copy clone method.
		/// </summary>
		/// <returns>A new, independent copy of this class</returns>
        public new FilledLineItem Clone()
		{
            return new FilledLineItem(this);
		}

    }
}
