using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sports.Classes
{
    public static class DBHelper
    {
        public static T GetNameById<T>(T source, int id) where T : IDbSet<T>
        {
            return source.Find(id);
        }
    }
}
