using Docary.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Docary.Repositories;
using Docary.Models;

namespace Docary.Services.Tests
{  
    [TestClass()]
    public class EntryServiceTest
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_AddEntry_Throws_ArgumentNullException_On_Null_Entry()
        {            
            var target = new EntryService(null, null, null); 

            target.AddEntry(null);           
        }

        [TestMethod]
        public void Test_AddEntry_Can_Resolve_Location_By_Name() 
        {
            var target = new EntryService(null, null, null);

            target.AddEntry(null);
        }
    }
}
