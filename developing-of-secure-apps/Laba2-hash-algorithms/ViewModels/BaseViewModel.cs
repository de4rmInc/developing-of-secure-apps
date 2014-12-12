using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Laba2_hash_algorithms.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null && !string.IsNullOrEmpty(propertyName))
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, property.CreateChangeEventArgs());
            }
        }
    }
    public static class PropertyExtensions
    {
        public static PropertyChangedEventArgs CreateChangeEventArgs<T>(this Expression<Func<T>> property)
        {
            var expression = property.Body as MemberExpression;
            MemberInfo member = expression.Member;

            return new PropertyChangedEventArgs(member.Name);
        }
    }
}
