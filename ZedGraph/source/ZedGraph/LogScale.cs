//============================================================================
//ZedGraph Class Library - A Flexible Line Graph/Bar Graph Library in C#
//Copyright © 2005  John Champion
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

using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;
using SvgNet.SvgGdi;

namespace ZedGraph
{
	/// <summary>
	/// The LogScale class inherits from the <see cref="Scale" /> class, and implements
	/// the features specific to <see cref="AxisType.Log" />.
	/// </summary>
	/// <remarks>
	/// LogScale is a non-linear axis in which the values are scaled using the base 10
	/// <see cref="Math.Log(double)" />
	/// function.
	/// </remarks>
	/// 
	/// <author> John Champion  </author>
	/// <version> $Revision: 1.12 $ $Date: 2007/04/16 00:03:02 $ </version>
	[Serializable]
	class LogScale : Scale, ISerializable //, ICloneable
	{

        
	#region constructors

		/// <summary>
		/// Default constructor that defines the owner <see cref="Axis" />
		/// (containing object) for this new object.
		/// </summary>
		/// <param name="owner">The owner, or containing object, of this instance</param>
		public LogScale( Axis owner )
			: base( owner )
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The <see cref="LogScale" /> object from which to copy</param>
		/// <param name="owner">The <see cref="Axis" /> object that will own the
		/// new instance of <see cref="LogScale" /></param>
		public LogScale( Scale rhs, Axis owner )
			: base( rhs, owner )
		{
		}

		/// <summary>
		/// Create a new clone of the current item, with a new owner assignment
		/// </summary>
		/// <param name="owner">The new <see cref="Axis" /> instance that will be
		/// the owner of the new Scale</param>
		/// <returns>A new <see cref="Scale" /> clone.</returns>
		public override Scale Clone( Axis owner )
		{
			return new LogScale( this, owner );
		}

	#endregion

	#region properties

		/// <summary>
		/// Return the <see cref="AxisType" /> for this <see cref="Scale" />, which is
		/// <see cref="AxisType.Log" />.
		/// </summary>
		public override AxisType Type
		{
			get
			{
			     return AxisType.Log;
			}
		}

		/// <summary>
		/// Gets or sets the minimum value for this scale.
		/// </summary>
		/// <remarks>
		/// The set property is specifically adapted for <see cref="AxisType.Log" /> scales,
		/// in that it automatically limits the setting to values greater than zero.
		/// </remarks>
		public override double Min
		{
			get
			{
			     return _min;
			}
			set
			{
			     if ( value > 0 )
			     {
			         base.Min = value;
                     if (UsingLinearlySpacedTickMarks)
                     {
                         _linScale.Min = value;
                     }
			     }
			}
		}

		/// <summary>
		/// Gets or sets the maximum value for this scale.
		/// </summary>
		/// <remarks>
		/// The set property is specifically adapted for <see cref="AxisType.Log" /> scales,
		/// in that it automatically limits the setting to values greater than zero.
		/// <see cref="XDate" /> struct.
		/// </remarks>
		public override double Max
		{
			get { return _max; }
			set
			{
			     if ( value > 0 )
			     {
			         base.Max = value;
                     if (UsingLinearlySpacedTickMarks)
                     {
                         _linScale.Min = value;
                     }
			     }
			}
		}

	#endregion

	#region methods

		/// <summary>
		/// Setup some temporary transform values in preparation for rendering the <see cref="Axis"/>.
		/// </summary>
		/// <remarks>
		/// This method is typically called by the parent <see cref="GraphPane"/>
		/// object as part of the <see cref="GraphPane.Draw"/> method.  It is also
		/// called by <see cref="GraphPane.GeneralTransform(double,double,CoordType)"/> and
		/// <see cref="GraphPane.ReverseTransform( PointF, out double, out double )"/>
		/// methods to setup for coordinate transformations.
		/// </remarks>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="axis">
		/// The parent <see cref="Axis" /> for this <see cref="Scale" />
		/// </param>
		override public void SetupScaleData( GraphPane pane, Axis axis )
		{
			base.SetupScaleData( pane, axis );

			_minLinTemp = Linearize( _min );
			_maxLinTemp = Linearize( _max );
		}

