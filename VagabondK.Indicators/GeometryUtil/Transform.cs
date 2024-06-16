using System;

namespace VagabondK.Indicators.GeometryUtil
{
    /// <summary>
    /// 매트릭스 기반 변환을 정의합니다.
    /// </summary>
    public readonly struct Transform
    {
        private readonly bool isIdentity;

        internal readonly double m11;
        internal readonly double m12;
        internal readonly double m21;
        internal readonly double m22;
        internal readonly double m31;
        internal readonly double m32;
        internal readonly bool translationOnly;

        /// <summary>
        /// 변환 행렬의 1행, 1열의 값을 가져옵니다.
        /// </summary>
        public double M11 => m11;
        /// <summary>
        /// 변환 행렬의 1행, 2열의 값을 가져옵니다.
        /// </summary>
        public double M12 => m12;
        /// <summary>
        /// 변환 행렬의 2행, 1열의 값을 가져옵니다.
        /// </summary>
        public double M21 => m21;
        /// <summary>
        /// 변환 행렬의 2행, 2열의 값을 가져옵니다.
        /// </summary>
        public double M22 => m22;
        /// <summary>
        /// 변환 행렬의 3행, 1열의 값을 가져옵니다.
        /// </summary>
        public double M31 => m31;
        /// <summary>
        /// 변환 행렬의 3행, 2열의 값을 가져옵니다.
        /// </summary>
        public double M32 => m32;
        /// <summary>
        /// 변환 없는 단위 행렬인지 여부를 가져옵니다. 
        /// </summary>
        public bool IsIdentity => isIdentity;
        /// <summary>
        /// 0 행렬인지 여부를 가져옵니다.
        /// </summary>
        public bool IsZero => m11 == 0d && m12 == 0d && m21 == 0d && m22 == 0d && m31 == 0d && m32 == 0d;

        /// <summary>
        /// 현재 변환의 역변환을 가져옵니다.
        /// </summary>
        public Transform Invert => TryInvert(out var invert) ? invert : default;

        private Transform(in double m11, in double m12, in double m21, in double m22, in double m31, in double m32)
        {
            this.m11 = m11;
            this.m12 = m12;
            this.m21 = m21;
            this.m22 = m22;
            this.m31 = m31;
            this.m32 = m32;
            var identity = m11 == 1d && m12 == 0d && m21 == 0d && m22 == 1d;
            var translation = m31 != 0d || m32 != 0d;
            isIdentity = identity && !translation;
            translationOnly = identity && translation;
        }

        /// <summary>
        /// 이동 변환을 적용합니다.
        /// </summary>
        /// <param name="offsetX">X축 이동량</param>
        /// <param name="offsetY">Y축 이동량</param>
        /// <returns>이동 변환 적용 결과</returns>
        public Transform Translate(in double offsetX, in double offsetY) => new Transform(m11, m12, m21, m22, m31 + offsetX, m32 + offsetY);
        /// <summary>
        /// 원점(0, 0)을 기준으로 축척 변환을 적용합니다.
        /// </summary>
        /// <param name="scaleX">X축 크기 비율</param>
        /// <param name="scaleY">Y축 크기 비율</param>
        /// <returns>축척 변환 적용 결과</returns>
        public Transform Scale(in double scaleX, in double scaleY) => this * CreateScaling(scaleX, scaleY);
        /// <summary>
        /// 중심점을 지정하여 축척 변환을 적용합니다.
        /// </summary>
        /// <param name="scaleX">X축 크기 비율</param>
        /// <param name="scaleY">Y축 크기 비율</param>
        /// <param name="centerX">중심점의 X 좌표</param>
        /// <param name="centerY">중심점의 Y 좌표</param>
        /// <returns>축척 변환 적용 결과</returns>
        public Transform Scale(in double scaleX, in double scaleY, in double centerX, in double centerY) => this * CreateScaling(scaleX, scaleY, centerX, centerY);
        /// <summary>
        /// 중심점을 지정하여 축척 변환을 적용합니다.
        /// </summary>
        /// <param name="scaleX">X축 크기 비율</param>
        /// <param name="scaleY">Y축 크기 비율</param>
        /// <param name="center">중심점</param>
        /// <returns>축척 변환 적용 결과</returns>
        public Transform Scale(in double scaleX, in double scaleY, in Point center) => this * CreateScaling(scaleX, scaleY, center);
        /// <summary>
        /// 원점(0, 0)을 기준으로 가로 세로가 동일한 비율을 가진 축척 변환을 적용합니다.
        /// </summary>
        /// <param name="scale">크기 비율</param>
        /// <returns>축척 변환 적용 결과</returns>
        public Transform Scale(in double scale) => this * CreateScaling(scale);
        /// <summary>
        /// 중심점을 지정하여 가로 세로가 동일한 비율을 가진 축척 변환을 적용합니다.
        /// </summary>
        /// <param name="scale">크기 비율</param>
        /// <param name="centerX">중심점의 X 좌표</param>
        /// <param name="centerY">중심점의 Y 좌표</param>
        /// <returns>축척 변환 적용 결과</returns>
        public Transform Scale(in double scale, in double centerX, in double centerY) => this * CreateScaling(scale, centerX, centerY);
        /// <summary>
        /// 중심점을 기준으로 가로 세로가 동일한 비율을 가진 축척 변환을 적용합니다.
        /// </summary>
        /// <param name="scale">크기 비율</param>
        /// <param name="center">중심점</param>
        /// <returns>축척 변환 적용 결과</returns>
        public Transform Scale(in double scale, in Point center) => this * CreateScaling(scale, center);
        /// <summary>
        /// 원점(0, 0)을 기준으로 회전 변환을 적용합니다.
        /// </summary>
        /// <param name="radians">회전각(라디안)</param>
        /// <returns>회전 변환 적용 결과</returns>
        public Transform Rotate(in double radians) => this * CreateRotation(radians);
        /// <summary>
        /// 중심점을 지정하여 회전 변환을 적용합니다.
        /// </summary>
        /// <param name="radians">회전각(라디안)</param>
        /// <param name="centerX">중심점의 X 좌표</param>
        /// <param name="centerY">중심점의 Y 좌표</param>
        /// <returns>회전 변환 적용 결과</returns>
        public Transform Rotate(in double radians, in double centerX, in double centerY) => this * CreateRotation(radians, centerX, centerY);
        /// <summary>
        /// 중심점을 지정하여 회전 변환을 적용합니다.
        /// </summary>
        /// <param name="radians">회전각(라디안)</param>
        /// <param name="center">중심점</param>
        /// <returns>회전 변환 적용 결과</returns>
        public Transform Rotate(in double radians, in Point center) => this * CreateRotation(radians, center);
        /// <summary>
        /// 원점(0, 0)을 기준으로 기울임 변환을 적용합니다.
        /// </summary>
        /// <param name="radiansX">X축 방향 기울임각(라디안)</param>
        /// <param name="radiansY">Y축 방향 기울임각(라디안)</param>
        /// <returns>기울임 변환 적용 결과</returns>
        public Transform Skew(in double radiansX, in double radiansY) => this * CreateSkew(radiansX, radiansY);
        /// <summary>
        /// 중심점을 지정하여 기울임 변환을 적용합니다.
        /// </summary>
        /// <param name="radiansX">X축 방향 기울임각(라디안)</param>
        /// <param name="radiansY">Y축 방향 기울임각(라디안)</param>
        /// <param name="centerX">중심점의 X 좌표</param>
        /// <param name="centerY">중심점의 Y 좌표</param>
        /// <returns>기울임 변환 적용 결과</returns>
        public Transform Skew(in double radiansX, in double radiansY, in double centerX, in double centerY) => this * CreateSkew(radiansX, radiansY, centerX, centerY);
        /// <summary>
        /// 중심점을 지정하여 기울임 변환을 적용합니다.
        /// </summary>
        /// <param name="radiansX">X축 방향 기울임각(라디안)</param>
        /// <param name="radiansY">Y축 방향 기울임각(라디안)</param>
        /// <param name="center">중심점</param>
        /// <returns>기울임 변환 적용 결과</returns>
        public Transform Skew(in double radiansX, in double radiansY, in Point center) => this * CreateSkew(radiansX, radiansY, center);

        /// <summary>
        /// 단위 행렬 기반 항등 변환을 가져옵니다.
        /// </summary>
        public static Transform Identity { get; } = new Transform(1d, 0d, 0d, 1d, 0d, 0d);
        /// <summary>
        /// 이동 변환을 생성합니다.
        /// </summary>
        /// <param name="offsetX">X축 이동량</param>
        /// <param name="offsetY">Y축 이동량</param>
        /// <returns>이동 변환</returns>
        public static Transform CreateTranslation(in double offsetX, in double offsetY) => new Transform(1d, 0d, 0d, 1d, offsetX, offsetY);
        /// <summary>
        /// 원점(0, 0) 기준의 축척 변환을 생성합니다.
        /// </summary>
        /// <param name="scaleX">X축 크기 비율</param>
        /// <param name="scaleY">Y축 크기 비율</param>
        /// <returns>축척 변환</returns>
        public static Transform CreateScaling(in double scaleX, in double scaleY) => new Transform(scaleX, 0d, 0d, scaleY, 0d, 0d);
        /// <summary>
        /// 중심점을 지정한 축척 변환을 생성합니다.
        /// </summary>
        /// <param name="scaleX">X축 크기 비율</param>
        /// <param name="scaleY">Y축 크기 비율</param>
        /// <param name="centerX">중심점의 X 좌표</param>
        /// <param name="centerY">중심점의 Y 좌표</param>
        /// <returns>축척 변환</returns>
        public static Transform CreateScaling(in double scaleX, in double scaleY, in double centerX, in double centerY) => new Transform(scaleX, 0d, 0d, scaleY, centerX * (1d - scaleX), centerY * (1d - scaleY));
        /// <summary>
        /// 중심점을 지정한 축척 변환을 생성합니다.
        /// </summary>
        /// <param name="scaleX">X축 크기 비율</param>
        /// <param name="scaleY">Y축 크기 비율</param>
        /// <param name="center">중심점</param>
        /// <returns>축척 변환</returns>
        public static Transform CreateScaling(in double scaleX, in double scaleY, in Point center) => CreateScaling(scaleX, scaleY, center.X, center.Y);
        /// <summary>
        /// 원점(0, 0) 기준의 가로 세로가 동일한 비율을 가진 축척 변환을 생성합니다.
        /// </summary>
        /// <param name="scale">크기 비율</param>
        /// <returns>축척 변환</returns>
        public static Transform CreateScaling(in double scale) => CreateScaling(scale, scale);
        /// <summary>
        /// 중심점을 지정한 가로 세로가 동일한 비율을 가진 축척 변환을 생성합니다.
        /// </summary>
        /// <param name="scale">크기 비율</param>
        /// <param name="centerX">중심점의 X 좌표</param>
        /// <param name="centerY">중심점의 Y 좌표</param>
        /// <returns>축척 변환</returns>
        public static Transform CreateScaling(in double scale, in double centerX, in double centerY) => CreateScaling(scale, scale, centerX, centerY);
        /// <summary>
        /// 중심점 기준의 가로 세로가 동일한 비율을 가진 축척 변환을 생성합니다.
        /// </summary>
        /// <param name="scale">크기 비율</param>
        /// <param name="center">중심점</param>
        /// <returns>축척 변환</returns>
        public static Transform CreateScaling(in double scale, in Point center) => CreateScaling(scale, scale, center.X, center.Y);
        /// <summary>
        /// 원점(0, 0) 기준의 회전 변환을 생성합니다.
        /// </summary>
        /// <param name="radians">회전각(라디안)</param>
        /// <returns>회전 변환</returns>
        public static Transform CreateRotation(in double radians)
        {
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            return new Transform(cos, sin, -sin, cos, 0d, 0d);
        }
        /// <summary>
        /// 중심점을 지정한 회전 변환을 생성합니다.
        /// </summary>
        /// <param name="radians">회전각(라디안)</param>
        /// <param name="centerX">중심점의 X 좌표</param>
        /// <param name="centerY">중심점의 Y 좌표</param>
        /// <returns>회전 변환</returns>
        public static Transform CreateRotation(in double radians, in double centerX, in double centerY)
        {
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            return new Transform(cos, sin, -sin, cos, centerX * (1d - cos) + centerY * sin, centerY * (1d - cos) - centerX * sin);
        }
        /// <summary>
        /// 중심점을 지정한 회전 변환을 생성합니다.
        /// </summary>
        /// <param name="radians">회전각(라디안)</param>
        /// <param name="center">중심점</param>
        /// <returns>회전 변환</returns>
        public static Transform CreateRotation(in double radians, in Point center) => CreateRotation(radians, center.X, center.Y);
        /// <summary>
        /// 원점(0, 0) 기준의 기울임 변환을 생성합니다.
        /// </summary>
        /// <param name="radiansX">X축 방향 기울임각(라디안)</param>
        /// <param name="radiansY">Y축 방향 기울임각(라디안)</param>
        /// <returns>기울임 변환</returns>
        public static Transform CreateSkew(in double radiansX, in double radiansY) => new Transform(1d, Math.Tan(radiansY), Math.Tan(radiansX), 1d, 0d, 0d);
        /// <summary>
        /// 중심점을 지정한 기울임 변환을 생성합니다.
        /// </summary>
        /// <param name="radiansX">X축 방향 기울임각(라디안)</param>
        /// <param name="radiansY">Y축 방향 기울임각(라디안)</param>
        /// <param name="centerX">중심점의 X 좌표</param>
        /// <param name="centerY">중심점의 Y 좌표</param>
        /// <returns>기울임 변환</returns>
        public static Transform CreateSkew(in double radiansX, in double radiansY, in double centerX, in double centerY)
        {
            double tanX = Math.Tan(radiansX);
            double tanY = Math.Tan(radiansY);
            return new Transform(1d, tanY, tanX, 1d, -centerY * tanX, -centerX * tanY);
        }
        /// <summary>
        /// 중심점을 지정한 기울임 변환을 생성합니다.
        /// </summary>
        /// <param name="radiansX">X축 방향 기울임각(라디안)</param>
        /// <param name="radiansY">Y축 방향 기울임각(라디안)</param>
        /// <param name="center">중심점</param>
        /// <returns>기울임 변환</returns>
        public static Transform CreateSkew(in double radiansX, in double radiansY, in Point center) => CreateSkew(radiansX, radiansY, center.X, center.Y);

        /// <summary>
        /// 역변환 계산을 시도합니다.
        /// </summary>
        /// <param name="result">역변환</param>
        /// <returns>계산 성공 여부</returns>
        public bool TryInvert(out Transform result)
        {
            double determinant = m11 * m22 - m21 * m12;
            if (Math.Abs(determinant) < double.Epsilon)
            {
                result = default;
                return false;
            }
            double invert = 1d / determinant;
            result = new Transform(m22 * invert, -m12 * invert, -m21 * invert, m11 * invert, (m21 * m32 - m31 * m22) * invert, (m31 * m12 - m11 * m32) * invert);
            return true;
        }

        /// <summary>
        /// 첫 번째 변환에 두 번째 변환을 적용합니다.
        /// </summary>
        /// <param name="transform1">첫 번째 변환</param>
        /// <param name="transform2">두 번째 변환</param>
        /// <returns>변환 적용 결과</returns>
        public static Transform operator *(in Transform transform1, in Transform transform2) => new Transform(
            transform1.m11 * transform2.m11 + transform1.m12 * transform2.m21,
            transform1.m11 * transform2.m12 + transform1.m12 * transform2.m22,
            transform1.m21 * transform2.m11 + transform1.m22 * transform2.m21,
            transform1.m21 * transform2.m12 + transform1.m22 * transform2.m22,
            transform1.m31 * transform2.m11 + transform1.m32 * transform2.m21 + transform2.m31,
            transform1.m31 * transform2.m12 + transform1.m32 * transform2.m22 + transform2.m32);

        /// <summary>
        /// 두 변환이 같은지 확인합니다.
        /// </summary>
        /// <param name="transform1">첫 번째 변환</param>
        /// <param name="transform2">두 번째 변환</param>
        /// <returns>같을 경우 true, 다를 경우 false.</returns>
        public static bool operator ==(in Transform transform1, in Transform transform2) =>
            transform1.m11 == transform2.m11 && transform1.m12 == transform2.m12 &&
            transform1.m21 == transform2.m21 && transform1.m22 == transform2.m22 &&
            transform1.m31 == transform2.m31 && transform1.m32 == transform2.m32;

        /// <summary>
        /// 두 변환이 다른지 확인합니다.
        /// </summary>
        /// <param name="transform1">첫 번째 변환</param>
        /// <param name="transform2">두 번째 변환</param>
        /// <returns>다를 경우 true, 같을 경우 false.</returns>
        public static bool operator !=(in Transform transform1, in Transform transform2) => !(transform1 == transform2);

        /// <summary>
        /// 점 좌표에 변환을 적용합니다.
        /// </summary>
        /// <param name="transform">적용할 변환</param>
        /// <param name="point">점 좌표</param>
        /// <returns>변환 적용 결과</returns>
        public static Point operator *(in Transform transform, in Point point) => transform.isIdentity ? point
            : transform.translationOnly ? new Point(point.X + transform.m31, point.Y + transform.m32) : new Point(
            point.X * transform.m11 + point.Y * transform.m21 + transform.m31,
            point.X * transform.m12 + point.Y * transform.m22 + transform.m32);

        /// <summary>
        /// 점 좌표에 변환을 적용한 결과를 생성합니다.
        /// </summary>
        /// <param name="point">점 좌표</param>
        /// <param name="transform">적용할 변환</param>
        /// <returns>변환 적용 결과</returns>
        public static Point operator *(in Point point, in Transform transform) => transform.isIdentity ? point : transform * point;

        /// <summary>
        /// 선분의 두 점에 변환을 적용한 새 선분을 생성합니다.
        /// </summary>
        /// <param name="transform">적용할 변환</param>
        /// <param name="line">선분</param>
        /// <returns>변환이 적용된 선분</returns>
        public static Line operator *(in Transform transform, in Line line) => transform.isIdentity ? line : new Line(transform * line.Start, transform * line.End);

        /// <summary>
        /// 선분의 두 점에 변환을 적용한 새 선분을 생성합니다.
        /// </summary>
        /// <param name="line">선분</param>
        /// <param name="transform">적용할 변환</param>
        /// <returns>변환이 적용된 선분</returns>
        public static Line operator *(in Line line, in Transform transform) => transform.isIdentity ? line : transform * line;

        /// <summary>
        /// 3차 베지어 곡선의 컨트롤 시작점과 두 개의 컨트롤 포인트 및 끝점에 변환을 적용한 새 3차 베지어 곡선을 생성합니다.
        /// </summary>
        /// <param name="transform">적용할 변환</param>
        /// <param name="bezier">3차 베지어 곡선</param>
        /// <returns>변환이 적용된 3차 베지어 곡선</returns>
        public static CubicBezier operator *(in Transform transform, in CubicBezier bezier) => transform.isIdentity ? bezier : new CubicBezier(transform * bezier.Start, transform * bezier.Control1, transform * bezier.Control2, transform * bezier.End);

        /// <summary>
        /// 3차 베지어 곡선의 컨트롤 시작점과 컨트롤 포인트 및 끝점에 변환을 적용한 새 3차 베지어 곡선을 생성합니다.
        /// </summary>
        /// <param name="bezier">3차 베지어 곡선</param>
        /// <param name="transform">적용할 변환</param>
        /// <returns>변환이 적용된 3차 베지어 곡선</returns>
        public static CubicBezier operator *(in CubicBezier bezier, in Transform transform) => transform.isIdentity ? bezier : transform * bezier;

        /// <summary>
        /// 2차 베지어 곡선의 컨트롤 시작점과 컨트롤 포인트 및 끝점에 변환을 적용한 새 2차 베지어 곡선을 생성합니다.
        /// </summary>
        /// <param name="transform">적용할 변환</param>
        /// <param name="bezier">2차 베지어 곡선</param>
        /// <returns>변환이 적용된 2차 베지어 곡선</returns>
        public static QuadraticBezier operator *(in Transform transform, in QuadraticBezier bezier) => transform.isIdentity ? bezier : new QuadraticBezier(transform * bezier.Start, transform * bezier.Control, transform * bezier.End);

        /// <summary>
        /// 2차 베지어 곡선의 컨트롤 시작점과 컨트롤 포인트 및 끝점에 변환을 적용한 새 2차 베지어 곡선을 생성합니다.
        /// </summary>
        /// <param name="bezier">2차 베지어 곡선</param>
        /// <param name="transform">적용할 변환</param>
        /// <returns>변환이 적용된 2차 베지어 곡선</returns>
        public static QuadraticBezier operator *(in QuadraticBezier bezier, in Transform transform) => transform.isIdentity ? bezier : transform * bezier;

        /// <summary>
        /// 지정한 개체와 현재 변환이 같은지 여부를 확인합니다.
        /// </summary>
        /// <param name="obj">현재 변환과 비교할 개체입니다.</param>
        /// <returns>지정한 개체가 현재 변환과 같으면 true이고, 다르면 false입니다.</returns>
        public override bool Equals(object obj) => obj is Transform transform &&
            m11 == transform.m11 && m12 == transform.m12 &&
            m21 == transform.m21 && m22 == transform.m22 &&
            m31 == transform.m31 && m32 == transform.m32;

        /// <summary>
        /// 현재 개체의 해시 코드를 가져옵니다.
        /// </summary>
        /// <returns>현재 개체의 해시 코드</returns>
        public override int GetHashCode()
        {
            int hashCode = 1986762283;
            hashCode = hashCode * -1521134295 + m11.GetHashCode();
            hashCode = hashCode * -1521134295 + m12.GetHashCode();
            hashCode = hashCode * -1521134295 + m21.GetHashCode();
            hashCode = hashCode * -1521134295 + m22.GetHashCode();
            hashCode = hashCode * -1521134295 + m31.GetHashCode();
            hashCode = hashCode * -1521134295 + m32.GetHashCode();
            return hashCode;
        }
    }

}