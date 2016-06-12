// ***********************************************************************
// Assembly         : Rachistructure
// Author           : Salih KARAHAN <salihkarahan@outlook.com>
// Created          : 04-14-2016
//
// Last Modified By : Salih KARAHAN <salihkarahan@outlook.com>
// Last Modified On : 06-12-2016
// ***********************************************************************
// <copyright file="RealProxyBuilder.cs" company="">
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
    using System;
    using System.Reflection;
    using System.Runtime.Remoting.Messaging;
    using System.Runtime.Remoting.Proxies;
    using Rachistructure.Attribute;
#if(!DEBUG)
    using System.Diagnostics;
#endif

    /// <summary>
    /// Class RealProxyBuilder.
    /// </summary>
    /// <seealso cref="System.Runtime.Remoting.Proxies.RealProxy" />
    /// <remarks>Rachistructure</remarks>
#if (!DEBUG)
    [DebuggerStepThrough]
#endif
    internal class RealProxyBuilder : RealProxy
    {
#if(!DEBUG)
        /// <summary>
        /// The _type
        /// </summary>
        /// TODO Edit XML Comment Template for _type
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private readonly Type _type;

#if(!DEBUG)
        /// <summary>
        /// The _concrete base type
        /// </summary>
        /// TODO Edit XML Comment Template for _concreteBaseType
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private readonly dynamic _concreteBaseType;

        /// <summary>
        /// Initializes a new instance of the <see cref="RealProxyBuilder" /> class.
        /// </summary>
        /// <param name="proxyBaseType">Type of the proxy base.</param>
        /// <remarks>Rachistructure</remarks>
        public RealProxyBuilder(Type proxyBaseType)
            : base(proxyBaseType)
        {
            this._type = proxyBaseType;
            this._concreteBaseType = Activator.CreateInstance(proxyBaseType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RealProxyBuilder" /> class.
        /// </summary>
        /// <param name="proxyBaseType">Type of the proxy base.</param>
        /// <param name="proxyBaseInstance">The proxy base instance.</param>
        /// <remarks>Rachistructure</remarks>
        public RealProxyBuilder(Type proxyBaseType, ref dynamic proxyBaseInstance)
            : base(proxyBaseType)
        {
            this._type = proxyBaseType;
            this._concreteBaseType = proxyBaseInstance;
        }

        /// <summary>
        /// When overridden in a derived class, invokes the method that is specified in the provided <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> on the remote object that is represented by the current instance.
        /// </summary>
        /// <param name="msg">A <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> that contains a <see cref="T:System.Collections.IDictionary" /> of information about the method call.</param>
        /// <returns>The message returned by the invoked method, containing the return value and any out or ref parameters.</returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
        /// </PermissionSet>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for Invoke
        public override IMessage Invoke(IMessage msg)
        {
            ReturnMessage returnMessage = null;

            IMethodCallMessage calledMethodInProxy = (IMethodCallMessage)msg;
            MethodInfo abstractMethodInfo = (MethodInfo)calledMethodInProxy.MethodBase;

            MethodInfo methodInfo = this._type.GetMethod(calledMethodInProxy.MethodBase.Name);
            object[] customAttributesOnMethod = methodInfo.GetCustomAttributes(typeof(BaseAspectAttribute), true);

            Context context = new Context();
            context.Name = calledMethodInProxy.MethodName + ";" + calledMethodInProxy.TypeName; // must full name 
            context.Arguments = calledMethodInProxy.InArgs;

            ContextManager.SetContext(context);

            for (int i = 0; i < customAttributesOnMethod.Length; i++)
            {
                object customAttributeOnMethod = customAttributesOnMethod[i];
                ((BaseAspectAttribute)customAttributeOnMethod).OnMethodBefore(context);
            }

            try
            {
                if (!ContextManager.GetIsExecuted(context.Name))
                {
                    lock (ContextManager.GetContext())
                    {
                        ContextManager.SetIsExecuted(context.Name, true);
                        var result = abstractMethodInfo.Invoke(_concreteBaseType, calledMethodInProxy.InArgs);
                        returnMessage = new ReturnMessage(result, null, 0, calledMethodInProxy.LogicalCallContext,
                            calledMethodInProxy);
                    }
                }
            }
            catch (Exception exception)
            {
                ContextManager.SetException(context.Name, exception.InnerException);

                returnMessage = new ReturnMessage(null, null, 0, calledMethodInProxy.LogicalCallContext,
                        calledMethodInProxy);

                for (int i = 0; i < customAttributesOnMethod.Length; i++)
                {
                    object customAttributeOnMethod = customAttributesOnMethod[i];
                    ((BaseAspectAttribute)customAttributeOnMethod).OnMethodException(context);
                }
                if (!ContextManager.IsExceptionHandled(context.Name))
                {
                    exception = ContextManager.GetCurrentException(context.Name);
                    throw exception;
                }
                else
                {
                    exception = null;
                    //AppDomain.CurrentDomain
                }
            }
            finally
            {
                for (int i = 0; i < customAttributesOnMethod.Length; i++)
                {
                    object customAttributeOnMethod = customAttributesOnMethod[i];
                    ((BaseAspectAttribute)customAttributeOnMethod).OnMethodAfter(context);
                }
            }

            return returnMessage;
        }

    }
}
