using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace VagabondK.Indicators
{
    /// <summary>
    /// 파라미터 기반 객체 보관 캐시입니다.
    /// </summary>
    /// <typeparam name="T">캐시에 보관할 객체 형식</typeparam>
    public abstract class ParametricCache<T> : INotifyPropertyChanged where T : class
    {
        private T value;
        private Guid hash;
        private bool isUpdating;
        private bool isUpdated;
        private bool isValid;

        /// <summary>
        /// 파라미터들에 의해 정의된 해시를 가져옵니다.
        /// </summary>
        public Guid Hash { get => hash; private set => SetProperty(ref hash, value); }
        /// <summary>
        /// BeginUpdate 메서드를 호출하여 현재 파라미터를 수정하고 있는지 여부를 가져옵니다. EndUpdate 메서드를 호출하면 false가 됩니다.
        /// </summary>
        public bool IsUpdating { get => isUpdating; private set => SetProperty(ref isUpdating, value); }

        /// <summary>
        /// 캐시에서 객체를 가져옵니다.
        /// </summary>
        /// <returns>캐시된 객체</returns>
        protected T GetObject()
        {
            if (!isValid)
            {
                value = cache.GetValue(hash, () => OnCreate());
                isValid = true;
            }
            return value;
        }

        private readonly static Cache<Guid, T> cache = new Cache<Guid, T>();

        /// <summary>
        /// 생성자
        /// </summary>
        protected ParametricCache() => Invalidate();

        /// <summary>
        /// 종료자
        /// </summary>
        ~ParametricCache()
        {
            cache.Release(hash);
        }

        /// <summary>
        /// 속성 값이 변경될 때 발생합니다.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 파라미터 값이 변경될 때 발생합니다. IsUpdating이 true일 때는 발생하지 않으며, EndUpdate가 호출될 때 파라미터 값이 하나라도 변경되었으면 발생합니다.
        /// </summary>
        public event EventHandler ParameterChanged;

        private void Invalidate()
        {
            isValid = false;
            cache.Release(hash);
            OnInvalidated(hash);
            Hash = new Guid(MD5.ComputeHash(Encoding.UTF8.GetBytes(OnGenerateParametersString(new StringBuilder()).ToString())));
        }

        /// <summary>
        /// 파라미터들에 의해 정의된 해시가 파라미터 변경에 의해 무효화 되었을 때 호출됩니다.
        /// </summary>
        /// <param name="hash">무효화 된 파라미터들에 의해 정의된 해시</param>
        protected virtual void OnInvalidated(Guid hash) { }

        /// <summary>
        /// 속성에 값을 설정합니다.
        /// </summary>
        /// <typeparam name="TProperty">속성 형식</typeparam>
        /// <param name="target">속성 저장 변수</param>
        /// <param name="value">설정할 값</param>
        /// <param name="propertyName">속성 이름</param>
        /// <returns>현재 설정으로 인해 속성이 바뀌었는지 여부</returns>
        protected bool SetProperty<TProperty>(ref TProperty target, TProperty value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<TProperty>.Default.Equals(target, value))
            {
                target = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }

        /// <summary>
        /// 파라미터에 값을 설정합니다. 파라미터를 바꾸면 Hash가 바뀌게 되며 기존의 Hash는 무효화 됩니다. 
        /// 단, IsUpdating이 true일 때는 Hash를 바로 업데이트하지 않으며, EndUpdate가 호출될 때 파라미터 값이 하나라도 변경되었으면 Hash를 업데이트합니다. 
        /// 그 이후에 캐시 객체를 요청할 경우 객체를 새로 생성하게 됩니다.
        /// </summary>
        /// <typeparam name="TProperty">파라미터 형식</typeparam>
        /// <param name="target"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetParameter<TProperty>(ref TProperty target, TProperty value, [CallerMemberName] string propertyName = null)
        {
            if (SetProperty(ref target, value, propertyName))
            {
                if (isUpdating)
                    isUpdated = true;
                else
                {
                    RaiseParameterChanged();
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// ParameterChanged 이벤트를 발생시킵니다.
        /// </summary>
        protected void RaiseParameterChanged()
        {
            Invalidate();
            ParameterChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 파라미터 업데이트를 시작합니다. 업데이트가 끝났을 때 EndUpdate 메서드를 호출해야 합니다.
        /// </summary>
        public void BeginUpdate()
        {
            IsUpdating = true;
            isUpdated = false;
        }

        /// <summary>
        /// 파라미터 업데이트를 종료합니다. 값이 바뀐 파라미터가 하나라도 있을 경우, 기존의 Hash를 무효화 합니다.
        /// </summary>
        public void EndUpdate()
        {
            if (isUpdated)
                RaiseParameterChanged();
            IsUpdating = false;
        }

        /// <summary>
        /// 대상 캐시의 모든 파라미터가 현재 캐시의 파라미터들과 같은지 여부를 가져옵니다.
        /// </summary>
        /// <param name="target">대상 캐시</param>
        /// <returns>모든 파라미터의 일치 여부</returns>
        public virtual bool EqualsParameters(ParametricCache<T> target) => target is ParametricCache<T> parameters && parameters.GetType().Equals(GetType());
        /// <summary>
        /// 파라미터를 나타내는 문자열을 생성할 때 호출됩니다. 해당 문자열은 Hash를 생성할 때 사용되므로, 반드시 모든 파라미터의 값을 콤마 등의 구분기호로 나누어 문자열에 포합해야 합니다.
        /// </summary>
        /// <param name="stringBuilder">StringBuilder 객체</param>
        /// <returns>파라미터 문자열</returns>
        protected virtual StringBuilder OnGenerateParametersString(StringBuilder stringBuilder) => stringBuilder.Append(GetType());

        /// <summary>
        /// 파라미터를 이용하여 객체를 생성할 때 호출됩니다.
        /// </summary>
        /// <returns>캐시할 객체</returns>
        protected abstract T OnCreate();
    }
}
