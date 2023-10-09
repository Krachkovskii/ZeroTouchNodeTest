using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using Autodesk.Revit.DB;

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

        private static double LevenshteinAccuracy(int len, int distance)
        {
            return ( 1.0D - ( (double)distance / len ) );
        }

        [MultiReturn(new[] { "Result", "Similarity", "Identical"})]
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
        public static Dictionary<string, object> StringAlmostEqual(string ReferenceText, string CompareTo, bool CaseSensitive = true, double Threshold = 0.9D)
        {
            if (string.IsNullOrEmpty(CompareTo))
            {
                return new Dictionary<string, object>
                {
                    { "Result", false },
                    { "Similarity", 0.0},
                    { "Identical", false }
                };
            }

            if (string.IsNullOrEmpty(ReferenceText))
            {
                return new Dictionary<string, object>
                {
                    { "Result", false },
                    { "Similarity", 0.0},
                    { "Identical", false }
                };
            }

            if (ReferenceText == CompareTo)
            {
                return new Dictionary<string, object>
                {
                    { "Result", true },
                    { "Similarity", 1.0},
                    { "Identical", true }
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
            double Similarity = LevenshteinAccuracy(ReferenceText.Length, distance);
            bool result = Similarity > Threshold ? true : false;

            return new Dictionary<string, object>
            {
                { "Result", result },
                { "Similarity", Similarity },
                { "Identical", false }
            };
        }

        [MultiReturn(new[] { "Elements", "Family Types" })]
        public static Dictionary<string, object> LinkedElementsByCategory(Document LinkedDoc, BuiltInCategory Category)
        {
            FilteredElementCollector elFilter = new FilteredElementCollector(LinkedDoc);
            elFilter.OfCategory(Category);
            elFilter.WhereElementIsNotElementType();
            List<Element> elements = new List<Element>();
            foreach (FamilyInstance fi in elFilter)
            {
                elements.Add(fi);
            }

            FilteredElementCollector typeFilter = new FilteredElementCollector(LinkedDoc);
            typeFilter.OfCategory(Category);
            typeFilter.WhereElementIsElementType();
            List<Element> symbols = new List<Element>();
            foreach (FamilySymbol symbol in typeFilter)
            {
                symbols.Add(symbol);
            }

            return new Dictionary<string, object>
            {
                { "Elements", elements },
                { "Types", symbols }
            };
        }

        private MultipleOutputs() 
        { 

        }

    }
}
