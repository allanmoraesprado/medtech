using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Tools
{
    public class Helpers
    {
        public static int TraceLineMessage([System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            return sourceLineNumber;
        }
    }
}
