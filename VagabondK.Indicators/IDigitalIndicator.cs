using VagabondK.Indicators.DigitalFonts;

namespace VagabondK.Indicators
{
    /// <summary>
    /// 값을 여러 세그먼트로 구성된 디지털 문자들로 표시하는 인디케이터입니다.
    /// </summary>
    public interface IDigitalIndicator : IIndicator
    {
        /// <summary>
        /// 디지털 문자 양식을 가져오거나 설정합니다.
        /// </summary>
        DigitalFont DigitalFont { get; set; }

        /// <summary>
        /// 디지털 문자의 가로 크기 대비 문자간 거리 비율을 가져오거나 설정합니다.
        /// </summary>
        double Spacing { get; set; }
    }
}
