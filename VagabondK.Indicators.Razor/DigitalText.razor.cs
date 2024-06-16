using Microsoft.AspNetCore.Components;
using VagabondK.Indicators.DigitalFonts;
using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators.Razor
{
    /// <summary>
    /// ���� ���ڿ� �������� ��ȯ�Ͽ� ������ ���ڵ�� ǥ���ϴ� �ε��������Դϴ�.
    /// </summary>
    public partial class DigitalText : DigitalIndicator, IDigitalText
    {
        /// <inheritdoc/>
        [Parameter]
        public int Length { get; set; } = 10;
        /// <inheritdoc/>
        [Parameter]
        public string Format { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public DigitalText()
        {
            DigitalFont = new RoundedRectCell5x7Font();
        }

        /// <inheritdoc/>
        protected override Size Measure() => this.MeasureIndicator();
    }
}