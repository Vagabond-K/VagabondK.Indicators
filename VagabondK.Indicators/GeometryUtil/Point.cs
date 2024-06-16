using System;

namespace VagabondK.Indicators.GeometryUtil
{
    /// <summary>
    /// 2차원 공간에서 가로 및 세로 좌표 쌍을 나타냅니다.
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="xy">X 및 Y 좌표</param>
        public Point(in double xy)
        {
            x = xy;
            y = xy;
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="x">X 좌표</param>
        /// <param name="y">Y 좌표</param>
        public Point(in double x, in double y)
        {
            this.x = x;
            this.y = y;
        }

        internal double x;
        internal double y;

        /// <summary>
        /// X 좌표를 가져오거나 설정합니다.
        /// </summary>
        public double X { get => x; set => x = value; }
        /// <summary>
        /// Y 좌표를 가져오거나 설정합니다.
        /// </summary>
        public double Y { get => y; set => y = value; }

        /// <summary>
        /// 포인트를 이동시킵니다.
        /// </summary>
        /// <param name="dx">X 좌표 이동량</param>
        /// <param name="dy">Y 좌표 이동량</param>
        /// <returns></returns>
        public Point Offset(in double dx, in double dy) => new Point(x + dx, y + dy);
        /// <summary>
        /// X축을 기준으로 미러링합니다.
        /// </summary>
        /// <param name="x">미러링 기준 좌표</param>
        /// <returns>미러링 결과 포인트</returns>
        public Point MirrorX(in double x) => new Point(x * 2 - this.x, y);
        /// <summary>
        /// Y축을 기준으로 미러링합니다.
        /// </summary>
        /// <param name="y">미러링 기준 좌표</param>
        /// <returns>미러링 결과 포인트</returns>
        public Point MirrorY(in double y) => new Point(x, y * 2 - this.y);
        /// <summary>
        /// X축 및 Y축을 기준으로 미러링합니다.
        /// </summary>
        /// <param name="x">X축 미러링 기준 좌표</param>
        /// <param name="y">Y축 미러링 기준 좌표</param>
        /// <returns>미러링 결과 포인트</returns>
        public Point MirrorXY(in double x, in double y) => new Point(x * 2 - this.x, y * 2 - Y);
        /// <summary>
        /// 지정한 방향으로 지정한 거리만큼 포인트를 이동시킵니다.
        /// </summary>
        /// <param name="radians">방향각(라디안)(</param>
        /// <param name="length">이동 거리</param>
        /// <returns>이동 결과 포인트</returns>
        public Point MoveByDirection(in double radians, in double length) => new Point(x + Math.Cos(radians) * length, y + Math.Sin(radians) * length);
        /// <summary>
        /// 지정한 방향으로 지정한 거리만큼 선분을 생성합니다.
        /// </summary>
        /// <param name="radians">방향각(라디안)</param>
        /// <param name="length">선분 길이</param>
        /// <returns>선분</returns>
        public Line LineByDirection(in double radians, in double length) => new Line(this, MoveByDirection(radians, length));
        /// <summary>
        /// 현재 점에서 지정한 끝점까지 선분을 생성합니다.
        /// </summary>
        /// <param name="end">끝점</param>
        /// <returns>선분</returns>
        public Line LineTo(in Point end) => new Line(this, end);
        /// <summary>
        /// 지정한 시작점에서 현재 점까지 선분을 생성합니다.
        /// </summary>
        /// <param name="start">시작점</param>
        /// <returns>선분</returns>
        public Line LineFrom(in Point start) => new Line(this, start);
        /// <summary>
        /// 지정한 변환을 적용합니다.
        /// </summary>
        /// <param name="transform">변환</param>
        public void Transform(in Transform transform)
        {
            if (transform.IsIdentity) return;
            if (transform.translationOnly)
            {
                x += transform.m31;
                y += transform.m32;
            }
            else
            {
                x = x * transform.m11 + y * transform.m21 + transform.m31;
                y = x * transform.m12 + y * transform.m22 + transform.m32;
            }
        }

        /// <summary>
        /// 지정한 개체와 현재 포인트가 같은지 여부를 확인합니다.
        /// </summary>
        /// <param name="obj">현재 포인트와 비교할 개체입니다.</param>
        /// <returns>지정한 개체가 현재 포인트와 같으면 true이고, 다르면 false입니다.</returns>
        public override bool Equals(object obj) => obj is Point point && x == point.x && y == point.y;
        /// <summary>
        /// 현재 개체의 해시 코드를 가져옵니다.
        /// </summary>
        /// <returns>현재 개체의 해시 코드</returns>
        public override int GetHashCode()
        {
            int hashCode = -671570706;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// 지정한 포인트가 같은지 여부를 확인합니다.
        /// </summary>
        /// <param name="point1">비교할 첫 번째 포인트입니다.</param>
        /// <param name="point2">비교할 두 번째 포인트입니다.</param>
        /// <returns>포인트가 같으면 true이고, 다르면 false입니다.</returns>
        public static bool operator ==(in Point point1, in Point point2) => point1.x == point2.x && point1.y == point2.y;
        /// <summary>
        /// 지정한 포인트가 다른지 여부를 확인합니다.
        /// </summary>
        /// <param name="point1">비교할 첫 번째 포인트입니다.</param>
        /// <param name="point2">비교할 두 번째 포인트입니다.</param>
        /// <returns>포인트가 다르면 true이고, 같으면 false입니다.</returns>
        public static bool operator !=(in Point point1, in Point point2) => !(point1 == point2);
    }
}
