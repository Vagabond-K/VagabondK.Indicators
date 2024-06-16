namespace VagabondK.Indicators
{
    /// <summary>
    /// 파트 드로잉을 정의합니다.
    /// </summary>
    public abstract class PartDrawing
    {
        static PartDrawing()
        {
            Empty = new EmptyPartDrawing();
        }

        class EmptyPartDrawing : PartDrawing
        {
            protected override void OnDraw(IPartDrawingContext context) { }
            public override bool IsEmpty => true;
        }

        /// <summary>
        /// 비어 있는 파트 드로잉을 가져옵니다.
        /// </summary>
        public static PartDrawing Empty { get; }

        /// <summary>
        /// 드로잉 행위가 없는지 여부를 가져옵니다.
        /// </summary>
        public abstract bool IsEmpty { get; }

        internal void DrawTo(IPartDrawingContext context) => OnDraw(context);

        /// <summary>
        /// 파트 드로잉 컨텍스트에 그립니다.
        /// </summary>
        /// <param name="context">파트 드로잉 컨텍스트</param>
        protected abstract void OnDraw(IPartDrawingContext context);
    }
}
