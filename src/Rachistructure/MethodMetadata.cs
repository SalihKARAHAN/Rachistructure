#if(!DEBUG)
using System.Diagnostics;
#endif

namespace Rachistructure
{
    /// <summary>
    /// 
    /// </summary>
#if (!DEBUG)
    [DebuggerStepThrough]
    [DebuggerDisplay("Name={Name}, Argumets={Arguments}")]
#endif
    public class MethodMetadata
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
#if (!DEBUG)
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
#if (!DEBUG)
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        public object[] Arguments { get; set; }
    }
}
