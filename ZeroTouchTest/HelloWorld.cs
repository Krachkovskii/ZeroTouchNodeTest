using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ZeroTouchNodes
{
    public class NumberFuckery
    {
        private NumberFuckery()
        {

        }

        public double MultiplyTwoNumbers(int num1, int num2)
        {
            return num1 * num2;
        }

        public double DivideTwoNumbers(int num1, int num2)
        {
            return (num1 / num2);
        }
    }

    public class StringFuckery
    {
        private string _InputString
        {
            get { return _InputString; }
        }
        private StringFuckery()
        {

        }

        public string AddFuck(string start)
        {
            return start + " Fuck";
        }

        public string RemoveFuck(string start)
        {
            return start.Replace("Fuck", "");
        }
    }
}
