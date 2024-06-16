using Microsoft.AspNetCore.Components;
using VagabondK.Indicators.DigitalFonts;
using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators.Razor
{
    /// <summary>
    /// 값을 여러 세그먼트로 구성된 디지털 문자들로 표시하는 인디케이터입니다.
    /// </summary>
    public abstract class DigitalIndicator : ComponentBase, IDigitalIndicator
    {
        /// <summary>
        /// 인디케이터 컴포넌트에서 생성할 svg 태그의 class 특성에 적용할 내용을 가져오거나 설정합니다. 
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        /// <summary>
        /// 인디케이터 컴포넌트에서 생성할 svg 태그의 style 특성에 적용할 내용을 가져오거나 설정합니다.
        /// </summary>
        [Parameter]
        public string Style { get; set; }

        /// <summary>
        /// 활성 세그먼트 영역 svg 태그의 class 특성에 적용할 내용을 가져오거나 설정합니다. 
        /// 해당 class 특성에는 항상 자동으로 vki-digital-presenter 클래스가 추가됩니다.
        /// </summary>
        [Parameter]
        public string ActiveClass { get; set; }

        /// <summary>
        /// 활성 세그먼트 영역 svg 태그의 style 특성에 적용할 내용을 가져오거나 설정합니다. 
        /// </summary>
        [Parameter]
        public string ActiveStyle { get; set; }

        /// <summary>
        /// 비활성 세그먼트 영역 svg 태그의 class 특성에 적용할 내용을 가져오거나 설정합니다. 
        /// 해당 class 특성에는 항상 자동으로 vki-digital-presenter 클래스가 추가됩니다.
        /// </summary>
        [Parameter]
        public string InactiveClass { get; set; }

        /// <summary>
        /// 비활성 세그먼트 영역 svg 태그의 style 특성에 적용할 내용을 가져오거나 설정합니다. 
        /// </summary>
        [Parameter]
        public string InactiveStyle { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public DigitalFont DigitalFont { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public double Spacing { get; set; } = 0.2;

        /// <summary>
        /// 활성 세그먼트 뒤에 비활성 세그먼트도 함께 렌더링 여부를 가져오거나 설정합니다.
        /// </summary>
        [Parameter]
        public bool RenderAllInactive { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public object Value { get; set; }

        /// <summary>
        /// 공유 세그먼트를 사용할지 여부를 가져오거나 설정합니다.
        /// </summary>
        [Parameter]
        public bool UseSharedSegments { get; set; }

        /// <summary>
        /// svg 태그의 defs 태그 내용을 가져오거나 설정합니다.
        /// </summary>
        [Parameter]
        public RenderFragment SvgDefs { get; set; }

        /// <summary>
        /// SVG의 크기를 가져옵니다.
        /// </summary>
        protected Size Size { get; private set; }

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            Size = Measure();
        }

        /// <summary>
        /// SVG의 크기를 측정합니다.
        /// </summary>
        /// <returns>크기</returns>
        protected abstract Size Measure();
    }
}
