using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VagabondK.Indicators.PartBuilders;

namespace VagabondK.Indicators.DigitalFonts
{
    /// <summary>
    /// 가로 5개, 세로 7개의 둥근 직사각형 모양 셀로 구성된 디지털 문자 양식입니다.
    /// </summary>
    public class RoundedRectCell5x7Font : Cell5x7Font
    {
        private readonly static RoundedRectangleBuilder rectangleBuilder = new RoundedRectangleBuilder();
        private double cornerRadius = 0.5;

        /// <summary>
        /// 셀의 모퉁이 반지름을 가져오거나 설정합니다.
        /// </summary>
        [DefaultValue(0.5d)]
        public double CornerRadius { get => cornerRadius; set => SetParameter(ref cornerRadius, value); }

        /// <summary>
        /// 대상 캐시의 모든 파라미터가 현재 캐시의 파라미터들과 같은지 여부를 가져옵니다.
        /// </summary>
        /// <param name="target">대상 캐시</param>
        /// <returns>모든 파라미터의 일치 여부</returns>
        public override bool EqualsParameters(ParametricCache<IReadOnlyList<Part>> target)
            => base.EqualsParameters(target)
            && target is RoundedRectCell5x7Font parameters
            && cornerRadius.Equals(parameters.cornerRadius);

        /// <summary>
        /// 파라미터를 나타내는 문자열을 생성할 때 호출됩니다. 해당 문자열은 Hash를 생성할 때 사용되므로, 반드시 모든 파라미터의 값을 콤마 등의 구분기호로 나누어 문자열에 포합해야 합니다.
        /// </summary>
        /// <param name="stringBuilder">StringBuilder 객체</param>
        /// <returns>파라미터 문자열</returns>
        protected override StringBuilder OnGenerateParametersString(StringBuilder stringBuilder)
            => base.OnGenerateParametersString(stringBuilder)
            .Append(',').Append(cornerRadius);

        /// <summary>
        /// 특정 너비와 높이로 셀 파트 드로잉을 생성합니다. 기준점은 셀의 좌측 상단 포인트입니다.
        /// </summary>
        /// <param name="width">셀 너비</param>
        /// <param name="height">셀 높이</param>
        /// <returns></returns>
        protected override PartDrawing CreateCellDrawing(double width, double height)
        {
            rectangleBuilder.BeginUpdate();
            rectangleBuilder.Width = width;
            rectangleBuilder.Height = height;
            rectangleBuilder.CornerRadius = cornerRadius;
            rectangleBuilder.EndUpdate();

            return rectangleBuilder.BuildPartDrawing();
        }
    }
}
