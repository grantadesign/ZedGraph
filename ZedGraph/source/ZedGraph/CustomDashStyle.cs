using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Drawing.Drawing2D;

namespace ZedGraph
{
    public abstract class CustomDashStyle
    {
        public abstract void SetupPen(Pen pen, LineBase line, PointPair dataValue);

        protected void DefaultSetup(LineBase line, Pen pen)
        {
            float dashOff = line.DashOff;
            float dashOn = line.DashOn;

            if (dashOff > 1e-10 && dashOn > 1e-10)
            {
                pen.DashStyle = DashStyle.Custom;
                float[] pattern = new float[2];
                pattern[0] = dashOn;
                pattern[1] = dashOff;
                pen.DashPattern = pattern;
            }
            else
                pen.DashStyle = DashStyle.Solid;
        }
    }

    /// <summary>
    /// This "custom" dash style represents no customization at all.  It's just normal custom dashes a la 
    /// System.Drawing.Drawing2D.DashStyle.Custom.
    /// </summary>
    public class NormalCustomDashStyle : CustomDashStyle
    {
        public override void SetupPen(Pen pen, LineBase line, PointPair dataValue)
        {
            DefaultSetup(line, pen);
        }
    }

    public class DisabledWithinSingleXRegionCustomDashStyle : CustomDashStyle
    {
        public override void SetupPen(Pen pen, LineBase line, PointPair dataValue)
        {
            if (dataValue != null && ( dataValue.X < DisabledMin || dataValue.X > DisabledMax))
            {
                DefaultSetup(line, pen);
            }
            else
            {
                pen.DashStyle = DashStyle.Solid;
            }
        }

        public double DisabledMin { get;set; }
        public double DisabledMax { get;set; }

        public DisabledWithinSingleXRegionCustomDashStyle()
        {
            DisabledMin = double.MinValue;
            DisabledMax = double.MaxValue;
        }
    }


    public class CallbackCustomDashStyle : CustomDashStyle
    {
        public delegate bool ShowDashesCallback(PointPair point);
        private ShowDashesCallback callback;

        public CallbackCustomDashStyle(ShowDashesCallback callback)
        {
            this.callback = callback;
        }

        public override void SetupPen(Pen pen, LineBase line, PointPair dataValue)
        {
            if(callback(dataValue))
                DefaultSetup(line, pen);
            else
                pen.DashStyle = DashStyle.Solid;
        }
    }
}
