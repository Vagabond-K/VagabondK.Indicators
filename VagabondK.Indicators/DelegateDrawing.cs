using System;

namespace VagabondK.Indicators
{
    /// <summary>
    /// 대리자를 이용한 파트 드로잉을 정의합니다.
    /// </summary>
    public class DelegateDrawing : PartDrawing
    {
        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="drawingAction">드로잉이 정의된 Action</param>
        public DelegateDrawing(Action<IPartDrawingContext> drawingAction)
        {
            this.drawingAction = drawingAction;
        }
        private readonly Action<IPartDrawingContext> drawingAction;
        /// <summary>
        /// 드로잉 행위가 없는지 여부를 가져옵니다.
        /// </summary>
        public override bool IsEmpty => drawingAction == null;
        /// <summary>
        /// 파트 드로잉 컨텍스트에 그립니다.
        /// </summary>
        /// <param name="context">파트 드로잉 컨텍스트</param>
        protected override void OnDraw(IPartDrawingContext context) => drawingAction?.Invoke(context);
    }
}