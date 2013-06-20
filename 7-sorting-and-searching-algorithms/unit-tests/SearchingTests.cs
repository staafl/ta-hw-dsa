using System;
using System.Collections.Generic;
using System.Linq;

using SortingHomework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace unit_tests
{
    [TestClass]
    public class SearchingTests
    {
        // generated with Ruby

        [TestMethod]
        public void TestLinear0NotFrom()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 0);
            int elem = TestHelpers.ElementNotFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.LinearSearch(elem);

            Assert.IsTrue(result == false);
        }
        [TestMethod]
        public void TestLinear1From()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 1);
            int elem = TestHelpers.ElementFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.LinearSearch(elem);

            Assert.IsTrue(result == true);
        }
        [TestMethod]
        public void TestLinear1NotFrom()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 1);
            int elem = TestHelpers.ElementNotFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.LinearSearch(elem);

            Assert.IsTrue(result == false);
        }
        [TestMethod]
        public void TestLinear10From()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 10);
            int elem = TestHelpers.ElementFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.LinearSearch(elem);

            Assert.IsTrue(result == true);
        }
        [TestMethod]
        public void TestLinear10NotFrom()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 10);
            int elem = TestHelpers.ElementNotFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.LinearSearch(elem);

            Assert.IsTrue(result == false);
        }
        [TestMethod]
        public void TestLinear13From()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 13);
            int elem = TestHelpers.ElementFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.LinearSearch(elem);

            Assert.IsTrue(result == true);
        }
        [TestMethod]
        public void TestLinear13NotFrom()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 13);
            int elem = TestHelpers.ElementNotFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.LinearSearch(elem);

            Assert.IsTrue(result == false);
        }
        [TestMethod]
        public void TestLinear777From()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 777);
            int elem = TestHelpers.ElementFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.LinearSearch(elem);

            Assert.IsTrue(result == true);
        }
        [TestMethod]
        public void TestLinear777NotFrom()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 777);
            int elem = TestHelpers.ElementNotFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.LinearSearch(elem);

            Assert.IsTrue(result == false);
        }
        [TestMethod]
        public void TestLinear1000From()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 1000);
            int elem = TestHelpers.ElementFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.LinearSearch(elem);

            Assert.IsTrue(result == true);
        }
        [TestMethod]
        public void TestLinear1000NotFrom()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Random, 1000);
            int elem = TestHelpers.ElementNotFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.LinearSearch(elem);

            Assert.IsTrue(result == false);
        }
        [TestMethod]
        public void TestBinary0NotFrom()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 0);
            int elem = TestHelpers.ElementNotFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.BinarySearch(elem);

            Assert.IsTrue(result == false);
        }
        [TestMethod]
        public void TestBinary1From()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 1);
            int elem = TestHelpers.ElementFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.BinarySearch(elem);

            Assert.IsTrue(result == true);
        }
        [TestMethod]
        public void TestBinary1NotFrom()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 1);
            int elem = TestHelpers.ElementNotFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.BinarySearch(elem);

            Assert.IsTrue(result == false);
        }
        [TestMethod]
        public void TestBinary10From()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 10);
            int elem = TestHelpers.ElementFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.BinarySearch(elem);

            Assert.IsTrue(result == true);
        }
        [TestMethod]
        public void TestBinary10NotFrom()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 10);
            int elem = TestHelpers.ElementNotFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.BinarySearch(elem);

            Assert.IsTrue(result == false);
        }
        [TestMethod]
        public void TestBinary13From()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 13);
            int elem = TestHelpers.ElementFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.BinarySearch(elem);

            Assert.IsTrue(result == true);
        }
        [TestMethod]
        public void TestBinary13NotFrom()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 13);
            int elem = TestHelpers.ElementNotFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.BinarySearch(elem);

            Assert.IsTrue(result == false);
        }
        [TestMethod]
        public void TestBinary777From()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 777);
            int elem = TestHelpers.ElementFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.BinarySearch(elem);

            Assert.IsTrue(result == true);
        }
        [TestMethod]
        public void TestBinary777NotFrom()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 777);
            int elem = TestHelpers.ElementNotFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.BinarySearch(elem);

            Assert.IsTrue(result == false);
        }
        [TestMethod]
        public void TestBinary1000From()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 1000);
            int elem = TestHelpers.ElementFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.BinarySearch(elem);

            Assert.IsTrue(result == true);
        }
        [TestMethod]
        public void TestBinary1000NotFrom()
        {
            int[] array = TestHelpers.GenerateArray<int>(ArrayOptions.Sorted, 1000);
            int elem = TestHelpers.ElementNotFrom(array);
            var collection = new SortableCollection<int>(array.ToList());

            bool result = collection.BinarySearch(elem);

            Assert.IsTrue(result == false);
        }

    }
}
