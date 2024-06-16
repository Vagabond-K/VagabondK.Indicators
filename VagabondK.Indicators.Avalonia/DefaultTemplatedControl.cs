using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;

namespace VagabondK.Indicators.Avalonia
{
    /// <summary>
    /// 기본 템플릿을 자동으로 어셈블리에서 가져와서 적용하는 컨트롤입니다.
    /// </summary>
    public abstract class DefaultTemplatedControl : TemplatedControl
    {
        static DefaultTemplatedControl()
        {
            TemplateProperty.OverrideDefaultValue<DefaultTemplatedControl>(new FuncControlTemplate((templatedControl, ns)
                => templatedControl?.GetDefaultTemplate()?.Build(templatedControl)?.Result));
        }
    }
}
