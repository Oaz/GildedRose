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
          System.Console.WriteLine("< Name={0} SellIn={1} Quality={2}", i2.Name, i2.SellIn, i2.Quality);
          System.Console.WriteLine("> Name={0} SellIn={1} Quality={2}", i1.Name, i1.SellIn, i1.Quality);
          return i1.Name == i2.Name && i1.SellIn == i2.SellIn && i1.Quality == i2.Quality;
        }

        public bool AreEquivalent(IList<Item> l1, IList<Item> l2)
        {
          var areEquivalent = true;
          for(var i=0; i<l1.Count-1; i++)
            areEquivalent = areEquivalent && AreEqual(l1[i], l2[i]);
          return areEquivalent;
        }

        public void Update(int step)
        {
            System.Console.WriteLine("================ {0} ===================", step);
            var reference = new Program();
            var refactored = new Program();
            while(step-->0)
            {
              reference.OldUpdateQuality();
              refactored.NewUpdateQuality();
            }
            Assert.IsTrue( AreEquivalent(refactored.Items, reference.Items) );
        }

        [Test]
        public void Updates()
        {
           for(var u=0; u<=16; u++)
             Update(u);
        }
    }
}
