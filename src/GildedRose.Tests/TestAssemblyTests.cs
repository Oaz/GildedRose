using NUnit.Framework;
using GildedRose.Console;
﻿using System.Collections.Generic;

namespace GildedRose.Tests
{
    [TestFixture]
    public class TestAssemblyTests
    {
        [Test]
        public void TestTheTruth()
        {
            Assert.IsTrue(true);
        }

        public bool AreEqual(Item i1, Item i2)
        {
          return i1.Name == i2.Name && i1.SellIn == i2.SellIn && i1.Quality == i2.Quality;
        }

        public bool AreEquivalent(IList<Item> l1, IList<Item> l2)
        {
          var areEquivalent = true;
          for(var i=0; i<l1.Count; i++)
            areEquivalent = areEquivalent && AreEqual(l1[i], l2[i]);
          return areEquivalent;
        }

        [Test]
        public void Update0()
        {
            var reference = new Program();
            var refactored = new Program();
            Assert.IsTrue( AreEqual(refactored.Items[0], reference.Items[0]) );
            Assert.IsFalse( AreEqual(refactored.Items[0], reference.Items[1]) );
            Assert.IsTrue( AreEquivalent(refactored.Items, reference.Items) );
        }
    }
}
