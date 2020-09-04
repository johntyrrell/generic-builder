using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GenericBuilder
{
    public class Builder<T> where T : class
    {
        protected readonly T ObjectToBuild;
        private readonly List<KeyValuePair<PropertyInfo, Action<T>>> actions;

        public Builder()
        {
            ObjectToBuild = Activator.CreateInstance<T>();

            actions = new List<KeyValuePair<PropertyInfo, Action<T>>>();
        }


        public Builder<T> With<P>(Expression<Func<T, P>> selector, P value)
        {
            var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;
            prop.SetValue(ObjectToBuild, value, null);

            ExecuteActions(prop);

            return this;
        }

        private void ExecuteActions(PropertyInfo prop)
        {
            foreach (var action in actions.Where(a => a.Key == prop))
            {
                action.Value.Invoke(ObjectToBuild);
            }
        }

        public Builder<T> SetAction<P>(Expression<Func<T, P>> selector, Action<T> action)
        {
            var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;
            actions.Add(new KeyValuePair<PropertyInfo, Action<T>>(prop, action));

            return this;
        }

        public T Build()
        {
            return ObjectToBuild;
        }
    }
}
