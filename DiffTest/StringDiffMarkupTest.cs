using System;
using NUnit.Framework;
using DiffWBias;
using System.Collections.Generic;

namespace DiffTest
{
    [TestFixture]
    public class StringDiffMarkupTest
    {
        [Test]
        public void TalkShowHostVsTalkingSnakeHost()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            CorrectionSegment[] parts = metric.GetRightWrongParts("talk show host", "talking snake host");
            Assert.AreEqual(5, parts.Length);
            Assert.IsTrue(parts[0].correct);
            Assert.AreEqual(parts[0].actualText, "talk");

            Assert.IsFalse(parts[1].correct);
            Assert.AreEqual(parts[1].actualText, "ing");
            Assert.AreEqual(parts[1].expectedText, "");

            Assert.IsTrue(parts[2].correct);
            Assert.AreEqual(parts[2].actualText, " s");

            Assert.IsFalse(parts[3].correct);
            Assert.AreEqual(parts[3].actualText, "nake");
            Assert.AreEqual(parts[3].expectedText, "how");

            Assert.IsTrue(parts[4].correct);
            Assert.AreEqual(parts[4].actualText, " host");
        }

        [Test]
        public void RunVsRan()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            CorrectionSegment[] parts = metric.GetRightWrongParts("run", "ran");
            Assert.AreEqual(3, parts.Length);
            Assert.IsTrue(parts[0].correct);
            Assert.AreEqual(parts[0].actualText, "r");

            Assert.IsFalse(parts[1].correct);
            Assert.AreEqual(parts[1].actualText, "a");
            Assert.AreEqual(parts[1].expectedText, "u");

