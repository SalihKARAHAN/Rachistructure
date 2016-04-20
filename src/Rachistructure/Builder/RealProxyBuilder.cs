using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using Rachistructure.Attribute;
#if(!DEBUG)
using System.Diagnostics;
#endif

namespace Rachistructure.Builder
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Runtime.Remoting.Proxies.RealProxy" />
#if (!DEBUG)
    [DebuggerStepThrough]
#endif
    internal class RealProxyBuilder : RealProxy
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _type;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly dynamic _concreteBaseType;

        public RealProxyBuilder(Type proxyBaseType)
            : base(proxyBaseType)
        {
            this._type = proxyBaseType;
            this._concreteBaseType = Activator.CreateInstance(proxyBaseType);
        }

        public RealProxyBuilder(Type proxyBaseType, ref dynamic proxyBaseInstance)
            : base(proxyBaseType)
        {
            this._type = proxyBaseType;
            this._concreteBaseType = proxyBaseInstance;
        }

        public override IMessage Invoke(IMessage msg)
        {
            ReturnMessage returnMessage = null;

            IMethodCallMessage calledMethodInProxy = (IMethodCallMessage)msg;
            MethodInfo abstractMethodInfo = (MethodInfo)calledMethodInProxy.MethodBase;

            MethodInfo methodInfo = this._type.GetMethod(calledMethodInProxy.MethodBase.Name);
            object[] customAttributesOnMethod = methodInfo.GetCustomAttributes(typeof(BaseAspectAttribute), true);
            for (int i = 0; i < customAttributesOnMethod.Length; i++)
            {
                object customAttributeOnMethod = customAttributesOnMethod[i];
                ((BaseAspectAttribute)customAttributeOnMethod).OnMethodBefore(new MethodMetadata { Name = "SıradanBirMethod" });
            }

            try
            {
                var result = abstractMethodInfo.Invoke(_concreteBaseType, calledMethodInProxy.InArgs);
                returnMessage = new ReturnMessage(result, null, 0, calledMethodInProxy.LogicalCallContext, calledMethodInProxy);
            }
            catch (Exception exception)
            {

                //throw;
            }

            for (int i = 0; i < customAttributesOnMethod.Length; i++)
            {
                object customAttributeOnMethod = customAttributesOnMethod[i];
                ((BaseAspectAttribute)customAttributeOnMethod).OnMethodAfter(new MethodMetadata { Name = "SıradanBirMethod" });
            }

            return returnMessage;
        }

    }
}
