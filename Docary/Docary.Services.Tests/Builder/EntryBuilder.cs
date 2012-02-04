using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;

namespace Docary.Services.Tests.Builder
{
    public class EntryBuilder
    {
        public static Entry Build(string locationName, string tagName, string userId)
        {
            return new Entry()
            {
                Location = new Location() { Name = locationName },
                Tag = new EntryTag() { Name = tagName },
                UserId = "1"
            };       
        }                
    }
}
