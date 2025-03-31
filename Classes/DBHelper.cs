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
        public static string GetNameById<TEntity>(DbContext context, object id) where TEntity : class
        {
            TEntity entity = context.Set<TEntity>().Find(id);
            if (entity == null)
            {
                return null;
            }

            var property = entity.GetType().GetProperty("Name");
            if (property == null)
            {
                throw new InvalidOperationException("Свойство 'Name' не найдено в типе " + typeof(TEntity).Name);
            }

            return property.GetValue(entity)?.ToString();
        }

    }
}
