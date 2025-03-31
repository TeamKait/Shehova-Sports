using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

        public static object GetIdByName<TEntity>(DbContext context, string name) where TEntity : class
        {
            var nameProperty = typeof(TEntity).GetProperty("Name");
            if (nameProperty == null)
            {
                throw new InvalidOperationException("Свойство 'Name' не найдено в типе " + typeof(TEntity).Name);
            }

            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
            MemberExpression propertyAccess = Expression.Property(parameter, nameProperty);
            ConstantExpression constant = Expression.Constant(name);
            BinaryExpression equalExpression = Expression.Equal(propertyAccess, constant);
            Expression<Func<TEntity, bool>> lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpression, parameter);

            TEntity entity = context.Set<TEntity>().FirstOrDefault(lambda);
            if (entity == null)
            {
                return null;
            }

            var idProperty = typeof(TEntity).GetProperty("Id");
            if (idProperty == null)
            {
                throw new InvalidOperationException("Свойство 'Id' не найдено в типе " + typeof(TEntity).Name);
            }

            return idProperty.GetValue(entity);
        }

    }
}