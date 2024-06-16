using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using VagabondK.Indicators.DigitalFonts;

namespace VagabondK.Indicators.Razor
{
    /// <summary>
    /// 페이지 상단에 공유할 디지털 문자 양식 
    /// </summary>
    public partial class SharedDigitalFontSegments : ComponentBase
    {
        /// <summary>
        /// 디지털 문자 양식을 가져오거나 설정합니다.
        /// </summary>
        [Parameter]
        public DigitalFont DigitalFont { get; set; }

        /// <summary>
        /// svg 태그의 defs 태그 내용을 가져오거나 설정합니다.
        /// </summary>
        [Parameter]
        public RenderFragment SvgDefs { get; set; }

        private string segmentIdPrefix;
        private readonly List<string> segmentPaths = new();
        private readonly static SvgPathDrawingContext partDrawingContext = new() { Renderer = new System.Text.StringBuilder() };

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            var digitalFont = DigitalFont;
            if (digitalFont == null) return;

            segmentIdPrefix = digitalFont.Hash.ToString().Replace("-", "") + "_";
            var segments = digitalFont.Segments;
            var segmentParts = segments.Select(segment => segment.Drawing).Distinct().ToList();
            segmentPaths.Clear();
            segmentPaths.AddRange(segments.Select(segment => segment.Drawing).Distinct().Select(segment =>
            {
                partDrawingContext.Renderer.Clear();
                partDrawingContext.DrawPart(new Part(segment));
                return partDrawingContext.Renderer.ToString();
            }));
        }
    }
}
