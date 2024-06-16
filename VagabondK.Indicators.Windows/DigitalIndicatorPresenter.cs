using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using VagabondK.Indicators.DigitalFonts;

namespace VagabondK.Indicators.Windows
{
    /// <summary>
    /// 표시할 세그먼트에 대한 필터에 따라 디지털 문자들을 표시합니다.
    /// </summary>
    public abstract class DigitalIndicatorPresenter : FrameworkElement, IDigitalIndicatorPresenter
    {
        static DigitalIndicatorPresenter()
        {
            FillProperty = RegisterProperty(nameof(Fill), typeof(Brush), null, FrameworkPropertyMetadataOptions.AffectsRender);
            StrokeProperty = RegisterProperty(nameof(Stroke), typeof(Brush), new SolidColorBrush(Colors.Transparent), FrameworkPropertyMetadataOptions.AffectsRender);
            StrokeThicknessProperty = RegisterProperty(nameof(StrokeThickness), typeof(double), 0d, FrameworkPropertyMetadataOptions.AffectsRender);
            StrokeStartLineCapProperty = RegisterProperty(nameof(StrokeStartLineCap), typeof(PenLineCap), PenLineCap.Round, FrameworkPropertyMetadataOptions.AffectsRender);
            StrokeEndLineCapProperty = RegisterProperty(nameof(StrokeEndLineCap), typeof(PenLineCap), PenLineCap.Round, FrameworkPropertyMetadataOptions.AffectsRender);
            StrokeLineJoinProperty = RegisterProperty(nameof(StrokeLineJoin), typeof(PenLineJoin), PenLineJoin.Round, FrameworkPropertyMetadataOptions.AffectsRender);
            StrokeMiterLimitProperty = RegisterProperty(nameof(StrokeMiterLimit), typeof(double), 10d, FrameworkPropertyMetadataOptions.AffectsRender);
            StrokeDashOffsetProperty = RegisterProperty(nameof(StrokeDashOffset), typeof(double), 0d, FrameworkPropertyMetadataOptions.AffectsRender);
            StrokeDashArrayProperty = RegisterProperty(nameof(StrokeDashArray), typeof(DoubleCollection), null, FrameworkPropertyMetadataOptions.AffectsRender);
            StrokeDashCapProperty = RegisterProperty(nameof(StrokeDashCap), typeof(PenLineCap), PenLineCap.Round, FrameworkPropertyMetadataOptions.AffectsRender);
            DigitalFontProperty = RegisterProperty(nameof(DigitalFont), typeof(DigitalFont), new SevenSegmentFont(), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, OnDigitalFontChanged);
            SpacingProperty = RegisterProperty(nameof(Spacing), typeof(double), 0.2, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender);
            SegmentFilterProperty = RegisterProperty(nameof(SegmentFilter), typeof(DigitalSegmentFilter), DigitalSegmentFilter.ActiveOnly, FrameworkPropertyMetadataOptions.AffectsRender);
            ValueProperty = RegisterProperty(nameof(Value), typeof(object), null, FrameworkPropertyMetadataOptions.AffectsRender);

            DefaultStyleKeyProperty.OverrideMetadata(typeof(DigitalIndicatorPresenter), new FrameworkPropertyMetadata(typeof(DigitalIndicatorPresenter)));
        }

        private static DependencyProperty RegisterProperty(string name, Type type, object defaultValue,
            FrameworkPropertyMetadataOptions flags = FrameworkPropertyMetadataOptions.None,
            PropertyChangedCallback propertyChangedCallback = null)
            => DependencyProperty.Register(name, type, typeof(DigitalIndicatorPresenter), new FrameworkPropertyMetadata(defaultValue, flags, propertyChangedCallback));

        /// <summary>
        /// Fill 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty FillProperty;
        /// <summary>
        /// Stroke 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty StrokeProperty;
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
        /// SegmentFilter 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty SegmentFilterProperty;
        /// <summary>
        /// Spacing 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty SpacingProperty;
        /// <summary>
        /// Value 종속성 속성의 식별자입니다.
        /// </summary>
        public static readonly DependencyProperty ValueProperty;

