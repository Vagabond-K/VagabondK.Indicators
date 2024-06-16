using VagabondK.Indicators.DigitalFonts;

namespace VagabondK.Indicators
{
    /// <summary>
    /// 표시할 세그먼트에 대한 필터에 따라 디지털 문자들을 표시합니다.
    /// </summary>
    public interface IDigitalIndicatorPresenter : IDigitalIndicator
    {
        /// <summary>
        /// 표시할 세그먼트에 대한 필터를 가져오거나 설정합니다.
        /// </summary>
        DigitalSegmentFilter SegmentFilter { get; set; }
    }
}
