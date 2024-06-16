using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using VagabondK.Indicators.DigitalFonts;

namespace VagabondK.Indicators.Maui
{
    /// <summary>
    /// 표시할 세그먼트에 대한 필터에 따라 디지털 문자들을 표시합니다.
    /// </summary>
    public abstract class DigitalIndicatorPresenter : Shape, IView, IShape, IDigitalIndicatorPresenter
    {
        static DigitalIndicatorPresenter()
        {
            SegmentFilterProperty = CreateProperty(nameof(SegmentFilter), typeof(DigitalSegmentFilter), DigitalSegmentFilter.ActiveOnly, OnLayoutChanged);
            DigitalFontProperty = CreateProperty(nameof(DigitalFont), typeof(DigitalFont), null, OnDigitalFontChanged, bindable => new SevenSegmentFont());
            SpacingProperty = CreateProperty(nameof(Spacing), typeof(double), 0.2, OnLayoutChanged);
            ValueProperty = CreateProperty(nameof(Value), typeof(object), null, OnVisualChanged);
        }

        private static BindableProperty CreateProperty(string name, Type type, object defaultValue, BindableProperty.BindingPropertyChangedDelegate propertyChanged = null, BindableProperty.CreateDefaultValueDelegate defaultValueCreator = null)
            => BindableProperty.Create(name, type, typeof(DigitalIndicatorPresenter), defaultValue, propertyChanged: propertyChanged, defaultValueCreator: defaultValueCreator);

        /// <summary>
        /// SegmentFilter 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty SegmentFilterProperty;
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

        /// <inheritdoc/>
        public DigitalSegmentFilter SegmentFilter { get => (DigitalSegmentFilter)GetValue(SegmentFilterProperty); set => SetValue(SegmentFilterProperty, value); }
        /// <inheritdoc/>
        public DigitalFont DigitalFont { get => (DigitalFont)GetValue(DigitalFontProperty); set => SetValue(DigitalFontProperty, value); }
        /// <inheritdoc/>
        public double Spacing { get => (double)GetValue(SpacingProperty); set => SetValue(SpacingProperty, value); }
        /// <inheritdoc/>
        public object Value { get => GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        /// <summary>
        /// Shape에서 사용할 패스를 가져옵니다.
        /// </summary>
        /// <returns>패스</returns>
        public override PathF GetPath() => path;

        private PathF path = new();

        private static void OnDigitalFontChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (sender is DigitalIndicatorPresenter instance)
            {
                if (oldValue is DigitalFont oldStyle)
                    oldStyle.ParameterChanged -= instance.OnDigitalFontChanged;
                if (newValue is DigitalFont newStyle)
                {
                    newStyle.ParameterChanged += instance.OnDigitalFontChanged;
                    instance.OnDigitalFontChanged(newValue, EventArgs.Empty);
                }
            }
        }

        private void OnDigitalFontChanged(object sender, EventArgs e) => Update(true);
        /// <summary>
        /// 내용이 바뀌어야 할 때 호출됩니다.
        /// </summary>
        /// <param name="bindable">호출자</param>
        /// <param name="oldValue">기존 속성 값</param>
        /// <param name="newValue">새 속성 값</param>
        protected static void OnVisualChanged(BindableObject bindable, object oldValue, object newValue) => (bindable as DigitalIndicatorPresenter)?.Update(false);
        /// <summary>
        /// 레이아웃이 바뀌어야 할 속성이 변화되었을 때 호출됩니다.
        /// </summary>
        /// <param name="bindable">호출자</param>
        /// <param name="oldValue">기존 속성 값</param>
        /// <param name="newValue">새 속성 값</param>
        protected static void OnLayoutChanged(BindableObject bindable, object oldValue, object newValue) => (bindable as DigitalIndicatorPresenter)?.Update(true);

        /// <inheritdoc/>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == StrokeThicknessProperty.PropertyName)
                Update(true);
        }

        private readonly PartDrawingContext partDrawingContext = new PartDrawingContext();

        /// <summary>
        /// 그림자 들어가면 엉망 진창 됨. 이건 다음에 다시 연구해 봐야겠음.
        /// </summary>
        IShadow IView.Shadow => null;// Shadow;

