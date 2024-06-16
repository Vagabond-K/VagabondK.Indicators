using Avalonia;
using Avalonia.Collections;
using Avalonia.Media;
using VagabondK.Indicators.DigitalFonts;

namespace VagabondK.Indicators.Avalonia
{
    /// <summary>
    /// 값을 여러 세그먼트로 구성된 디지털 문자들로 표시하는 인디케이터입니다.
    /// </summary>
    public abstract class DigitalIndicator : DefaultTemplatedControl, IDigitalIndicator
    {
        /// <summary>
        /// Active 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<IBrush> ActiveProperty = AvaloniaProperty.Register<DigitalIndicator, IBrush>(nameof(Active), new SolidColorBrush(Colors.Lime));
        /// <summary>
        /// Inactive 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<IBrush> InactiveProperty = AvaloniaProperty.Register<DigitalIndicator, IBrush>(nameof(Inactive), new SolidColorBrush(Color.FromUInt32(0x40808080)));
        /// <summary>
        /// ActiveStroke 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<IBrush> ActiveStrokeProperty = AvaloniaProperty.Register<DigitalIndicator, IBrush>(nameof(ActiveStroke), new SolidColorBrush(Colors.Transparent));
        /// <summary>
        /// InactiveStroke 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<IBrush> InactiveStrokeProperty = AvaloniaProperty.Register<DigitalIndicator, IBrush>(nameof(InactiveStroke), new SolidColorBrush(Colors.Transparent));
        /// <summary>
        /// StrokeThickness 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<double> StrokeThicknessProperty = AvaloniaProperty.Register<DigitalIndicator, double>(nameof(StrokeThickness), 0);
        /// <summary>
        /// StrokeLineCap 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<PenLineCap> StrokeLineCapProperty = AvaloniaProperty.Register<DigitalIndicator, PenLineCap>(nameof(StrokeLineCap), PenLineCap.Round);
        /// <summary>
        /// StrokeJoin 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<PenLineJoin> StrokeJoinProperty = AvaloniaProperty.Register<DigitalIndicator, PenLineJoin>(nameof(StrokeJoin), PenLineJoin.Round);
        /// <summary>
        /// StrokeDashOffset 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<double> StrokeDashOffsetProperty = AvaloniaProperty.Register<DigitalIndicator, double>(nameof(StrokeDashOffset), 0);
        /// <summary>
        /// StrokeDashArray 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<AvaloniaList<double>> StrokeDashArrayProperty = AvaloniaProperty.Register<DigitalIndicator, AvaloniaList<double>>(nameof(StrokeDashArray), null);
        /// <summary>
        /// DigitalFont 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<DigitalFont> DigitalFontProperty = AvaloniaProperty.Register<DigitalIndicator, DigitalFont>(nameof(DigitalFont), new SevenSegmentFont());
        /// <summary>
        /// Spacing 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<double> SpacingProperty = AvaloniaProperty.Register<DigitalIndicator, double>(nameof(Spacing), 0.2);
        /// <summary>
        /// Value 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<object> ValueProperty = AvaloniaProperty.Register<DigitalIndicator, object>(nameof(Value));

        /// <summary>
        /// 활성 세그먼트 영역을 그릴 때 사용할 브러시를 가져오거나 설정합니다.
        /// </summary>
        public IBrush Active { get => GetValue(ActiveProperty); set => SetValue(ActiveProperty, value); }
        /// <summary>
        /// 비활성 세그먼트 영역을 그릴 때 사용할 브러시를 가져오거나 설정합니다.
        /// </summary>
        public IBrush Inactive { get => GetValue(InactiveProperty); set => SetValue(InactiveProperty, value); }
        /// <summary>
        /// 활성 세그먼트 윤곽선을 그릴 때 사용할 브러시를 가져오거나 설정합니다.
        /// </summary>
        public IBrush ActiveStroke { get => GetValue(ActiveStrokeProperty); set => SetValue(ActiveStrokeProperty, value); }
        /// <summary>
        /// 비활성 세그먼트 윤곽선을 그릴 때 사용할 브러시를 가져오거나 설정합니다.
        /// </summary>
        public IBrush InactiveStroke { get => GetValue(InactiveStrokeProperty); set => SetValue(InactiveStrokeProperty, value); }
        /// <summary>
        /// 세그먼트 윤곽선 두께를 가져오거나 설정합니다.
        /// </summary>
        public double StrokeThickness { get => GetValue(StrokeThicknessProperty); set => SetValue(StrokeThicknessProperty, value); }
        /// <summary>
        /// 윤곽선의 끝 부분에 사용할 도형 형식을 가져오거나 설정합니다.
        /// </summary>
        public PenLineCap StrokeLineCap { get => GetValue(StrokeLineCapProperty); set => SetValue(StrokeLineCapProperty, value); }
        /// <summary>
        /// 윤곽선의 꼭짓점에 사용되는 이음새의 형식을 가져오거나 설정합니다.
        /// </summary>
        public PenLineJoin StrokeJoin { get => GetValue(StrokeJoinProperty); set => SetValue(StrokeJoinProperty, value); }
        /// <summary>
        /// 대시 시퀀스와 윤곽선 시작 위치 사이의 간격을 가져오거나 설정합니다.
        /// </summary>
        public double StrokeDashOffset { get => GetValue(StrokeDashOffsetProperty); set => SetValue(StrokeDashOffsetProperty, value); }
        /// <summary>
        /// 윤곽선의 대시 및 간격 컬렉션을 가져오거나 설정합니다.
        /// </summary>
        public AvaloniaList<double> StrokeDashArray { get => GetValue(StrokeDashArrayProperty); set => SetValue(StrokeDashArrayProperty, value); }

        /// <inheritdoc/>
        public DigitalFont DigitalFont { get => GetValue(DigitalFontProperty); set => SetValue(DigitalFontProperty, value); }
        /// <inheritdoc/>
        public double Spacing { get => (double)GetValue(SpacingProperty); set => SetValue(SpacingProperty, value); }
        /// <inheritdoc/>
        public object Value { get => GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
    }
}
