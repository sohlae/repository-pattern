using ImpromptuInterface;
using System;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace RP.Data.StoredProcedures.ContextHelper
{
    public class DynamicProxy<T> : DynamicObject, INotifyPropertyChanged
        where T : class, new()
    {
        private readonly T _subject;
        public event PropertyChangedEventHandler PropertyChanged;

        public DynamicProxy(T subject)
        {
            _subject = subject;
        }

        protected PropertyInfo GetPropertyInfo(string propertyName)
        {
            return _subject.GetType()
                .GetProperties()
                .First(propertyInfo => propertyInfo.Name == propertyName);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(_subject, new PropertyChangedEventArgs(propertyName));
        }

        public static I As<I>() where I : class
        {
            if (!typeof(I).IsInterface)
                throw new ArgumentException("I must be an interface type!");

            return new DynamicProxy<T>(new T())
                .ActLike<I>();
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type == typeof(INotifyPropertyChanged))
            {
                result = this;
                return true;
            }

            if (_subject != null && binder.Type.IsAssignableFrom(_subject.GetType()))
            {
                result = _subject;
                return true;
            }
            else
                return base.TryConvert(binder, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetPropertyInfo(binder.Name)
                .GetValue(_subject, null);

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            GetPropertyInfo(binder.Name)
                .SetValue(_subject, value, null);

            OnPropertyChanged(binder.Name);

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                result = _subject.GetType()
                    .GetMethod(binder.Name)
                    .Invoke(_subject, args);

                return true;
            }

            catch
            {
                result = null;
                return false;
            }
        }
    }
}
