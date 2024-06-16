using Microsoft.Maui.Controls;
using System;

namespace VagabondK.Indicators.Maui
{
    /// <summary>
    /// ��ġ�� ���� ������ ���ڵ�� ǥ���ϴ� �ε��������Դϴ�.
    /// </summary>
    public partial class DigitalNumber : DigitalIndicator, IDigitalNumber
    {
        static DigitalNumber()
        {
            IntegerDigitsProperty = CreateProperty(nameof(IntegerDigits), typeof(int), 5);
            DecimalPlacesProperty = CreateProperty(nameof(DecimalPlaces), typeof(int), 0);
            DecimalSeparatorSizeProperty = CreateProperty(nameof(DecimalSeparatorSize), typeof(double), 0.1);
            DecimalPlaceScaleProperty = CreateProperty(nameof(DecimalPlaceScale), typeof(double), 0.8);
            PadZeroLeftProperty = CreateProperty(nameof(PadZeroLeft), typeof(bool), false);
            PadZeroRightProperty = CreateProperty(nameof(PadZeroRight), typeof(bool), false);
            MinusAlignLeftProperty = CreateProperty(nameof(MinusAlignLeft), typeof(bool), true);
            AspectProperty = CreateProperty(nameof(Aspect), typeof(Stretch), Stretch.None);
        }
        private static BindableProperty CreateProperty(string name, Type type, object defaultValue)
            => BindableProperty.Create(name, type, typeof(DigitalNumber), defaultValue);

        /// <summary>
        /// IntegerDigits ���ε� ���� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly BindableProperty IntegerDigitsProperty;
        /// <summary>
        /// DecimalPlaces ���ε� ���� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly BindableProperty DecimalPlacesProperty;
        /// <summary>
        /// DecimalSeparatorSize ���ε� ���� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly BindableProperty DecimalSeparatorSizeProperty;
        /// <summary>
        /// DecimalPlaceScale ���ε� ���� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly BindableProperty DecimalPlaceScaleProperty;
        /// <summary>
        /// PadZeroLeft ���ε� ���� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly BindableProperty PadZeroLeftProperty;
        /// <summary>
        /// PadZeroRight ���ε� ���� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly BindableProperty PadZeroRightProperty;
        /// <summary>
        /// MinusAlignLeft ���ε� ���� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly BindableProperty MinusAlignLeftProperty;
        /// <summary>
        /// Aspect ���ε� ���� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly BindableProperty AspectProperty;

        /// <inheritdoc/>
        public int IntegerDigits { get => (int)GetValue(IntegerDigitsProperty); set => SetValue(IntegerDigitsProperty, value); }
        /// <inheritdoc/>
        public int DecimalPlaces { get => (int)GetValue(DecimalPlacesProperty); set => SetValue(DecimalPlacesProperty, value); }
        /// <inheritdoc/>
        public double DecimalSeparatorSize { get => (double)GetValue(DecimalSeparatorSizeProperty); set => SetValue(DecimalSeparatorSizeProperty, value); }
        /// <inheritdoc/>
        public double DecimalPlaceScale { get => (double)GetValue(DecimalPlaceScaleProperty); set => SetValue(DecimalPlaceScaleProperty, value); }
        /// <inheritdoc/>
        public bool PadZeroLeft { get => (bool)GetValue(PadZeroLeftProperty); set => SetValue(PadZeroLeftProperty, value); }
        /// <inheritdoc/>
        public bool PadZeroRight { get => (bool)GetValue(PadZeroRightProperty); set => SetValue(PadZeroRightProperty, value); }
        /// <inheritdoc/>
        public bool MinusAlignLeft { get => (bool)GetValue(MinusAlignLeftProperty); set => SetValue(MinusAlignLeftProperty, value); }
        /// <summary>
        /// �� ���� ä��� ���� ������ �ÿ��� �ϴ� ����� �����ϴ� ���� �������ų� �����մϴ�.
        /// </summary>
        public Stretch Aspect { get => (Stretch)GetValue(AspectProperty); set => SetValue(AspectProperty, value); }

        /// <summary>
        /// ������
        /// </summary>
        public DigitalNumber()
        {
            InitializeComponent();
        }
    }
}