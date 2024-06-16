namespace VagabondK.Indicators.GeometryUtil
{
    /// <summary>
    /// 3차 베지어 곡선을 나타냅니다.
    /// </summary>
    public struct CubicBezier
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="start">시작점</param>
        /// <param name="control1">첫 번째 컨트롤 포인트</param>
        /// <param name="control2">두 번째 컨트롤 포인트</param>
        /// <param name="end">끝점</param>
        public CubicBezier(in Point start, in Point control1, in Point control2, in Point end)
        {
            this.start = start;
            this.control1 = control1;
            this.control2 = control2;
            this.end = end;
        }

        private Point start;
        private Point control1;
        private Point control2;
        private Point end;

        /// <summary>
        /// 시작점을 가져오거나 설정합니다.
        /// </summary>
        public Point Start { get => start; set => start = value; }
        /// <summary>
        /// 첫 번째 컨트롤 포인트를 가져오거나 설정합니다.
        /// </summary>
        public Point Control1 { get => control1; set => control1 = value; }
        /// <summary>
        /// 두 번째 컨트롤 포인트를 가져오거나 설정합니다.
        /// </summary>
        public Point Control2 { get => control2; set => control2 = value; }
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
            control1.Transform(transform);
            control2.Transform(transform);
            end.Transform(transform);
        }
    }
}
