using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using SvgNet.SvgGdi;

namespace ZedGraph
{
    public class BubbleItem : CurveItem
    {
        private PointPair _lowerLeft = new PointPair(0, 0);
        private PointPair _upperRight = new PointPair(0, 0);

        public PointPair LowerLeft { get { return _lowerLeft; } set {_lowerLeft = value; } }
        public PointPair UpperRight { get { return _upperRight; } set { _upperRight = value; } }
        public Color BubbleColor{get;set;}
        public bool Selected{get;set;}

        /// <summary>
        /// An image to be drawn in the center of the bubble.  This is a hack to get around the fact that 
        /// you can't specify pixel coordinates to an ImageObj.
        /// </summary>
        public Image Image{get;set;}

        public override void GetRange(out double xMin, out double xMax, out double yMin, out double yMax, bool ignoreInitial, bool isBoundedRanges, GraphPane pane)
        {
            xMin = LowerLeft.X;
            xMax = UpperRight.X;
            yMin = LowerLeft.Y;
            yMax = UpperRight.Y;
        }

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
        /// called by the Draw method of the parent <see cref="CurveList"/>
        /// collection object.
        /// </summary>
        /// <param name="g">
        /// A graphic device object to be drawn into.  This is normally e.Graphics from the
        /// PaintEventArgs argument to the Paint() method.
        /// </param>
        /// <param name="pane">
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="pos">The ordinal position of the current <see cref="Bar"/>
        /// curve.</param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        public override void Draw(IGraphics g, GraphPane pane, int pos, float scaleFactor)
        {
            RectangleF rect = GetDrawingRect(pane);

            g.FillEllipse(MakeBrush(), rect);
            g.DrawEllipse(MakePen(), rect);

            if(Image != null)
            {
                var left = rect.Left + rect.Width / 2.0 - Image.Width / 2.0;
                var top = rect.Top + rect.Height / 2.0 - Image.Height / 2.0;
                var tmpRect = new RectangleF((float)left, (float)top, Image.Width, Image.Height);
                Region clip = g.Clip;
                g.SetClip(tmpRect);
                g.DrawImageUnscaled(Image, Rectangle.Round(tmpRect));
                g.SetClip(clip, CombineMode.Replace);

            }
        }

        public RectangleF GetDrawingRect(GraphPane pane)
        {
            const float MIN_BUBBLE_SIZE = 6.0f;

            PointF min = GetPixelMin(pane);
            PointF max = GetPixelMax(pane);
            var left = min.X;
            var right = max.X;
            var top = max.Y;       // Y axis counts down from the top, so use max instead of min
            var bottom = min.Y;
            var width = Math.Abs(max.X - min.X);
            var height = Math.Abs(max.Y - min.Y);


            if (width < MIN_BUBBLE_SIZE)
            {
                left = (right + left) / 2 - (MIN_BUBBLE_SIZE / 2);
                width = MIN_BUBBLE_SIZE;
            }
            if (height < MIN_BUBBLE_SIZE)
            {
                top = (bottom + top) / 2 - (MIN_BUBBLE_SIZE / 2);
                height = MIN_BUBBLE_SIZE;
            }

            return new RectangleF(left, top, width, height);
        }

        private SolidBrush MakeBrush()
        {
            return Selected ? new SolidBrush(Color) : new SolidBrush(Color.White);
        }

        private PointF GetPixelMax(GraphPane pane)
        {
            float xPixMax = pane.XAxis.Scale.Transform(UpperRight.X);
            float yPixMax = pane.YAxis.Scale.Transform(UpperRight.Y);
            return new PointF(xPixMax, yPixMax);
        }

        private PointF GetPixelMin(GraphPane pane)
        {
            float xPixMin = pane.XAxis.Scale.Transform(LowerLeft.X);
            float yPixMin = pane.YAxis.Scale.Transform(LowerLeft.Y);
            return new PointF(xPixMin, yPixMin);
        }

        protected Pen MakePen()
        {
            float width = 1;
            Pen pen = new Pen(BubbleColor, width);
            pen.DashStyle = DashStyle.Solid;
            return pen;
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
        /// A reference to the <see cref="GraphPane"/> object that is the parent or
        /// owner of this object.
        /// </param>
        /// <param name="rect">The <see cref="RectangleF"/> struct that specifies the
        /// location for the legend key</param>
        /// <param name="scaleFactor">
        /// The scaling factor to be used for rendering objects.  This is calculated and
        /// passed down by the parent <see cref="GraphPane"/> object using the
        /// <see cref="PaneBase.CalcScaleFactor"/> method, and is used to proportionally adjust
        /// font sizes, etc. according to the actual size of the graph.
        /// </param>
        public override void DrawLegendKey(IGraphics g, GraphPane pane, RectangleF rect, float scaleFactor)
        {
            //// Draw a sample curve to the left of the label text
            //int xMid = (int)(rect.Left + rect.Width / 2.0F);
            //int yMid = (int)(rect.Top + rect.Height / 2.0F);
            ////RectangleF rect2 = rect;
            ////rect2.Y = yMid;
            ////rect2.Height = rect.Height / 2.0f;

            //_line.Fill.Draw(g, rect);

            //_line.DrawSegment(g, pane, rect.Left, yMid, rect.Right, yMid, scaleFactor);

            //// Draw a sample symbol to the left of the label text				
            //_symbol.DrawSymbol(g, pane, xMid, yMid, scaleFactor, false, null);

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
            coords = string.Empty;

            if (i != 0)
                return false;

            var rect = GetDrawingRect(pane);

            coords = String.Format("{0:f0},{1:f0},{2:f0},{3:f0}",
                                   rect.Left, rect.Top, rect.Right, rect.Bottom);

            return true;
        }
    }
}
