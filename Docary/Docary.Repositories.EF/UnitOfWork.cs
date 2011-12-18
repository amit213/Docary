using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Repositories.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private DocaryContext _context;

        public UnitOfWork(DocaryContext docaryContext) 
        {
            _context = docaryContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
