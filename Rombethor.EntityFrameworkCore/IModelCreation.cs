using Microsoft.EntityFrameworkCore;
using System;

namespace Rombethor.EntityFrameworkCore
{
    public interface IModelCreation
    {
        /// <summary>
        /// For use with DbContext.UseSelfDefiningModels to define rules for the entity model within the class definition.
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <see cref="EntityFrameworkCoreExtensions.UseSelfDefiningModels(DbContext, ModelBuilder)"/>
        public void OnModelCreating(ModelBuilder modelBuilder);
    }
}