		/// <summary>
		/// Convert a value to its linear equivalent for this type of scale.
		/// </summary>
		/// <remarks>
		/// The default behavior is to just return the value unchanged.  However,
		/// for <see cref="AxisType.Log" /> and <see cref="AxisType.Exponent" />,
		/// it returns the log or power equivalent.
		/// </remarks>
		/// <param name="val">The value to be converted</param>
		override public double Linearize( double val )
		{
			return SafeLog( val );
		}

		/// <summary>
		/// Convert a value from its linear equivalent to its actual scale value
		/// for this type of scale.
		/// </summary>
		/// <remarks>
		/// The default behavior is to just return the value unchanged.  However,
		/// for <see cref="AxisType.Log" /> and <see cref="AxisType.Exponent" />,
		/// it returns the anti-log or inverse-power equivalent.
		/// </remarks>
		/// <param name="val">The value to be converted</param>
		override public double DeLinearize( double val )
		{
			return Math.Pow( 10.0, val );
		}

		/// <summary>
		/// Determine the value for any major tic.
		/// </summary>
		/// <remarks>
		/// This method properly accounts for <see cref="Scale.IsLog"/>, <see cref="Scale.IsText"/>,  and other axis format settings.
		/// For log values, this should return the Log10(coordinateValue).
		/// </remarks>
		/// <param name="baseVal">
		/// The Log10(value) of the first major tic (floating point double).  
		/// </param>
		/// <param name="tic">
		/// The major tic number (0 = first major tic).  For log scales, this is the actual power of 10.
		/// </param>
		/// <returns>
		/// The specified major tic value (floating point double).
		/// </returns>
		override internal double CalcMajorTicValue( double baseVal, double tic )
		{
            if(UsingLinearlySpacedTickMarks)
            {
                double d = DeLinearize(baseVal);
                double v = _linScale.CalcMajorTicValue(d, tic);
                double r = Linearize((v));
                return r; 
            }
            // this is the log of the coordValue.
            // baseVal is the log of the actual base coord val
			return baseVal + tic * CyclesPerStep;

		//	double val = baseVal + (double)tic * CyclesPerStep;
		//	double frac = val - Math.Floor( val );
		}

        
		/// <summary>
		/// Determine the value for any minor tic.
		/// Returns the Log10(minorTickCoordinateValue)
		/// </summary>
		/// <remarks>
		/// This method properly accounts for <see cref="Scale.IsLog"/>, <see cref="Scale.IsText"/>, and other axis format settings.
		/// </remarks>
		/// <param name="baseVal">
		/// The value of the first major tic (floating point double).  This tic value is the base
		/// reference for all tics (including minor ones).  This is the Log10 of the coordinate value.
		/// </param>
		/// <param name="iTic">
		/// The major tic number (0 = first major tic).  For log scales, this is the actual power of 10.
		/// </param>
		/// <returns>
		/// The specified minor tic value (floating point double).
		/// </returns>
		override internal double CalcMinorTicValue( double baseVal, int iTic )
		{
            if(UsingLinearlySpacedTickMarks)
            {
                double coordValue = DeLinearize(baseVal);
                double value = _linScale.CalcMinorTicValue(coordValue, iTic);
                double r = Linearize(value);
                return r;
            }
            double[] dLogVal = { 0, 0.301029995663981, 0.477121254719662, 0.602059991327962,
									0.698970004336019, 0.778151250383644, 0.845098040014257,
									0.903089986991944, 0.954242509439325, 1 };

			return baseVal + Math.Floor( iTic / 9.0 ) + dLogVal[( iTic + 9 ) % 9];
		}

