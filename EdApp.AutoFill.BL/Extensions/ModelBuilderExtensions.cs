using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EdApp.AutoFill.DAL.Contract;
using Microsoft.EntityFrameworkCore;

namespace EdApp.AutoFill.BL.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void JoinOneToMany<TPrincipalEntity, TRelatedEntity>(this ModelBuilder modelBuilder, Expression<Func<TRelatedEntity, TPrincipalEntity>> navigationPrincipalExpression, Expression<Func<TPrincipalEntity, IEnumerable<TRelatedEntity>>> navigationRelatedExpression, Expression<Func<TRelatedEntity, object>> foreignKeyExpression) where TPrincipalEntity : class, IIdentifier where TRelatedEntity : class, IIdentifier
        {
            modelBuilder.Entity<TPrincipalEntity>()
                .HasMany(navigationRelatedExpression)
                .WithOne(navigationPrincipalExpression)
                .HasPrincipalKey(principalEntity => principalEntity.Id)
                .HasForeignKey(foreignKeyExpression)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
