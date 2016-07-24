using System;
using System.Collections.Generic;

namespace Rachistructure.Registrations
{
    internal class BaseRegistration
    {
        internal string Name { get; set; }
        internal Type Type { get; set; }
        internal SortedList<string, Argument> Arguments { get; set; }

        internal BaseRegistration()
        {
            this.Arguments = new SortedList<string, Argument>();
        }


    }
}