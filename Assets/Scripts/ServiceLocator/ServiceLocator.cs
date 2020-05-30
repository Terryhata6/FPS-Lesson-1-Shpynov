using System;
using System.Collections.Generic;

namespace Game
{
    public static class ServiceLocator
    {
        #region ServiceLocator

        private static readonly Dictionary<Type, object> _servicecontainer =
             new Dictionary<Type, object>();

        #endregion
        #region Methods
        public static void SetService<T>(T value) where T : class
        {
            var typeValue = value.GetType();
            if (!_servicecontainer.ContainsKey(typeValue))
            {
                _servicecontainer[typeValue] = value;
            }
        }

        public static T Resolve<T>()
        {
            return (T)_servicecontainer[typeof(T)];
        }
        #endregion
    }
}