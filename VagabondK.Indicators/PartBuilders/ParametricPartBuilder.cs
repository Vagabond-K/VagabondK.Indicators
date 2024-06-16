namespace VagabondK.Indicators.PartBuilders
{
    /// <summary>
    /// 파라미터 기반 파트 드로잉 빌더입니다. 파라미터에 따라 파트 트로잉이 캐시에 보관됩니다.
    /// </summary>
    public abstract class ParametricPartBuilder : ParametricCache<PartDrawing>
    {
        /// <summary>
        /// 파트 드로잉을 생성하거나 캐시에서 가져옵니다.
        /// </summary>
        /// <returns>파트 드로잉</returns>
        public PartDrawing BuildPartDrawing() => GetObject();
    }
}
