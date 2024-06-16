using System.Collections.Generic;

namespace VagabondK.Indicators
{
    /// <summary>
    /// IPartDrawingContext 인터페이스의 확장 메서드 모음입니다.
    /// </summary>
    public static class PartDrawingContextExtensions
    {
        /// <summary>
        /// 여러 개의 파트를 그립니다.
        /// </summary>
        /// <param name="context">IPartDrawingContext 객체</param>
        /// <param name="parts">파트 목록</param>
        public static void DrawParts(this IPartDrawingContext context, IEnumerable<Part> parts)
            => context?.DrawParts(parts);
    }
}
