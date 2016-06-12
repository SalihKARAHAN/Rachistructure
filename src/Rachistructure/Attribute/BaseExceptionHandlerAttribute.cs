// ***********************************************************************
// Assembly         : Rachistructure
// Author           : Salih KARAHAN <salihkarahan@outlook.com>
// Created          : 06-12-2016
//
// Last Modified By : Salih KARAHAN <salihkarahan@outlook.com>
// Last Modified On : 06-12-2016
// ***********************************************************************
// <copyright file="BaseExceptionHandlerAttribute.cs" company="">
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
    using System.Diagnostics;

    /// <summary>
    /// Class BaseExceptionHandlerAttribute.
    /// </summary>
    /// <seealso cref="Rachistructure.Attribute.BaseAspectAttribute" />
    /// <remarks>Rachistructure</remarks>
    /// TODO Edit XML Comment Template for BaseExceptionHandlerAttribute
#if (!DEBUG)
    [DebuggerStepThrough]
#endif
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseExceptionHandlerAttribute : BaseAspectAttribute
    {
        /// <summary>
        /// The _is throwable
        /// </summary>
        /// TODO Edit XML Comment Template for _isThrowable
#if (!DEBUG)
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private readonly bool _isThrowable;

        /// <summary>
        /// Gets a value indicating whether this instance is throwable.
        /// </summary>
        /// <value><c>true</c> if this instance is throwable; otherwise, <c>false</c>.</value>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for IsThrowable
#if (!DEBUG)
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        protected bool IsThrowable { get { return this._isThrowable; } }


        /// <summary>
        /// Initializes a new instance of the <see cref="BaseExceptionHandlerAttribute" /> class.
        /// </summary>
        /// <param name="isThrowable">if set to <c>true</c> [is throwable].</param>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for #ctor
        protected BaseExceptionHandlerAttribute(bool isThrowable)
        {
            this._isThrowable = isThrowable;
        }
    }
}