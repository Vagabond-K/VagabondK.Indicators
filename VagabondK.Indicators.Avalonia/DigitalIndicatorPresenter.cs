using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using VagabondK.Indicators.DigitalFonts;

namespace VagabondK.Indicators.Avalonia
{
    /// <summary>
    /// 표시할 세그먼트에 대한 필터에 따라 디지털 문자들을 표시합니다.
    /// </summary>
    public abstract class DigitalIndicatorPresenter : Control, IDigitalIndicatorPresenter
    {
        static DigitalIndicatorPresenter()
        {
            FillProperty.Changed.AddClassHandler<DigitalIndicatorPresenter>(OnVisualChanged);
            StrokeProperty.Changed.AddClassHandler<DigitalIndicatorPresenter>(OnVisualChanged);
            StrokeThicknessProperty.Changed.AddClassHandler<DigitalIndicatorPresenter>(OnLayoutChanged);
            StrokeLineCapProperty.Changed.AddClassHandler<DigitalIndicatorPresenter>(OnVisualChanged);
            StrokeJoinProperty.Changed.AddClassHandler<DigitalIndicatorPresenter>(OnVisualChanged);
            StrokeDashOffsetProperty.Changed.AddClassHandler<DigitalIndicatorPresenter>(OnVisualChanged);
            StrokeDashArrayProperty.Changed.AddClassHandler<DigitalIndicatorPresenter>(OnVisualChanged);
            SegmentFilterProperty.Changed.AddClassHandler<DigitalIndicatorPresenter>(OnVisualChanged);
            DigitalFontProperty.Changed.AddClassHandler<DigitalIndicatorPresenter>(OnDigitalFontChanged);
            SpacingProperty.Changed.AddClassHandler<DigitalIndicatorPresenter>(OnLayoutChanged);
            ValueProperty.Changed.AddClassHandler<DigitalIndicatorPresenter>(OnVisualChanged);
        }

        /// <summary>
        /// Fill 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<IBrush> FillProperty = AvaloniaProperty.Register<DigitalIndicatorPresenter, IBrush>(nameof(Fill), new SolidColorBrush(Colors.Lime));
        /// <summary>
        /// Stroke 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<IBrush> StrokeProperty = AvaloniaProperty.Register<DigitalIndicatorPresenter, IBrush>(nameof(Stroke), new SolidColorBrush(Colors.Transparent));
        /// <summary>
        /// StrokeThickness 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<double> StrokeThicknessProperty = AvaloniaProperty.Register<DigitalIndicatorPresenter, double>(nameof(StrokeThickness), 0);
        /// <summary>
        /// StrokeLineCap 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<PenLineCap> StrokeLineCapProperty = AvaloniaProperty.Register<DigitalIndicatorPresenter, PenLineCap>(nameof(StrokeLineCap), PenLineCap.Round);
        /// <summary>
        /// StrokeJoin 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<PenLineJoin> StrokeJoinProperty = AvaloniaProperty.Register<DigitalIndicatorPresenter, PenLineJoin>(nameof(StrokeJoin), PenLineJoin.Round);
        /// <summary>
        /// StrokeDashOffset 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<double> StrokeDashOffsetProperty = AvaloniaProperty.Register<DigitalIndicatorPresenter, double>(nameof(StrokeDashOffset), 0);
        /// <summary>
        /// StrokeDashArray 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<AvaloniaList<double>> StrokeDashArrayProperty = AvaloniaProperty.Register<DigitalIndicatorPresenter, AvaloniaList<double>>(nameof(StrokeDashArray), null);
        /// <summary>
        /// SegmentFilter 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<DigitalSegmentFilter> SegmentFilterProperty = AvaloniaProperty.Register<DigitalIndicatorPresenter, DigitalSegmentFilter>(nameof(SegmentFilter), DigitalSegmentFilter.ActiveOnly);
        /// <summary>
        /// DigitalFont 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<DigitalFont> DigitalFontProperty = AvaloniaProperty.Register<DigitalIndicatorPresenter, DigitalFont>(nameof(DigitalFont), new SevenSegmentFont());
        /// <summary>
        /// Spacing 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<double> SpacingProperty = AvaloniaProperty.Register<DigitalIndicatorPresenter, double>(nameof(Spacing), 0.2);
        /// <summary>
        /// Value 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<object> ValueProperty = AvaloniaProperty.Register<DigitalIndicatorPresenter, object>(nameof(Value));

