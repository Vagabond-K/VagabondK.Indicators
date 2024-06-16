using System;
using System.Windows;

namespace VagabondK.Indicators.Windows
{
    /// <summary>
    /// 값을 문자열 형식으로 변환하여 디지털 문자들로 표시하는 인디케이터입니다.
    /// </summary>
    public class DigitalText : DigitalIndicator, IDigitalText
    {
        static DigitalText()
        {
            LengthProperty = RegisterProperty(nameof(Length), typeof(int), 10);
            FormatProperty = RegisterProperty(nameof(Format), typeof(string), null);

            DefaultStyleKeyProperty.OverrideMetadata(typeof(DigitalText), new FrameworkPropertyMetadata(typeof(DigitalText)));
        }

        private static DependencyProperty RegisterProperty(string name, Type type, object defaultValue)
            => DependencyProperty.Register(name, type, typeof(DigitalText), new PropertyMetadata(defaultValue));

        /// <summary>
        /// Length 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty LengthProperty;
        /// <summary>
        /// Format 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty FormatProperty;

        /// <inheritdoc/>
        public int Length { get => (int)GetValue(LengthProperty); set => SetValue(LengthProperty, value); }
        /// <inheritdoc/>
        public string Format { get => (string)GetValue(FormatProperty); set => SetValue(FormatProperty, value); }
    }
}
