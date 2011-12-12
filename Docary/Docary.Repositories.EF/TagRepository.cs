using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public class TagRepository : RepositoryBase<EntryTag>, ITagRepository
    {  
        public TagRepository(DocaryContext context) : base(context){ }       

        public EntryTag Find(string name, string userId)
        {
            return Context.Tags.Where(a => a.Name == name && a.UserId == userId).FirstOrDefault();
        }
    }
}
