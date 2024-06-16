using Avalonia;

namespace VagabondK.Indicators.Avalonia
{
    /// <summary>
    /// ���� ���ڿ� �������� ��ȯ�Ͽ� ������ ���ڵ�� ǥ���ϴ� �ε��������Դϴ�.
    /// </summary>
    public class DigitalText : DigitalIndicator, IDigitalText
    {
        /// <summary>
        /// Length ��Ÿ�ϵ� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly StyledProperty<int> LengthProperty = AvaloniaProperty.Register<DigitalText, int>(nameof(Length));
        /// <summary>
        /// Format ��Ÿ�ϵ� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly StyledProperty<string> FormatProperty = AvaloniaProperty.Register<DigitalText, string>(nameof(Format));

        /// <inheritdoc/>
        public int Length { get => GetValue(LengthProperty); set => SetValue(LengthProperty, value); }
        /// <inheritdoc/>
        public string Format { get => GetValue(FormatProperty); set => SetValue(FormatProperty, value); }
    }
}
