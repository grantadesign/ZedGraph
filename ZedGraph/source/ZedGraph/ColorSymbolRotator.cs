//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright � 2004  John Champion
//
//This library is free software; you can redistribute it and/or
//modify it under the terms of the GNU Lesser General Public
//License as published by the Free Software Foundation; either
//version 2.1 of the License, or (at your option) any later version.
//
//This library is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public
//License along with this library; if not, write to the Free Software
//Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//=============================================================================

using Color = System.Drawing.Color;

namespace ZedGraph
{
	/// <summary>
	/// Class used to get the next color/symbol for GraphPane.AddCurve methods.
	/// </summary>
	/// 
	/// <author> Jerry Vos modified by John Champion </author>
	/// <version> $Revision: 3.4 $ $Date: 2006/06/24 20:26:43 $ </version>
	public class ColorSymbolRotator
	{
        #region Static fields
		/// <summary>
		/// The <see cref="Color"/>s <see cref="ColorSymbolRotator"/> 
		/// rotates through.
		/// </summary>
		public static readonly Color[] Colors = new[]
		{
			Color.Red,
			Color.Blue,
			Color.Green,
			Color.Purple,
			Color.Cyan,
			Color.Pink,
			Color.LightBlue,
			Color.PaleVioletRed,
			Color.SeaGreen,
			Color.Yellow
		};

		/// <summary>
		/// The <see cref="SymbolType"/>s <see cref="ColorSymbolRotator"/> 
		/// rotates through.
		/// </summary>
		public static readonly SymbolType[] Symbols = new[]
		{
			SymbolType.Circle,
			SymbolType.Diamond,
			SymbolType.Plus,
			SymbolType.Square,
			SymbolType.Star,
			SymbolType.Triangle,
			SymbolType.TriangleDown,
			SymbolType.XCross,
			SymbolType.HDash,
			SymbolType.VDash
		};		

		private static ColorSymbolRotator staticInstance;
        #endregion
        
        #region Fields
		/// <summary>
		/// The index of the next color to be used. Note: may be 
		/// > COLORS.Length, it is reset to 0 on the next call if it is.
		/// </summary>
		protected int ColorIndex;

		/// <summary>
		/// The index of the next symbol to be used. Note: may be 
		/// > SYMBOLS.Length, it is reset to 0 on the next call if it is.
		/// </summary>
		protected int SymbolIndex;
        #endregion
        
        #region Properties
		/// <summary>
		/// Retrieves the next color in the rotation  Calling this
		/// method has the side effect of incrementing the color index.
		/// <seealso cref="NextSymbol"/>
		/// <seealso cref="NextColorIndex"/>
		/// </summary>
		public Color NextColor
		{
			get { return Colors[NextColorIndex]; }
		}

		/// <summary>
		/// Retrieves the index of the next color to be used.  Calling this
		/// method has the side effect of incrementing the color index.
		/// </summary>
		public int NextColorIndex
		{
			get
			{
				if (ColorIndex >= Colors.Length)
					ColorIndex = 0;

				return ColorIndex++;
			}
			set
			{
				ColorIndex = value;
			}
		}

		/// <summary>
		/// Retrieves the next color in the rotation.  Calling this
		/// method has the side effect of incrementing the symbol index.
		/// <seealso cref="NextColor"/>
		/// <seealso cref="NextSymbolIndex"/>
		/// </summary>
		public SymbolType NextSymbol
		{
			get { return Symbols[NextSymbolIndex]; }
		}

		/// <summary>
		/// Retrieves the index of the next symbol to be used.  Calling this
		/// method has the side effect of incrementing the symbol index.
		/// </summary>
		public int NextSymbolIndex
		{
			get
			{
				if (SymbolIndex >= Symbols.Length)
					SymbolIndex = 0;

				return SymbolIndex++;
			}
			set
			{
				SymbolIndex = value;
			}
		}

		/// <summary>
		/// Retrieves the <see cref="ColorSymbolRotator"/> instance used by the
		/// static methods.
		/// <seealso cref="StaticNextColor"/>
		/// <seealso cref="StaticNextSymbol"/>
		/// </summary>
		public static ColorSymbolRotator StaticInstance
		{
			get
			{
				if (staticInstance == null)
					staticInstance = new ColorSymbolRotator();

				return staticInstance;
			}
		}
		
		/// <summary>
		/// Retrieves the next color from this class's static 
		/// <see cref="ColorSymbolRotator"/> instance
		/// <seealso cref="StaticInstance"/>
		/// <seealso cref="StaticNextSymbol"/>
		/// </summary>
		public static Color StaticNextColor
		{
			get { return StaticInstance.NextColor; } 
		}

		/// <summary>
		/// Retrieves the next symbol type from this class's static 
		/// <see cref="ColorSymbolRotator"/> instance
		/// <seealso cref="StaticInstance"/>
		/// <seealso cref="StaticNextColor"/>
		/// </summary>
		public static SymbolType StaticNextSymbol
		{
			get { return StaticInstance.NextSymbol; } 
		}
        #endregion

        #region Methods
        public void ResetCounter()
        {
            NextColorIndex = 0;
        }
        #endregion
    }
}
