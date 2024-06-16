using System.Collections.Generic;

namespace VagabondK.Indicators
{
    /// <summary>
    /// 세그먼트 상태에 따라 값을 문자열로 변환하여 디지털 문자들을 표시합니다.
    /// </summary>
    public interface IDigitalTextPresenter : IDigitalText, IDigitalIndicatorPresenter
    {
    }

    /// <summary>
    /// IDigitalTextPresenter 인터페이스에 대한 확장 메서드 모음입니다.
    /// </summary>
    public static class DigitalTextPresenterExtentions
    {
        /// <summary>
        /// IDigitalTextPresenter 객체를 그리기 위한 파트 목록을 가져옵니다.
        /// </summary>
        /// <param name="presenter">IDigitalTextPresenter 객체</param>
        /// <returns>인디케이터 파트 목록</returns>
        public static IEnumerable<Part> CreateParts(this IDigitalTextPresenter presenter) => presenter?.CreateParts(presenter.SegmentFilter);
    }
}
