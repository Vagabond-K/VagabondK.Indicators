using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Styling;
using System;
using System.Linq;

namespace VagabondK.Indicators.Avalonia
{
    /// <summary>
    /// 템플릿 컨트롤 관련 확장 메서드 모음입니다.
    /// </summary>
    public static class TemplatedControlExtensions
    {
        /// <summary>
        /// 템플릿 컨트롤의 기본 스타일을 컨트롤이 정의된 어셈블리에서 가져옵니다.
        /// </summary>
        /// <param name="templatedControl">템플릿 컨트롤</param>
        /// <returns>스타일</returns>
        public static IStyle GetDefaultStyle(this TemplatedControl templatedControl)
        {
            if (templatedControl != null)
            {
                var type = templatedControl.GetType();
                return new StyleInclude(new Uri($"avares://{type.Assembly.GetName().Name}"))
                {
                    Source = new Uri($"{type.Name}.axaml", UriKind.RelativeOrAbsolute)
                }.Loaded;
            }
            return null;
        }

        /// <summary>
        /// 템플릿 컨트롤의 기본 템플릿을 컨트롤이 정의된 어셈블리에서 가져옵니다.
        /// </summary>
        /// <param name="templatedControl">템플릿 컨트롤</param>
        /// <returns>템플릿</returns>
        public static ControlTemplate GetDefaultTemplate(this TemplatedControl templatedControl)
            => ((templatedControl?.GetDefaultStyle()?.Children?.LastOrDefault(style => (style as Style)?.Selector?.ToString() == templatedControl?.GetType()?.Name) as StyleBase)
            ?.Setters?.FirstOrDefault(setter => (setter as Setter)?.Property == TemplatedControl.TemplateProperty) as Setter)?.Value as ControlTemplate;
    }
}
