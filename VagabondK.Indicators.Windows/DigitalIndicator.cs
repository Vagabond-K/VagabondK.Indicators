using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VagabondK.Indicators.DigitalFonts;

namespace VagabondK.Indicators.Windows
{
    /// <summary>
    /// 값을 여러 세그먼트로 구성된 디지털 문자들로 표시하는 인디케이터입니다.
    /// </summary>
    public abstract class DigitalIndicator : Control, IDigitalIndicator
    {
        static DigitalIndicator()
        {
            ActiveProperty = RegisterProperty(nameof(Active), typeof(Brush), new SolidColorBrush(Colors.Lime));
            InactiveProperty = RegisterProperty(nameof(Inactive), typeof(Brush), new SolidColorBrush(Color.FromArgb(0x40, 0x80, 0x80, 0x80)));
            ActiveStrokeProperty = RegisterProperty(nameof(ActiveStroke), typeof(Brush), new SolidColorBrush(Colors.Transparent));
            InactiveStrokeProperty = RegisterProperty(nameof(InactiveStroke), typeof(Brush), new SolidColorBrush(Colors.Transparent));
            StrokeThicknessProperty = RegisterProperty(nameof(StrokeThickness), typeof(double), 0d);
            StrokeStartLineCapProperty = RegisterProperty(nameof(StrokeStartLineCap), typeof(PenLineCap), PenLineCap.Round);
            StrokeEndLineCapProperty = RegisterProperty(nameof(StrokeEndLineCap), typeof(PenLineCap), PenLineCap.Round);
            StrokeLineJoinProperty = RegisterProperty(nameof(StrokeLineJoin), typeof(PenLineJoin), PenLineJoin.Round);
            StrokeMiterLimitProperty = RegisterProperty(nameof(StrokeMiterLimit), typeof(double), 10d);
            StrokeDashOffsetProperty = RegisterProperty(nameof(StrokeDashOffset), typeof(double), 0d);
            StrokeDashArrayProperty = RegisterProperty(nameof(StrokeDashArray), typeof(DoubleCollection), null);
            StrokeDashCapProperty = RegisterProperty(nameof(StrokeDashCap), typeof(PenLineCap), PenLineCap.Round);
            DigitalFontProperty = RegisterProperty(nameof(DigitalFont), typeof(DigitalFont), new SevenSegmentFont());
            SpacingProperty = RegisterProperty(nameof(Spacing), typeof(double), 0.2);
            ValueProperty = RegisterProperty(nameof(Value), typeof(object), null);
        }

        private static DependencyProperty RegisterProperty(string name, Type type, object defaultValue)
            => DependencyProperty.Register(name, type, typeof(DigitalIndicator), new PropertyMetadata(defaultValue));

        /// <summary>
        /// Active 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty ActiveProperty;
        /// <summary>
        /// Inactive 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty InactiveProperty;
        /// <summary>
        /// ActiveStroke 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty ActiveStrokeProperty;
        /// <summary>
        /// InactiveStroke 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty InactiveStrokeProperty;
        /// <summary>
        /// StrokeThickness 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty;
        /// <summary>
        /// StrokeStartLineCap 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty StrokeStartLineCapProperty;
        /// <summary>
        /// StrokeEndLineCap 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty StrokeEndLineCapProperty;
        /// <summary>
        /// StrokeLineJoin 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty StrokeLineJoinProperty;
        /// <summary>
        /// StrokeMiterLimit 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty StrokeMiterLimitProperty;
        /// <summary>
        /// StrokeDashOffset 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty StrokeDashOffsetProperty;
        /// <summary>
        /// StrokeDashArray 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty StrokeDashArrayProperty;
        /// <summary>
        /// StrokeDashCap 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty StrokeDashCapProperty;
        /// <summary>
        /// DigitalFont 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty DigitalFontProperty;
        /// <summary>
        /// Spacing 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty SpacingProperty;
        /// <summary>
        /// Value 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty ValueProperty;

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
        /// 윤곽선의 시작 부분에 사용할 도형 형식을 가져오거나 설정합니다.
        /// </summary>
        public PenLineCap StrokeStartLineCap { get => (PenLineCap)GetValue(StrokeStartLineCapProperty); set => SetValue(StrokeStartLineCapProperty, value); }
        /// <summary>
        /// 윤곽선의 끝 부분에 사용할 도형 형식을 가져오거나 설정합니다.
        /// </summary>
        public PenLineCap StrokeEndLineCap { get => (PenLineCap)GetValue(StrokeEndLineCapProperty); set => SetValue(StrokeEndLineCapProperty, value); }
        /// <summary>
        /// 윤곽선의 꼭짓점에 사용되는 이음새의 형식을 가져오거나 설정합니다.
        /// </summary>
        public PenLineJoin StrokeLineJoin { get => (PenLineJoin)GetValue(StrokeLineJoinProperty); set => SetValue(StrokeLineJoinProperty, value); }
        /// <summary>
        /// 윤곽선 두께의 절반에 대한 마이터 길이의 비율 제한을 가져오거나 설정합니다.
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
        /// <summary>
        /// 윤곽선의 각 대시 끝을 그리는 방법을 지정하는 값을 가져오거나 설정합니다.
        /// </summary>
        public PenLineCap StrokeDashCap { get => (PenLineCap)GetValue(StrokeDashCapProperty); set => SetValue(StrokeDashCapProperty, value); }

        /// <inheritdoc/>
        public DigitalFont DigitalFont { get => (DigitalFont)GetValue(DigitalFontProperty); set => SetValue(DigitalFontProperty, value); }
        /// <inheritdoc/>
        public double Spacing { get => (double)GetValue(SpacingProperty); set => SetValue(SpacingProperty, value); }
        /// <inheritdoc/>
        public object Value { get => GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
    }
}
