using System;

namespace VagabondK.Indicators.GeometryUtil
{
    /// <summary>
    /// 두 개의 점으로 구성된 선분을 나타냅니다.
    /// </summary>
    public struct Line
    {
        /// <summary>
        /// 길이가 0인 선분 생성자
        /// </summary>
        /// <param name="point">위치</param>
        public Line(in Point point)
        {
            start = point;
            end = point;
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="start">시작점</param>
        /// <param name="end">끝점</param>
        public Line(in Point start, in Point end)
        {
            this.start = start;
            this.end = end;
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="startX">시작점 X 좌표</param>
        /// <param name="startY">시작점 Y 좌표</param>
        /// <param name="endX">끝점 X 좌표</param>
        /// <param name="endY">끝점 Y 좌표</param>
        public Line(in double startX, in double startY, in double endX, in double endY)
        {
            start = new Point(startX, startY);
            end = new Point(endX, endY);
        }

        private Point start;
        private Point end;

        /// <summary>
        /// 선분의 시작점을 가져오거나 설정합니다.
        /// </summary>
        public Point Start { get => start; set => start = value; }
        /// <summary>
        /// 선분의 끝점을 가져오거나 설정합니다.
        /// </summary>
        public Point End { get => end; set => end = value; }
        /// <summary>
        /// 시작점과 끝점의 X 좌표 차이를 가져옵니다.
        /// </summary>
        public double DeltaX => end.x - start.x;
        /// <summary>
        /// 시작점과 끝점의 Y 좌표 차이를 가져옵니다.
        /// </summary>
        public double DeltaY => end.y - start.y;
        /// <summary>
        /// 시작점을 기준으로 선분의 방향을 가져옵니다. 단위는 라디안입니다.
        /// </summary>
        public double Direction => Math.Atan2(DeltaY, DeltaX);
        /// <summary>
        /// 선분의 길이를 가져옵니다.
        /// </summary>
        public double Length => Math.Sqrt(Math.Pow(DeltaX, 2) + Math.Pow(DeltaY, 2));

        /// <summary>
        /// 시작점과 끝점의 위치에 변환을 적용합니다.
        /// </summary>
        /// <param name="transform">변환</param>
        public void Transform(in Transform transform)
        {
            if (transform.IsIdentity) return;
            start.Transform(transform);
            end.Transform(transform);
        }

        /// <summary>
        /// 지정한 선분이 현재 선분과 교차하는지 확인합니다.
        /// </summary>
        /// <param name="line">교차 여부를 확인할 선분</param>
        /// <returns>교차 여부</returns>
        public bool Intersection(in Line line)
        {
            int Ccw(in Point point1, in Point point2, in Point point3)
            {
                var crossProduct = (point2.x - point1.x) * (point3.y - point1.y) - (point3.x - point1.x) * (point2.y - point1.y);
                return crossProduct > 0 ? 1 : crossProduct < 0 ? -1 : 0;
            }

            bool Comparator(in Point point1, in Point point2) => point1.x == point2.x ? point1.y <= point2.y : point1.x <= point2.x;

            int l1_l2 = Ccw(start, end, line.start) * Ccw(start, end, line.end);
            int l2_l1 = Ccw(line.start, line.end, start) * Ccw(line.start, line.end, end);

            if (l1_l2 == 0 && l2_l1 == 0)
            {
                if (Comparator(end, start))
                    return Comparator(line.start, start) && Comparator(end, line.end);
                else if (Comparator(line.end, line.start))
                    return Comparator(line.end, end) && Comparator(start, line.start);
                else
                    return Comparator(line.start, end) && Comparator(start, line.end);
            }
            else
                return l1_l2 <= 0 && l2_l1 <= 0;
        }

        /// <summary>
        /// 지정한 선분의 교점을 가져옵니다.
        /// </summary>
        /// <param name="line">교점을 계산할 선분</param>
        /// <param name="crossPoint">교점</param>
        /// <param name="checkIntersection">교차 확인 여부. false면 선분에 의한 직선의 교점을 계산.</param>
        /// <returns>교점 계산 성공 여부. false이면서 checkIntersection이 true이면 교차하지 않거나 평행이고, false이면서 checkIntersection이 false이면 평행.</returns>
        public bool TryGetCrossPoint(in Line line, out Point crossPoint, bool checkIntersection = true)
        {
            if (!checkIntersection || Intersection(line))
            {
                var dx1 = DeltaX;
                var dy1 = DeltaY;
                var dx2 = line.DeltaX;
                var dy2 = line.DeltaY;
                var d = dx1 * dy2 - dy1 * dx2;
                if (d != 0)
                {
                    var a = start.y * end.x - start.x * end.y;
                    var b = line.start.y * line.end.x - line.start.x * line.end.y;
                    crossPoint = new Point((a * dx2 - dx1 * b) / d, (a * dy2 - dy1 * b) / d);
                    return true;
                }
            }
            crossPoint = default;
            return false;
        }
    }
}
