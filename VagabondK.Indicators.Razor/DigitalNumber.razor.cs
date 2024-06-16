using Microsoft.AspNetCore.Components;
using VagabondK.Indicators.DigitalFonts;
using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators.Razor
{
    /// <summary>
    /// 수치형 값을 디지털 문자들로 표시하는 인디케이터입니다.
    /// </summary>
    public partial class DigitalNumber : DigitalIndicator, IDigitalNumber
    {
        /// <inheritdoc/>
        [Parameter]
        public int IntegerDigits { get; set; } = 5;
        /// <inheritdoc/>
        [Parameter]
        public int DecimalPlaces { get; set; }
        /// <inheritdoc/>
        [Parameter]
        public double DecimalSeparatorSize { get; set; } = 0.1;
        /// <inheritdoc/>
        [Parameter]
        public double DecimalPlaceScale { get; set; } = 0.8;
        /// <inheritdoc/>
        [Parameter]
        public bool PadZeroLeft { get; set; }
        /// <inheritdoc/>
        [Parameter]
        public bool PadZeroRight { get; set; }
        /// <inheritdoc/>
        [Parameter]
        public bool MinusAlignLeft { get; set; } = true;

        /// <summary>
        /// 생성자
        /// </summary>
        public DigitalNumber()
        {
            DigitalFont = new SevenSegmentFont();
        }

        /// <inheritdoc/>
        protected override Size Measure() => this.MeasureIndicator();
    }
}