            Assert.IsTrue(parts[2].correct);
            Assert.AreEqual(parts[2].actualText, "n");
        }

        [Test]
        public void ExtraStartActual_RanVsSrand()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            CorrectionSegment[] parts = metric.GetRightWrongParts("ran", "srand");
            Assert.AreEqual(3, parts.Length);

            Assert.IsFalse(parts[0].correct);
            Assert.AreEqual(parts[0].actualText, "s");
            Assert.AreEqual(parts[0].expectedText, "");

            Assert.IsTrue(parts[1].correct);
            Assert.AreEqual(parts[1].actualText, "ran");

            Assert.IsFalse(parts[2].correct);
            Assert.AreEqual(parts[2].actualText, "d");
            Assert.AreEqual(parts[2].expectedText, "");
        }

        [Test]
        public void ExtraStartExpected_SandVsAnd()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            CorrectionSegment[] parts = metric.GetRightWrongParts("sand", "and");
            Assert.AreEqual(2, parts.Length);

            Assert.IsFalse(parts[0].correct);
            Assert.AreEqual(parts[0].actualText, "");
            Assert.AreEqual(parts[0].expectedText, "s");

            Assert.IsTrue(parts[1].correct);
            Assert.AreEqual(parts[1].actualText, "and");
        }

        [Test]
        public void ExtraEndActual_RunVsRunning()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            CorrectionSegment[] parts = metric.GetRightWrongParts("run", "running");
            Assert.AreEqual(2, parts.Length);

            Assert.IsTrue(parts[0].correct);
            Assert.AreEqual(parts[0].actualText, "run");

            Assert.IsFalse(parts[1].correct);
            Assert.AreEqual(parts[1].actualText, "ning");
            Assert.AreEqual(parts[1].expectedText, "");
        }

        [Test]
        public void ExtraEndExpected_RunningVsRun()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            CorrectionSegment[] parts = metric.GetRightWrongParts("running", "run");
            Assert.AreEqual(2, parts.Length);

            Assert.IsTrue(parts[0].correct);
            Assert.AreEqual("run", parts[0].actualText);

            Assert.IsFalse(parts[1].correct);
            Assert.AreEqual("", parts[1].actualText);
            Assert.AreEqual("ning", parts[1].expectedText);
        }

        [Test]
        public void ExtraEndExpected_RunningsVsRunning()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            CorrectionSegment[] parts = metric.GetRightWrongParts("runnings", "running");
            Assert.AreEqual(2, parts.Length);

            Assert.IsTrue(parts[0].correct);
            Assert.AreEqual("running", parts[0].actualText);

            Assert.IsFalse(parts[1].correct);
            Assert.AreEqual("", parts[1].actualText);
            Assert.AreEqual("s", parts[1].expectedText);
        }

        [Test]
        public void RunVsRuun()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            CorrectionSegment[] parts = metric.GetRightWrongParts("run", "ruun");
            Assert.AreEqual(3, parts.Length);
            Assert.IsTrue(parts[0].correct);
            Assert.AreEqual(parts[0].actualText, "ru");

            Assert.IsFalse(parts[1].correct);
            Assert.AreEqual(parts[1].actualText, "u");
            Assert.AreEqual(parts[1].expectedText, "");

            Assert.IsTrue(parts[2].correct);
            Assert.AreEqual(parts[2].actualText, "n");
        }

        [Test]
        public void RunVsRunu()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            CorrectionSegment[] parts = metric.GetRightWrongParts("run", "runu");
            Assert.AreEqual(2, parts.Length);
            Assert.IsTrue(parts[0].correct);
            Assert.AreEqual(parts[0].actualText, "run");

            Assert.IsFalse(parts[1].correct);
            Assert.AreEqual(parts[1].actualText, "u");
            Assert.AreEqual(parts[1].expectedText, "");
        }

        [Test]
        public void RunVsRnu()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            CorrectionSegment[] parts = metric.GetRightWrongParts("run", "rnu");
            string s = metric.GetComparativeLines("run", "rnu");
            Assert.AreEqual(4, parts.Length);
            Assert.IsTrue(parts[0].correct);
            Assert.AreEqual(parts[0].actualText, "r");

            Assert.IsFalse(parts[1].correct);
            Assert.AreEqual(parts[1].actualText, "n");
            Assert.AreEqual(parts[1].expectedText, "");

            Assert.IsTrue(parts[2].correct);
            Assert.AreEqual(parts[2].actualText, "u");

            Assert.IsFalse(parts[3].correct);
            Assert.AreEqual(parts[3].actualText, "");
            Assert.AreEqual(parts[3].expectedText, "n");
        }

        [Test]
        public void RunningVsRnuning()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            CorrectionSegment[] parts = metric.GetRightWrongParts("running", "rnuning");
            string s = metric.GetComparativeLines("running", "rnuning");
            Assert.AreEqual(5, parts.Length);
            Assert.IsTrue(parts[0].correct);
            Assert.AreEqual("r", parts[0].actualText);

            Assert.IsFalse(parts[1].correct);
            Assert.AreEqual("n", parts[1].actualText);
            Assert.AreEqual("", parts[1].expectedText);

            Assert.IsTrue(parts[2].correct);
            Assert.AreEqual("un", parts[2].actualText);

            Assert.IsFalse(parts[3].correct);
            Assert.AreEqual("", parts[3].actualText);
            Assert.AreEqual("n", parts[3].expectedText);

            Assert.IsTrue(parts[4].correct);
            Assert.AreEqual("ing", parts[4].actualText);
        }

        [Test]
        public void RunningVsRnnuning()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            CorrectionSegment[] parts = metric.GetRightWrongParts("running", "rnnuning");
            string s = metric.GetComparativeLines("running", "rnuning");
            Assert.AreEqual(5, parts.Length);
            Assert.IsTrue(parts[0].correct);
            Assert.AreEqual(parts[0].actualText, "r");

            Assert.IsFalse(parts[1].correct);
            Assert.AreEqual(parts[1].actualText, "nn");
            Assert.AreEqual(parts[1].expectedText, "");

            Assert.IsTrue(parts[2].correct);
            Assert.AreEqual(parts[2].actualText, "un");

            Assert.IsFalse(parts[3].correct);
            Assert.AreEqual(parts[3].actualText, "");
            Assert.AreEqual(parts[3].expectedText, "n");

            Assert.IsTrue(parts[4].correct);
            Assert.AreEqual(parts[4].actualText, "ing");
        }

        [Ignore("Wanted to observe behavior, could've written a driver but didn't.")]
        [Test]
        public void LetterFlip()
        {
            StringDiffMarkup metric = new StringDiffMarkup();

            int[,] table;
            StringDiffMarkup.GetLCSInternal("iron dog", "iron odg", out table);
            string word = "i,r,o,n, ,o,d,g";
            int[] last = new int["iron dog".Length + 1];
            for (int i = 0; i <= "iron dog".Length; i++)
            {
                last[i] = table[table.GetLength(0) - 1, i];
            }
            string numbers = string.Join(" ", last);
        }

        [Ignore("Another driver-less test of different \"edit distance\" methods")]
        [Test]
        public void LetterFlip2_DistanceMethod()
        {
            StringDiffMarkup metric = new StringDiffMarkup();

            int[,] table =
            StringDiffMarkup.getEditDistMD("iron dog", "iron odg");
            string word = "i,r,o,n, ,o,d,g";
            int[] last = new int["iron dog".Length + 1];
            for (int i = 0; i <= "iron dog".Length; i++)
            {
                last[i] = table[table.GetLength(0) - 1, i];
            }
            List<int> lastList = new List<int>(last);
            lastList.Reverse();
            string numbers = string.Join(" ", lastList);
        }

        [Ignore("Driverless test")]
        [Test]
        public void TestMethod3()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            //string output = metric.GetColorCodedCorrectionLine("talk show host", "car show");
            //output += "<br />car show";
            //string output2 = metric.GetColorCodedCorrectionLine("talk show host", "talking snake");
            //output2 += "<br />talking snake";
            string output3 = metric.GetColorCodedCorrectionLine("iron dog walker", "iron dagger watch");
            //CorrectionSegment[] parts = metric.GetRightWrongParts("talk show host", "car show");

        }


        [Test]
        public void IronDogWalker()
        {
            StringDiffMarkup metric = new StringDiffMarkup();
            string output3 = metric.GetColorCodedCorrectionLine("iron dog walker", "iron dagger watch");
            CorrectionSegment[] csArry = metric.GetRightWrongParts("iron dog walker", "iron dagger watch");
            Assert.IsTrue(csArry[0].correct);
            Assert.AreEqual("iron d", csArry[0].actualText);

            Assert.IsFalse(csArry[1].correct);
            Assert.AreEqual("a", csArry[1].actualText);
            Assert.AreEqual("o", csArry[1].expectedText);


            Assert.IsTrue(csArry[2].correct);
            Assert.AreEqual("g", csArry[2].actualText);

            Assert.IsFalse(csArry[3].correct);
            Assert.AreEqual("ger", csArry[3].actualText);
            Assert.AreEqual("", csArry[3].expectedText);

            Assert.IsTrue(csArry[4].correct);
            Assert.AreEqual(" wa", csArry[4].actualText);

            Assert.IsFalse(csArry[5].correct);
            Assert.AreEqual("tch", csArry[5].actualText);
            Assert.AreEqual("lker", csArry[5].expectedText);
        }

        [Ignore("Wanted to verify assumption that it'd match on spaces, acceptable for our use")]
        [Test]
        public void IronDogWalker2()
        {
            string expected = "We can write up a script to test this feature. . .but for now, here's some text";
            string actual = "I'll try typing some stuff here";//"We can't do anything about it.";
            StringDiffMarkup metric = new StringDiffMarkup();
            string output3 = metric.GetColorCodedCorrectionLine(expected, actual);
            string output4 = metric.GetComparativeLines(expected, actual);
            CorrectionSegment[] csArry = metric.GetRightWrongParts(expected, actual);
            //return;
        }
    }
}
