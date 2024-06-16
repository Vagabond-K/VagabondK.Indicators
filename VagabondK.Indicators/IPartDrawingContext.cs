using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators
{
    /// <summary>
    /// 파트 드로잉 컨텍스트 인터페이스. 파트의 모양을 패스 기반으로 그리기 위한 메서드를 정의합니다.
    /// </summary>
    public interface IPartDrawingContext
    {
        /// <summary>
        /// 시작점을 지정하여 패스를 그리기를 시작합니다.
        /// </summary>
        /// <param name="point">시작점</param>
        void BeginPath(Point point);
        /// <summary>
        /// 특정 지점을 향해 선분을 그립니다.
        /// </summary>
        /// <param name="point">선분의 끝점</param>
        void DrawLine(Point point);
        /// <summary>
        /// 3차 베지어 곡선을 그립니다.
        /// </summary>
        /// <param name="control1">첫 번째 컨트롤 포인트</param>
        /// <param name="control2">두 번째 컨트롤 포인트</param>
        /// <param name="end">곡선의 끝점</param>
        void DrawCubicBezier(Point control1, Point control2, Point end);
        /// <summary>
        /// 2차 베지어 곡선을 그립니다.
        /// </summary>
        /// <param name="control">컨트롤 포인트</param>
        /// <param name="end">곡선의 끝점</param>
        void DrawQuadraticBezier(Point control, Point end);
        /// <summary>
        /// 파트를 그립니다.
        /// </summary>
        /// <param name="part">파트</param>
        void DrawPart(in Part part);
        /// <summary>
        /// 패스를 닫습니다.
        /// </summary>
        void Close();
    }
}
