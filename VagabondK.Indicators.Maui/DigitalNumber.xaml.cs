using Microsoft.Maui.Controls;
using System;

namespace VagabondK.Indicators.Maui
{
    /// <summary>
    /// 수치형 값을 디지털 문자들로 표시하는 인디케이터입니다.
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
        /// IntegerDigits 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty IntegerDigitsProperty;
        /// <summary>
        /// DecimalPlaces 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty DecimalPlacesProperty;
        /// <summary>
        /// DecimalSeparatorSize 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty DecimalSeparatorSizeProperty;
        /// <summary>
        /// DecimalPlaceScale 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty DecimalPlaceScaleProperty;
        /// <summary>
        /// PadZeroLeft 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty PadZeroLeftProperty;
        /// <summary>
        /// PadZeroRight 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty PadZeroRightProperty;
        /// <summary>
        /// MinusAlignLeft 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty MinusAlignLeftProperty;
        /// <summary>
        /// Aspect 바인딩 가능 속성의 식별자입니다.
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
        /// 뷰 안을 채우기 위해 내용을 늘여야 하는 방법을 설명하는 값을 가져오거나 설정합니다.
        /// </summary>
        public Stretch Aspect { get => (Stretch)GetValue(AspectProperty); set => SetValue(AspectProperty, value); }

        /// <summary>
        /// 생성자
        /// </summary>
        public DigitalNumber()
        {
            InitializeComponent();
        }
    }
}