		/// <summary>
		/// Internal routine to determine the ordinals of the first minor tic mark
		/// </summary>
		/// <param name="baseVal">
		/// The value of the first major tic for the axis.
		/// </param>
		/// <returns>
		/// The ordinal position of the first minor tic, relative to the first major tic.
		/// This value can be negative (e.g., -3 means the first minor tic is 3 minor step
		/// increments before the first major tic.
		/// </returns>
		override internal int CalcMinorStart( double baseVal )
		{
            if(UsingLinearlySpacedTickMarks)
            {
                double coordVal = DeLinearize(baseVal);
                int r = _linScale.CalcMinorStart((coordVal));
                return r;
            }
			return -9;
		}

		/// <summary>
		/// Determine the value for the first major tic.  This method returns the Log10(firstTickCoordinateValue)
		/// </summary>
		/// <remarks>
		/// This is done by finding the first possible value that is an integral multiple of
		/// the step size, taking into account the date/time units if appropriate.
		/// This method properly accounts for <see cref="Scale.IsLog"/>, <see cref="Scale.IsText"/>,
		/// and other axis format settings.
		/// </remarks>
		/// <returns>
		/// First major tic value (floating point double).
		/// </returns>
		override internal double CalcBaseTic()
		{
            if(UsingLinearlySpacedTickMarks)
            {
                double value = _linScale.CalcBaseTic();
                double r = Linearize(value);
                return r;
            }
            // This method returns the Log10(firstTickCoordinateValue)
			if ( _baseTic != PointPair.Missing )
				return _baseTic;
			else
			{
				// go to the nearest even multiple of the step size
				return Math.Ceiling( Scale.SafeLog( _min ) - 0.00000001 );
			}

		}
		
		/// <summary>
		/// Internal routine to determine the ordinals of the first and last major axis label.
		/// </summary>
		/// <returns>
		/// This is the total number of major tics for this axis.
		/// </returns>
		override internal int CalcNumTics()
		{
            if(UsingLinearlySpacedTickMarks)
            {
                return _linScale.CalcNumTics();
            }
			int nTics = 1;

            if(_minAuto && _maxAuto)
            {
                //iStart = (int) ( Math.Ceiling( SafeLog( this.min ) - 1.0e-12 ) );
                //iEnd = (int) ( Math.Floor( SafeLog( this.max ) + 1.0e-12 ) );

                //nTics = (int)( ( Math.Floor( Scale.SafeLog( _max ) + 1.0e-12 ) ) -
                //		( Math.Ceiling( Scale.SafeLog( _min ) - 1.0e-12 ) ) + 1 ) / CyclesPerStep;
                nTics = (int)( ( SafeLog( _max ) - SafeLog( _min ) ) / CyclesPerStep ) + 1;
            }
            else
            {
                double start = SafeLog(_min);
                if(_minAuto)
                {
                    start = Math.Floor(start);
                }
                double end = SafeLog(_max);
                if(_maxAuto)
                {
                    end = Math.Ceiling(end);
                }
                nTics = CountIntegerFencePosts(start, end);
            }

            
			if ( nTics < 1 )
				nTics = 1;
			else if ( nTics > 1000 )
				nTics = 1000;

			return nTics;
		}

        private static int CountIntegerFencePosts(double x, double y)
        {
            if (x > y)
            {
                double d = x;
                x = y;
                y = d;
            }

            double yf = Math.Floor(y);
            double xc = Math.Ceiling(x);

            if (xc > yf)
            {
                return 0;
            }
            if (xc == yf)
            {
                return 1;
            }

            int diff = (int)(yf - xc) + 1;
            return diff;
        }

		private double CyclesPerStep
		{
			//get { return (int)Math.Max( Math.Floor( Scale.SafeLog( _majorStep ) ), 1 ); }
			get
			{
                if(UsingLinearlySpacedTickMarks)
                {
                    return 1;
                }
			    return _majorStep;
			}
		}

