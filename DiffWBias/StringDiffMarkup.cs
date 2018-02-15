using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffWBias
{
    public class StringDiffMarkup
    {
        public CorrectionSegment[] GetRightWrongParts(string expected, string actual)
        {
            int[,] table;
            GetLCSInternal(expected, actual, out table);
            List<CorrectionSegment> rtn;
            if (table == null)
                rtn = new List<CorrectionSegment>(1) { new CorrectionSegment(expected, actual) };
            else
                rtn = DisectTable2(table, expected, actual);
            return rtn.ToArray();
        }

        private List<CorrectionSegment> DisectTable2(int[,] table, string expected, string actual)
        {
            List<CorrectionSegment> rtn = new List<CorrectionSegment>();
            CorrectionSegment lastCS = null;
            int val = table[expected.Length, actual.Length];
            int x = actual.Length, y = expected.Length;
            int i = y + 1;
            int j = x + 1;
            while(--i > 0)
            {
                while(i > 0 && --j > 0)
                {
                    if (table[i - 1, j] == table[i, j])
                    {
                        i--;
                        j++;
                        continue;
                    }   
                    else if (table[i, j - 1] == table[i, j])
                        continue;
                    else if (table[i - 1, j - 1] + 1 == table[i, j])
                    {
                        char eChar = expected[i - 1];
                        char aChar = actual[j - 1];

                        if (i < y - 1 && expected[i] == eChar && (i > 1 && j > 1 && table[i - 2, j - 2] + 2 != table[i, j])) i++;

                        if (lastCS == null) { y++; x++; }
                        if (i == y - 1 && j == x - 1)
                        {
                            if (lastCS == null)
                            {
                                lastCS = new CorrectionSegment(expected[i - 1].ToString());
                                rtn.Add(lastCS);
                            }
                            else if (lastCS.correct)
                            {
                                lastCS.SetExpectedText(expected[i - 1] + lastCS.actualText);
                            }
                        }
                        else
                        {
                            string expSub = expected.Substring(i, y - i - 1);                            
                            string actSub = actual.Substring(j, x - j - 1);
                            rtn.Add(new CorrectionSegment(expSub, actSub));
                            if (i > 0 && j > 0)
                            {
                                lastCS = new CorrectionSegment(expected[i - 1].ToString());
                                rtn.Add(lastCS);
                            }
                        }
                        y = i;
                        x = j;
                        i--;
                        continue;
                    }
                }
            }

            if (x > 1 || y > 1)
            {
                string expSub = expected.Substring(0, y - 1);
                string actSub = actual.Substring(0, x - 1);
                rtn.Add(new CorrectionSegment(expSub, actSub));
            }

            rtn.Reverse();

            return rtn;
        }

        public string GetComparativeLines(string expected, string actual)
        {
            CorrectionSegment[] parts = GetRightWrongParts(expected, actual);
            string good = string.Empty;
            string bad = string.Empty;

            foreach (var item in parts)
            {
                string goodword = item.correct ? item.actualText : item.expectedText;
                string badword = item.actualText;
                int maxLength = Math.Max(goodword.Length, badword.Length);

                good += goodword.PadRight(maxLength + 1);
                bad += badword.PadRight(maxLength + 1);

            }
            return "Good: " + good + Environment.NewLine + "Bad:  " + bad;
        }

        public string GetColorCodedCorrectionLine(string expected, string actual)
        {
            CorrectionSegment[] parts = GetRightWrongParts(expected, actual);

            string good = "";
            string bad = "";
            foreach (var item in parts)
            {
                if (item.correct)
                {
                    good += item.actualText;
                    bad += item.actualText;
                }
                else
                {
                    int maxLength = Math.Max(item.expectedText.Length, item.actualText.Length);

                    bad += ("<span style = \"color:red\" >" + item.actualText.PadRight(maxLength, '`').Replace("`", "&nbsp;") + "</span>");
                    good +=("<span style = \"color:green\" >" + item.expectedText.PadRight(maxLength, '`').Replace("`", "&nbsp;") + "</span>");
                }
            }
            return good + "<br />" + bad;
        }


        public static int GetLCSInternal(string str1, string str2, out int[,] matrix)
        {
            matrix = null;

            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return 0;
            }

            int[,] table = new int[str1.Length + 1, str2.Length + 1];
            for (int i = 0; i < table.GetLength(0); i++)
            {
                table[i, 0] = 0;
            }
            for (int j = 0; j < table.GetLength(1); j++)
            {
                table[0, j] = 0;
            }

            for (int i = 1; i < table.GetLength(0); i++)
            {
                for (int j = 1; j < table.GetLength(1); j++)
                {
                    if (str1[i - 1] == str2[j - 1])
                        table[i, j] = table[i - 1, j - 1] + 1;
                    else
                    {
                        if (table[i, j - 1] > table[i - 1, j])
                            table[i, j] = table[i, j - 1];
                        else
                            table[i, j] = table[i - 1, j];
                    }
                }
            }

            matrix = table;
            return table[str1.Length, str2.Length];
        }

        internal static int GetNumberOfCorrectChars(string text1, string text2)
        {
            //return text1.Length - editDistDP(text1, text2);
            int[,] table;
            return GetLCSInternal(text1, text2, out table);
        }

        static int min(int x, int y, int z)
        {
            return Math.Min(Math.Min(x, y), z);
        }

        public static int[,] getEditDistMD(string str1, string str2)
        {
            // Create a table to store results of subproblems
            int[,] dp = new int[str1.Length + 1, str2.Length + 1];

            // Fill d[][] in bottom up manner
            for (int i = 0; i <= str1.Length; i++)
            {
                for (int j = 0; j <= str2.Length; j++)
                {
                    // If first string is empty, only option is to
                    // isnert all characters of second string
                    if (i == 0)
                        dp[i,j] = j;  // Min. operations = j

                    // If second string is empty, only option is to
                    // remove all characters of second string
                    else if (j == 0)
                        dp[i,j] = i; // Min. operations = i

                    // If last characters are same, ignore last char
                    // and recur for remaining string
                    else if (str1[i - 1] == str2[j - 1])
                        dp[i,j] = dp[i - 1,j - 1];

                    // If last character are different, consider all
                    // possibilities and find minimum
                    else
                        dp[i,j] = 1 + min(dp[i,j - 1],  // Insert
                                           dp[i - 1,j],  // Remove
                                           dp[i - 1,j - 1]); // Replace
                }
            }

            return dp;
        }

        static int editDistDP(string str1, string str2)
        {
            // Create a table to store results of subproblems
            int[][] dp = new int[str1.Length + 1][];
            for (int i = 0; i < str1.Length + 1; i++)
            {
                dp[i] = new int[str2.Length + 1];
            }

            // Fill d[][] in bottom up manner
            for (int i = 0; i <= str1.Length; i++)
            {
                for (int j = 0; j <= str2.Length; j++)
                {
                    // If first string is empty, only option is to
                    // isnert all characters of second string
                    if (i == 0)
                        dp[i][j] = j;  // Min. operations = j

                    // If second string is empty, only option is to
                    // remove all characters of second string
                    else if (j == 0)
                        dp[i][j] = i; // Min. operations = i

                    // If last characters are same, ignore last char
                    // and recur for remaining string
                    else if (str1[i - 1] == str2[j - 1])
                        dp[i][j] = dp[i - 1][j - 1];

                    // If last character are different, consider all
                    // possibilities and find minimum
                    else
                        dp[i][j] = 1 + min(dp[i][j - 1],  // Insert
                                           dp[i - 1][j],  // Remove
                                           dp[i - 1][j - 1]); // Replace
                }
            }

            return dp[str1.Length][str2.Length];
        }
    }

    public class CorrectionSegment
    {
        public string actualText { get; private set; }
        public string expectedText { get; private set; }
        public bool correct;

        public CorrectionSegment(string v)
        {
            this.correct = true;
            this.actualText = expectedText = v;
        }

        public void SetExpectedText(string newExp)
        {
            if (!correct)
                throw new ArgumentException("Current implementation only sets \"Correct\" entries");
            actualText = expectedText = newExp;
        }

        public CorrectionSegment(string expected, string actual)
        {
            this.correct = false;
            this.actualText = actual;
            this.expectedText = expected;
        }
    }
}
