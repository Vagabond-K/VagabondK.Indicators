using Microsoft.Maui.Graphics;

namespace VagabondK.Indicators.Maui
{
    /// <summary>
    /// PathF 기반의 파트 드로잉 컨텍스트.
    /// </summary>
    public class PartDrawingContext : PartDrawingContext<PathF, Point>
    {
        /// <inheritdoc/>
        protected override void OnBeginPath(in Point startPoint)
            => Renderer.MoveTo(startPoint);

        /// <inheritdoc/>
        protected override void OnDrawLine(in Point endPoint)
            => Renderer.LineTo(endPoint);

        /// <inheritdoc/>
        protected override void OnDrawCubicBezier(in Point controlPoint1, in Point controlPoint2, in Point endPoint)
            => Renderer.CurveTo(controlPoint1, controlPoint2, endPoint);

        /// <inheritdoc/>
        protected override void OnDrawQuadraticBezier(in Point controlPoint, in Point endPoint)
            => Renderer.QuadTo(controlPoint, endPoint);

        /// <inheritdoc/>
        protected override void OnClosePath()
            => Renderer.Close();

        /// <inheritdoc/>
        protected override Point OnConvertToRendererPoint(in GeometryUtil.Point point) => new(point.X, point.Y);
    }
}
