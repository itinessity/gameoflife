using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.MVVM
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();


        public event PropertyChangedEventHandler PropertyChanged;

        protected T Get<T>(string name)
        {
            return Get(name, default(T));
        }

        protected T Get<T>(string name, T defaultValue)
        {
            if (_values.ContainsKey(name))
            {
                return (T)_values[name];
            }

            return defaultValue;
        }

        protected T Get<T>(string name, Func<T> initialValue)
        {
            if (_values.ContainsKey(name))
            {
                return (T)_values[name];
            }

            Set(name, initialValue());
            return Get<T>(name);
        }

        protected T Get<T>(Expression<Func<T>> expression)
        {
            return Get<T>(PropertyName(expression));
        }

        protected T Get<T>(Expression<Func<T>> expression, T defaultValue)
        {
            return Get(PropertyName(expression), defaultValue);
        }

        protected T Get<T>(Expression<Func<T>> expression, Func<T> initialValue)
        {
            return Get(PropertyName(expression), initialValue);
        }

        public void Set<T>(string name, T value)
        {
            if (_values.ContainsKey(name))
            {
                if (_values[name] == null && value == null)
                    return;

                if (_values[name] != null && _values[name].Equals(value))
                    return;

                _values[name] = value;
            }
            else
            {
                _values.Add(name, value);
            }

            OnPropertyChanged(name);
        }


        private static string PropertyName<T>(Expression<Func<T>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException("expression must be a property expression");

            return memberExpression.Member.Name;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
