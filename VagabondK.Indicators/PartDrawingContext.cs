using VagabondK.Indicators.GeometryUtil;

namespace VagabondK.Indicators
{
    /// <summary>
    /// 파트 드로잉 컨텍스트. 파트의 모양을 패스 기반으로 그리기 위한 메서드를 제공합니다.
    /// </summary>
    /// <typeparam name="TRenderer">컨텍스트를 통한 패스 그리기를 실제로 수행할 렌더러의 형식입니다.</typeparam>
    /// <typeparam name="TRendererPoint">>렌더러에서 사용할 포인트 형식입니다.</typeparam>
    public abstract class PartDrawingContext<TRenderer, TRendererPoint> : IPartDrawingContext
    {
        /// <summary>
        /// 컨텍스트를 통한 패스 그리기를 실제로 수행할 렌더러를 가져오거나 설정합니다.
        /// </summary>
        public TRenderer Renderer { get; set; }
        private Transform currentTransform = Transform.Identity;

        /// <summary>
        /// 시작점을 지정하여 패스를 그리기를 시작합니다.
        /// </summary>
        /// <param name="startPoint">시작점</param>
        public void BeginPath(Point startPoint)
        {
            if (!currentTransform.IsIdentity)
                startPoint.Transform(currentTransform);
            OnBeginPath(OnConvertToRendererPoint(startPoint));
        }

        /// <summary>
        /// 특정 지점을 향해 선분을 그립니다.
        /// </summary>
        /// <param name="endPoint">선분의 끝점</param>
        public void DrawLine(Point endPoint)
        {
            if (!currentTransform.IsIdentity)
                endPoint.Transform(currentTransform);
            OnDrawLine(OnConvertToRendererPoint(endPoint));
        }

        /// <summary>
        /// 3차 베지어 곡선을 그립니다.
        /// </summary>
        /// <param name="control1">첫 번째 컨트롤 포인트</param>
        /// <param name="control2">두 번째 컨트롤 포인트</param>
        /// <param name="end">곡선의 끝점</param>
        public void DrawCubicBezier(Point control1, Point control2, Point end)
        {
            if (!currentTransform.IsIdentity)
            {
                control1.Transform(currentTransform);
                control2.Transform(currentTransform);
                end.Transform(currentTransform);
            }
            OnDrawCubicBezier(OnConvertToRendererPoint(control1), OnConvertToRendererPoint(control2), OnConvertToRendererPoint(end));
        }

        /// <summary>
        /// 2차 베지어 곡선을 그립니다.
        /// </summary>
        /// <param name="control">컨트롤 포인트</param>
        /// <param name="end">곡선의 끝점</param>
        public void DrawQuadraticBezier(Point control, Point end)
        {
            if (!currentTransform.IsIdentity)
            {
                control.Transform(currentTransform);
                end.Transform(currentTransform);
            }
            OnDrawQuadraticBezier(OnConvertToRendererPoint(control), OnConvertToRendererPoint(end));
        }

        /// <summary>
        /// 패스를 닫습니다.
        /// </summary>
        public void Close() => OnClosePath();

        /// <summary>
        /// 파트를 그립니다.
        /// </summary>
        /// <param name="part">파트</param>
        public virtual void DrawPart(in Part part)
        {
            var temp = currentTransform;
            currentTransform = part.Transform;
            part.Drawing.DrawTo(this);
            currentTransform = temp;
        }

        /// <summary>
        /// 시작점을 지정하여 렌더러로 패스를 그리기를 시작합니다.
        /// </summary>
        /// <param name="startPoint">시작점</param>
        protected abstract void OnBeginPath(in TRendererPoint startPoint);
        /// <summary>
        /// 렌더러를 이용하여 특정 지점을 향해 선분을 그립니다.
        /// </summary>
        /// <param name="endPoint">선분의 끝점</param>
        protected abstract void OnDrawLine(in TRendererPoint endPoint);
        /// <summary>
        /// 렌더러를 이용하여 3차 베지어 곡선을 그립니다.
        /// </summary>
        /// <param name="controlPoint1">첫 번째 컨트롤 포인트</param>
        /// <param name="controlPoint2">두 번째 컨트롤 포인트</param>
        /// <param name="endPoint">곡선의 끝점</param>
        protected abstract void OnDrawCubicBezier(in TRendererPoint controlPoint1, in TRendererPoint controlPoint2, in TRendererPoint endPoint);
        /// <summary>
        /// 렌더러를 이용하여 2차 베지어 곡선을 그립니다.
        /// </summary>
        /// <param name="controlPoint">컨트롤 포인트</param>
        /// <param name="endPoint">곡선의 끝점</param>
        protected abstract void OnDrawQuadraticBezier(in TRendererPoint controlPoint, in TRendererPoint endPoint);
        /// <summary>
        /// 렌더러에서 패스 닫기 작업을 수행합니다.
        /// </summary>
        protected abstract void OnClosePath();
        /// <summary>
        /// VagabondK.Indicators.GeometryUtil.Point 형식을 렌더러에서 사용하는 포인트 형식으로 변환합니다.
        /// </summary>
        /// <param name="point">변환할 Point 값</param>
        /// <returns>변환된 포인트</returns>
        protected abstract TRendererPoint OnConvertToRendererPoint(in Point point);
    }

    /// <summary>
    /// 파트 드로잉 컨텍스트. 파트의 모양을 패스 기반으로 그리기 위한 메서드를 제공합니다.
    /// </summary>
    /// <typeparam name="TRenderer">컨텍스트를 통한 패스 그리기를 실제로 수행할 렌더러의 형식입니다.</typeparam>
    public abstract class PartDrawingContext<TRenderer> : PartDrawingContext<TRenderer, Point>
    {
        /// <summary>
        /// VagabondK.Indicators.GeometryUtil.Point 형식을 렌더러에서 사용하는 포인트 형식으로 변환합니다.
        /// </summary>
        /// <param name="point">변환할 Point 값</param>
        /// <returns>변환된 포인트</returns>
        protected override Point OnConvertToRendererPoint(in Point point) => point;
    }
}
