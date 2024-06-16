using System;
using System.Collections.Generic;
using VagabondK.Indicators.DigitalFonts;
using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators
{
    /// <summary>
    /// 값을 문자열 형식으로 변환하여 디지털 문자들로 표시하는 인디케이터입니다.
    /// </summary>
    public interface IDigitalText : IDigitalIndicator
    {
        /// <summary>
        /// 표시할 문자열의 개수를 가져오거나 설정합니다.
        /// </summary>
        int Length { get; set; }
        /// <summary>
        /// 문자열 변환 포맷을 가져오거나 설정합니다.
        /// </summary>
        string Format { get; set; }
    }

    /// <summary>
    /// IDigitalText 인터페이스에 대한 확장 메서드 모음입니다.
    /// </summary>
    public static class DigitalTextExtentions
    {
        /// <summary>
        /// IDigitalText 크기를 측정합니다.
        /// </summary>
        /// <param name="indicator">IDigitalText 객체</param>
        /// <returns>크기</returns>
        public static Size MeasureIndicator(this IDigitalText indicator)
        {
            if (indicator == null) return default;

            var characterStyle = indicator.DigitalFont;

            var length = indicator.Length;
            var charHeight = (characterStyle?.ActualSize ?? default).Height;
            var charWidth = characterStyle?.Width ?? 0d;
            var slantAngle = Math.Max(Math.Min((characterStyle?.SlantAngle ?? 0d).ToValidValue(), 45), 0);
            var spacing = indicator.Spacing.ToValidValue() * charWidth;
            var strokeThickness = 0d;
            var slantWidth = Math.Tan(slantAngle / 180 * Math.PI) * (charHeight + strokeThickness);

            return new Size((charWidth + strokeThickness) * length + spacing * (length + 1) + slantWidth, charHeight + spacing * 2 + strokeThickness);
        }

        /// <summary>
        /// IDigitalText 객체를 그리기 위한 파트 목록을 가져옵니다.
        /// </summary>
        /// <param name="indicator">IDigitalText 객체</param>
        /// <param name="segmentFilter">표시할 세그먼트 상태 필터</param>
        /// <returns>인디케이터 파트 목록</returns>
        public static IEnumerable<Part> CreateParts(this IDigitalText indicator, DigitalSegmentFilter segmentFilter)
        {
            var characterStyle = indicator?.DigitalFont;
            if (characterStyle == null) yield break;

            var length = indicator.Length;
            var text = (string.IsNullOrWhiteSpace(indicator.Format)
                ? (indicator.Value?.ToString() ?? string.Empty)
                : string.Format($"{{0:{indicator.Format}}}", indicator.Value)).PadRight(length, ' ');
            var strokeThickness = 0;
            var charWidth = indicator.DigitalFont?.Width ?? 0d;
            var spacing = charWidth * indicator.Spacing.ToValidValue();
            var halfStrokeThickness = strokeThickness / 2;
            var pitch = charWidth + strokeThickness + spacing;

            for (int i = 0; i < length; i++)
                foreach (var part in characterStyle.GetCharacterSegments(text[i], segmentFilter))
                    yield return part * Transform.CreateTranslation(halfStrokeThickness + pitch * i + spacing, spacing + halfStrokeThickness);
        }
    }
}
