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
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
    public class MethodMetadata
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public object[] Arguments { get; set; }
    }
}
