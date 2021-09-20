using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rombethor.EntityFrameworkCore
{
    public static class EntityFrameworkCoreExtensions
    {
        /// <summary>
        /// Checks through all DbSets defined in the Data Context 'dbContext' and runs the method OnModelCreating for each model
        /// pertaining to the interface IModelCreation
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="modelBuilder"></param>
        public static void UseSelfDefiningModels(this DbContext dbContext, ModelBuilder modelBuilder)
        {
            Type dbContextType = dbContext.GetType();
            foreach (var prop in dbContextType.GetProperties())
            {
                var propType = prop.PropertyType;
                if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(DbSet<>))
                {
                    var itemType = propType.GenericTypeArguments[0];//.GetGenericTypeDefinition().GetGenericArguments()[0];
                    var method = itemType.GetMethod("OnModelCreating");
                    if (typeof(IModelCreation).IsAssignableFrom(itemType) && method != null)
                    {
                        //If the "OnModelCreating(ModelBuilder modelBuilder)" method exists, run it with the given ModelBuilder
                        method.Invoke(Activator.CreateInstance(itemType, false), new object[] { modelBuilder });
                    }
                }
            }
        }

    }
}
