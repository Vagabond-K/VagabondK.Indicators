using Avalonia;

namespace VagabondK.Indicators.Avalonia
{
    /// <summary>
    /// 값을 문자열 형식으로 변환하여 디지털 문자들로 표시하는 인디케이터입니다.
    /// </summary>
    public class DigitalText : DigitalIndicator, IDigitalText
    {
        /// <summary>
        /// Length 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<int> LengthProperty = AvaloniaProperty.Register<DigitalText, int>(nameof(Length));
        /// <summary>
        /// Format 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<string> FormatProperty = AvaloniaProperty.Register<DigitalText, string>(nameof(Format));

        /// <inheritdoc/>
        public int Length { get => GetValue(LengthProperty); set => SetValue(LengthProperty, value); }
        /// <inheritdoc/>
        public string Format { get => GetValue(FormatProperty); set => SetValue(FormatProperty, value); }
    }
}
