using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using VagabondK.Indicators;
using VagabondK.Indicators.Windows;

namespace WindowsSample
{
    public class ExportToSvgCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => parameter is DigitalIndicator;

        public void Execute(object parameter)
        {
            if (parameter is DigitalIndicator indicator)
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Title = "Export shape to SVG",
                    FileName = indicator.GetType().Name + ".svg",
                    Filter = "Scalable Vector Graphics|*.svg"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    uint activeColor = indicator.Active is SolidColorBrush active ? (uint)(active.Color.A << 24 | active.Color.R << 16 | active.Color.G << 8 | active.Color.B) : 0xff000000;
                    uint inactiveColor = indicator.Inactive is SolidColorBrush inactive ? (uint)(inactive.Color.A << 24 | inactive.Color.R << 16 | inactive.Color.G << 8 | inactive.Color.B) : 0x40808080;
                    using (var stream = File.Open(saveFileDialog.FileName, FileMode.Create))
                    using (var writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        if (indicator is IDigitalNumber digitalNumber)
                            digitalNumber.ExportToSvg(writer, activeColor, inactiveColor);
                        else if (indicator is IDigitalText digitalText)
                            digitalText.ExportToSvg(writer, activeColor, inactiveColor);
                    }
                }
            }
        }
    }
}
