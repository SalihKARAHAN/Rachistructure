using System;
#if(!DEBUG)
using System.Diagnostics;
#endif

namespace Rachistructure.Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Attribute" />
#if (!DEBUG)
    [DebuggerStepThrough]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
    public abstract class BaseAspectAttribute : System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAspectAttribute"/> class.
        /// </summary>
        protected BaseAspectAttribute()
        {

        }

        /// <summary>
        /// Called when [method before].
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        public virtual void OnMethodBefore(MethodMetadata methodInfo)
        {

        }

        /// <summary>
        /// Called when [method exception].
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="exception">The exception.</param>
        public virtual void OnMethodException(MethodMetadata methodInfo, Exception exception)
        {

        }

        /// <summary>
        /// Called when [method after].
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        public virtual void OnMethodAfter(MethodMetadata methodInfo)
        {

        }
    }
}
