using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using VagabondK.Indicators.DigitalFonts;
using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators.Razor
{
    /// <summary>
    /// 표시할 세그먼트에 대한 필터에 따라 디지털 문자들을 표시합니다.
    /// </summary>
    public abstract partial class DigitalIndicatorPresenter : ComponentBase, IDigitalIndicatorPresenter
    {
        /// <summary>
        /// 프리젠터 컴포넌트에서 생성할 svg 태그의 class 특성에 적용할 내용을 가져오거나 설정합니다. 
        /// 해당 class 특성에는 항상 자동으로 vki-digital-presenter 클래스가 추가됩니다.
        /// </summary>
        [Parameter]
        public string Class { get; set; }

        /// <summary>
        /// 프리젠터 컴포넌트에서 생성할 svg 태그의 style 특성에 적용할 내용을 가져오거나 설정합니다.
        /// </summary>
        [Parameter]
        public string Style { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public DigitalSegmentFilter SegmentFilter { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public DigitalFont DigitalFont { get; set; }

        /// <inheritdoc/>
        [Parameter]
        public double Spacing { get; set; } = 0.2;

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
        private string segmentIdPrefix;
        private readonly List<string> segmentPaths = new();
        private readonly List<DrawingInfo> segmentDrawings = new();
        private readonly List<DrawingInfo> customDrawings = new();
        private readonly static SvgPathDrawingContext partDrawingContext = new() { Renderer = new System.Text.StringBuilder() };
        private string defaultClassName;

        readonly struct DrawingInfo
        {
            public DrawingInfo(string value, Transform transform)
            {
                Value = value;
                Transform = transform;
            }
            public string Value { get; }
            public Transform Transform { get; }
        }

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            defaultClassName = SegmentFilter == DigitalSegmentFilter.ActiveOnly ? "vki-digital-active" : "vki-digital-inactive";
            Size = Measure();

            var characterStyle = DigitalFont;
            if (characterStyle == null) return;

            segmentIdPrefix = characterStyle.Hash.ToString().Replace("-", "") + "_";
            var parts = OnCreateParts().ToArray();
            var segments = characterStyle.Segments;
            var segmentParts = segments.Select(segment => segment.Drawing).Distinct().ToList();
            segmentPaths.Clear();
            segmentPaths.AddRange(segments.Select(segment => segment.Drawing).Distinct().Select(segment =>
            {
                partDrawingContext.Renderer.Clear();
                partDrawingContext.DrawPart(new Part(segment));
                return partDrawingContext.Renderer.ToString();
            }));

            segmentDrawings.Clear();
            customDrawings.Clear();
            foreach (var part in parts)
            {
                var partIndex = segmentParts.IndexOf(part.Drawing);
                if (partIndex >= 0)
                    segmentDrawings.Add(new DrawingInfo(segmentIdPrefix + partIndex, part.Transform));
                else
                {
                    partDrawingContext.Renderer.Clear();
                    partDrawingContext.DrawPart(new Part(part.Drawing));
                    customDrawings.Add(new DrawingInfo(partDrawingContext.Renderer.ToString(), part.Transform));
                }
            }
        }

        /// <summary>
        /// 인디케이터의 크기를 측정합니다.
        /// </summary>
        /// <returns>크기</returns>
        protected abstract Size Measure();

        /// <summary>
        /// SVG에 사용할 인디케이터 파트 목록을 생성합니다.
        /// </summary>
        /// <returns>인디케이터 파트 목록</returns>
        protected abstract IEnumerable<Part> OnCreateParts();
    }
}