		/// <summary>
		/// Select a reasonable base 10 logarithmic axis scale given a range of data values.
		/// </summary>
		/// <remarks>
		/// This method only applies to <see cref="AxisType.Log"/> type axes, and it
		/// is called by the general <see cref="PickScale"/> method.  The scale range is chosen
		/// based always on powers of 10 (full log cycles).  This
		/// method honors the <see cref="Scale.MinAuto"/>, <see cref="Scale.MaxAuto"/>,
		/// and <see cref="Scale.MajorStepAuto"/> autorange settings.
		/// In the event that any of the autorange settings are false, the
		/// corresponding <see cref="Scale.Min"/>, <see cref="Scale.Max"/>, or <see cref="Scale.MajorStep"/>
		/// setting is explicitly honored, and the remaining autorange settings (if any) will
		/// be calculated to accomodate the non-autoranged values.  For log axes, the MinorStep
		/// value is not used.
		/// <para>On Exit:</para>
		/// <para><see cref="Scale.Min"/> is set to scale minimum (if <see cref="Scale.MinAuto"/> = true)</para>
		/// <para><see cref="Scale.Max"/> is set to scale maximum (if <see cref="Scale.MaxAuto"/> = true)</para>
		/// <para><see cref="Scale.MajorStep"/> is set to scale step size (if <see cref="Scale.MajorStepAuto"/> = true)</para>
		/// <para><see cref="Scale.Mag"/> is set to a magnitude multiplier according to the data</para>
		/// <para><see cref="Scale.Format"/> is set to the display format for the values (this controls the
		/// number of decimal places, whether there are thousands separators, currency types, etc.)</para>
		/// </remarks>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object
		/// associated with this <see cref="Axis"/></param>
		/// <param name="g">
		/// A graphic device object to be drawn into.  This is normally e.Graphics from the
		/// PaintEventArgs argument to the Paint() method.
		/// </param>
		/// <param name="scaleFactor">
		/// The scaling factor to be used for rendering objects.  This is calculated and
		/// passed down by the parent <see cref="GraphPane"/> object using the
		/// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
		/// font sizes, etc. according to the actual size of the graph.
		/// </param>
		/// <seealso cref="PickScale"/>
		/// <seealso cref="AxisType.Log"/>
		override public void PickScale( GraphPane pane, IGraphics g, float scaleFactor )
		{
            double minVal = _rangeMin;
            double maxVal = _rangeMax;

            // Make sure that minVal and maxVal are legitimate values
            if (Double.IsInfinity(minVal) || Double.IsNaN(minVal) || minVal == Double.MaxValue)
                minVal = 0.0;
            if (Double.IsInfinity(maxVal) || Double.IsNaN(maxVal) || maxVal == Double.MaxValue)
                maxVal = 0.0;

            if(minVal > maxVal)
            {
                double t = maxVal;
                maxVal = minVal;
                minVal = t;
            }

            // Check for bad data range
            if (minVal <= 0.0 && maxVal <= 0.0)
            {
                minVal = 1.0;
                maxVal = 10.0;
            }
            else if (minVal <= 0.0)
            {
                minVal = maxVal / 10.0;
            }

            double range = maxVal - minVal;

            // For autoranged values, assign the value.  If appropriate, adjust the value by the
            // "Grace" value.
            if(_minAuto)
            {
                if(_minGrace < 0)
                {
                    _minGrace = 0;
                }
                double graceVal = CalcMinValue(minVal, maxVal, _minGrace);
                double nextProperPowerOf10 = Math.Pow(10.0, Math.Floor(Math.Log10(minVal)));
                _min = graceVal < nextProperPowerOf10 ? nextProperPowerOf10 : graceVal; // don't use grace val if there is a power of 10 between the data start and the garce value
            }

            if(_maxAuto)
            {
                if(_maxGrace < 0)
                {
                    _maxGrace = 0;
                }
                double graceVal = CalcMaxValue(minVal, maxVal, _maxGrace);
                double nextProperPowerOf10 = Math.Pow(10.0, Math.Ceiling(Math.Log10(maxVal)));
                _max = graceVal > nextProperPowerOf10 ? nextProperPowerOf10 : graceVal;
            }
            
            // if min == max and we are auto scaling, make the scale go from -5% to +5%
            if (_max == _min && _maxAuto && _minAuto)
            {
                if (Math.Abs(_max) > 1e-100)
                {
                    _max *= (_min < 0 ? 0.95 : 1.05);
                    _min *= (_min < 0 ? 1.05 : 0.95);
                }
                else
                {
                    _max = 1.0;
                    _min = -1.0;
                }
            }

			_mag = 0;		// Never use a magnitude shift for log scales
			//this.numDec = 0;		// The number of decimal places to display is not used

			// Test for trivial condition of range = 0 and pick a suitable default
			if ( _max - _min < 1.0e-20 )
			{
				if ( _maxAuto )
					_max = _max * 2.0;
				if ( _minAuto )
					_min = _min / 2.0;
			}

            if(UsingLinearlySpacedTickMarks) // this method gets called again after the initial setup - so _linScale may already be set
            {
                //_linScale.PickScale(pane, g, scaleFactor);
                _min = _linScale._min;
                _max = _linScale._max;
                return;
            }
		    
            int nTicks = CalcNumTics(); // get the number of ticks 
            if (UseMostSuitableTickMarkSpacing && nTicks < MinimumNumberOfMajorTicks) // then we shall use a linear scale instead
            {
                // Calculate the new step size
                double majorStep = _majorStep;
                if (_majorStepAuto)
                {
                    double targetSteps = (_ownerAxis is XAxis || _ownerAxis is X2Axis) ? Default.TargetXSteps : Default.TargetYSteps;
                    // Calculate the step size based on target steps
                    majorStep = CalcStepSize(_max - _min, targetSteps);
                    if (_isPreventLabelOverlap)
                    {
                        // Calculate the maximum number of labels
                        double maxLabels = (double)this.CalcMaxLabels(g, pane, scaleFactor); // Will this work?
                        if (maxLabels < (_max - _min) / _majorStep)
                        {
                            majorStep = CalcBoundedStepSize(_max - _min, maxLabels);
                        }
                    }
                }

                LinearScale altLinScale = new LinearScale(this, _ownerAxis)
                                              {
                                                          _rangeMin = this._rangeMin,
                                                          _rangeMax = this._rangeMax,
                                                          MajorStep = majorStep,
                                                          _min = this._min,
                                                          _max = this._max
                                                };

                // Special case.  If range is zero, then we have already adjusted our _min and max to 95% to 105%.
                // I do not want the linear scale to adda a grace on to this as well.
                if(range == 0)
                {
                    altLinScale._minGrace = 0;
                    altLinScale._maxGrace = 0;
                }
                _linScale = altLinScale;
                return;
            }

            // Majorstep is always 1 for log scales
            if (_majorStepAuto)
                _majorStep = 1.0;
		}

