using System;
using System.Collections.Generic;

namespace Core
{
    public class InstanceContainer : IInstanceContainer
    {
        private static InstanceContainer _expampler;
        private IDictionary<Type, object> _instancesByType = new Dictionary<Type, object>();

        private InstanceContainer()
        {

        }

        public static IInstanceContainer Instance
        {
            get { return _expampler ?? (_expampler = new InstanceContainer()); }
        }

        public void Set<T>(T instance) { _instancesByType[typeof(T)] = instance; }

        public T Resolve<T>()
        {
            object instance;
            if (!_instancesByType.TryGetValue(typeof(T), out instance))
            {
                throw new InvalidOperationException(
                    string.Format("Instance by type '{0}' is not exist in the container", typeof(T)));
            }

            return (T) instance;
        }
    }
}
