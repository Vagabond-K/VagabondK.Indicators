using Avalonia;

namespace VagabondK.Indicators.Avalonia
{
    /// <summary>
    /// ��ġ�� ���� ������ ���ڵ�� ǥ���ϴ� �ε��������Դϴ�.
    /// </summary>
    public class DigitalNumber : DigitalIndicator, IDigitalNumber
    {
        /// <summary>
        /// IntegerDigits ��Ÿ�ϵ� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly StyledProperty<int> IntegerDigitsProperty = AvaloniaProperty.Register<DigitalNumber, int>(nameof(IntegerDigits), 5);
        /// <summary>
        /// DecimalPlaces ��Ÿ�ϵ� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly StyledProperty<int> DecimalPlacesProperty = AvaloniaProperty.Register<DigitalNumber, int>(nameof(DecimalPlaces), 0);
        /// <summary>
        /// DecimalSeparatorSize ��Ÿ�ϵ� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly StyledProperty<double> DecimalSeparatorSizeProperty = AvaloniaProperty.Register<DigitalNumber, double>(nameof(DecimalSeparatorSize), 0.1);
        /// <summary>
        /// DecimalPlaceScale ��Ÿ�ϵ� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly StyledProperty<double> DecimalPlaceScaleProperty = AvaloniaProperty.Register<DigitalNumber, double>(nameof(DecimalPlaceScale), 0.8);
        /// <summary>
        /// PadZeroLeft ��Ÿ�ϵ� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly StyledProperty<bool> PadZeroLeftProperty = AvaloniaProperty.Register<DigitalNumber, bool>(nameof(PadZeroLeft), false);
        /// <summary>
        /// PadZeroRight ��Ÿ�ϵ� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly StyledProperty<bool> PadZeroRightProperty = AvaloniaProperty.Register<DigitalNumber, bool>(nameof(PadZeroRight), false);
        /// <summary>
        /// MinusAlignLeft ��Ÿ�ϵ� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly StyledProperty<bool> MinusAlignLeftProperty = AvaloniaProperty.Register<DigitalNumber, bool>(nameof(MinusAlignLeft), true);

        /// <inheritdoc/>
        public int IntegerDigits { get => GetValue(IntegerDigitsProperty); set => SetValue(IntegerDigitsProperty, value); }
        /// <inheritdoc/>
        public int DecimalPlaces { get => GetValue(DecimalPlacesProperty); set => SetValue(DecimalPlacesProperty, value); }
        /// <inheritdoc/>
        public double DecimalSeparatorSize { get => GetValue(DecimalSeparatorSizeProperty); set => SetValue(DecimalSeparatorSizeProperty, value); }
        /// <inheritdoc/>
        public double DecimalPlaceScale { get => GetValue(DecimalPlaceScaleProperty); set => SetValue(DecimalPlaceScaleProperty, value); }
        /// <inheritdoc/>
        public bool PadZeroLeft { get => GetValue(PadZeroLeftProperty); set => SetValue(PadZeroLeftProperty, value); }
        /// <inheritdoc/>
        public bool PadZeroRight { get => GetValue(PadZeroRightProperty); set => SetValue(PadZeroRightProperty, value); }
        /// <inheritdoc/>
        public bool MinusAlignLeft { get => GetValue(MinusAlignLeftProperty); set => SetValue(MinusAlignLeftProperty, value); }
    }
}
