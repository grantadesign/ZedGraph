/*
	Copyright c 2003 by RiskCare Ltd.  All rights reserved.

	Redistribution and use in source and binary forms, with or without
	modification, are permitted provided that the following conditions
	are met:
	1. Redistributions of source code must retain the above copyright
	notice, this list of conditions and the following disclaimer.
	2. Redistributions in binary form must reproduce the above copyright
	notice, this list of conditions and the following disclaimer in the
	documentation and/or other materials provided with the distribution.

	THIS SOFTWARE IS PROVIDED BY THE AUTHOR AND CONTRIBUTORS ``AS IS'' AND
	ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
	IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
	ARE DISCLAIMED.  IN NO EVENT SHALL THE AUTHOR OR CONTRIBUTORS BE LIABLE
	FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
	DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS
	OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
	HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
	LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
	OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
	SUCH DAMAGE.
*/

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace SvgNet.SvgGdi
{
    /// <summary>
    /// An IGraphics implementation that simply passes every call through to a GDI+ <c>Graphics</c> object.
    /// </summary>
    public class GdiGraphics : IGraphics
    {
        private readonly Graphics graphics;

        ///<summary>
        /// Construct a new GDIGraphics object which passes each call to <c>g</c>.
        ///</summary>
        ///<param name="g"></param>
        public GdiGraphics(Graphics g)
        {
            graphics = g;
        }

#pragma warning disable 1591
        public void Flush()
        {
            graphics.Flush();
        }

        public void Flush(FlushIntention intention)
        {
            graphics.Flush(intention);
        }

        public void ResetTransform()
        {
            graphics.ResetTransform();
        }

        public void MultiplyTransform(Matrix matrix)
        {
            graphics.MultiplyTransform(matrix);
        }

        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
            graphics.MultiplyTransform(matrix, order);
        }

        public void TranslateTransform(Single dx, Single dy)
        {
            graphics.TranslateTransform(dx, dy);
        }

        public void TranslateTransform(Single dx, Single dy, MatrixOrder order)
        {
            graphics.TranslateTransform(dx, dy, order);
        }

        public void ScaleTransform(Single sx, Single sy)
        {
            graphics.ScaleTransform(sx, sy);
        }

        public void ScaleTransform(Single sx, Single sy, MatrixOrder order)
        {
            graphics.ScaleTransform(sx, sy);
        }

        public void RotateTransform(Single angle)
        {
            graphics.RotateTransform(angle);
        }

        public void RotateTransform(Single angle, MatrixOrder order)
        {
            graphics.RotateTransform(angle, order);
        }

        public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] pts)
        {
            graphics.TransformPoints(destSpace, srcSpace, pts);
        }

        public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, Point[] pts)
        {
            graphics.TransformPoints(destSpace, srcSpace, pts);
        }

        public Color GetNearestColor(Color color)
        {
            return graphics.GetNearestColor(color);
        }

        public void DrawLine(Pen pen, Single x1, Single y1, Single x2, Single y2)
        {
            graphics.DrawLine(pen, x1, y1, x2, y2);
        }

        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            graphics.DrawLine(pen, pt1, pt2);
        }

        public void DrawLines(Pen pen, PointF[] points)
        {
            graphics.DrawLines(pen, points);
        }

        public void DrawLine(Pen pen, Int32 x1, Int32 y1, Int32 x2, Int32 y2)
        {
            graphics.DrawLine(pen, x1, y1, x2, y2);
        }

        public void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            graphics.DrawLine(pen, pt1, pt2);
        }

        public void DrawLines(Pen pen, Point[] points)
        {
            graphics.DrawLines(pen, points);
        }

        public void DrawArc(Pen pen, Single x, Single y, Single width, Single height, Single startAngle, Single sweepAngle)
        {
            graphics.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
        }

        public void DrawArc(Pen pen, RectangleF rect, Single startAngle, Single sweepAngle)
        {
            graphics.DrawArc(pen, rect, startAngle, sweepAngle);
        }

        public void DrawArc(Pen pen, Int32 x, Int32 y, Int32 width, Int32 height, Int32 startAngle, Int32 sweepAngle)
        {
            graphics.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
        }

        public void DrawArc(Pen pen, Rectangle rect, Single startAngle, Single sweepAngle)
        {
            graphics.DrawArc(pen, rect, startAngle, sweepAngle);
        }

        public void DrawBezier(Pen pen, Single x1, Single y1, Single x2, Single y2, Single x3, Single y3, Single x4, Single y4)
        {
            graphics.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x4, y4);
        }

        public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4)
        {
            graphics.DrawBezier(pen, pt1, pt2, pt3, pt4);
        }

        public void DrawBeziers(Pen pen, PointF[] points)
        {
            graphics.DrawBeziers(pen, points);
        }

        public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4)
        {
            graphics.DrawBezier(pen, pt1, pt2, pt3, pt4);
        }

        public void DrawBeziers(Pen pen, Point[] points)
        {
            graphics.DrawBeziers(pen, points);
        }

        public void DrawRectangle(Pen pen, Rectangle rect)
        {
            graphics.DrawRectangle(pen, rect);
        }

        public void DrawRectangle(Pen pen, Single x, Single y, Single width, Single height)
        {
            graphics.DrawRectangle(pen, x, y, width, height);
        }

        public void DrawRectangles(Pen pen, RectangleF[] rects)
        {
            graphics.DrawRectangles(pen, rects);
        }

        public void DrawRectangle(Pen pen, Int32 x, Int32 y, Int32 width, Int32 height)
        {
            graphics.DrawRectangle(pen, x, y, width, height);
        }

        public void DrawRectangles(Pen pen, Rectangle[] rects)
        {
            graphics.DrawRectangles(pen, rects);
        }

        public void DrawEllipse(Pen pen, RectangleF rect)
        {
            graphics.DrawEllipse(pen, rect);
        }

        public void DrawEllipse(Pen pen, Single x, Single y, Single width, Single height)
        {
            graphics.DrawEllipse(pen, x, y, width, height);
        }

        public void DrawEllipse(Pen pen, Rectangle rect)
        {
            graphics.DrawEllipse(pen, rect);
        }

        public void DrawEllipse(Pen pen, Int32 x, Int32 y, Int32 width, Int32 height)
        {
            graphics.DrawEllipse(pen, x, y, width, height);
        }

        public void DrawPie(Pen pen, RectangleF rect, Single startAngle, Single sweepAngle)
        {
            graphics.DrawPie(pen, rect, startAngle, sweepAngle);
        }

        public void DrawPie(Pen pen, Single x, Single y, Single width, Single height, Single startAngle, Single sweepAngle)
        {
            graphics.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
        }

        public void DrawPie(Pen pen, Rectangle rect, Single startAngle, Single sweepAngle)
        {
            graphics.DrawPie(pen, rect, startAngle, sweepAngle);
        }

        public void DrawPie(Pen pen, Int32 x, Int32 y, Int32 width, Int32 height, Int32 startAngle, Int32 sweepAngle)
        {
            graphics.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
        }

        public void DrawPolygon(Pen pen, PointF[] points)
        {
            graphics.DrawPolygon(pen, points);
        }

        public void DrawPolygon(Pen pen, Point[] points)
        {
            graphics.DrawPolygon(pen, points);
        }

        public void DrawPath(Pen pen, GraphicsPath path)
        {
            graphics.DrawPath(pen, path);
        }

        public void DrawCurve(Pen pen, PointF[] points)
        {
            graphics.DrawCurve(pen, points);
        }

        public void DrawCurve(Pen pen, PointF[] points, Single tension)
        {
            graphics.DrawCurve(pen, points, tension);
        }

        public void DrawCurve(Pen pen, PointF[] points, Int32 offset, Int32 numberOfSegments)
        {
            graphics.DrawCurve(pen, points, offset, numberOfSegments);
        }

        public void DrawCurve(Pen pen, PointF[] points, Int32 offset, Int32 numberOfSegments, Single tension)
        {
            graphics.DrawCurve(pen, points, offset, numberOfSegments, tension);
        }

        public void DrawCurve(Pen pen, Point[] points)
        {
            graphics.DrawCurve(pen, points);
        }

        public void DrawCurve(Pen pen, Point[] points, Single tension)
        {
            graphics.DrawCurve(pen, points, tension);
        }

        public void DrawCurve(Pen pen, Point[] points, Int32 offset, Int32 numberOfSegments, Single tension)
        {
            graphics.DrawCurve(pen, points, offset, numberOfSegments, tension);
        }

        public void DrawClosedCurve(Pen pen, PointF[] points)
        {
            graphics.DrawClosedCurve(pen, points);
        }

        public void DrawClosedCurve(Pen pen, PointF[] points, Single tension, FillMode fillmode)
        {
            graphics.DrawClosedCurve(pen, points, tension, fillmode);
        }

        public void DrawClosedCurve(Pen pen, Point[] points)
        {
            graphics.DrawClosedCurve(pen, points);
        }

        public void DrawClosedCurve(Pen pen, Point[] points, Single tension, FillMode fillmode)
        {
            graphics.DrawClosedCurve(pen, points, tension, fillmode);
        }

        public void Clear(Color color)
        {
            graphics.Clear(color);
        }

        public void FillRectangle(Brush brush, RectangleF rect)
        {
            graphics.FillRectangle(brush, rect);
        }

        public void FillRectangle(Brush brush, Single x, Single y, Single width, Single height)
        {
            graphics.FillRectangle(brush, x, y, width, height);
        }

        public void FillRectangles(Brush brush, RectangleF[] rects)
        {
            graphics.FillRectangles(brush, rects);
        }

        public void FillRectangle(Brush brush, Rectangle rect)
        {
            graphics.FillRectangle(brush, rect);
        }

        public void FillRectangle(Brush brush, Int32 x, Int32 y, Int32 width, Int32 height)
        {
            graphics.FillRectangle(brush, x, y, width, height);
        }

        public void FillRectangles(Brush brush, Rectangle[] rects)
        {
            graphics.FillRectangles(brush, rects);
        }

        public void FillPolygon(Brush brush, PointF[] points)
        {
            graphics.FillPolygon(brush, points);
        }

        public void FillPolygon(Brush brush, PointF[] points, FillMode fillMode)
        {
            graphics.FillPolygon(brush, points, fillMode);
        }

        public void FillPolygon(Brush brush, Point[] points)
        {
            graphics.FillPolygon(brush, points);
        }

        public void FillPolygon(Brush brush, Point[] points, FillMode fillMode)
        {
            graphics.FillPolygon(brush, points, fillMode);
        }

        public void FillEllipse(Brush brush, RectangleF rect)
        {
            graphics.FillEllipse(brush, rect);
        }

        public void FillEllipse(Brush brush, Single x, Single y, Single width, Single height)
        {
            graphics.FillEllipse(brush, x, y, width, height);
        }

        public void FillEllipse(Brush brush, Rectangle rect)
        {
            graphics.FillEllipse(brush, rect);
        }

        public void FillEllipse(Brush brush, Int32 x, Int32 y, Int32 width, Int32 height)
        {
            graphics.FillEllipse(brush, x, y, width, height);
        }

        public void FillPie(Brush brush, Rectangle rect, Single startAngle, Single sweepAngle)
        {
            graphics.FillPie(brush, rect, startAngle, sweepAngle);
        }

        public void FillPie(Brush brush, Single x, Single y, Single width, Single height, Single startAngle, Single sweepAngle)
        {
            graphics.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
        }

        public void FillPie(Brush brush, Int32 x, Int32 y, Int32 width, Int32 height, Int32 startAngle, Int32 sweepAngle)
        {
            graphics.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
        }

        public void FillPath(Brush brush, GraphicsPath path)
        {
            graphics.FillPath(brush, path);
        }

        public void FillClosedCurve(Brush brush, PointF[] points)
        {
            graphics.FillClosedCurve(brush, points);
        }

        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode)
        {
            graphics.FillClosedCurve(brush, points, fillmode);
        }

        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode, Single tension)
        {
            graphics.FillClosedCurve(brush, points, fillmode, tension);
        }

        public void FillClosedCurve(Brush brush, Point[] points)
        {
            graphics.FillClosedCurve(brush, points);
        }

        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode)
        {
            graphics.FillClosedCurve(brush, points, fillmode);
        }

        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode, Single tension)
        {
            graphics.FillClosedCurve(brush, points, fillmode, tension);
        }

        public void FillRegion(Brush brush, Region region)
        {
            graphics.FillRegion(brush, region);
        }

        public void DrawString(String s, Font font, Brush brush, Single x, Single y)
        {
            graphics.DrawString(s, font, brush, x, y);
        }

        public void DrawString(String s, Font font, Brush brush, PointF point)
        {
            graphics.DrawString(s, font, brush, point);
        }

        public void DrawString(String s, Font font, Brush brush, Single x, Single y, StringFormat format)
        {
            graphics.DrawString(s, font, brush, x, y, format);
        }

        public void DrawString(String s, Font font, Brush brush, PointF point, StringFormat format)
        {
            graphics.DrawString(s, font, brush, point, format);
        }

        public void DrawString(String s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            graphics.DrawString(s, font, brush, layoutRectangle);
        }

        public void DrawString(String s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            graphics.DrawString(s, font, brush, layoutRectangle, format);
        }

        public SizeF MeasureString(String text, Font font, SizeF layoutArea, StringFormat stringFormat, out Int32 charactersFitted, out Int32 linesFilled)
        {
            int a, b;
            SizeF siz = graphics.MeasureString(text, font, layoutArea, stringFormat, out a, out b);
            charactersFitted = a;
            linesFilled = b;
            return siz;
        }

        public SizeF MeasureString(String text, Font font, PointF origin, StringFormat stringFormat)
        {
            return graphics.MeasureString(text, font, origin, stringFormat);
        }

        public SizeF MeasureString(String text, Font font, SizeF layoutArea)
        {
            return graphics.MeasureString(text, font, layoutArea);
        }

        public SizeF MeasureString(String text, Font font, SizeF layoutArea, StringFormat stringFormat)
        {
            return graphics.MeasureString(text, font, layoutArea, stringFormat);
        }

        public SizeF MeasureString(String text, Font font)
        {
            return graphics.MeasureString(text, font);
        }

        public SizeF MeasureString(String text, Font font, Int32 width)
        {
            return graphics.MeasureString(text, font, width);
        }

        public SizeF MeasureString(String text, Font font, Int32 width, StringFormat format)
        {
            return graphics.MeasureString(text, font, width, format);
        }

        public Region[] MeasureCharacterRanges(String text, Font font, RectangleF layoutRect, StringFormat stringFormat)
        {
            return graphics.MeasureCharacterRanges(text, font, layoutRect, stringFormat);
        }

        public void DrawIcon(Icon icon, Int32 x, Int32 y)
        {
            graphics.DrawIcon(icon, x, y);
        }

        public void DrawIcon(Icon icon, Rectangle targetRect)
        {
            graphics.DrawIcon(icon, targetRect);
        }

        public void DrawIconUnstretched(Icon icon, Rectangle targetRect)
        {
            graphics.DrawIconUnstretched(icon, targetRect);
        }

        public void DrawImage(Image image, PointF point)
        {
            graphics.DrawImage(image, point);
        }

        public void DrawImage(Image image, Single x, Single y)
        {
            graphics.DrawImage(image, x, y);
        }

        public void DrawImage(Image image, RectangleF rect)
        {
            graphics.DrawImage(image, rect);
        }

        public void DrawImage(Image image, Single x, Single y, Single width, Single height)
        {
            graphics.DrawImage(image, x, y, width, height);
        }

        public void DrawImage(Image image, Point point)
        {
            graphics.DrawImage(image, point);
        }

        public void DrawImage(Image image, Int32 x, Int32 y)
        {
            graphics.DrawImage(image, x, y);
        }

        public void DrawImage(Image image, Rectangle rect)
        {
            graphics.DrawImage(image, rect);
        }

        public void DrawImage(Image image, Int32 x, Int32 y, Int32 width, Int32 height)
        {
            graphics.DrawImage(image, x, y, width, height);
        }

        public void DrawImageUnscaled(Image image, Point point)
        {
            graphics.DrawImageUnscaled(image, point);
        }

        public void DrawImageUnscaled(Image image, Int32 x, Int32 y)
        {
            graphics.DrawImageUnscaled(image, x, y);
        }

        public void DrawImageUnscaled(Image image, Rectangle rect)
        {
            graphics.DrawImageUnscaled(image, rect);
        }

        public void DrawImageUnscaled(Image image, Int32 x, Int32 y, Int32 width, Int32 height)
        {
            graphics.DrawImageUnscaled(image, x, y, width, height);
        }

        public void DrawImage(Image image, PointF[] destPoints)
        {
            graphics.DrawImage(image, destPoints);
        }

        public void DrawImage(Image image, Point[] destPoints)
        {
            graphics.DrawImage(image, destPoints);
        }

        public void DrawImage(Image image, Single x, Single y, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            graphics.DrawImage(image, x, y, srcRect, srcUnit);
        }

        public void DrawImage(Image image, Int32 x, Int32 y, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            graphics.DrawImage(image, x, y, srcRect, srcUnit);
        }

        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            graphics.DrawImage(image, destRect, srcRect, srcUnit);
        }

        public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            graphics.DrawImage(image, destRect, srcRect, srcUnit);
        }

        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            graphics.DrawImage(image, destPoints, srcRect, srcUnit);
        }

        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
        {
            graphics.DrawImage(image, destPoints, srcRect, srcUnit);
        }

        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
        {
            graphics.DrawImage(image, destPoints, srcRect, srcUnit);
        }

        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            graphics.DrawImage(image, destPoints, srcRect, srcUnit);
        }

        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
        {
            graphics.DrawImage(image, destPoints, srcRect, srcUnit);
        }

        public void DrawImage(Image image, Rectangle destRect, Single srcX, Single srcY, Single srcWidth, Single srcHeight, GraphicsUnit srcUnit)
        {
            graphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit);
        }

        public void DrawImage(Image image, Rectangle destRect, Single srcX, Single srcY, Single srcWidth, Single srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs)
        {
            graphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit);
        }

        public void DrawImage(Image image, Rectangle destRect, Int32 srcX, Int32 srcY, Int32 srcWidth, Int32 srcHeight, GraphicsUnit srcUnit)
        {
            graphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit);
        }

        public void DrawImage(Image image, Rectangle destRect, Int32 srcX, Int32 srcY, Int32 srcWidth, Int32 srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr)
        {
            graphics.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr);
        }

        public void SetClip(Graphics g)
        {
            graphics.SetClip(g);
        }

        public void SetClip(Graphics g, CombineMode combineMode)
        {
            graphics.SetClip(g, combineMode);
        }

        public void SetClip(Rectangle rect)
        {
            graphics.SetClip(rect);
        }

        public void SetClip(Rectangle rect, CombineMode combineMode)
        {
            graphics.SetClip(rect, combineMode);
        }

        public void SetClip(RectangleF rect)
        {
            graphics.SetClip(rect);
        }

        public void SetClip(RectangleF rect, CombineMode combineMode)
        {
            graphics.SetClip(rect, combineMode);
        }

        public void SetClip(GraphicsPath path)
        {
            graphics.SetClip(path);
        }

        public void SetClip(GraphicsPath path, CombineMode combineMode)
        {
            graphics.SetClip(path, combineMode);
        }

        public void SetClip(Region region, CombineMode combineMode)
        {
            graphics.SetClip(region, combineMode);
        }

        public void IntersectClip(Rectangle rect)
        {
            graphics.IntersectClip(rect);
        }

        public void IntersectClip(RectangleF rect)
        {
            graphics.IntersectClip(rect);
        }

        public void IntersectClip(Region region)
        {
            graphics.IntersectClip(region);
        }

        public void ExcludeClip(Rectangle rect)
        {
            graphics.ExcludeClip(rect);
        }

        public void ExcludeClip(Region region)
        {
            graphics.ExcludeClip(region);
        }

        public void ResetClip()
        {
            graphics.ResetClip();
        }

        public void TranslateClip(Single dx, Single dy)
        {
            graphics.TranslateClip(dx, dy);
        }

        public void TranslateClip(Int32 dx, Int32 dy)
        {
            graphics.TranslateClip(dx, dy);
        }

        public Boolean IsVisible(Int32 x, Int32 y)
        {
            return graphics.IsVisible(x, y);
        }

        public Boolean IsVisible(Point point)
        {
            return graphics.IsVisible(point);
        }

        public Boolean IsVisible(Single x, Single y)
        {
            return graphics.IsVisible(x, y);
        }

        public Boolean IsVisible(PointF point)
        {
            return graphics.IsVisible(point);
        }

        public Boolean IsVisible(Int32 x, Int32 y, Int32 width, Int32 height)
        {
            return graphics.IsVisible(x, y, width, height);
        }

        public Boolean IsVisible(Rectangle rect)
        {
            return graphics.IsVisible(rect);
        }

        public Boolean IsVisible(Single x, Single y, Single width, Single height)
        {
            return graphics.IsVisible(x, y, width, height);
        }

        public Boolean IsVisible(RectangleF rect)
        {
            return graphics.IsVisible(rect);
        }

        public GraphicsState Save()
        {
            return graphics.Save();
        }

        public void Restore(GraphicsState gstate)
        {
            graphics.Restore(gstate);
        }

        public GraphicsContainer BeginContainer(RectangleF dstrect, RectangleF srcrect, GraphicsUnit unit)
        {
            return graphics.BeginContainer(dstrect, srcrect, unit);
        }

        public GraphicsContainer BeginContainer()
        {
            return graphics.BeginContainer();
        }

        public void EndContainer(GraphicsContainer container)
        {
            graphics.EndContainer(container);
        }

        public GraphicsContainer BeginContainer(Rectangle dstrect, Rectangle srcrect, GraphicsUnit unit)
        {
            return graphics.BeginContainer(dstrect, srcrect, unit);
        }

        public void AddMetafileComment(Byte[] data)
        {
            graphics.AddMetafileComment(data);
        }

        public CompositingMode CompositingMode{get {return graphics.CompositingMode;}set {graphics.CompositingMode = value;}}
        public Point RenderingOrigin{get {return graphics.RenderingOrigin;}set {graphics.RenderingOrigin = value;}}
        public CompositingQuality CompositingQuality{get {return graphics.CompositingQuality;}set {graphics.CompositingQuality = value;}}
        public TextRenderingHint TextRenderingHint{get {return graphics.TextRenderingHint;}set {graphics.TextRenderingHint = value;}}
        public Int32 TextContrast{get {return graphics.TextContrast;}set {graphics.TextContrast = value;}}
        public SmoothingMode SmoothingMode{get {return graphics.SmoothingMode;}set {graphics.SmoothingMode = value;}}
        public PixelOffsetMode PixelOffsetMode{get {return graphics.PixelOffsetMode;}set {graphics.PixelOffsetMode = value;}}
        public InterpolationMode InterpolationMode{get {return graphics.InterpolationMode;}set {graphics.InterpolationMode = value;}}
        public Matrix Transform{get {return graphics.Transform;}set {graphics.Transform = value;}}
        public GraphicsUnit PageUnit{get {return graphics.PageUnit;}set {graphics.PageUnit = value;}}
        public Single PageScale{get {return graphics.PageScale;}set {graphics.PageScale = value;}}
        public Single DpiX{get {return graphics.DpiX;}}
        public Single DpiY{get {return graphics.DpiY;}}
        public Region Clip{get {return graphics.Clip;}set {graphics.Clip = value;}}
        public RectangleF ClipBounds{get {return graphics.ClipBounds;}}
        public Boolean IsClipEmpty{get {return graphics.IsClipEmpty;}}
        public RectangleF VisibleClipBounds{get {return graphics.VisibleClipBounds;}}
        public Boolean IsVisibleClipEmpty{get {return graphics.IsVisibleClipEmpty;}}
#pragma warning restore 1591
        public void Dispose()
        {
            graphics.Dispose();
        }
    }
}