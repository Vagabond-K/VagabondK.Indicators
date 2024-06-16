namespace VagabondK.Indicators.GeometryUtil
{
    /// <summary>
    /// 너비와 높이로 구성된 크기를 나타냅니다.
    /// </summary>
    public struct Size
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="size">가로 및 세로 크기</param>
        public Size(in double size)
        {
            Width = size;
            Height = size;
        }
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="width">가로 크기</param>
        /// <param name="height">세로 크기</param>
        public Size(in double width, in double height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// 가로 크기를 가져오거나 설정합니다.
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// 세로 크기를 가져오거나 설정합니다.
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// 빈 면적을 나타내는지 여부를 가져옵니다.
        /// </summary>
        public bool IsEmpty => Width == 0 && Height == 0;

        /// <summary>
        /// 지정한 개체와 현재 크기가 같은지 여부를 확인합니다.
        /// </summary>
        /// <param name="obj">현재 크기와 비교할 개체입니다.</param>
        /// <returns>지정한 개체가 현재 크기와 같으면 true이고, 다르면 false입니다.</returns>
        public override bool Equals(object obj) => obj is Size size && Width == size.Width && Height == size.Height;

        /// <summary>
        /// 현재 개체의 해시 코드를 가져옵니다.
        /// </summary>
        /// <returns>현재 개체의 해시 코드</returns>
        public override int GetHashCode()
        {
            int hashCode = 859600377;
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// 지정한 크기가 같은지 여부를 확인합니다.
        /// </summary>
        /// <param name="size1">비교할 첫 번째 크기입니다.</param>
        /// <param name="size2">비교할 두 번째 크기입니다.</param>
        /// <returns>크기가 같으면 true이고, 다르면 false입니다.</returns>
        public static bool operator ==(Size size1, Size size2) => size1.Width == size2.Width && size1.Height == size2.Height;
        /// <summary>
        /// 지정한 크기가 다른지 여부를 확인합니다.
        /// </summary>
        /// <param name="size1">비교할 첫 번째 크기입니다.</param>
        /// <param name="size2">비교할 두 번째 크기입니다.</param>
        /// <returns>크기가 다르면 true이고, 같으면 false입니다.</returns>
        public static bool operator !=(Size size1, Size size2) => !(size1 == size2);
    }
}