        /// <summary>
        /// Works out the minimum and maximum axis values based upon a given grace value.
        /// </summary>
        /// <remarks>
        /// Thanks to Rob Wills for working out the maths for this function as it has been too long since I did any log arithmetic.
        /// 
        /// The 'trouble' with the normal ZedGraph algorithm for working with grace is that it is based upon a linear scale and as such
        /// works out the new min/max based upon a fraction of the *data* range (not the scale length).  The results of this often look 
        /// "silly" for log scales.  For example: min = 10 max = 100 and grace = 0.1 .  Using a linear based approach we would get
        /// newMin = 10 - (0.1 * (100-10)) = 1, newMax = 100 + (0.1 * (100-10)) = 109.  So the scale would start at 1 and finish at 109.
        /// so you would get the major tick marks 1, 10, 100 - but the data only starts at 10, which is 1/3 of the way along the axis - that 
        /// is too much space!
        /// 
        /// So instead, for log scales, we work out then min/max values so that we leave a certain fraction of the scale length at the start 
        /// and end of the data.
        /// 
        /// If grace is 0 then newMin == min and newMax == max.
        /// 
        /// No checking of input parameters is done - so if either min or max is less than zero - or max is less than min, then you 
        /// only have yourself to blame.  (You will most likely get NaN for newMin or newMax).  
        /// </remarks>
        /// <param name="grace">The grace value.  
        /// This is the amount of space between the axis start and the start of the data as expressed
        /// as a fraction of the total axis length.  This value should be between 0.0 and 1.0 </param>
        internal static void __CalcGrace(double minValue, double maxValue, double grace, out double newMin, out double newMax)
        {
            newMin = minValue;
            newMax = maxValue;
            if (grace == 0 || minValue.Equals(maxValue))
            {
                return;
            }

            double logMin = Math.Log10(minValue);
            double logMax = Math.Log10(maxValue);

            double s = grace * (logMax - logMin);
            newMin = Math.Pow(10, logMin - s);
            newMax = Math.Pow(10, logMax + s);
        }

