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
        private static int multResult { get; set; }
        private static float divResult { get; set; }

        [MultiReturn(new[] { "Multiplication", "Division" })]
        public static Dictionary<string, object> SomeNode(int num1, int num2)
        {
            multResult = num1 * num2;
            divResult = (float)num1 / (float)num2;

            return new Dictionary<string, object> 
            {
                { "Multiplication", multResult },
                { "Division", divResult }
            };
        }

        private MultipleOutputs() 
        { 

        }

    }
}
