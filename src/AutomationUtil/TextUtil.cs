using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AutomationUtil
{
    public struct DigitSplitResult
    {
        string substring;
        bool isDigit;
    }

    public class FormatStringMatchEvaluator
    {
        private int _i = 0;

        public string Evaluate(Match match)
        {
            var rtn = $"{{{_i}}}";
            ++_i;
            return rtn;

        }
    }

    public class TextUtil
    {
        public static Tuple<string, string[]> ExtractDigits(string text)
        {

            var evaluator = new MatchEvaluator(new FormatStringMatchEvaluator().Evaluate);

            var exp = @"\d+";

            var formatString = Regex.Replace(text, exp, evaluator);
            var digitStrs = Regex.Matches(text, exp).Cast<Match>().Select(m => m.Value).ToArray();

            return Tuple.Create(formatString, digitStrs);
        }
    }
}
