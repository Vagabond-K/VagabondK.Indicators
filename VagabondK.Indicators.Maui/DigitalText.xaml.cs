using Microsoft.Maui.Controls;
using System;

namespace VagabondK.Indicators.Maui
{
    /// <summary>
    /// ���� ���ڿ� �������� ��ȯ�Ͽ� ������ ���ڵ�� ǥ���ϴ� �ε��������Դϴ�.
    /// </summary>
    public partial class DigitalText : DigitalIndicator, IDigitalText
    {
        static DigitalText()
        {
            LengthProperty = CreateProperty(nameof(Length), typeof(int), 10);
            FormatProperty = CreateProperty(nameof(Format), typeof(string), null);
            AspectProperty = CreateProperty(nameof(Aspect), typeof(Stretch), Stretch.None);
        }
        private static BindableProperty CreateProperty(string name, Type type, object defaultValue)
            => BindableProperty.Create(name, type, typeof(DigitalText), defaultValue);

        /// <summary>
        /// Length ���ε� ���� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly BindableProperty LengthProperty;
        /// <summary>
        /// Format ���ε� ���� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly BindableProperty FormatProperty;
        /// <summary>
        /// Aspect ���ε� ���� �Ӽ��� �ĺ����Դϴ�.
        /// </summary>
        public static readonly BindableProperty AspectProperty;

        /// <inheritdoc/>
        public int Length { get => (int)GetValue(LengthProperty); set => SetValue(LengthProperty, value); }
        /// <inheritdoc/>
        public string Format { get => (string)GetValue(FormatProperty); set => SetValue(FormatProperty, value); }
        /// <summary>
        /// �� ���� ä��� ���� ������ �ÿ��� �ϴ� ����� �����ϴ� ���� �������ų� �����մϴ�.
        /// </summary>
        public Stretch Aspect { get => (Stretch)GetValue(AspectProperty); set => SetValue(AspectProperty, value); }

        /// <summary>
        /// ������
        /// </summary>
        public DigitalText()
        {
            InitializeComponent();
        }
    }
}