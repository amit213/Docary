using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Repositories.EF
{
    public abstract class RepositoryBase
    {
        private DocaryContext _context;

        public RepositoryBase(DocaryContext context)
        {
            _context = context;
        }

        protected DocaryContext Context
        {
            get
            {
                return _context;
            }
        }
    }
}
