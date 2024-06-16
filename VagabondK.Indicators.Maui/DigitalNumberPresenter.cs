using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;

namespace VagabondK.Indicators.Maui
{
    /// <summary>
    /// 세그먼트 상태에 따라 수치형 값을 디지털 문자들을 표시합니다.
    /// </summary>
    public class DigitalNumberPresenter : DigitalIndicatorPresenter, IDigitalNumberPresenter
    {
        static DigitalNumberPresenter()
        {
            IntegerDigitsProperty = CreateProperty(nameof(IntegerDigits), typeof(int), 5, OnLayoutChanged);
            DecimalPlacesProperty = CreateProperty(nameof(DecimalPlaces), typeof(int), 0, OnLayoutChanged);
            DecimalSeparatorSizeProperty = CreateProperty(nameof(DecimalSeparatorSize), typeof(double), 0.1, OnLayoutChanged);
            DecimalPlaceScaleProperty = CreateProperty(nameof(DecimalPlaceScale), typeof(double), 0.8, OnLayoutChanged);
            PadZeroLeftProperty = CreateProperty(nameof(PadZeroLeft), typeof(bool), false, OnVisualChanged);
            PadZeroRightProperty = CreateProperty(nameof(PadZeroRight), typeof(bool), false, OnVisualChanged);
            MinusAlignLeftProperty = CreateProperty(nameof(MinusAlignLeft), typeof(bool), true, OnVisualChanged);
        }

        private static BindableProperty CreateProperty(string name, Type type, object defaultValue, BindableProperty.BindingPropertyChangedDelegate propertyChanged = null)
            => BindableProperty.Create(name, type, typeof(DigitalNumberPresenter), defaultValue, propertyChanged: propertyChanged);

        /// <summary>
        /// IntegerDigits 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty IntegerDigitsProperty;
        /// <summary>
        /// DecimalPlaces 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty DecimalPlacesProperty;
        /// <summary>
        /// DecimalSeparatorSize 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty DecimalSeparatorSizeProperty;
        /// <summary>
        /// DecimalPlaceScale 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty DecimalPlaceScaleProperty;
        /// <summary>
        /// PadZeroLeft 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty PadZeroLeftProperty;
        /// <summary>
        /// PadZeroRight 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty PadZeroRightProperty;
        /// <summary>
        /// MinusAlignLeft 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty MinusAlignLeftProperty;

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

        /// <inheritdoc/>
        protected override Size GetIndicatorSize()
        {
            var size = this.MeasureIndicator();
            return new Size(size.Width, size.Height);
        }

        /// <inheritdoc/>
        protected override IEnumerable<Part> OnCreateParts() => this.CreateParts();
    }

    /// <summary>
    /// DigitalNumberPresenter 뷰에 대한 핸들러입니다.
    /// </summary>
    public partial class DigitalNumberPresenterHandler : ShapeViewHandler
    {
    }
}
