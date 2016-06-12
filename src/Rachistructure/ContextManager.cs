// ***********************************************************************
// Assembly         : Rachistructure
// Author           : Salih KARAHAN <salihkarahan@outlook.com>
// Created          : 06-11-2016
//
// Last Modified By : Salih KARAHAN <salihkarahan@outlook.com>
// Last Modified On : 06-12-2016
// ***********************************************************************
// <copyright file="ContextManager.cs" company="">
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
    using System.Collections.Concurrent;
    using System.Diagnostics;

    /// <summary>
    /// Class ContextManager.
    /// </summary>
    /// <remarks>Rachistructure</remarks>
#if (!DEBUG)
    [DebuggerStepThrough]
#endif
    public class ContextManager
    {
        //private static readonly ContextManager _InstanceOfExecutionContext = new ContextManager();
        /// <summary>
        /// The _ context dictionary
        /// </summary>
        /// TODO Edit XML Comment Template for _ContextDictionary
        private static readonly ConcurrentDictionary<string, Context> _ContextDictionary = new ConcurrentDictionary<string, Context>();
        /// <summary>
        /// The _current context
        /// </summary>
        /// TODO Edit XML Comment Template for _currentContext
        private static Context _currentContext;

        //public static ContextManager Instance
        //{
        //    get { return _InstanceOfExecutionContext; }
        //}

        /// <summary>
        /// Prevents a default instance of the <see cref="ContextManager"/> class from being created.
        /// </summary>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for #ctor
        private ContextManager()
        {
        }

        /// <summary>
        /// Sets the context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for SetContext
        internal static void SetContext(Context context)
        {
            if (!_ContextDictionary.ContainsKey(context.Name))
            {
                bool result = true;
                while (result)
                {
                    result = !_ContextDictionary.TryAdd(context.Name, context);
                }
            }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <returns>Context.</returns>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for GetContext
        internal static Context GetContext()
        {
            if (_currentContext != null)
            {
                return _currentContext;
            }

            return null; // throw ex or return null?
        }

        /// <summary>
        /// Gets the is executed.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for GetIsExecuted
        internal static bool GetIsExecuted(string methodName)
        {
            DefineContext(methodName);
            return _currentContext.IsExecuted;
        }

        /// <summary>
        /// Sets the is executed.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for SetIsExecuted
        internal static void SetIsExecuted(string methodName, bool value)
        {
            DefineContext(methodName);
            _currentContext.IsExecuted = value;
        }

        /// <summary>
        /// Defines the context.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for DefineContext
        private static void DefineContext(string methodName)
        {
            Context targetContext = null;
            _ContextDictionary.TryGetValue(methodName, out targetContext);
            if (targetContext != null)
            {
                _currentContext = targetContext;
            }
        }

        /// <summary>
        /// Sets the exception.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="exception">The exception.</param>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for SetException
        internal static void SetException(string methodName, Exception exception)
        {
            DefineContext(methodName);
            _currentContext.SetException(exception);
        }

        //public Context Context
        //{
        //    get { return this._methodMetadata; }
        //}

        //internal void SetException(System.Exception exception)
        //{
        //    if (this._exceptionInfo == null)
        //    {
        //        this._exceptionInfo = new ExceptionInfo(exception);
        //    }
        //    else
        //    {
        //        this._exceptionInfo.SetException(exception);
        //    }
        //}

        //internal System.Exception GetCurrentException()
        //{
        //    if (this._exceptionInfo == null)
        //    {
        //        throw new NullReferenceException("No exception in your context!");
        //    }

        //    return this._exceptionInfo.GetCurrentException();
        //}


        //public ExceptionInfo Exception { get { return this._exceptionInfo; } }

        //public class ExceptionInfo
        //{
        //    private bool? _isExceptionHandled = null;
        //    private bool _hasExceptionOnThread = false;
        //    private System.Exception _currentException = null;

        //    internal ExceptionInfo(Exception exception)
        //    {
        //        this.SetException(exception);
        //    }

        //    public string Message
        //    {
        //        get { return this._currentException.Message; }
        //    }

        //    public bool IsHandled
        //    {
        //        get { return this._isExceptionHandled.Value; }
        //        set { this._isExceptionHandled = !value; }
        //    }

        //    internal void SetException(System.Exception exception)
        //    {
        //        if (this._currentException == null && !this._hasExceptionOnThread)
        //        {
        //            // first exception
        //            // set initial value
        //            this._isExceptionHandled = false;
        //            this._hasExceptionOnThread = true;
        //            this._currentException = exception;
        //        }

        //        else if (this._currentException != null && this._hasExceptionOnThread)
        //        {
        //            // second exception
        //            // wrap old excetion into new exception


        //        }
        //        else
        //        {
        //            // throw exception with releated this library
        //        }
        //    }

        //    internal bool IsExceptionHandled()
        //    {
        //        if (this._isExceptionHandled.HasValue)
        //        {
        //            return this._isExceptionHandled.Value;
        //        }

        //        return false;
        //    }

        //    internal System.Exception GetCurrentException()
        //    {
        //        return this._currentException;
        //    }
        //}

        //internal bool IsExceptionHandled()
        //{
        //    if (this._exceptionInfo == null)
        //    {
        //        throw new NullReferenceException("No exception in your context!");
        //    }

        //    return this._exceptionInfo.IsExceptionHandled();
        //}
        /// <summary>
        /// Determines whether [is exception handled] [the specified method name].
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns><c>true</c> if [is exception handled] [the specified method name]; otherwise, <c>false</c>.</returns>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for IsExceptionHandled
        internal static bool IsExceptionHandled(string methodName)
        {
            DefineContext(methodName);
            return _currentContext.IsExceptionHandled();
        }

        /// <summary>
        /// Gets the current exception.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <returns>Exception.</returns>
        /// <remarks>Rachistructure</remarks>
        /// TODO Edit XML Comment Template for GetCurrentException
        internal static Exception GetCurrentException(string methodName)
        {
            DefineContext(methodName);
            return _currentContext.GetCurrentException();
        }
    }
}
