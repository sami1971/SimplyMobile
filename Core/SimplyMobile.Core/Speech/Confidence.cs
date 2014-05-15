using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Speech
{
    public enum Confidence
    {
        // Summary:
        //     The spoken phrase was not matched to any phrase in any active grammar.
        Rejected = 0,
        //
        // Summary:
        //     The confidence level is low.
        Low = 1,
        //
        // Summary:
        //     The confidence level is medium.
        Medium = 2,
        //
        // Summary:
        //     The confidence level is high.
        High = 3,
    }
}
