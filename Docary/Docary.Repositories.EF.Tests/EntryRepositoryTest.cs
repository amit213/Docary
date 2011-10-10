using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Docary.Models;
using System.Linq;

using Docary.Repositories;
using Docary.Repositories.EF;
using Docary.Repositories.EF.Tests;

namespace Docary.Repositories
{
    [TestClass()]
    public class EntryRepositoryTest
    {
        [TestMethod()]
        public void Test_GetEntries_Returns_More_Than_Zero_Results()
        {
            var target = new EntryRepository(new DocaryContextStub());

            var actual = target.GetEntries().ToList();
            var actualContainsResults = actual.Count > 0;

            Assert.IsTrue(actualContainsResults);
        }
    }
}
