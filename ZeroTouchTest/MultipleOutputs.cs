using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;

namespace ZeroTouchNodes
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

        private static float LevenshteinAccuracy(int len, int distance)
        {
            return ( 1 - ( distance / len ) );
        }

        [MultiReturn(new[] { "Result", "Likeness"})]
        /// <summary>
        /// This node checks how similar one string is to another and returns True if the result is above the threshold. Calculations are based on Levenshtein distance.
        /// </summary>
        /// <param name="ReferenceText">
        /// Any piece of text that will serve as a comparing reference.
        /// </param>
        /// <param name="CompareTo">
        /// Piece of text against which the reference text will be compared.
        /// </param>
        /// <param name="Threshold">
        /// Value between 1 and 0 - how much should both pieces of text be similar to be considered the same.
        /// </param>
        public static Dictionary<string, object> StringAlmostEqual(string ReferenceText, string CompareTo, bool CaseSensitive, float Threshold)
        {
            if (string.IsNullOrEmpty(CompareTo))
            {
                return new Dictionary<string, object>
                {
                    { "Result", false },
                    { "Likeness", 0.0}
                };
            }

            if (string.IsNullOrEmpty(ReferenceText))
            {
                return new Dictionary<string, object>
                {
                    { "Result", false },
                    { "Likeness", 0.0}
                };
            }

            if (ReferenceText == CompareTo)
            {
                return new Dictionary<string, object>
                {
                    { "Result", true },
                    { "Likeness", 1.0}
                };
            }

            if (!CaseSensitive)
            {
                ReferenceText = ReferenceText.ToLowerInvariant();
                CompareTo = CompareTo.ToLowerInvariant();
            }

            int[,] dp = new int[ReferenceText.Length + 1, CompareTo.Length + 1];

            for (int i = 0; i <= ReferenceText.Length; i++)
            {
                dp[i, 0] = i;
            }

            for (int j = 0; j <= CompareTo.Length; j++)
            {
                dp[0, j] = j;
            }

            for (int i = 1; i <= ReferenceText.Length; i++)
            {
                for (int j = 1; j <= CompareTo.Length; j++)
                {
                    int cost = (ReferenceText[i - 1] == CompareTo[j - 1]) ? 0 : 1;

                    dp[i, j] = Math.Min(
                        Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + cost
                    );
                }
            }

            int distance = dp[ReferenceText.Length, CompareTo.Length];
            float likeness = LevenshteinAccuracy(ReferenceText.Length, distance);
            bool result = likeness > Threshold ? true : false;

            return new Dictionary<string, object>
            {
                { "Result", result },
                { "Likeness", likeness }
            };
        }

        private MultipleOutputs() 
        { 

        }

    }
}
