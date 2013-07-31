using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using SvgNet.SvgGdi;

namespace ZedGraph
{
    class FilledLine : Line, ICloneable, ISerializable
    {

		public FilledLine() : this(Color.Empty )
		{
		}

		public FilledLine( Color color ) : base(color)
		{
		}

		/// <summary>
		/// The Copy Constructor
		/// </summary>
		/// <param name="rhs">The Line object from which to copy</param>
        public FilledLine(FilledLine rhs) : base(rhs)
		{
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
        public new FilledLine Clone()
		{
            return new FilledLine(this);
		}

        protected FilledLine(SerializationInfo info, StreamingContext context)
			: base( info, context )
		{
		}

        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
        /// </summary>
        /// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
        /// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public override void CloseCurve(GraphPane pane, CurveItem curve, PointF[] arrPoints, int count, double yMin, System.Drawing.Drawing2D.GraphicsPath path)
        {
            if(pane.LineType == LineType.Stack)
                throw new NotSupportedException("Filled lines cannot be stacked");

            FilledLineItem filledCurve = curve as FilledLineItem;
            if(filledCurve == null)
                throw new ArgumentException("Curve was of the wrong type.  Expected FilledLineItem but was " + curve.GetType(), "curve");

            // Build another points array consisting of the low points (It gets these from the LowerPoints property of the curve)
            PointF[] arrPoints2;
            int count2;
            BuildLowPointsArray(pane, curve, out arrPoints2, out count2);

            // Add the new points to the GraphicsPath
            float tension = _isSmooth ? _smoothTension : 0f;
            path.AddCurve(arrPoints2, 0, count2 - 2, tension);
        }

        protected override IPointList GetPointsForLowPointsArray(CurveItem curve)
        {
            FilledLineItem filledCurve = curve as FilledLineItem;
            if (filledCurve == null)
                throw new ArgumentException("Curve was of the wrong type.  Expected FilledLineItem but was " + curve.GetType(), "curve");

            return filledCurve.LowerPoints;
        }

        public override void DrawSmoothFilledCurve(IGraphics g, GraphPane pane, CurveItem curve, float scaleFactor)
        {
            base.DrawSmoothFilledCurve(g, pane, curve, scaleFactor);

            // Draw the curve at the bottom of the graph. 
            DrawCurve(g, pane, curve, scaleFactor, GetPointsForLowPointsArray(curve));
        }
    }
}
