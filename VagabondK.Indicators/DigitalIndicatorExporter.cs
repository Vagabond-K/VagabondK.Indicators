using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using VagabondK.Indicators.DigitalFonts;
using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators
{
    /// <summary>
    /// 디지털 인디케이터 내보내기 기능을 제공합니다.
    /// </summary>
    public static class DigitalIndicatorExporter
    {
        /// <summary>
        /// 디지털 넘버 인디케이터를 SVG 형식으로 내보냅니다.
        /// </summary>
        /// <param name="digitalNumber">디지털 넘버 인디케이터</param>
        /// <param name="writer">SVG 태그를 작성할 TextWriter</param>
        /// <param name="activeColor">활성 세그먼트 색상</param>
        /// <param name="inactiveColor">비활성 세그먼트 색상</param>
        public static void ExportToSvg(this IDigitalNumber digitalNumber, TextWriter writer, uint activeColor, uint inactiveColor)
            => ExportToSvg(writer, digitalNumber.DigitalFont, digitalNumber.MeasureIndicator(), digitalNumber.CreateParts(DigitalSegmentFilter.ActiveOnly), digitalNumber.CreateParts(DigitalSegmentFilter.InactiveOnly), activeColor, inactiveColor);

        /// <summary>
        /// 디지털 텍스트 인디케이터를 SVG 형식으로 내보냅니다.
        /// </summary>
        /// <param name="digitalText">디지털 텍스트 인디케이터</param>
        /// <param name="writer">SVG 태그를 작성할 TextWriter</param>
        /// <param name="activeColor">활성 세그먼트 색상</param>
        /// <param name="inactiveColor">비활성 세그먼트 색상</param>
        public static void ExportToSvg(this IDigitalText digitalText, TextWriter writer, uint activeColor, uint inactiveColor)
            => ExportToSvg(writer, digitalText.DigitalFont, digitalText.MeasureIndicator(), digitalText.CreateParts(DigitalSegmentFilter.ActiveOnly), digitalText.CreateParts(DigitalSegmentFilter.InactiveOnly), activeColor, inactiveColor);

        private static void ExportToSvg(TextWriter writer, DigitalFont characterStyle, Size size, IEnumerable<Part> activeDrawings, IEnumerable<Part> inactiveDrawings, uint activeColor, uint inactiveColor)
        {
            if (characterStyle == null) return;

            var stringBuilder = new StringBuilder();

            XmlDocument document = new XmlDocument();
            var namespaceURI = "http://www.w3.org/2000/svg";
            document.CreateXmlDeclaration("1.0", writer.Encoding.WebName, "no");
            var svg = document.CreateElement("svg", namespaceURI);
            document.AppendChild(svg);

            var attribute = document.CreateAttribute("width");
            attribute.Value = size.Width.ToString();
            svg.Attributes.Append(attribute);
            attribute = document.CreateAttribute("height");
            attribute.Value = size.Height.ToString();
            svg.Attributes.Append(attribute);
            attribute = document.CreateAttribute("viewBox");
            attribute.Value = $"0 0 {size.Width} {size.Height}";
            svg.Attributes.Append(attribute);
            var defs = document.CreateElement("defs", namespaceURI);
            svg.AppendChild(defs);
            var inactive = document.CreateElement("g", namespaceURI);
            svg.AppendChild(inactive);
            var active = document.CreateElement("g", namespaceURI);
            svg.AppendChild(active);

            attribute = document.CreateAttribute("fill");
            attribute.Value = "#" + (activeColor & 0xffffff).ToString("X6");
            active.Attributes.Append(attribute);
            attribute = document.CreateAttribute("fill-opacity");
            attribute.Value = ((activeColor >> 24) / 255d).ToString();
            active.Attributes.Append(attribute);

            attribute = document.CreateAttribute("fill");
            attribute.Value = "#" + (inactiveColor & 0xffffff).ToString("X6");
            inactive.Attributes.Append(attribute);
            attribute = document.CreateAttribute("fill-opacity");
            attribute.Value = ((inactiveColor >> 24) / 255d).ToString();
            inactive.Attributes.Append(attribute);

            var partDrawingContext = new SvgPathDrawingContext { Renderer = new StringBuilder() };
            var segmentIdPrefix = characterStyle.Hash.ToString().Replace("-", "") + "_";
            var segments = characterStyle.Segments;
            var segmentParts = segments.Select(segment => segment.Drawing).Distinct().ToList();
            foreach (var path in segments.Select(segment => segment.Drawing).Distinct().Select((segment, i) =>
            {
                var path = document.CreateElement("path", namespaceURI);
                attribute = document.CreateAttribute("id");
                attribute.Value = segmentIdPrefix + i;
                path.Attributes.Append(attribute);
                attribute = document.CreateAttribute("d");
                partDrawingContext.Renderer.Clear();
                partDrawingContext.DrawPart(new Part(segment));
                attribute.Value = partDrawingContext.Renderer.ToString();
                path.Attributes.Append(attribute);
                return path;
            }))
                defs.AppendChild(path);

            void AddDrawings(XmlElement element, IEnumerable<Part> parts)
            {
                foreach (var part in parts)
                {
                    XmlElement item;
                    var partIndex = segmentParts.IndexOf(part.Drawing);
                    if (partIndex >= 0)
                    {
                        item = document.CreateElement("use", namespaceURI);
                        attribute = document.CreateAttribute("href");
                        attribute.Value = $"#{segmentIdPrefix}{partIndex}";
                        item.Attributes.Append(attribute);
                    }
                    else
                    {
                        item = document.CreateElement("path", namespaceURI);
                        attribute = document.CreateAttribute("d");
                        partDrawingContext.Renderer.Clear();
                        partDrawingContext.DrawPart(new Part(part.Drawing));
                        attribute.Value = partDrawingContext.Renderer.ToString();
                        item.Attributes.Append(attribute);
                    }
                    attribute = document.CreateAttribute("transform");
                    attribute.Value = $"matrix({part.Transform.M11} {part.Transform.M12} {part.Transform.M21} {part.Transform.M22} {part.Transform.M31} {part.Transform.M32})";
                    item.Attributes.Append(attribute);
                    element.AppendChild(item);
                }
            }

            AddDrawings(inactive, inactiveDrawings);
            AddDrawings(active, activeDrawings);

            document.Save(writer);
        }
    }
}
