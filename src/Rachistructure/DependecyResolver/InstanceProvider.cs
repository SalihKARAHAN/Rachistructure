// ***********************************************************************
// Assembly         : Rachistructure
// Author           : Salih KARAHAN <salihkarahan@outlook.com>
// Created          : 04-13-2016
//
// Last Modified By : Salih KARAHAN <salihkarahan@outlook.com>
// Last Modified On : 06-12-2016
// ***********************************************************************
// <copyright file="InstanceProvider.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Rachistructure.DependecyResolver
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Rachistructure.Builder;
#if(!DEBUG)
    using System.Diagnostics;
#endif

    /// <summary>
    /// Class InstanceProvider.
    /// </summary>
    /// <remarks>Rachistructure</remarks>
#if (!DEBUG)
    [DebuggerStepThrough]
    [DebuggerDisplay("Items: {this._instanceMap} ItemsCount: {this._instanceMap.Count}")]
#endif
    public class InstanceProvider
    {
#if (!DEBUG)
        /// <summary>
        /// The _instance map
        /// </summary>
        /// TODO Edit XML Comment Template for _instanceMap
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
#endif
        private Dictionary<string, Type> _instanceMap = new Dictionary<string, Type>();

        public void Regiser<TConcreteType>()
        {
            Type concreteType = typeof (TConcreteType);
            this.Register(concreteType.FullName, concreteType);
        }

        public void Register<TConcreteType>(string registrationName)
        {
            Type concreteType = typeof(TConcreteType);
            this.Register(registrationName, concreteType);   
        }
        
        public void Register<TAbstractType, TConcreteType>()
        {
            Type concreteType = typeof(TConcreteType);
            Type abstractType = typeof (TAbstractType);
            this.Register(abstractType.FullName, concreteType);
        }

        //public void Register<TAbstractType, TConcreteType>(string registrationName)
        //{
        //}

        public void Register(string registrationName, Type type)
        {
            if (_instanceMap.ContainsKey(registrationName))
            {
                throw new Exception(string.Format("{0} name already in this container!", registrationName));
            }

            _instanceMap.Add(registrationName, type);
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="TAbstractType">The type of the abstract type.</typeparam>
        /// <param name="isProxy">if set to <c>true</c> [is proxy].</param>
        /// <returns>TAbstractType.</returns>
        /// <remarks>Rachistructure</remarks>
        public TAbstractType Resolve<TAbstractType>(bool isProxy = false)
        {
            return (TAbstractType) this.Resolve(typeof (TAbstractType).FullName, isProxy);
        }

        public TAbstractType Resolve<TAbstractType>(string name, bool isProxy)
        {
            TAbstractType resolvedAbstractType = (TAbstractType)Resolve(name, isProxy);
            return resolvedAbstractType;
        }

        /// <summary>
        /// Resolves the specified abstract type.
        /// </summary>
        /// <param name="abstractType">Type of the abstract.</param>
        /// <param name="isProxy">if set to <c>true</c> [is proxy].</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="System.Exception"></exception>
        /// <remarks>Rachistructure</remarks>
        private object Resolve(string registrationNames, bool isProxy)
        {
            if (_instanceMap.ContainsKey(registrationNames))
            {
                Type concreteType = _instanceMap[registrationNames];
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
                        // parametre türü nedir?
                        object parameterInstance = Resolve("parameterType", isProxy);
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

            throw new Exception(string.Format("{0} do not resolved because {1} not registered on this container!"));
        }
    }
}