        /// <summary>
        /// 세그먼트 영역을 그릴 때 사용할 브러시를 가져오거나 설정합니다.
        /// </summary>
        public IBrush Fill { get => GetValue(FillProperty); set => SetValue(FillProperty, value); }
        /// <summary>
        /// 세그먼트 윤곽선을 그릴 때 사용할 브러시를 가져오거나 설정합니다.
        /// </summary>
        public IBrush Stroke { get => GetValue(StrokeProperty); set => SetValue(StrokeProperty, value); }
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
        public DigitalSegmentFilter SegmentFilter { get => GetValue(SegmentFilterProperty); set => SetValue(SegmentFilterProperty, value); }
        /// <inheritdoc/>
        public DigitalFont DigitalFont { get => GetValue(DigitalFontProperty); set => SetValue(DigitalFontProperty, value); }
        /// <inheritdoc/>
        public double Spacing { get => (double)GetValue(SpacingProperty); set => SetValue(SpacingProperty, value); }
        /// <inheritdoc/>
        public object Value { get => GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        /// <summary>
        /// 생성자
        /// </summary>
        protected DigitalIndicatorPresenter()
        {
            strokePen.Bind(Pen.BrushProperty, new Binding(nameof(Stroke)) { Source = this });
            strokePen.Bind(Pen.ThicknessProperty, new Binding(nameof(StrokeThickness)) { Source = this });
            strokePen.Bind(Pen.LineCapProperty, new Binding(nameof(StrokeLineCap)) { Source = this });
            strokePen.Bind(Pen.LineJoinProperty, new Binding(nameof(StrokeJoin)) { Source = this });
            (strokePen.DashStyle as DashStyle).Bind(DashStyle.OffsetProperty, new Binding(nameof(StrokeDashOffset)) { Source = this });
            (strokePen.DashStyle as DashStyle).Bind(DashStyle.DashesProperty, new Binding(nameof(StrokeDashArray)) { Source = this, Mode = BindingMode.TwoWay });
            if (DigitalFont is DigitalFont style)
                style.ParameterChanged += OnDigitalFontChanged;
        }

        private static void OnDigitalFontChanged(DigitalIndicatorPresenter instance, AvaloniaPropertyChangedEventArgs change)
        {
            if (instance != null)
            {
                if (change.OldValue is DigitalFont oldStyle)
                    oldStyle.ParameterChanged -= instance.OnDigitalFontChanged;
                if (change.NewValue is DigitalFont newStyle)
                {
                    newStyle.ParameterChanged += instance.OnDigitalFontChanged;
                    instance.OnDigitalFontChanged(newStyle, EventArgs.Empty);
                }
            }
        }

        private void OnDigitalFontChanged(object sender, EventArgs e) => OnLayoutChanged();
        /// <summary>
        /// 내용이 바뀌어야 할 속성이 변화되었을 때 호출됩니다.
        /// </summary>
        /// <param name="instance">호출자</param>
        /// <param name="change">속성 변화 이벤트 매개변수</param>
        protected static void OnVisualChanged(DigitalIndicatorPresenter instance, AvaloniaPropertyChangedEventArgs change) => instance?.InvalidateVisual();
        /// <summary>
        /// 레이아웃이 바뀌어야 할 속성이 변화되었을 때 호출됩니다.
        /// </summary>
        /// <param name="instance">호출자</param>
        /// <param name="change">속성 변화 이벤트 매개변수</param>
        protected static void OnLayoutChanged(DigitalIndicatorPresenter instance, AvaloniaPropertyChangedEventArgs change) => instance?.OnLayoutChanged();

        private void OnLayoutChanged()
        {
            InvalidateMeasure();
            InvalidateVisual();
        }

        private readonly PartDrawingContext partDrawingContext = new PartDrawingContext();
        private readonly Pen strokePen = new Pen() { DashStyle = new DashStyle() };

        /// <inheritdoc/>
        public override void Render(DrawingContext context)
        {
            var strokeThickness = strokePen.Thickness;
            var geometry = new StreamGeometry();
            using (var geometryContext = geometry.Open())
            {
                partDrawingContext.Renderer = geometryContext;
                foreach (var part in OnCreateParts())
                    partDrawingContext.DrawPart(part);
            }
            context.DrawGeometry(Fill, strokeThickness > 0 && strokePen.Brush != null ? strokePen : null, geometry);
        }

        /// <summary>
        /// DrawingContext에 사용할 인디케이터 파트 목록을 생성합니다.
        /// </summary>
        /// <returns>인디케이터 파트 목록</returns>
        protected abstract IEnumerable<Part> OnCreateParts();
    }
}
