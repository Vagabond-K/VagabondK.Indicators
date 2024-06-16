using Avalonia;
using System.Collections.Generic;

namespace VagabondK.Indicators.Avalonia
{
    /// <summary>
    /// 세그먼트 상태에 따라 수치형 값을 디지털 문자들을 표시합니다.
    /// </summary>
    public class DigitalNumberPresenter : DigitalIndicatorPresenter, IDigitalNumberPresenter
    {
        static DigitalNumberPresenter()
        {
            IntegerDigitsProperty.Changed.AddClassHandler<DigitalNumberPresenter>(OnLayoutChanged);
            DecimalPlacesProperty.Changed.AddClassHandler<DigitalNumberPresenter>(OnLayoutChanged);
            DecimalSeparatorSizeProperty.Changed.AddClassHandler<DigitalNumberPresenter>(OnLayoutChanged);
            DecimalPlaceScaleProperty.Changed.AddClassHandler<DigitalNumberPresenter>(OnLayoutChanged);
            PadZeroLeftProperty.Changed.AddClassHandler<DigitalNumberPresenter>(OnVisualChanged);
            PadZeroRightProperty.Changed.AddClassHandler<DigitalNumberPresenter>(OnVisualChanged);
            MinusAlignLeftProperty.Changed.AddClassHandler<DigitalNumberPresenter>(OnVisualChanged);
        }

        /// <summary>
        /// IntegerDigits 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<int> IntegerDigitsProperty = AvaloniaProperty.Register<DigitalNumberPresenter, int>(nameof(IntegerDigits), 5);
        /// <summary>
        /// DecimalPlaces 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<int> DecimalPlacesProperty = AvaloniaProperty.Register<DigitalNumberPresenter, int>(nameof(DecimalPlaces), 0);
        /// <summary>
        /// DecimalSeparatorSize 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<double> DecimalSeparatorSizeProperty = AvaloniaProperty.Register<DigitalNumberPresenter, double>(nameof(DecimalSeparatorSize), 0.1);
        /// <summary>
        /// DecimalPlaceScale 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<double> DecimalPlaceScaleProperty = AvaloniaProperty.Register<DigitalNumberPresenter, double>(nameof(DecimalPlaceScale), 0.8);
        /// <summary>
        /// PadZeroLeft 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<bool> PadZeroLeftProperty = AvaloniaProperty.Register<DigitalNumberPresenter, bool>(nameof(PadZeroLeft), false);
        /// <summary>
        /// PadZeroRight 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<bool> PadZeroRightProperty = AvaloniaProperty.Register<DigitalNumberPresenter, bool>(nameof(PadZeroRight), false);
        /// <summary>
        /// MinusAlignLeft 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<bool> MinusAlignLeftProperty = AvaloniaProperty.Register<DigitalNumberPresenter, bool>(nameof(MinusAlignLeft), true);

        /// <inheritdoc/>
        public int IntegerDigits { get => GetValue(IntegerDigitsProperty); set => SetValue(IntegerDigitsProperty, value); }
        /// <inheritdoc/>
        public int DecimalPlaces { get => GetValue(DecimalPlacesProperty); set => SetValue(DecimalPlacesProperty, value); }
        /// <inheritdoc/>
        public double DecimalSeparatorSize { get => GetValue(DecimalSeparatorSizeProperty); set => SetValue(DecimalSeparatorSizeProperty, value); }
        /// <inheritdoc/>
        public double DecimalPlaceScale { get => GetValue(DecimalPlaceScaleProperty); set => SetValue(DecimalPlaceScaleProperty, value); }
        /// <inheritdoc/>
        public bool PadZeroLeft { get => GetValue(PadZeroLeftProperty); set => SetValue(PadZeroLeftProperty, value); }
        /// <inheritdoc/>
        public bool PadZeroRight { get => GetValue(PadZeroRightProperty); set => SetValue(PadZeroRightProperty, value); }
        /// <inheritdoc/>
        public bool MinusAlignLeft { get => GetValue(MinusAlignLeftProperty); set => SetValue(MinusAlignLeftProperty, value); }

        /// <inheritdoc/>
        protected override Size MeasureOverride(Size constraint)
        {
            var size = this.MeasureIndicator();
            return new Size(size.Width, size.Height);
        }

        /// <inheritdoc/>
        protected override IEnumerable<Part> OnCreateParts() => this.CreateParts();
    }
}
