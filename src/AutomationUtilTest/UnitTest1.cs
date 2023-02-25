using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AutomationUtil;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace AutomationUtilTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestExtractDigit()
        {
            var case1 = Tuple.Create(
                "123+aaa[kk]908o2", 
                "{0}+aaa[kk]{1}o{2}",
                new [] { "123", "908", "2" }
                );
            var case2 = Tuple.Create(
                "00188", 
                "{0}",
                new [] { "00188" }
                );
            var case3 = Tuple.Create(
                "abcd", 
                "abcd",
                new string[] { }
                );
            var case4 = Tuple.Create(
                "", 
                "",
                new string[] { }
                );

            var testcases = new []
            {
                case1,
                case2,
                case3,
                case4
            };

            foreach(var c in testcases)
            {
                var resultTuple = TextUtil.ExtractDigits(c.Item1);
                Assert.AreEqual(c.Item2, resultTuple.Item1);
                CollectionAssert.AreEqual(c.Item3, resultTuple.Item2);

            }
        }

        [TestMethod]
        public void TestParseInt()
        {
            Assert.AreEqual(123L, Convert.ToInt32("123"));
            Assert.AreEqual(-123L, Convert.ToInt32("-123"));
            Assert.AreEqual(0L, Convert.ToInt32("-0"));
            Assert.AreEqual(0L, Convert.ToInt32("0"));
            Assert.AreEqual(0L, Convert.ToInt32("0000"));
            Assert.AreEqual(18, Convert.ToInt32("0018"));
            assertFormatException(() => { Assert.AreEqual(0, Convert.ToInt64("")); });
        }

        public delegate void TestTargetFunc();

        private void assertFormatException(TestTargetFunc func)
        {
            bool isExceptionThrownAsExpected = false;

            try
            {
                func();
            }
            catch (FormatException fe)
            {
                isExceptionThrownAsExpected = true;
            }

            Assert.IsTrue(isExceptionThrownAsExpected);
        }

        
    }
}
