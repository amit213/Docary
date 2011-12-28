using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Repositories.EF
{
    public class Scope : IScope
    {
        private DocaryContext _context;

        public Scope(DocaryContext docaryContext) 
        {
            _context = docaryContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
