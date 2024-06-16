namespace VagabondK.Indicators.GeometryUtil
{
    /// <summary>
    /// 2차 베지어 곡선을 나타냅니다.
    /// </summary>
    public struct QuadraticBezier
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="start">시작점</param>
        /// <param name="control">컨트롤 포인트</param>
        /// <param name="end">끝점</param>
        public QuadraticBezier(in Point start, in Point control, in Point end)
        {
            this.start = start;
            this.control = control;
            this.end = end;
        }

        private Point start;
        private Point control;
        private Point end;

        /// <summary>
        /// 시작점을 가져오거나 설정합니다.
        /// </summary>
        public Point Start { get => start; set => start = value; }
        /// <summary>
        /// 컨트롤 포인트를 가져오거나 설정합니다.
        /// </summary>
        public Point Control { get => control; set => control = value; }
        /// <summary>
        /// 끝점을 가져오거나 설정합니다.
        /// </summary>
        public Point End { get => end; set => end = value; }

        /// <summary>
        /// 베지어 곡선에 지정한 변환을 적용합니다.
        /// </summary>
        /// <param name="transform">변환</param>
        public void Transform(in Transform transform)
        {
            if (transform.IsIdentity) return;
            start.Transform(transform);
            control.Transform(transform);
            end.Transform(transform);
        }
    }
}
