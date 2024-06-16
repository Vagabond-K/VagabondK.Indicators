using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using System;
using VagabondK.Indicators.DigitalFonts;

namespace VagabondK.Indicators.Maui
{
    /// <summary>
    /// 값을 여러 세그먼트로 구성된 디지털 문자들로 표시하는 인디케이터입니다.
    /// </summary>
    public abstract class DigitalIndicator : TemplatedView, IDigitalIndicator
    {
        static DigitalIndicator()
        {
            ActiveProperty = CreateProperty(nameof(Active), typeof(Brush), new SolidColorBrush(Colors.Lime));
            InactiveProperty = CreateProperty(nameof(Inactive), typeof(Brush), new SolidColorBrush(Color.FromUint(0x60808080)));
            ActiveStrokeProperty = CreateProperty(nameof(ActiveStroke), typeof(Brush), new SolidColorBrush(Colors.Transparent));
            InactiveStrokeProperty = CreateProperty(nameof(InactiveStroke), typeof(Brush), new SolidColorBrush(Colors.Transparent));
            StrokeThicknessProperty = CreateProperty(nameof(StrokeThickness), typeof(double), 0d);
            StrokeLineCapProperty = CreateProperty(nameof(StrokeLineCap), typeof(PenLineCap), PenLineCap.Round);
            StrokeLineJoinProperty = CreateProperty(nameof(StrokeLineJoin), typeof(PenLineJoin), PenLineJoin.Round);
            StrokeMiterLimitProperty = CreateProperty(nameof(StrokeMiterLimit), typeof(double), 10d);
            StrokeDashOffsetProperty = CreateProperty(nameof(StrokeDashOffset), typeof(double), 0d);
            StrokeDashArrayProperty = CreateProperty(nameof(StrokeDashArray), typeof(DoubleCollection), null, bindable => new DoubleCollection());
            DigitalFontProperty = CreateProperty(nameof(DigitalFont), typeof(DigitalFont), new SevenSegmentFont());
            SpacingProperty = CreateProperty(nameof(Spacing), typeof(double), 0.2);
            ValueProperty = CreateProperty(nameof(Value), typeof(object), null);
        }
        private static BindableProperty CreateProperty(string name, Type type, object defaultValue, BindableProperty.CreateDefaultValueDelegate defaultValueCreator = null)
            => BindableProperty.Create(name, type, typeof(DigitalIndicator), defaultValue, defaultValueCreator: defaultValueCreator);

        /// <summary>
        /// Active 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty ActiveProperty;
        /// <summary>
        /// Inactive 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty InactiveProperty;
        /// <summary>
        /// ActiveStroke 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty ActiveStrokeProperty;
        /// <summary>
        /// InactiveStroke 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty InactiveStrokeProperty;
        /// <summary>
        /// StrokeThickness 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty StrokeThicknessProperty;
        /// <summary>
        /// StrokeLineCap 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty StrokeLineCapProperty;
        /// <summary>
        /// StrokeLineJoin 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty StrokeLineJoinProperty;
        /// <summary>
        /// StrokeMiterLimit 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty StrokeMiterLimitProperty;
        /// <summary>
        /// StrokeDashOffset 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty StrokeDashOffsetProperty;
        /// <summary>
        /// StrokeDashArray 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty StrokeDashArrayProperty;
        /// <summary>
        /// DigitalFont 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty DigitalFontProperty;
        /// <summary>
        /// Spacing 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty SpacingProperty;
        /// <summary>
        /// Value 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty ValueProperty;

        /// <summary>
        /// 활성 세그먼트 영역을 그릴 때 사용할 브러시를 가져오거나 설정합니다.
        /// </summary>
        public Brush Active { get => (Brush)GetValue(ActiveProperty); set => SetValue(ActiveProperty, value); }
        /// <summary>
        /// 비활성 세그먼트 영역을 그릴 때 사용할 브러시를 가져오거나 설정합니다.
        /// </summary>
        public Brush Inactive { get => (Brush)GetValue(InactiveProperty); set => SetValue(InactiveProperty, value); }
        /// <summary>
        /// 활성 세그먼트 윤곽선을 그릴 때 사용할 브러시를 가져오거나 설정합니다.
        /// </summary>
        public Brush ActiveStroke { get => (Brush)GetValue(ActiveStrokeProperty); set => SetValue(ActiveStrokeProperty, value); }
        /// <summary>
        /// 비활성 세그먼트 윤곽선을 그릴 때 사용할 브러시를 가져오거나 설정합니다.
        /// </summary>
        public Brush InactiveStroke { get => (Brush)GetValue(InactiveStrokeProperty); set => SetValue(InactiveStrokeProperty, value); }
        /// <summary>
        /// 세그먼트 윤곽선 두께를 가져오거나 설정합니다.
        /// </summary>
        public double StrokeThickness { get => (double)GetValue(StrokeThicknessProperty); set => SetValue(StrokeThicknessProperty, value); }
        /// <summary>
        /// 윤곽선의 끝 부분에 사용할 도형 형식을 가져오거나 설정합니다.
        /// </summary>
        public PenLineCap StrokeLineCap { get => (PenLineCap)GetValue(StrokeLineCapProperty); set => SetValue(StrokeLineCapProperty, value); }
        /// <summary>
        /// 윤곽선의 꼭짓점에 사용되는 이음새의 형식을 가져오거나 설정합니다.
        /// </summary>
        public PenLineJoin StrokeLineJoin { get => (PenLineJoin)GetValue(StrokeLineJoinProperty); set => SetValue(StrokeLineJoinProperty, value); }
        /// <summary>
        /// 마이터 길이의 비율 제한을 가져오거나 설정합니다.
        /// </summary>
        public double StrokeMiterLimit { get => (double)GetValue(StrokeMiterLimitProperty); set => SetValue(StrokeMiterLimitProperty, value); }
        /// <summary>
        /// 대시 시퀀스와 윤곽선 시작 위치 사이의 간격을 가져오거나 설정합니다.
        /// </summary>
        public double StrokeDashOffset { get => (double)GetValue(StrokeDashOffsetProperty); set => SetValue(StrokeDashOffsetProperty, value); }
        /// <summary>
        /// 윤곽선의 대시 및 간격 컬렉션을 가져오거나 설정합니다.
        /// </summary>
        public DoubleCollection StrokeDashArray { get => (DoubleCollection)GetValue(StrokeDashArrayProperty); set => SetValue(StrokeDashArrayProperty, value); }
        /// <inheritdoc/>
        public DigitalFont DigitalFont { get => (DigitalFont)GetValue(DigitalFontProperty); set => SetValue(DigitalFontProperty, value); }
        /// <inheritdoc/>
        public double Spacing { get => (double)GetValue(SpacingProperty); set => SetValue(SpacingProperty, value); }
        /// <inheritdoc/>
        public object Value { get => GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
    }
}
