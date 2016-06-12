// ***********************************************************************
// Assembly         : Rachistructure
// Author           : Salih KARAHAN <salihkarahan@outlook.com>
// Created          : 04-14-2016
//
// Last Modified By : Salih KARAHAN <salihkarahan@outlook.com>
// Last Modified On : 06-12-2016
// ***********************************************************************
// <copyright file="Context.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <summary>
/// The Rachistructure namespace.
/// </summary>
/// <remarks>Rachistructure</remarks>
/// TODO Edit XML Comment Template for Rachistructure
namespace Rachistructure
{
    using System;
#if(!DEBUG)
    using System.Diagnostics;
#endif


    /// <summary>
    /// Class Context.
    /// </summary>
    /// <remarks>Rachistructure</remarks>
#if (!DEBUG)
    [DebuggerStepThrough]
    [DebuggerDisplay("Name={Name}, Arguments={Arguments}")]
#endif
    public class Context
    {
        /// <summary>
        /// The _exception
        /// </summary>
        /// TODO Edit XML Comment Template for _exception
        private ExceptionInfo _exception;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        /// <remarks>Rachistructure</remarks>
#if (!DEBUG)
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        /// <remarks>Rachistructure</remarks>
#if (!DEBUG)
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        public object[] Arguments { get; set; }

#if (!DEBUG)
        /// <summary>
        /// Gets or sets a value indicating whether this instance is executed.
        /// </summary>
        /// <value><c>true</c> if this instance is executed; otherwise, <c>false</c>.</value>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for IsExecuted
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        internal bool IsExecuted { get; set; }

        /// <summary>
        /// Sets the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for SetException
        internal void SetException(Exception exception)
        {
            if (this._exception == null)
            {
                this._exception = new ExceptionInfo(exception);
            }
            else
            {
                this._exception.SetException(exception);
            }
        }

        /// <summary>
        /// Determines whether [is exception handled].
        /// </summary>
        /// <returns><c>true</c> if [is exception handled]; otherwise, <c>false</c>.</returns>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for IsExceptionHandled
        internal bool IsExceptionHandled()
        {
            return this._exception.IsHandled;
        }

        /// <summary>
        /// Gets the current exception.
        /// </summary>
        /// <returns>Exception.</returns>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for GetCurrentException
        internal Exception GetCurrentException()
        {
            return this._exception.GetException();
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>The exception.</value>
        /// <remarks>Rachistructure</remarks>
#if (!DEBUG)
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        public ExceptionInfo Exception
        {
            get { return this._exception; }
        }
    }

    /// <summary>
    /// Class ExceptionInfo.
    /// </summary>
    /// <remarks>Rachistructure</remarks>

#if (!DEBUG)
    [DebuggerStepThrough]
#endif
    public class ExceptionInfo
    {
#if (!DEBUG)
        /// <summary>
        /// The _is handled
        /// </summary>
        /// TODO Edit XML Comment Template for _isHandled
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private bool _isHandled;

#if (!DEBUG)
        /// <summary>
        /// The _current exception
        /// </summary>
        /// TODO Edit XML Comment Template for _currentException
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private Exception _currentException;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionInfo"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for #ctor
        internal ExceptionInfo(Exception exception)
        {
            this._isHandled = false;
            this.SetException(exception);
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        /// <remarks>Rachistructure</remarks>

#if (!DEBUG)
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        public string Message
        {
            get { return this._currentException.Message; }
        }

        /// <summary>
        /// Sets the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for SetException
        internal void SetException(Exception exception)
        {
            if (this._currentException == null)
            {
                this._currentException = exception;
            }
            else
            {
                //??
            }
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <returns>Exception.</returns>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for GetException
        internal Exception GetException()
        {
            return this._currentException;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is handled.
        /// </summary>
        /// <value><c>true</c> if this instance is handled; otherwise, <c>false</c>.</value>
        /// <remarks>Rachistructure</remarks>
#if (!DEBUG)
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        public bool IsHandled
        {
            get { return this._isHandled; }
            set { this._isHandled = value; }
        }
    }
}
