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

        public static bool AreEqual(Item i1, Item i2)
        {
          System.Console.WriteLine("< Name={0} SellIn={1} Quality={2}", i2.Name, i2.SellIn, i2.Quality);
          System.Console.WriteLine("> Name={0} SellIn={1} Quality={2}", i1.Name, i1.SellIn, i1.Quality);
          return i1.Name == i2.Name && i1.SellIn == i2.SellIn && i1.Quality == i2.Quality;
        }

        public static bool QualityAreEqual(Item i1, Item i2)
        {
          System.Console.WriteLine("< Name={0} Quality={1}", i2.Name, i2.Quality);
          System.Console.WriteLine("> Name={0} Quality={1}", i1.Name, i1.Quality);
          return i1.Name == i2.Name && i1.Quality == i2.Quality;
        }

        public static bool AreEquivalent(IList<Item> l1, IList<Item> l2, Func<Item,Item,bool> comparer)
        {
          var areEquivalent = true;
          for(var i=0; i<l1.Count-1; i++)
            areEquivalent = areEquivalent && comparer(l1[i], l2[i]);
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
            Assert.IsTrue( AreEquivalent(refactored.Items, reference.Items, TestAssemblyTests.AreEqual) );
        }

        [Test]
        public void Updates()
        {
           for(var u=0; u<=35; u++)
             Update(u);
        }

        [Test]
        public void ThirtyFiveUpdatesIsEnough()
        {
            System.Console.WriteLine("================ 35 updates is enough ===================");
            var updateA = new Program();
            var updateB = new Program();
            for(var i=1; i<=34; i++)
            {
              updateA.OldUpdateQuality();
              updateB.OldUpdateQuality();
            }
            updateB.OldUpdateQuality();
            Assert.IsTrue( AreEquivalent(updateA.Items, updateB.Items, TestAssemblyTests.QualityAreEqual) );
        }
    }
}
