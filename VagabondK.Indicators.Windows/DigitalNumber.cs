using System;
using System.Windows;

namespace VagabondK.Indicators.Windows
{
    /// <summary>
    /// 수치형 값을 디지털 문자들로 표시하는 인디케이터입니다.
    /// </summary>
    public class DigitalNumber : DigitalIndicator, IDigitalNumber
    {
        static DigitalNumber()
        {
            IntegerDigitsProperty = RegisterProperty(nameof(IntegerDigits), typeof(int), 5);
            DecimalPlacesProperty = RegisterProperty(nameof(DecimalPlaces), typeof(int), 0);
            DecimalSeparatorSizeProperty = RegisterProperty(nameof(DecimalSeparatorSize), typeof(double), 0.1);
            DecimalPlaceScaleProperty = RegisterProperty(nameof(DecimalPlaceScale), typeof(double), 0.8);
            PadZeroLeftProperty = RegisterProperty(nameof(PadZeroLeft), typeof(bool), false);
            PadZeroRightProperty = RegisterProperty(nameof(PadZeroRight), typeof(bool), false);
            MinusAlignLeftProperty = RegisterProperty(nameof(MinusAlignLeft), typeof(bool), true);

            DefaultStyleKeyProperty.OverrideMetadata(typeof(DigitalNumber), new FrameworkPropertyMetadata(typeof(DigitalNumber)));
        }

        private static DependencyProperty RegisterProperty(string name, Type type, object defaultValue)
            => DependencyProperty.Register(name, type, typeof(DigitalNumber), new PropertyMetadata(defaultValue));

        /// <summary>
        /// IntegerDigits 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty IntegerDigitsProperty;
        /// <summary>
        /// DecimalPlaces 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty DecimalPlacesProperty;
        /// <summary>
        /// DecimalSeparatorSize 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty DecimalSeparatorSizeProperty;
        /// <summary>
        /// DecimalPlaceScale 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty DecimalPlaceScaleProperty;
        /// <summary>
        /// PadZeroLeft 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty PadZeroLeftProperty;
        /// <summary>
        /// PadZeroRight 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty PadZeroRightProperty;
        /// <summary>
        /// MinusAlignLeft 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty MinusAlignLeftProperty;

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
    }
}
