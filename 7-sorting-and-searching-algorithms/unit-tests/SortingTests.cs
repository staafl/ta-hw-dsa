using System;
using System.Collections.Generic;
using System.Linq;

using SortingHomework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace unit_tests
{
    
    [TestClass]
    public class SortingTests
    {
        // generated with Ruby

        [TestMethod]
        public void TestSelection0()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 0);
            var list = array.ToList();

            new SelectionSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestSelection1()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 1);
            var list = array.ToList();

            new SelectionSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestSelectionRandom1000()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 1000);
            var list = array.ToList();

            new SelectionSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestSelectionSorted1000()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 1000);
            var list = array.ToList();

            new SelectionSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestSelectionReversed1000()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Reversed, 1000);
            var list = array.ToList();

            new SelectionSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestQuick0()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 0);
            var list = array.ToList();

            new QuickSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestQuick1()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 1);
            var list = array.ToList();

            new QuickSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestQuickRandom1000()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 1000);
            var list = array.ToList();

            new QuickSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestQuickSorted1000()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 1000);
            var list = array.ToList();

            new QuickSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestQuickReversed1000()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Reversed, 1000);
            var list = array.ToList();

            new QuickSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestMerge0()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 0);
            var list = array.ToList();

            new MergeSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestMerge1()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 1);
            var list = array.ToList();

            new MergeSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestMergeRandom1000()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 1000);
            var list = array.ToList();

            new MergeSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestMergeSorted1000()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 1000);
            var list = array.ToList();

            new MergeSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }
        [TestMethod]
        public void TestMergeReversed1000()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Reversed, 1000);
            var list = array.ToList();

            new MergeSorter<int>().Sort(list);

            Assert.IsTrue(list.IsSorted());
            Assert.IsTrue(list.SameContents(array));
        }

    }
}
