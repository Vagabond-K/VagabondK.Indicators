using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators.DigitalFonts
{
    /// <summary>
    /// 여러 세그먼트로 구성된 디지털 문자 양식을 정의합니다.
    /// </summary>
    public abstract class DigitalFont : ParametricCache<IReadOnlyList<Part>>
    {
        private DigitalCharacterSet characterSet;
        private double size = 50;
        private double widthRatio = 1;
        private double slantAngle = 0;
        private Size actualSize;
        private double width;

        /// <summary>
        /// 각 문자에 대한 사용자 정의 세그먼트 상태 이진 코드 Dictionary입니다.
        /// </summary>
        public Dictionary<char, long> CustomBinaryCodes { get; set; }

        /// <summary>
        /// 종료자
        /// </summary>
        ~DigitalFont()
        {
            cache.Release(Hash);
        }

        /// <summary>
        /// 디지털 문자의 세로 크기를 가져오거나 설정합니다.
        /// </summary>
        [DefaultValue(50d)]
        public double Size { get => size; set => SetParameter(ref size, value); }
        /// <summary>
        /// 디지털 문자의 세로 크기 기준 가로 비율을 가져오거나 설정합니다. 디지털 문자 양식에 따라 가로 크기에 제한이 발생할 수 있습니다.
        /// </summary>
        [DefaultValue(1d)]
        public double WidthRatio { get => widthRatio; set => SetParameter(ref widthRatio, value); }
        /// <summary>
        /// 디지털 문자의 기울임 각도를 가져오거나 설정합니다.
        /// </summary>
        [DefaultValue(0d)]
        public double SlantAngle { get => slantAngle; set => SetParameter(ref slantAngle, value); }

        /// <summary>
        /// 디지털 문자의 최종 적용 크기를 가져옵니다. 여기서 Size.Width 값은 기울임 각도가 적용될 때 발생한 확장 영역을 포함합니다.
        /// </summary>
        public Size ActualSize { get => actualSize; private set => SetProperty(ref actualSize, value); }

        /// <summary>
        /// 디지털 문자의 기울임 각도를 적용하지 않았을 때의 가로 크기를 가져옵니다. 
        /// </summary>
        public double Width { get => width; protected set => SetProperty(ref width, value); }

        /// <summary>
        /// 대상 문자에 대한 세그먼트 상태 이진 코드를 가져옵니다. 세그먼트 상태 순서는 LSB부터 시작됩니다.
        /// </summary>
        /// <param name="character">문자</param>
        /// <returns>세그먼트 상태 이진 코드</returns>
        public long GetBinaryCode(char character)
            => CustomBinaryCodes?.TryGetValue(character, out var code) == true ? code : GetDefaultBinaryCode(character);

        /// <summary>
        /// 세그먼트 파트 목록을 가져옵니다.
        /// </summary>
        public IReadOnlyList<Part> Segments => GetObject();

        /// <summary>
        /// 현재 디지털 문자 양식에서 기본 제공하는 세그먼트 상태 이진 코드를 가져옵니다. 세그먼트 상태 순서는 LSB부터 시작됩니다.
        /// </summary>
        /// <param name="character">문자</param>
        /// <returns>세그먼트 상태 이진 코드</returns>
        protected abstract long GetDefaultBinaryCode(char character);

        /// <summary>
        /// 파라미터들에 의해 정의된 해시가 파라미터 변경에 의해 무효화 되었을 때 호출됩니다.
        /// </summary>
        /// <param name="hash">무효화 된 파라미터들에 의해 정의된 해시</param>
        protected override void OnInvalidated(Guid hash)
        {
            characterSet = null;
            cache.Release(hash);
            ActualSize = Measure();
        }

        /// <summary>
        /// 대상 캐시의 모든 파라미터가 현재 캐시의 파라미터들과 같은지 여부를 가져옵니다.
        /// </summary>
        /// <param name="target">대상 캐시</param>
        /// <returns>모든 파라미터의 일치 여부</returns>
        public override bool EqualsParameters(ParametricCache<IReadOnlyList<Part>> target)
            => base.EqualsParameters(target)
            && target is DigitalFont parameters
            && size.Equals(parameters.size)
            && widthRatio.Equals(parameters.widthRatio)
            && slantAngle.Equals(parameters.slantAngle);

        /// <summary>
        /// 파라미터를 나타내는 문자열을 생성할 때 호출됩니다. 해당 문자열은 Hash를 생성할 때 사용되므로, 반드시 모든 파라미터의 값을 콤마 등의 구분기호로 나누어 문자열에 포합해야 합니다.
        /// </summary>
        /// <param name="stringBuilder">StringBuilder 객체</param>
        /// <returns>파라미터 문자열</returns>
        protected override StringBuilder OnGenerateParametersString(StringBuilder stringBuilder)
            => base.OnGenerateParametersString(stringBuilder)
            .Append(',').Append(slantAngle)
            .Append(',').Append(size)
            .Append(',').Append(widthRatio);

        /// <summary>
        /// 디지털 문자 양식의 크기를 측정합니다.
        /// </summary>
        /// <returns>디지털 문자 양식 크기</returns>
        protected abstract Size Measure();

        private readonly static Cache<Guid, DigitalCharacterSet> cache = new Cache<Guid, DigitalCharacterSet>();

        /// <summary>
        /// 대상 문자의 현재 디지털 문자 양식을 적용한 세그먼트 파트 목록을 가져옵니다.
        /// </summary>
        /// <param name="character">문자</param>
        /// <param name="segmentFilter">표시할 세그먼트에 대한 필터</param>
        /// <returns>세그먼트 파트 목록</returns>
        public IEnumerable<Part> GetCharacterSegments(char character, DigitalSegmentFilter segmentFilter)
        {
            if (characterSet == null)
                characterSet = cache.GetValue(Hash, () => new DigitalCharacterSet(Segments));

            switch (segmentFilter)
            {
                case DigitalSegmentFilter.InactiveOnly:
                    return characterSet.GetDrawings(GetBinaryCode(character)).Inactive;
                case DigitalSegmentFilter.All:
                    return characterSet.GetDrawings(GetBinaryCode(' ')).Inactive;
                default:
                    return characterSet.GetDrawings(GetBinaryCode(character)).Active;
            }
        }

        class DigitalCharacterDrawings
        {
            public DigitalCharacterDrawings(IEnumerable<Part> active, IEnumerable<Part> inactive)
            {
                Active = active;
                Inactive = inactive;
            }
            public IEnumerable<Part> Active { get; }
            public IEnumerable<Part> Inactive { get; }
        }

        class DigitalCharacterSet
        {
            public DigitalCharacterSet(IEnumerable<Part> segments)
            {
                this.segments = segments.ToArray();
            }

            private readonly Dictionary<long, DigitalCharacterDrawings> cache = new Dictionary<long, DigitalCharacterDrawings>();
            private readonly Part[] segments;

            public DigitalCharacterDrawings GetDrawings(long code)
            {
                if (!cache.TryGetValue(code, out var character))
                {
                    int index = 0;
                    var active = new List<Part>();
                    var inactive = new List<Part>();
                    foreach (var segment in segments)
                    {
                        if ((code >> index & 1) == 1)
                            active.Add(segment);
                        else
                            inactive.Add(segment);
                        index++;
                    }
                    character = new DigitalCharacterDrawings(
                        new ReadOnlyCollection<Part>(active),
                        new ReadOnlyCollection<Part>(inactive));
                    cache[code] = character;
                }
                return character;
            }
        }
    }
}