        /// <summary>
        /// 세그먼트 영역을 그릴 때 사용할 브러시를 가져오거나 설정합니다.
        /// </summary>
        public Brush Fill { get => (Brush)GetValue(FillProperty); set => SetValue(FillProperty, value); }
        /// <summary>
        /// 세그먼트 윤곽선을 그릴 때 사용할 브러시를 가져오거나 설정합니다.
        /// </summary>
        public Brush Stroke { get => (Brush)GetValue(StrokeProperty); set => SetValue(StrokeProperty, value); }
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
        public DigitalSegmentFilter SegmentFilter { get => (DigitalSegmentFilter)GetValue(SegmentFilterProperty); set => SetValue(SegmentFilterProperty, value); }
        /// <inheritdoc/>
        public object Value { get => GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        /// <summary>
        /// 생성자
        /// </summary>
        protected DigitalIndicatorPresenter()
        {
            WeakEventManager<DigitalFont, EventArgs>.AddHandler(DigitalFont, nameof(DigitalFonts.DigitalFont.ParameterChanged), OnCharParameterChanged);
            BindingOperations.SetBinding(strokePen, Pen.BrushProperty, new Binding(nameof(Stroke)) { Source = this });
            BindingOperations.SetBinding(strokePen, Pen.ThicknessProperty, new Binding(nameof(StrokeThickness)) { Source = this });
            BindingOperations.SetBinding(strokePen, Pen.StartLineCapProperty, new Binding(nameof(StrokeStartLineCap)) { Source = this });
            BindingOperations.SetBinding(strokePen, Pen.EndLineCapProperty, new Binding(nameof(StrokeEndLineCap)) { Source = this });
            BindingOperations.SetBinding(strokePen, Pen.DashCapProperty, new Binding(nameof(StrokeDashCap)) { Source = this });
            BindingOperations.SetBinding(strokePen, Pen.LineJoinProperty, new Binding(nameof(StrokeLineJoin)) { Source = this });
            BindingOperations.SetBinding(strokePen, Pen.MiterLimitProperty, new Binding(nameof(StrokeMiterLimit)) { Source = this });
            BindingOperations.SetBinding(dashStyle, DashStyle.OffsetProperty, new Binding(nameof(StrokeDashOffset)) { Source = this });
            BindingOperations.SetBinding(dashStyle, DashStyle.DashesProperty, new Binding(nameof(StrokeDashArray)) { Source = this });
        }

        private static void OnDigitalFontChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is DigitalIndicatorPresenter instance)
            {
                if (e.OldValue is DigitalFont oldValue)
                    WeakEventManager<DigitalFont, EventArgs>.RemoveHandler(oldValue, nameof(DigitalFonts.DigitalFont.ParameterChanged), instance.OnCharParameterChanged);
                if (e.NewValue is DigitalFont newValue)
                {
                    WeakEventManager<DigitalFont, EventArgs>.AddHandler(newValue, nameof(DigitalFonts.DigitalFont.ParameterChanged), instance.OnCharParameterChanged);
                    instance.OnCharParameterChanged(newValue, EventArgs.Empty);
                }
            }
        }

        private void OnCharParameterChanged(object sender, EventArgs e)
        {
            InvalidateMeasure();
            InvalidateVisual();
        }

        private readonly DashStyle dashStyle = new DashStyle();
        private readonly PartDrawingContext partDrawingContext = new PartDrawingContext();
        private readonly Pen strokePen = new Pen();

        /// <inheritdoc/>
        protected override void OnRender(DrawingContext drawingContext)
        {
            strokePen.DashStyle = StrokeDashArray == null && StrokeDashOffset == 0.0 ? null : dashStyle;
            var strokeThickness = strokePen.Thickness;
            var geometry = new StreamGeometry();
            using (var geometryContext = geometry.Open())
            {
                partDrawingContext.Renderer = geometryContext;
                foreach (var part in OnCreateParts())
                    partDrawingContext.DrawPart(part);
            }
            geometry.Freeze();
            drawingContext.DrawGeometry(Fill, strokeThickness > 0 && strokePen.Brush != null ? strokePen : null, geometry);
        }

        /// <summary>
        /// DrawingContext에 사용할 인디케이터 파트 목록을 생성합니다.
        /// </summary>
        /// <returns>인디케이터 파트 목록</returns>
        protected abstract IEnumerable<Part> OnCreateParts();
    }
}
