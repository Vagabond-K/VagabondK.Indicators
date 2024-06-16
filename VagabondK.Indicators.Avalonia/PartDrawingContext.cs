using Avalonia;
using Avalonia.Media;

namespace VagabondK.Indicators.Avalonia
{
    /// <summary>
    /// StreamGeometryContext 기반의 파트 드로잉 컨텍스트.
    /// </summary>
    public class PartDrawingContext : PartDrawingContext<StreamGeometryContext, Point>
    {
        /// <inheritdoc/>
        protected override void OnBeginPath(in Point startPoint)
            => Renderer.BeginFigure(startPoint, true);

        /// <inheritdoc/>
        protected override void OnDrawLine(in Point endPoint)
            => Renderer.LineTo(endPoint);

        /// <inheritdoc/>
        protected override void OnDrawCubicBezier(in Point controlPoint1, in Point controlPoint2, in Point endPoint)
            => Renderer.CubicBezierTo(controlPoint1, controlPoint2, endPoint);

        /// <inheritdoc/>
        protected override void OnDrawQuadraticBezier(in Point controlPoint, in Point endPoint)
            => Renderer.QuadraticBezierTo(controlPoint, endPoint);

        /// <inheritdoc/>
        protected override void OnClosePath()
            => Renderer.EndFigure(true);

        /// <inheritdoc/>
        protected override Point OnConvertToRendererPoint(in GeometryUtil.Point point) => new Point(point.X, point.Y);
    }
}
