using Docary.MvcExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Docary.MvcExtensions.Test
{  
    [TestClass()]
    public class LevenshteinTest
    {
        [TestMethod()]
        public void Test_CalculateDistance_With_Two_Empty_String()
        {           
            Assert.AreEqual(0, Levenshtein.CalculateDistance(string.Empty, string.Empty));
        }   

        [TestMethod()]
        public void Test_CalculateDistance_With_Empty_First_String()
        {         
            Assert.AreEqual(6, Levenshtein.CalculateDistance(string.Empty, "kitten"));
        }

        [TestMethod()]
        public void Test_CalculateDistance_With_Empty_Second_String()
        {           
            Assert.AreEqual(6, Levenshtein.CalculateDistance("kitten", string.Empty));
        }

        [TestMethod()]
        public void Test_CalculateDistance_With_Missing_Characters()
        {           
            Assert.AreEqual(2, Levenshtein.CalculateDistance("kitten", "kitt"));
        }

        [TestMethod()]
        public void Test_CalculateDistance_With_Wrong_Characters()
        {           
            Assert.AreEqual(1, Levenshtein.CalculateDistance("kitten", "kittyn"));
        }

        [TestMethod()]
        public void Test_CalculateDistance_With_Too_Much_Characters()
        {           
            Assert.AreEqual(5, Levenshtein.CalculateDistance("kitten", "kittenkitty"));
        }

        [TestMethod()]
        public void Test_CalculateDistance_With_Equal_Strings()
        {          
            Assert.AreEqual(0, Levenshtein.CalculateDistance("kitten", "kitten"));
        }
    }
}
