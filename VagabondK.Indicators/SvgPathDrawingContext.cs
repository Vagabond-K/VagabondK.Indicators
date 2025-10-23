using System.Globalization;
using System.Text;
using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators
{
    /// <summary>
    /// 파트 드로잉 컨텍스를 통해 SVG 패스 태그 구문을 생성합니다.
    /// </summary>
    public class SvgPathDrawingContext : PartDrawingContext<StringBuilder>
    {
        /// <summary>
        /// 시작 포인트를 지정하여 렌더러로 패스를 그리기를 시작합니다.
        /// </summary>
        /// <param name="startPoint">시작 포인트</param>
        protected override void OnBeginPath(in Point startPoint)
            => Renderer.Append('M').AppendPoint(startPoint);

        /// <summary>
        /// 렌더러를 이용하여 특정 포인트를 향해 선분을 그립니다.
        /// </summary>
        /// <param name="endPoint">선분의 끝 포인트</param>
        protected override void OnDrawLine(in Point endPoint)
            => Renderer.Append('L').AppendPoint(endPoint);

        /// <summary>
        /// 렌더러를 이용하여 3차 베지어 곡선을 그립니다.
        /// </summary>
        /// <param name="controlPoint1">첫 번째 컨트롤 포인트</param>
        /// <param name="controlPoint2">두 번째 컨트롤 포인트</param>
        /// <param name="endPoint">곡선의 끝 포인트</param>
        protected override void OnDrawCubicBezier(in Point controlPoint1, in Point controlPoint2, in Point endPoint)
            => Renderer.Append('C').AppendPoint(controlPoint1).AppendPoint(controlPoint2).AppendPoint(endPoint);

        /// <summary>
        /// 렌더러를 이용하여 2차 베지어 곡선을 그립니다.
        /// </summary>
        /// <param name="controlPoint">컨트롤 포인트</param>
        /// <param name="endPoint">곡선의 끝 포인트</param>
        protected override void OnDrawQuadraticBezier(in Point controlPoint, in Point endPoint)
            => Renderer.Append('Q').AppendPoint(controlPoint).AppendPoint(endPoint);

        /// <summary>
        /// 렌더러에서 패스 닫기 작업을 수행합니다.
        /// </summary>
        protected override void OnClosePath() => Renderer.Append('Z');
    }

    static class StringBuilderExtensions
    {
        public static StringBuilder AppendPoint(this StringBuilder stringBuilder, in Point point)
            => stringBuilder.Append(' ').AppendFormat(CultureInfo.InvariantCulture, "{0}", point.X)
            .Append(' ').AppendFormat(CultureInfo.InvariantCulture, "{0}", point.Y);
    }
}
