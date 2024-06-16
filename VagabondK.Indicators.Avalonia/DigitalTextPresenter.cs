using Avalonia;
using System.Collections.Generic;

namespace VagabondK.Indicators.Avalonia
{
    /// <summary>
    /// 세그먼트 상태에 따라 값을 문자열로 변환하여 디지털 문자들을 표시합니다.
    /// </summary>
    public class DigitalTextPresenter : DigitalIndicatorPresenter, IDigitalTextPresenter
    {
        static DigitalTextPresenter()
        {
            LengthProperty.Changed.AddClassHandler<DigitalTextPresenter>(OnLayoutChanged);
            FormatProperty.Changed.AddClassHandler<DigitalTextPresenter>(OnVisualChanged);
        }

        /// <summary>
        /// Length 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<int> LengthProperty = AvaloniaProperty.Register<DigitalTextPresenter, int>(nameof(Length));
        /// <summary>
        /// Format 스타일드 속성의 식별자입니다.
        /// </summary>
        public static readonly StyledProperty<string> FormatProperty = AvaloniaProperty.Register<DigitalTextPresenter, string>(nameof(Format));

        /// <inheritdoc/>
        public int Length { get => GetValue(LengthProperty); set => SetValue(LengthProperty, value); }
        /// <inheritdoc/>
        public string Format { get => GetValue(FormatProperty); set => SetValue(FormatProperty, value); }

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
