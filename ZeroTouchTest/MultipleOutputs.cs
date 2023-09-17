using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroTouchTest
{
    public class MultipleOutputs
    {
        private int multResult { get; set; }
        private float divResult { get; set; }
        public static MultipleOutputs SomeNode(int num1, int num2)
        {
            MultipleOutputs multipleOutputs = new MultipleOutputs();

            multipleOutputs.multResult = num1 * num2;
            multipleOutputs.divResult = num1 / num2;

            return multipleOutputs;
        }

        private MultipleOutputs() 
        { 

        }

    }
}
