namespace VagabondK.Indicators.DigitalFonts
{
    /// <summary>
    /// 표시할 세그먼트에 대한 필터를 정의합니다.
    /// </summary>
    public enum DigitalSegmentFilter
    {
        /// <summary>
        /// 활성 세그먼트만 필터링 합니다.
        /// </summary>
        ActiveOnly = 0,
        /// <summary>
        /// 비활성 세그먼트만 필터링 합니다.
        /// </summary>
        InactiveOnly = 1,
        /// <summary>
        /// 모든 세그먼트를 표시합니다.
        /// </summary>
        All = 2,
    }
}
