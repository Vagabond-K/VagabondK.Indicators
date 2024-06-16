using System;
using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators
{
    /// <summary>
    /// 인디케이터를 구성하는 파트를 정의합니다.
    /// </summary>
    public readonly struct Part
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="drawing">파트 드로잉</param>
        public Part(PartDrawing drawing) : this(drawing, Transform.Identity) { }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="drawing">파트 드로잉</param>
        /// <param name="transform">변환</param>
        public Part(PartDrawing drawing, Transform transform)
        {
            Drawing = drawing;
            Transform = transform;
        }

        /// <summary>
        /// 파트 드로잉
        /// </summary>
        public PartDrawing Drawing { get; }
        /// <summary>
        /// 변환
        /// </summary>
        public Transform Transform { get; }

        /// <summary>
        /// 파트에 변환을 추가 적용합니다.
        /// </summary>
        /// <param name="part">파트</param>
        /// <param name="transform">변환</param>
        /// <returns>변환이 적용된 파트</returns>
        public static Part operator *(in Part part, in Transform transform)
            => new Part(part.Drawing, part.Transform * transform);

        /// <summary>
        /// 대상 변환에 파트의 기존 변환을 추가 적용합니다.
        /// </summary>
        /// <param name="transform">변환</param>
        /// <param name="part">파트</param>
        /// <returns>변환이 적용된 파트</returns>
        public static Part operator *(in Transform transform, in Part part)
            => new Part(part.Drawing, transform * part.Transform);

        abstract class StaticPart<TPart> : PartDrawing where TPart : StaticPart<TPart>, new()
        {
            public static TPart Instance { get; } = new TPart();
            public override bool IsEmpty => false;
        }

        class BasicSquare : StaticPart<BasicSquare>
        {
            protected override void OnDraw(IPartDrawingContext context)
            {
                if (context == null) return;
                context.BeginPath(new Point(0));
                context.DrawLine(new Point(1, 0));
                context.DrawLine(new Point(1, 1));
                context.DrawLine(new Point(0, 1));
                context.Close();
            }
        }

        class BasicCircle : StaticPart<BasicCircle>
        {
            static readonly double cpOffset = 2 * (Math.Sqrt(2) - 1) / 3;
            protected override void OnDraw(IPartDrawingContext context)
            {
                if (context == null) return;
                context.BeginPath(new Point(0, 0.5));
                context.DrawCubicBezier(new Point(0, 0.5 - cpOffset), new Point(0.5 - cpOffset, 0), new Point(0.5, 0));
                context.DrawCubicBezier(new Point(0.5 + cpOffset, 0), new Point(1d, 0.5 - cpOffset), new Point(1d, 0.5));
                context.DrawCubicBezier(new Point(1d, 0.5 + cpOffset), new Point(0.5 + cpOffset, 1d), new Point(0.5, 1d));
                context.DrawCubicBezier(new Point(0.5 - cpOffset, 1d), new Point(0, 0.5 + cpOffset), new Point(0, 0.5));
                context.Close();
            }
        }

        /// <summary>
        /// 직사각형 파트를 생성합니다.
        /// </summary>
        /// <param name="x">X 좌표</param>
        /// <param name="y">Y 좌표</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        /// <returns>사각형 파트</returns>
        public static Part CreateRectangle(in double x, in double y, in double width, in double height) => CreateRectangle(x, y, width, height, Transform.Identity);
        /// <summary>
        /// 직사각형 파트를 생성합니다.
        /// </summary>
        /// <param name="x">X 좌표</param>
        /// <param name="y">Y 좌표</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        /// <param name="transform">변환</param>
        /// <returns>사각형 파트</returns>
        public static Part CreateRectangle(in double x, in double y, in double width, in double height, Transform transform)
            => new Part(BasicSquare.Instance, Transform.CreateScaling(width, height).Translate(x, y)* transform);

        /// <summary>
        /// 타원 파트를 생성합니다.
        /// </summary>
        /// <param name="x">X 좌표</param>
        /// <param name="y">Y 좌표</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        /// <returns>타원 파트</returns>
        public static Part CreateEllipse(in double x, in double y, in double width, in double height) => CreateEllipse(x, y, width, height, Transform.Identity);
        /// <summary>
        /// 타원 파트를 생성합니다.
        /// </summary>
        /// <param name="x">X 좌표</param>
        /// <param name="y">Y 좌표</param>
        /// <param name="width">너비</param>
        /// <param name="height">높이</param>
        /// <param name="transform">변환</param>
        /// <returns>타원 파트</returns>
        public static Part CreateEllipse(in double x, in double y, in double width, in double height, Transform transform)
            => new Part(BasicCircle.Instance, Transform.CreateScaling(width, height).Translate(x, y) * transform);

        /// <summary>
        /// 원형 파트를 생성합니다.
        /// </summary>
        /// <param name="centerPoint">중심점</param>
        /// <param name="radius">반지름</param>
        /// <returns>원형 파트</returns>
        public static Part CreateCircle(in Point centerPoint, in double radius) => CreateCircle(centerPoint, radius, Transform.Identity);
        /// <summary>
        /// 원형 파트를 생성합니다.
        /// </summary>
        /// <param name="centerPoint">중심점</param>
        /// <param name="radius">반지름</param>
        /// <param name="transform">변환</param>
        /// <returns>원형 파트</returns>
        public static Part CreateCircle(in Point centerPoint, in double radius, Transform transform)
            => CreateEllipse(centerPoint.X - radius, centerPoint.Y - radius, radius * 2, radius * 2, transform);
    }
}
