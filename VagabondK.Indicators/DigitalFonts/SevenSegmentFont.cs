using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators.DigitalFonts
{
    /// <summary>
    /// 일곱개의 세그먼트로 구성된 디지털 문자 양식입니다.
    /// </summary>
    public class SevenSegmentFont : DigitalFont
    {
        private static readonly Dictionary<char, byte> binaryCodes = new Dictionary<char, byte>
        {
            { '0', 0b0111111 },
            { '1', 0b0000110 },
            { '2', 0b1011011 },
            { '3', 0b1001111 },
            { '4', 0b1100110 },
            { '5', 0b1101101 },
            { '6', 0b1111101 },
            { '7', 0b0000111 },
            { '8', 0b1111111 },
            { '9', 0b1101111 },

            { 'A', 0b1110111 },
            { 'B', 0b1111100 },
            { 'C', 0b0111001 },
            { 'D', 0b1011110 },
            { 'E', 0b1111001 },
            { 'F', 0b1110001 },
            { 'G', 0b0111101 },
            { 'H', 0b1110110 },
            { 'I', 0b0000110 },
            { 'J', 0b0011111 },
            { 'K', 0b1110110 },
            { 'L', 0b0111000 },
            { 'M', 0b0110111 },
            { 'N', 0b0110111 },
            { 'O', 0b0111111 },
            { 'P', 0b1110011 },
            { 'Q', 0b0111111 },
            { 'R', 0b1110111 },
            { 'S', 0b1101101 },
            { 'T', 0b0000111 },
            { 'U', 0b0111110 },
            { 'V', 0b0111110 },
            { 'W', 0b1111110 },
            { 'X', 0b1110110 },
            { 'Y', 0b1101110 },
            { 'Z', 0b1011011 },

            { 'a', 0b1011111 },
            { 'b', 0b1111100 },
            { 'c', 0b1011100 },
            { 'd', 0b1011110 },
            { 'e', 0b1111011 },
            { 'f', 0b1110001 },
            { 'g', 0b1101111 },
            { 'h', 0b1110100 },
            { 'i', 0b0000110 },
            { 'j', 0b0001110 },
            { 'k', 0b1110110 },
            { 'l', 0b0110000 },
            { 'm', 0b1010100 },
            { 'n', 0b1010100 },
            { 'o', 0b1011100 },
            { 'p', 0b1110011 },
            { 'q', 0b1100111 },
            { 'r', 0b1010000 },
            { 's', 0b1101101 },
            { 't', 0b1111000 },
            { 'u', 0b0011100 },
            { 'v', 0b0011100 },
            { 'w', 0b0011100 },
            { 'x', 0b0000000 },
            { 'y', 0b1101110 },
            { 'z', 0b1011011 },

            { ' ', 0b0000000 },
            { '-', 0b1000000 },
            { '_', 0b0001000 },
            { '[', 0b0111001 },
            { ']', 0b0001111 },
            { '(', 0b0111001 },
            { ')', 0b0001111 },
        };

        private double thickness = 0.4;
        private double gap = 0.12;
        private double cornerChamfer = 0.5;
        private double cornerGapWarping = 0.33;
        private double middleChamfer = 0.4;

        /// <summary>
        /// 디지털 문자 세로 크기 대비 세그먼트 두께 비율을 가져오거나 설정합니다.
        /// </summary>
        [DefaultValue(0.4)]
        public double Thickness { get => thickness; set => SetParameter(ref thickness, value); }
        /// <summary>
        /// 세그먼트 두께 대비 틈새 비율을 가져오거나 설정합니다.
        /// </summary>
        [DefaultValue(0.12)]
        public double Gap { get => gap; set => SetParameter(ref gap, value); }
        /// <summary>
        /// 세그먼트 두께 대비 모퉁이 깎기 비율을 가져오거나 설정합니다.
        /// </summary>
        [DefaultValue(0.5)]
        public double CornerChamfer { get => cornerChamfer; set => SetParameter(ref cornerChamfer, value); }
        /// <summary>
        /// 모퉁이 틈새의 뒤틀림 비율을 가져오거나 설정합니다. 0이면 각 모퉁이 틈새를 정렬된 45도로 구성합니다.
        /// </summary>
        [DefaultValue(0.38)]
        public double CornerGapWarping { get => cornerGapWarping; set => SetParameter(ref cornerGapWarping, value); }
        /// <summary>
        /// 문자의 허리측 세그먼트 모퉁이 깎기 비율을 가져오거나 설정합니다. 0이면 깎지 않습니다.
        /// </summary>
        [DefaultValue(0.4)]
        public double MiddleChamfer { get => middleChamfer; set => SetParameter(ref middleChamfer, value); }

        /// <summary>
        /// 디지털 문자 양식의 크기를 측정합니다.
        /// </summary>
        /// <returns>디지털 문자 양식 크기</returns>
        protected override Size Measure()
        {
            var actualHeight = Size.ToValidValue();
            var thickness = Math.Min(this.thickness.ToValidValue(), 1) * actualHeight / 3;
            var maxGapXY = (actualHeight - thickness * 3) / 4;
            var gapXY = Math.Min(gap.ToValidValue(), 1) * maxGapXY;
            var slantRadian = Math.Max(Math.Min(SlantAngle.ToValidValue(), 45), 0) / 180d * Math.PI;
            var width = Math.Max(WidthRatio.ToValidValue() * (thickness + maxGapXY) * 2, (thickness + gapXY) * 2);
            var actualWidth = width + Math.Tan(slantRadian) * actualHeight;

            Width = width;
            return new Size(actualWidth, actualHeight);
        }

        /// <summary>
        /// 현재 디지털 문자 양식에서 기본 제공하는 세그먼트 상태 이진 코드를 가져옵니다. 세그먼트 상태 순서는 LSB부터 시작됩니다.
        /// </summary>
        /// <param name="character">문자</param>
        /// <returns>세그먼트 상태 이진 코드</returns>
        protected override long GetDefaultBinaryCode(char character) => binaryCodes.TryGetValue(character, out var code) ? code : 0;

        /// <summary>
        /// 대상 캐시의 모든 파라미터가 현재 캐시의 파라미터들과 같은지 여부를 가져옵니다.
        /// </summary>
        /// <param name="target">대상 캐시</param>
        /// <returns>모든 파라미터의 일치 여부</returns>
        public override bool EqualsParameters(ParametricCache<IReadOnlyList<Part>> target)
            => base.EqualsParameters(target)
            && target is SevenSegmentFont parameters
            && thickness.Equals(parameters.thickness)
            && gap.Equals(parameters.gap)
            && cornerChamfer.Equals(parameters.cornerChamfer)
            && cornerGapWarping.Equals(parameters.cornerGapWarping)
            && middleChamfer.Equals(parameters.middleChamfer);

        /// <summary>
        /// 파라미터를 나타내는 문자열을 생성할 때 호출됩니다. 해당 문자열은 Hash를 생성할 때 사용되므로, 반드시 모든 파라미터의 값을 콤마 등의 구분기호로 나누어 문자열에 포합해야 합니다.
        /// </summary>
        /// <param name="stringBuilder">StringBuilder 객체</param>
        /// <returns>파라미터 문자열</returns>
        protected override StringBuilder OnGenerateParametersString(StringBuilder stringBuilder)
            => base.OnGenerateParametersString(stringBuilder)
            .Append(',').Append(thickness)
            .Append(',').Append(gap)
            .Append(',').Append(cornerChamfer)
            .Append(',').Append(cornerGapWarping)
            .Append(',').Append(middleChamfer);

        /// <summary>
        /// 파라미터를 이용하여 세그먼트 파트 목록을 생성할 때 호출됩니다.
        /// </summary>
        /// <returns>세그먼트 파트 목록</returns>
        protected override IReadOnlyList<Part> OnCreate()
        {
            var height = Size.ToValidValue();
            var thickness = Math.Min(this.thickness.ToValidValue(), 1) * height / 3;
            if (thickness > 0)
            {
                var slantRadian = Math.Max(Math.Min(SlantAngle.ToValidValue(), 45), 0) / 180d * Math.PI;
                var maxGapXY = (height - thickness * 3) / 4;
                var gapXY = Math.Min(this.gap.ToValidValue(), 1) * maxGapXY;
                var width = Math.Max(WidthRatio.ToValidValue() * (thickness + maxGapXY) * 2, (thickness + gapXY) * 2);
                var fullLength = height + width;

                var gap = Math.Sqrt(Math.Pow(gapXY, 2) * 2);
                var cornerChamfer = Math.Min(this.cornerChamfer.ToValidValue(), 1);
                var cornerGapWarping = Math.Min(this.cornerGapWarping.ToValidValue(), 1);
                var gapAngle = (1.25 - 0.25 * cornerGapWarping) * Math.PI;
                var middleChamfer = Math.Min(this.middleChamfer.ToValidValue(), 1);


                var halfGap = gap / 2;
                var halfHeight = height / 2;
                var halfWidth = width / 2;
                var halfThickness = thickness / 2;

                var midCornerX = halfThickness * middleChamfer;
                var midJointX = thickness - midCornerX;

                var rightAngle = Math.PI / 2;

                var leftOutLine = new Line(0, halfThickness * cornerChamfer * 2, 0, halfHeight - halfThickness - gapXY);
                var rightOutLine = new Line(width, leftOutLine.Start.Y, width, leftOutLine.End.Y);
                var topOutLine = new Line(leftOutLine.Start.Y, 0, width - leftOutLine.Start.Y, 0);
                var topLeftCornerLine = new Line(leftOutLine.Start, topOutLine.Start);
                var topRightCornerLine = new Line(topOutLine.End, rightOutLine.Start);

                var topInLine = new Line(0, thickness, fullLength, thickness);
                var leftInLine = new Line(thickness, 0, thickness, fullLength);
                var rightInLine = new Line(width - thickness, 0, width - thickness, fullLength);

                var topLeftGapLine1 = new Point(Math.Cos(gapAngle + rightAngle) * gap + thickness, thickness).MoveByDirection(gapAngle + Math.PI, gap).LineByDirection(gapAngle, fullLength);
                var topLeftGapLine2 = new Point(thickness, thickness - Math.Sin(gapAngle + rightAngle) * gap).MoveByDirection(gapAngle + Math.PI, gap).LineByDirection(gapAngle, fullLength);
                gapAngle += rightAngle;
                var topRightGapLine1 = new Point(width - thickness, thickness - Math.Sin(gapAngle - rightAngle) * gap).MoveByDirection(gapAngle + Math.PI, gap).LineByDirection(gapAngle, fullLength);
                var topRightGapLine2 = new Point(Math.Cos(gapAngle - rightAngle) * gap + width - thickness, thickness).MoveByDirection(gapAngle + Math.PI, gap).LineByDirection(gapAngle, fullLength);
                gapAngle = Math.Atan2(halfThickness + halfGap, -midJointX - halfGap);
                var midLeftGapLineU = new Point(thickness, halfHeight - halfThickness - Math.Sin(gapAngle - rightAngle) * gap).MoveByDirection(gapAngle + Math.PI, gap).LineByDirection(gapAngle, fullLength);
                var midLeftGapLineD = new Point(Math.Cos(gapAngle - rightAngle) * gap + thickness, halfHeight - halfThickness).MoveByDirection(gapAngle + Math.PI, gap).LineByDirection(gapAngle, fullLength);
                gapAngle = Math.Atan2(halfThickness + halfGap, midJointX + halfGap);
                var midRightGapLineU = new Point(width - thickness, halfHeight - halfThickness - Math.Sin(gapAngle + rightAngle) * gap).MoveByDirection(gapAngle + Math.PI, gap).LineByDirection(gapAngle, fullLength);

                Point crossPoint;

                var middleLine = new Line(0, halfHeight - halfThickness, fullLength, halfHeight - halfThickness);
                var leftMidLine = new Line(leftOutLine.End, midLeftGapLineU.TryGetCrossPoint(new Line(midCornerX, 0, midCornerX, fullLength), out crossPoint, false) ? crossPoint : leftOutLine.End);
                var rightMidLine = new Line(rightOutLine.End, midRightGapLineU.TryGetCrossPoint(new Line(width - midCornerX, 0, width - midCornerX, fullLength), out crossPoint, false) ? crossPoint : rightOutLine.End);
                var halfLine = new Line(0, halfHeight, fullLength, halfHeight);
                if (midLeftGapLineD.TryGetCrossPoint(middleLine, out crossPoint))
                    middleLine.Start = crossPoint;
                if (midLeftGapLineD.TryGetCrossPoint(halfLine, out crossPoint, false))
                    halfLine.Start = crossPoint;

                Line topLeftLine1;
                Line topLeftLine2;
                if (topLeftGapLine1.TryGetCrossPoint(topLeftCornerLine, out crossPoint))
                {
                    topLeftLine2 = new Line(crossPoint, topOutLine.Start);
                    topLeftLine1 = new Line(topLeftLine2.Start);
                }
                else if (topLeftGapLine1.TryGetCrossPoint(topOutLine, out crossPoint))
                {
                    topLeftLine2 = new Line(crossPoint, topOutLine.Start);
                    topOutLine.Start = crossPoint;
                    topLeftLine1 = new Line(topLeftLine2.Start);
                }
                else if (topLeftGapLine1.TryGetCrossPoint(leftOutLine, out crossPoint, false))
                {
                    topLeftLine1 = new Line(crossPoint, leftOutLine.Start);
                    topLeftLine2 = new Line(leftOutLine.Start, topOutLine.Start);
                }
                else
                {
                    topLeftLine1 = new Line(leftOutLine.Start, topLeftCornerLine.Start);
                    topLeftLine2 = new Line(topLeftCornerLine.Start, topOutLine.Start);
                }

                Line topRightLine;
                if (topRightGapLine2.TryGetCrossPoint(topRightCornerLine, out crossPoint))
                    topRightLine = new Line(topOutLine.End, crossPoint);
                else if (topRightGapLine2.TryGetCrossPoint(topOutLine, out crossPoint))
                {
                    topRightLine = new Line(crossPoint);
                    topOutLine.End = crossPoint;
                }
                else
                {
                    topRightLine = topOutLine = new Line(topOutLine.End);
                }

                topInLine.End = topRightGapLine2.TryGetCrossPoint(topInLine, out crossPoint) ? crossPoint : new Point(0, rightInLine.Start.Y);



                Line leftTopLine;
                if (topLeftGapLine2.TryGetCrossPoint(topLeftCornerLine, out crossPoint))
                    leftTopLine = new Line(leftOutLine.Start, crossPoint);
                else if (topLeftGapLine2.TryGetCrossPoint(leftOutLine, out crossPoint))
                {
                    leftTopLine = new Line(crossPoint);
                    leftOutLine.Start = crossPoint;
                }
                else if (topLeftGapLine2.TryGetCrossPoint(leftMidLine, out crossPoint))
                {
                    leftOutLine = leftTopLine = new Line(crossPoint);
                }
                else
                {
                    leftTopLine = leftOutLine = leftMidLine = leftInLine = new Line(leftMidLine.Start);
                }

                if (leftMidLine.DeltaY > 0)
                {
                    if (topLeftGapLine2.TryGetCrossPoint(leftInLine, out crossPoint, false))
                        leftInLine.Start = crossPoint;
                    if (midLeftGapLineU.TryGetCrossPoint(leftInLine, out crossPoint, false))
                        leftInLine.End = crossPoint;
                }


                topInLine.Start = topLeftGapLine1.TryGetCrossPoint(new Line(topInLine.Start, new Point(fullLength, topInLine.End.Y)), out crossPoint, false)
                    ? crossPoint
                    : new Point(thickness, topInLine.Start.Y);
                topInLine.Start = new Point(Math.Min(Math.Max(topInLine.Start.X, 0), topInLine.End.X), topInLine.Start.Y);

                if (leftInLine.DeltaY < 0 && topLeftGapLine2.TryGetCrossPoint(midLeftGapLineU, out crossPoint, false))
                    leftInLine.Start = leftInLine.End = crossPoint;


                Line rightTopLine1;
                Line rightTopLine2;
                if (topRightGapLine1.TryGetCrossPoint(topRightCornerLine, out crossPoint))
                {
                    rightTopLine2 = new Line(crossPoint, rightOutLine.Start);
                    rightTopLine1 = new Line(rightTopLine2.Start);
                }
                else if (topRightGapLine1.TryGetCrossPoint(rightOutLine, out crossPoint))
                {
                    rightTopLine2 = new Line(crossPoint, rightOutLine.Start);
                    rightOutLine.Start = crossPoint;
                    rightTopLine1 = new Line(rightTopLine2.Start);
                }
                else if (topRightGapLine1.TryGetCrossPoint(new Line(0, 0, width, 0), out crossPoint))
                {
                    rightTopLine1 = new Line(crossPoint, topRightCornerLine.Start);
                    rightTopLine2 = new Line(topRightCornerLine.Start, rightOutLine.Start);
                }
                else
                {
                    rightTopLine1 = new Line(topOutLine.End, topRightCornerLine.Start);
                    rightTopLine2 = new Line(topRightCornerLine.Start, rightOutLine.Start);
                }

                rightInLine.Start = topRightGapLine1.TryGetCrossPoint(new Line(rightInLine.Start, new Point(rightInLine.Start.X, fullLength)), out crossPoint)
                    ? crossPoint
                    : new Point(width - thickness, rightInLine.Start.Y);
                rightInLine.Start = new Point(rightInLine.Start.X, Math.Min(Math.Max(rightInLine.Start.Y, 0), rightInLine.End.Y));
                rightInLine.End = midRightGapLineU.TryGetCrossPoint(rightInLine, out crossPoint, false) ? crossPoint : rightInLine.Start;
                if (rightInLine.DeltaY < 0 && topRightGapLine1.TryGetCrossPoint(midRightGapLineU, out crossPoint, false))
                    rightInLine.Start = rightInLine.End = crossPoint;

                var skew = Transform.CreateSkew(-slantRadian, 0, 0, height);

                IEnumerable<Point> GetSegment0()
                {
                    yield return topLeftLine1.Start;
                    yield return topLeftLine2.Start;
                    yield return topOutLine.Start;
                    yield return topOutLine.End;
                    yield return topRightLine.Start;
                    yield return topRightLine.End;
                    yield return topInLine.End;
                    yield return topInLine.Start;
                }
                IEnumerable<Point> GetSegment1()
                {
                    yield return rightTopLine1.Start;
                    yield return rightTopLine2.Start;
                    yield return rightOutLine.Start;
                    yield return rightOutLine.End;
                    yield return rightMidLine.End;
                    yield return rightInLine.End;
                    yield return rightInLine.Start;
                }
                IEnumerable<Point> GetSegment5()
                {
                    yield return leftOutLine.Start;
                    yield return leftTopLine.Start;
                    yield return leftTopLine.End;
                    yield return leftInLine.Start;
                    yield return leftInLine.End;
                    yield return leftMidLine.End;
                    yield return leftOutLine.End;
                }
                IEnumerable<Point> GetSegment6()
                {
                    yield return halfLine.Start;
                    yield return middleLine.Start;
                    yield return middleLine.Start.MirrorX(halfWidth);
                    yield return halfLine.Start.MirrorX(halfWidth);
                    yield return middleLine.Start.MirrorXY(halfWidth, halfHeight);
                    yield return middleLine.Start.MirrorY(halfHeight);
                }
                IEnumerable<Point> GetSegment2() => GetSegment1().Select(point => point.MirrorY(halfHeight)).Reverse();
                IEnumerable<Point> GetSegment3() => GetSegment0().Select(point => point.MirrorY(halfHeight)).Reverse();
                IEnumerable<Point> GetSegment4() => GetSegment5().Select(point => point.MirrorY(halfHeight)).Reverse();

                Part BuildSegment(IEnumerable<Point> points) => new Part(new DelegateDrawing(context =>
                {
                    bool exists = false;
                    foreach (var point in points)
                    {
                        point.Transform(skew);
                        if (!exists)
                            context.BeginPath(point);
                        else
                            context.DrawLine(point);
                        exists = true;
                    }
                    if (exists)
                        context.Close();
                }));

                return new[]
                {
                    BuildSegment(GetSegment0().Distinct().ToArray()),
                    BuildSegment(GetSegment1().Distinct().ToArray()),
                    BuildSegment(GetSegment2().Distinct().ToArray()),
                    BuildSegment(GetSegment3().Distinct().ToArray()),
                    BuildSegment(GetSegment4().Distinct().ToArray()),
                    BuildSegment(GetSegment5().Distinct().ToArray()),
                    BuildSegment(GetSegment6().Distinct().ToArray()),
                };
            }
            else
            {
                return Enumerable.Range(0, 7).Select(i => new Part(PartDrawing.Empty)).ToArray();
            }
        }
    }
}
