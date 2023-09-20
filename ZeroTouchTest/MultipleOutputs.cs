using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;

namespace ZeroTouchTest
{
    public class MultipleOutputs
    {

        [MultiReturn(new[] { "Nearest", "Even", "Floor", "Ceiling" })]
        public static Dictionary<string, object> RoundNumber(float num)
        {
            return new Dictionary<string, object> 
            {
                { "Nearest", Math.Round(num) },
                { "Even", Math.Round(num, 0, MidpointRounding.ToEven) },
                { "Floor", Math.Floor(num) },
                { "Ceiling", Math.Ceiling(num) }
            };
        }

        private MultipleOutputs() 
        { 

        }

    }
}
