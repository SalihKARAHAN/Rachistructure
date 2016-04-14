using System;
using System.Collections.Generic;
using System.Reflection;
using Rachistructure.Builder;
#if(!DEBUG)
using System.Diagnostics;
#endif

namespace Rachistructure.DependecyResolver
{
    /// <summary>
    /// 
    /// </summary>
#if (!DEBUG)
    [DebuggerStepThrough]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
    public class InstanceProvider
    {
        private Dictionary<Type, Type> _instanceMap = new Dictionary<Type, Type>();

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="TAbstractType">The type of the abstract type.</typeparam>
        /// <typeparam name="TConcreteType">The type of the concrete type.</typeparam>
        /// <exception cref="Exception"></exception>
        public void Register<TAbstractType, TConcreteType>()
            where TAbstractType : class
            where TConcreteType : TAbstractType
        {
            Type abstractType = typeof(TAbstractType);
            Type concreteType = typeof(TConcreteType);

            if (_instanceMap.ContainsKey(abstractType))
            {
                throw new Exception(string.Format("{0} type already in this container!", abstractType.FullName));
            }

            _instanceMap.Add(abstractType, concreteType);
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="TAbstractType">The type of the abstract type.</typeparam>
        /// <returns></returns>
        public TAbstractType Resolve<TAbstractType>(bool isProxy = false)
        {
            TAbstractType resolvedAbstractType = (TAbstractType)Resolve(typeof(TAbstractType), isProxy);
            return resolvedAbstractType;
        }

        private object Resolve(Type abstractType, bool isProxy)
        {
            if (_instanceMap.ContainsKey(abstractType))
            {
                Type concreteType = _instanceMap[abstractType];
                ConstructorInfo firstConstructor = concreteType.GetConstructors()[0];
                ParameterInfo[] parameters = firstConstructor.GetParameters();
                object resolvedConcreteInstance = null;

                if (parameters.Length > 0)
                {
                    object[] resolvedParameters = new object[parameters.Length];

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        ParameterInfo parameter = parameters[i];
                        Type parameterType = parameter.ParameterType;
                        object parameterInstance = Resolve(parameterType, isProxy);
                        resolvedParameters[i] = parameterInstance;
                    }

                    resolvedConcreteInstance = firstConstructor.Invoke(resolvedParameters);
                }
                else
                {
                    resolvedConcreteInstance = Activator.CreateInstance(concreteType);
                }

                if (isProxy)
                {
                    object proxy = ProxyBuilder.CreateProxyObject(ProxyKind.RealProxy, concreteType, resolvedConcreteInstance);
                    return proxy;
                }

                return resolvedConcreteInstance;
            }

            throw new Exception(string.Format("{0} do not resolved because {0} not registered on this container!"));
        }
    }
}
