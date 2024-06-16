using System;
using System.Text;
using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators.PartBuilders
{
    /// <summary>
    /// 둥근 직사각형 파트 드로잉 빌더입니다. 너비, 높이, 모퉁이 반지름 등의 파라미터를 지정할 수 있습니다.
    /// </summary>
    public class RoundedRectangleBuilder : ParametricPartBuilder
    {
        private static readonly double arcBezierRatio = 4 * (Math.Sqrt(2) - 1) / 3;

        private double width;
        private double height;
        private double cornerRadius = 0.5;

        /// <summary>
        /// 직사각형의 너비를 가져오거나 설정합니다.
        /// </summary>
        public double Width { get => width; set => SetParameter(ref width, value); }
        /// <summary>
        /// 직사각형의 높이를 가져오거나 설정합니다.
        /// </summary>
        public double Height { get => height; set => SetParameter(ref height, value); }
        /// <summary>
        /// 직사각형의 모퉁이 반지름을 가져오거나 설정합니다.
        /// </summary>
        public double CornerRadius { get => cornerRadius; set => SetParameter(ref cornerRadius, value); }

        /// <summary>
        /// 대상 캐시의 모든 파라미터가 현재 캐시의 파라미터들과 같은지 여부를 가져옵니다.
        /// </summary>
        /// <param name="target">대상 캐시</param>
        /// <returns>모든 파라미터의 일치 여부</returns>
        public override bool EqualsParameters(ParametricCache<PartDrawing> target)
            => base.EqualsParameters(target)
            && target is RoundedRectangleBuilder parameters
            && width.Equals(parameters.width)
            && height.Equals(parameters.height)
            && cornerRadius.Equals(parameters.cornerRadius);

        /// <summary>
        /// 파라미터를 나타내는 문자열을 생성할 때 호출됩니다. 해당 문자열은 Hash를 생성할 때 사용되므로, 반드시 모든 파라미터의 값을 콤마 등의 구분기호로 나누어 문자열에 포합해야 합니다.
        /// </summary>
        /// <param name="stringBuilder">StringBuilder 객체</param>
        /// <returns>파라미터 문자열</returns>
        protected override StringBuilder OnGenerateParametersString(StringBuilder stringBuilder)
            => base.OnGenerateParametersString(stringBuilder)
            .Append(',').Append(width)
            .Append(',').Append(height)
            .Append(',').Append(cornerRadius);

        /// <summary>
        /// 파라미터들을 이용하여 객체를 생성할 때 호출됩니다.
        /// </summary>
        /// <returns>캐시할 객체</returns>
        protected override PartDrawing OnCreate()
        {
            var width = this.width.ToValidValue();
            var height = this.height.ToValidValue();
            var cornerRadius = Math.Max(width, height) / 2 * this.cornerRadius.ToValidValue();
            if (cornerRadius > 0)
            {
                bool overRadiusX = cornerRadius > width / 2;
                bool overRadiusY = cornerRadius > height / 2;
                if (overRadiusX && overRadiusY)
                {
                    var center = width / 2;
                    var middle = height / 2;
                    var cpOffsetX = center * arcBezierRatio;
                    var cpOffsetY = middle * arcBezierRatio;
                    var cpOffsetX1 = center - cpOffsetX;
                    var cpOffsetX2 = center + cpOffsetX;
                    var cpOffsetY1 = middle - cpOffsetY;
                    var cpOffsetY2 = middle + cpOffsetY;
                    var leftTop = new CubicBezier(new Point(0, middle), new Point(0, cpOffsetY1), new Point(cpOffsetX1, 0), new Point(center, 0));
                    var rightTop = new CubicBezier(leftTop.End, new Point(cpOffsetX2, 0), new Point(width, cpOffsetY1), new Point(width, middle));
                    var rightBottom = new CubicBezier(rightTop.End, new Point(width, cpOffsetY2), new Point(cpOffsetX2, height), new Point(center, height));
                    var leftBottom = new CubicBezier(rightBottom.End, new Point(cpOffsetX1, height), new Point(0, cpOffsetY2), new Point(0, middle));
                    return new DelegateDrawing(context =>
                    {
                        context.BeginPath(leftTop.Start);
                        context.DrawCubicBezier(leftTop.Control1, leftTop.Control2, leftTop.End);
                        context.DrawCubicBezier(rightTop.Control1, rightTop.Control2, rightTop.End);
                        context.DrawCubicBezier(rightBottom.Control1, rightBottom.Control2, rightBottom.End);
                        context.DrawCubicBezier(leftBottom.Control1, leftBottom.Control2, leftBottom.End);
                        context.Close();
                    });
                }
                else if (overRadiusX)
                {
                    var center = width / 2;
                    var middle1 = Math.Min(cornerRadius, height / 2);
                    var middle2 = height - middle1;
                    var cpOffsetX = center * arcBezierRatio;
                    var cpOffsetY = middle1 * arcBezierRatio;
                    var cpOffsetX1 = center - cpOffsetX;
                    var cpOffsetX2 = center + cpOffsetX;
                    var cpOffsetY1 = middle1 - cpOffsetY;
                    var cpOffsetY2 = middle2 + cpOffsetY;
                    var leftTop = new CubicBezier(new Point(0, middle1), new Point(0, cpOffsetY1), new Point(cpOffsetX1, 0), new Point(center, 0));
                    var rightTop = new CubicBezier(leftTop.End, new Point(cpOffsetX2, 0), new Point(width, cpOffsetY1), new Point(width, middle1));
                    var rightBottom = new CubicBezier(new Point(width, middle2), new Point(width, cpOffsetY2), new Point(cpOffsetX2, height), new Point(center, height));
                    var leftBottom = new CubicBezier(rightBottom.End, new Point(cpOffsetX1, height), new Point(0, cpOffsetY2), new Point(0, middle2));
                    return new DelegateDrawing(context =>
                    {
                        context.BeginPath(leftTop.Start);
                        context.DrawCubicBezier(leftTop.Control1, leftTop.Control2, leftTop.End);
                        context.DrawCubicBezier(rightTop.Control1, rightTop.Control2, rightTop.End);
                        context.DrawLine(rightBottom.Start);
                        context.DrawCubicBezier(rightBottom.Control1, rightBottom.Control2, rightBottom.End);
                        context.DrawCubicBezier(leftBottom.Control1, leftBottom.Control2, leftBottom.End);
                        context.Close();
                    });
                }
                else if (!overRadiusY)
                {
                    var center1 = Math.Min(cornerRadius, width / 2);
                    var center2 = width - center1;
                    var middle = height / 2;
                    var cpOffsetX = center1 * arcBezierRatio;
                    var cpOffsetY = middle * arcBezierRatio;
                    var cpOffsetX1 = center1 - cpOffsetX;
                    var cpOffsetX2 = center2 + cpOffsetX;
                    var cpOffsetY1 = middle - cpOffsetY;
                    var cpOffsetY2 = middle + cpOffsetY;
                    var leftTop = new CubicBezier(new Point(0, middle), new Point(0, cpOffsetY1), new Point(cpOffsetX1, 0), new Point(center1, 0));
                    var rightTop = new CubicBezier(new Point(center2, 0), new Point(cpOffsetX2, 0), new Point(width, cpOffsetY1), new Point(width, middle));
                    var rightBottom = new CubicBezier(rightTop.End, new Point(width, cpOffsetY2), new Point(cpOffsetX2, height), new Point(center2, height));
                    var leftBottom = new CubicBezier(new Point(center1, height), new Point(cpOffsetX1, height), new Point(0, cpOffsetY2), new Point(0, middle));
                    return new DelegateDrawing(context =>
                    {
                        context.BeginPath(leftTop.Start);
                        context.DrawCubicBezier(leftTop.Control1, leftTop.Control2, leftTop.End);
                        context.DrawLine(rightTop.Start);
                        context.DrawCubicBezier(rightTop.Control1, rightTop.Control2, rightTop.End);
                        context.DrawCubicBezier(rightBottom.Control1, rightBottom.Control2, rightBottom.End);
                        context.DrawLine(leftBottom.Start);
                        context.DrawCubicBezier(leftBottom.Control1, leftBottom.Control2, leftBottom.End);
                        context.Close();
                    });
                }
                else
                {
                    var center1 = Math.Min(cornerRadius, width / 2);
                    var center2 = width - center1;
                    var middle1 = Math.Min(cornerRadius, height / 2);
                    var middle2 = height - middle1;
                    var cpOffsetX = center1 * arcBezierRatio;
                    var cpOffsetY = middle1 * arcBezierRatio;
                    var cpOffsetX1 = center1 - cpOffsetX;
                    var cpOffsetX2 = center2 + cpOffsetX;
                    var cpOffsetY1 = middle1 - cpOffsetY;
                    var cpOffsetY2 = middle2 + cpOffsetY;
                    var leftTop = new CubicBezier(new Point(0, middle1), new Point(0, cpOffsetY1), new Point(cpOffsetX1, 0), new Point(center1, 0));
                    var rightTop = new CubicBezier(new Point(center2, 0), new Point(cpOffsetX2, 0), new Point(width, cpOffsetY1), new Point(width, middle1));
                    var rightBottom = new CubicBezier(new Point(width, middle2), new Point(width, cpOffsetY2), new Point(cpOffsetX2, height), new Point(center2, height));
                    var leftBottom = new CubicBezier(new Point(center1, height), new Point(cpOffsetX1, height), new Point(0, cpOffsetY2), new Point(0, middle2));
                    return new DelegateDrawing(context =>
                    {
                        context.BeginPath(leftTop.Start);
                        context.DrawCubicBezier(leftTop.Control1, leftTop.Control2, leftTop.End);
                        context.DrawLine(rightTop.Start);
                        context.DrawCubicBezier(rightTop.Control1, rightTop.Control2, rightTop.End);
                        context.DrawLine(rightBottom.Start);
                        context.DrawCubicBezier(rightBottom.Control1, rightBottom.Control2, rightBottom.End);
                        context.DrawLine(leftBottom.Start);
                        context.DrawCubicBezier(leftBottom.Control1, leftBottom.Control2, leftBottom.End);
                        context.Close();
                    });
                }
            }
            else
            {
                var leftTop = new Point(0, 0);
                var rightTop = new Point(width, 0);
                var rightBottom = new Point(width, height);
                var leftBottom = new Point(0, height);
                return new DelegateDrawing(context =>
                {
                    context.BeginPath(leftTop);
                    context.DrawLine(rightTop);
                    context.DrawLine(rightBottom);
                    context.DrawLine(leftBottom);
                    context.Close();
                });
            }
        }
    }
}
