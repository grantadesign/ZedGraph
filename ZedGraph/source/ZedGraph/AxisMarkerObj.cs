using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SvgNet.SvgGdi;

namespace ZedGraph
{
    public class AxisMarkerCurveItem : CurveItem
    {
        public AxisMarkerCurveItem(double value, bool isXAxis)
        {
            Value = value;
            IsXAxisMarker = isXAxis;

            var points = new PointPairList();
            if(isXAxis)
                points.Add(value, PointPair.Missing);
            else
                points.Add(PointPair.Missing, value);
            Points = points;

            PenColor = Color.Red;
        }

        private Color PenColor{get;set;}

        public bool IsXAxisMarker{get;set;}

        public double Value {get;set;}

        /// <summary>
        /// Gets a flag indicating if the Z data range should be included in the axis scaling calculations.
        /// </summary>
        /// <param name="pane">The parent <see cref="GraphPane" /> of this <see cref="CurveItem" />.
        /// </param>
        /// <value>true if the Z data are included, false otherwise</value>
        internal override bool IsZIncluded(GraphPane pane)
        {
            return false;
        }

        /// <summary>
        /// Gets a flag indicating if the X axis is the independent axis for this <see cref="CurveItem" />
        /// </summary>
        /// <param name="pane">The parent <see cref="GraphPane" /> of this <see cref="CurveItem" />.
        /// </param>
        /// <value>true if the X axis is independent, false otherwise</value>
        internal override bool IsXIndependent(GraphPane pane)
        {
            return true;
        }

        /// <summary>
        /// Do all rendering associated with this <see cref="CurveItem"/> to the specified
        /// <see cref="Graphics"/> device.  This method is normally only
        /// called by the Draw method of the parent <see cref="ZedGraph.CurveList"/>
        /// collection object.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="pos">The ordinal position of the current <see cref="Bar"/>
        /// curve.</param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="ZedGraph.GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        public override void Draw(IGraphics g, GraphPane pane, int pos, float scaleFactor)
        {
            if (IsXAxisMarker)
                DrawXMarker(pane, g);
            else
                DrawYMarker(pane, g);
        }

        private void DrawXMarker(GraphPane pane, IGraphics g)
        {
            var x = pane.XAxis.Scale.Transform(Value);
            var yMid = (pane.Rect.Top + pane.Rect.Bottom) / 2;
            if (!pane.Rect.Contains(x, yMid))
                return;

            var ymax = pane.Rect.Bottom;
            using (var pen = new Pen(PenColor))
            {
                g.DrawLine(pen, x, 0, x, ymax);
            }
        }

        private void DrawYMarker(GraphPane pane, IGraphics g)
        {
            var y = pane.YAxis.Scale.Transform(Value);
            var xMid = (pane.Rect.Right + pane.Rect.Left) / 2;
            if (!pane.Rect.Contains(xMid, y))
                return;

            var xmax = pane.Rect.Right;
            using (var pen = new Pen(PenColor))
            {
                g.DrawLine(pen, 0, y, xmax, y);
            }
        }

        public override void GetRange(out double xMin, out double xMax, out double yMin, out double yMax, bool ignoreInitial, bool isBoundedRanges, GraphPane pane)
        {
            // initialize the values to outrageous ones to start
            xMin = yMin = Double.MaxValue;
            xMax = yMax = Double.MinValue;

            if(IsXAxisMarker)
            {
                xMin = xMax = Value;
            }
            else
            {
                yMin = yMax = Value;
            }
        }

        /// <summary>
        /// Draw a legend key entry for this <see cref="CurveItem"/> at the specified location.
        /// This abstract base method passes through to <see cref="BarItem.DrawLegendKey"/> or
        /// <see cref="LineItem.DrawLegendKey"/> to do the rendering.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="ZedGraph.GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="rect">The <see cref="RectangleF"/> struct that specifies the
        /// location for the legend key</param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="ZedGraph.GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        public override void DrawLegendKey(IGraphics g, GraphPane pane, RectangleF rect, float scaleFactor)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Determine the coords for the rectangle associated with a specified point for 
        /// this <see cref="CurveItem" />
        /// </summary>
        /// <param name="pane">The <see cref="GraphPane" /> to which this curve belongs</param>
        /// <param name="i">The index of the point of interest</param>
        /// <param name="coords">A list of coordinates that represents the "rect" for
        /// this point (used in an html AREA tag)</param>
        /// <returns>true if it's a valid point, false otherwise</returns>
        public override bool GetCoords(GraphPane pane, int i, out string coords)
        {
            return IsXAxisMarker ? GetXCoords(pane, out coords) : GetYCoords(pane, out coords);
        }

        private bool GetXCoords(GraphPane pane, out string coords)
        {
            var x = pane.XAxis.Scale.Transform(Value);
            var ymax = pane.Rect.Bottom;
            coords = string.Format("{0:f0},{1:f0},{2:f0},{3:f0},{4:f0},{5:f0},{6:f0},{7:f0},",
                                   x - 3, 0, x + 3, 0, x + 3, ymax, x - 3, ymax);
            return true;
        }

        private bool GetYCoords(GraphPane pane, out string coords)
        {
            var y = pane.YAxis.Scale.Transform(Value);
            var xmax = pane.Rect.Right;
            coords = string.Format("{0:f0},{1:f0},{2:f0},{3:f0},{4:f0},{5:f0},{6:f0},{7:f0},",
                                    0, y - 3, 0, y + 3, xmax, y + 3, xmax, y - 3);
            return true;
        }
    }

}
