using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators.Razor
{
    /// <summary>
    /// 세그먼트 상태에 따라 값을 문자열로 변환하여 디지털 문자들을 표시합니다.
    /// </summary>
    public class DigitalTextPresenter : DigitalIndicatorPresenter, IDigitalTextPresenter
    {
        /// <inheritdoc/>
        [Parameter]
        public int Length { get; set; }
        /// <inheritdoc/>
        [Parameter]
        public string Format { get; set; }

        /// <inheritdoc/>
        protected override Size Measure() => this.MeasureIndicator();
        /// <inheritdoc/>
        protected override IEnumerable<Part> OnCreateParts() => this.CreateParts();
    }
}
