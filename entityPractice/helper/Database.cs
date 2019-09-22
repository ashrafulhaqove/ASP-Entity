using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using entityPractice.Models;


namespace entityPractice.helper
{
    public class Database : IDisposable
    {
        private static DatabaseContext _context;
        static Database()
        {
            _context = new DatabaseContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        internal static DatabaseContext getContext()
        {
            //_context = new DatabaseContext();
            return _context;
        }

    }
}