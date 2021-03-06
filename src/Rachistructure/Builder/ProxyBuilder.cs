﻿// ***********************************************************************
// Assembly         : Rachistructure
// Author           : Salih KARAHAN <salihkarahan@outlook.com>
// Created          : 04-14-2016
//
// Last Modified By : Salih KARAHAN <salihkarahan@outlook.com>
// Last Modified On : 06-12-2016
// ***********************************************************************
// <copyright file="ProxyBuilder.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <summary>
/// The Builder namespace.
/// </summary>
/// <remarks>Rachistructure</remarks>
/// TODO Edit XML Comment Template for Builder
namespace Rachistructure.Builder
{
#if(!DEBUG)
    using System.Diagnostics;
#endif
    using System;

    /// <summary>
    /// Class ProxyBuilder. This class cannot be inherited.
    /// </summary>
    /// <remarks>Rachistructure</remarks>
    /// TODO Edit XML Comment Template for ProxyBuilder
#if (!DEBUG)
    [DebuggerStepThrough]
#endif
    public sealed class ProxyBuilder
    {
        /// <summary>
        /// Creates the proxy object.
        /// </summary>
        /// <typeparam name="TProxyBaseType">The type of the proxy base type.</typeparam>
        /// <param name="kind">The kind.</param>
        /// <returns>TProxyBaseType.</returns>
        /// <remarks>Rachistructure</remarks>
        public static TProxyBaseType CreateProxyObject<TProxyBaseType>(ProxyKind kind = ProxyKind.RealProxy)
        {
            return (TProxyBaseType)CreateProxyObject(kind, typeof(TProxyBaseType), null);
        }

        /// <summary>
        /// Creates the proxy object.
        /// </summary>
        /// <param name="proxyKind">Kind of the proxy.</param>
        /// <param name="proxyBaseType">Type of the proxy base.</param>
        /// <param name="proxyBaseInstance">The proxy base instance.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="System.Exception">Proxy object can not be abstract or interface!</exception>
        /// <exception cref="System.NotImplementedException">Not yet! This case reevaluate in other project!\nYou can see in Dynoxygen project in Github.</exception>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for CreateProxyObject
        public static object CreateProxyObject(ProxyKind proxyKind, Type proxyBaseType, object proxyBaseInstance)
        {
            Type baseType = proxyBaseType;
            object proxiesObject = null;
            if (baseType.IsAbstract || baseType.IsInterface)
            {
                throw new Exception("Proxy object can not be abstract or interface!");
            }
            if (baseType.IsClass)
            {
                switch (proxyKind)
                {
                    case ProxyKind.RealProxy:
                        dynamic instance = Convert.ChangeType(proxyBaseInstance, proxyBaseType);
                        if (instance != null)
                        {
                            proxiesObject = new RealProxyBuilder(baseType, ref instance).GetTransparentProxy();
                        }
                        else
                        {
                            proxiesObject = new RealProxyBuilder(baseType).GetTransparentProxy();
                        }
                        break;
                    case ProxyKind.DynamicProxy:
                        throw new NotImplementedException("Not yet! This case reevaluate in other project!\nYou can see in Dynoxygen project in Github.");
                        break;
                    default:
                        break;
                }
            }

            return proxiesObject;
        }
    }
}
