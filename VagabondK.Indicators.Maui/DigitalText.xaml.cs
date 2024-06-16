using Microsoft.Maui.Controls;
using System;

namespace VagabondK.Indicators.Maui
{
    /// <summary>
    /// 값을 문자열 형식으로 변환하여 디지털 문자들로 표시하는 인디케이터입니다.
    /// </summary>
    public partial class DigitalText : DigitalIndicator, IDigitalText
    {
        static DigitalText()
        {
            LengthProperty = CreateProperty(nameof(Length), typeof(int), 10);
            FormatProperty = CreateProperty(nameof(Format), typeof(string), null);
            AspectProperty = CreateProperty(nameof(Aspect), typeof(Stretch), Stretch.None);
        }
        private static BindableProperty CreateProperty(string name, Type type, object defaultValue)
            => BindableProperty.Create(name, type, typeof(DigitalText), defaultValue);

        /// <summary>
        /// Length 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty LengthProperty;
        /// <summary>
        /// Format 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty FormatProperty;
        /// <summary>
        /// Aspect 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty AspectProperty;

        /// <inheritdoc/>
        public int Length { get => (int)GetValue(LengthProperty); set => SetValue(LengthProperty, value); }
        /// <inheritdoc/>
        public string Format { get => (string)GetValue(FormatProperty); set => SetValue(FormatProperty, value); }
        /// <summary>
        /// 뷰 안을 채우기 위해 내용을 늘여야 하는 방법을 설명하는 값을 가져오거나 설정합니다.
        /// </summary>
        public Stretch Aspect { get => (Stretch)GetValue(AspectProperty); set => SetValue(AspectProperty, value); }

        /// <summary>
        /// 생성자
        /// </summary>
        public DigitalText()
        {
            InitializeComponent();
        }
    }
}