        private void Update(bool updateLayout)
        {
            Handler?.UpdateValue(nameof(IShapeView.Shape));
            if (Parent is IView parent && (updateLayout || Shadow != null))
                parent.InvalidateMeasure();
        }

        /// <summary>
        /// 인디케이터의 크기를 계산하여 가져옵니다.
        /// </summary>
        /// <returns>크기</returns>
        protected abstract Size GetIndicatorSize();

        /// <summary>
        /// PathF에 사용할 인디케이터 파트 목록을 생성합니다.
        /// </summary>
        /// <returns>인디케이터 파트 목록</returns>
        protected abstract IEnumerable<Part> OnCreateParts();

        PathF IShape.PathForBounds(Rect viewBounds)
        {
            path = new PathF();
            partDrawingContext.Renderer = path;
            foreach (var part in OnCreateParts())
                partDrawingContext.DrawPart(part);

            var size = GetIndicatorSize();

            var transform = Matrix3x2.Identity;

            float calculatedWidth = (float)(viewBounds.Width / size.Width);
            float calculatedHeight = (float)(viewBounds.Height / size.Height);

            float widthScale = float.IsNaN(calculatedWidth) ? 0 : calculatedWidth;
            float heightScale = float.IsNaN(calculatedHeight) ? 0 : calculatedHeight;

            switch (Aspect)
            {
                case Stretch.Fill:
                    transform *= Matrix3x2.CreateScale(widthScale, heightScale);
                    transform *= Matrix3x2.CreateTranslation((float)viewBounds.Left, (float)viewBounds.Top);
                    break;
                case Stretch.Uniform:
                    float minScale = Math.Min(widthScale, heightScale);
                    transform *= Matrix3x2.CreateScale(minScale, minScale);
                    transform *= Matrix3x2.CreateTranslation(
                        (float)(viewBounds.Left +
                        (viewBounds.Width - minScale * size.Width) / 2),
                        (float)(viewBounds.Top +
                        (viewBounds.Height - minScale * size.Height) / 2));
                    break;

                case Stretch.UniformToFill:
                    float maxScale = Math.Max(widthScale, heightScale);
                    transform *= Matrix3x2.CreateScale(maxScale, maxScale);
                    transform *= Matrix3x2.CreateTranslation((float)viewBounds.Left, (float)viewBounds.Top);
                    break;
            }

            if (!transform.IsIdentity)
                path.Transform(transform);

            return path;
        }

        /// <summary>
        /// 인디케이터의 크기를 결정합니다.
        /// </summary>
        /// <param name="widthConstraint">사용 가능한 너비입니다.</param>
        /// <param name="heightConstraint">사용 가능한 높이입니다.</param>
        /// <returns>크기</returns>
        protected override Size MeasureOverride(double widthConstraint, double heightConstraint)
        {
            var result = GetIndicatorSize();

            double scaleX = widthConstraint / result.Width;
            double scaleY = heightConstraint / result.Height;
            scaleX = double.IsNaN(scaleX) ? 0 : scaleX;
            scaleY = double.IsNaN(scaleY) ? 0 : scaleY;

            switch (Aspect)
            {
                case Stretch.Fill:
                    if (!double.IsInfinity(heightConstraint))
                        result.Height = heightConstraint;
                    if (!double.IsInfinity(widthConstraint))
                        result.Width = widthConstraint;
                    break;
                case Stretch.Uniform:
                    double minScale = Math.Min(scaleX, scaleY);
                    if (!double.IsInfinity(minScale))
                    {
                        result.Height *= minScale;
                        result.Width *= minScale;
                    }
                    break;
                case Stretch.UniformToFill:
                    scaleX = double.IsInfinity(scaleX) ? 0 : scaleX;
                    scaleY = double.IsInfinity(scaleY) ? 0 : scaleY;
                    double maxScale = Math.Max(scaleX, scaleY);
                    if (maxScale != 0)
                    {
                        result.Height *= maxScale;
                        result.Width *= maxScale;
                    }
                    break;
            }

            DesiredSize = result;
            return result;
        }

    }
}
