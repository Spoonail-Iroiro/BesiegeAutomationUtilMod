using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using AutomationUtil;

namespace AutomationUtilTest
{
    [TestClass]
    public class MessageEditorTest
    {
        [TestMethod]
        public void TestGetNewMessage()
        {
            var testcases = new[]
            {
                Tuple.Create(
                    new[] { "up1", "up2", "z", "1up3" }, new int[] { 0 }, 1,
                    new[] {"up2", "up3", "z", "1up4"}
                ),
                Tuple.Create(
                    new[] { "001up2y3", "1up3", "y", "a2" }, new int[] { 1, 0 }, 1,
                    new[] { "001up3y4", "2up4", "y", "a3" }                
                ),
                Tuple.Create(
                    new[] { "0up0", "1up1", "2up2" }, new int[] { 0 }, -1,
                    new[] { "0up99", "1up0", "2up1" }
                ),
                Tuple.Create(
                    new[] { "99up0", "98up1", "97up2" }, new int[] { 1 }, 1,
                    new[] { "0up0", "99up1", "98up2" }
                )
            };

            var editor = new MessageEditor();

            foreach (var tc in testcases)
            {
                var result = editor.GetNewMessage(tc.Item1, tc.Item2, tc.Item3);
                CollectionAssert.AreEqual(tc.Item4, result.ToArray());
            }
        }
    }
}
