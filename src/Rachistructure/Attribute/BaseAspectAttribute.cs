// ***********************************************************************
// Assembly         : Rachistructure
// Author           : Salih KARAHAN <salihkarahan@outlook.com>
// Created          : 04-14-2016
//
// Last Modified By : Salih KARAHAN <salihkarahan@outlook.com>
// Last Modified On : 06-12-2016
// ***********************************************************************
// <copyright file="BaseAspectAttribute.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <summary>
/// The Attribute namespace.
/// </summary>
/// <remarks>Rachistructure</remarks>
/// TODO Edit XML Comment Template for Attribute
namespace Rachistructure.Attribute
{
    using System;
#if(!DEBUG)
    using System.Diagnostics;
#endif

    /// <summary>
    /// Class BaseAspectAttribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <remarks>Rachistructure</remarks>
    /// TODO Edit XML Comment Template for BaseAspectAttribute
#if (!DEBUG)
    [DebuggerStepThrough]
#endif
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseAspectAttribute : System.Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAspectAttribute" /> class.
        /// </summary>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for #ctor
        protected BaseAspectAttribute()
        {

        }

        /// <summary>
        /// Called when [method before].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for OnMethodBefore
        public virtual void OnMethodBefore(Context context)
        {

        }

        /// <summary>
        /// Called when [method exception].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for OnMethodException
        public virtual void OnMethodException(Context context)
        {

        }

        /// <summary>
        /// Called when [method after].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for OnMethodAfter
        public virtual void OnMethodAfter(Context context)
        {

        }
    }
}
