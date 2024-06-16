using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;

namespace VagabondK.Indicators.Maui
{
    /// <summary>
    /// 세그먼트 상태에 따라 값을 문자열로 변환하여 디지털 문자들을 표시합니다.
    /// </summary>
    public class DigitalTextPresenter : DigitalIndicatorPresenter, IDigitalTextPresenter
    {
        static DigitalTextPresenter()
        {
            LengthProperty = CreateProperty(nameof(Length), typeof(int), 10, OnLayoutChanged);
            FormatProperty = CreateProperty(nameof(Format), typeof(string), null, OnVisualChanged);
        }

        private static BindableProperty CreateProperty(string name, Type type, object defaultValue, BindableProperty.BindingPropertyChangedDelegate propertyChanged = null)
            => BindableProperty.Create(name, type, typeof(DigitalTextPresenter), defaultValue, propertyChanged: propertyChanged);

        /// <summary>
        /// Length 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty LengthProperty;
        /// <summary>
        /// Format 바인딩 가능 속성의 식별자입니다.
        /// </summary>
        public static readonly BindableProperty FormatProperty;

        /// <inheritdoc/>
        public int Length { get => (int)GetValue(LengthProperty); set => SetValue(LengthProperty, value); }
        /// <inheritdoc/>
        public string Format { get => (string)GetValue(FormatProperty); set => SetValue(FormatProperty, value); }

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
    /// DigitalTextPresenter 뷰에 대한 핸들러입니다.
    /// </summary>
    public partial class DigitalTextPresenterHandler : ShapeViewHandler
    {
    }
}