        internal static double CalcMinValue(double minValue, double maxValue, double grace)
        {
            double newMin, newMax;
            __CalcGrace(minValue, maxValue, grace, out newMin, out newMax);
            return newMin;
        }

        internal static double CalcMaxValue(double minValue, double maxValue, double grace)
        {
            double newMin, newMax;
            __CalcGrace(minValue, maxValue, grace, out newMin, out newMax);
            return newMax;
        }

	    /// <summary>
        /// Convenience property: returns true if <see cref="Scale.MinimumNumberOfMajorTicks"/> is set to greater than 0 - in this case
        /// the scale may revert to using linarly spaced tick marks.
        /// </summary>
	    private bool UseMostSuitableTickMarkSpacing
	    {
            get { return MinimumNumberOfMajorTicks > 0; }
	    }

        public override bool UsingLinearlySpacedTickMarks { get {return _linScale != null; } }
	    private LinearScale _linScale;

	    public override int MinimumNumberOfMajorTicks
	    {
	        get {return base.MinimumNumberOfMajorTicks;}
            set
            {
                base.MinimumNumberOfMajorTicks = value;
                _linScale = null;
            }
	    }

	    /// <summary>
		/// Make a value label for an <see cref="AxisType.Log" /> <see cref="Axis" />.
		/// </summary>
		/// <param name="pane">
		/// A reference to the <see cref="GraphPane"/> object that is the parent or
		/// owner of this object.
		/// </param>
		/// <param name="index">
		/// The zero-based, ordinal index of the label to be generated.  For example, a value of 2 would
		/// cause the third value label on the axis to be generated.
		/// </param>
		/// <param name="dVal">
		/// The numeric value associated with the label.  This value is ignored for log (<see cref="Scale.IsLog"/>)
		/// and text (<see cref="Scale.IsText"/>) type axes.
		/// </param>
		/// <returns>The resulting value label as a <see cref="string" /></returns>
		override internal string MakeLabel( GraphPane pane, int index, double dVal )
		{
			if ( _format == null )
				_format = Scale.Default.Format;

			if ( _isUseTenPower )
				return string.Format( "{0:F0}", dVal );
			else
				return Math.Pow( 10.0, dVal ).ToString( _format );
		}

	#endregion

	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int schema2 = 10;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected LogScale( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema2" );
            _mag = info.GetInt32("minimumNumberOfMajorTicks");

		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData( info, context );
			info.AddValue( "schema2", schema2 );
            info.AddValue("minimumNumberOfMajorTicks", _minimumNumberOfMajorTicks);
		}
	#endregion

	}
}
