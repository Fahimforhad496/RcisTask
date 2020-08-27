﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DLL.Models;
using DLL.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DLL.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext
    <
        AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>
    >

    

    {
        private const string IsDeletedProperty = "IsDeleted";

        private static readonly MethodInfo _propertyMethod =
            typeof(EF).GetMethod(nameof(EF.Property),BindingFlags.Static | BindingFlags.Public)
                ?.MakeGenericMethod(typeof(bool));

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        private static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            var parm = Expression.Parameter(type, "it");
            var prop = Expression.Call(_propertyMethod, parm, Expression.Constant(IsDeletedProperty));
            var condition = Expression.MakeBinary(ExpressionType.Equal, prop, Expression.Constant(false));
            var lambda = Expression.Lambda(condition, parm);
            return lambda;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entity.ClrType) == true)
                {
                    entity.AddProperty(IsDeletedProperty, typeof(bool));
                    modelBuilder.Entity(entity.ClrType).HasQueryFilter(GetIsDeletedRestriction(entity.ClrType));
                }
            }

            modelBuilder.Entity<CourseStudent>()
                .HasKey(bc => new {bc.CourseId, bc.StudentId});
            modelBuilder.Entity<CourseStudent>()
                .HasOne(bc => bc.Course)
                .WithMany(b => b.CourseStudents)
                .HasForeignKey(bc => bc.CourseId);
            modelBuilder.Entity<CourseStudent>()
                .HasOne(bc => bc.Student)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(bc => bc.StudentId);

            modelBuilder.Entity<AppUserRole>().HasKey(ur => new {ur.UserId, ur.RoleId});
            modelBuilder.Entity<AppUser>(u =>
            {
                u.HasMany(e => e.AppUserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            modelBuilder.Entity<AppRole>(u =>
            {
                u.HasMany(e => e.AppUserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSavingData();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void OnBeforeSavingData()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Detached && e.State != EntityState.Unchanged);
            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            trackable.CreatedAt=DateTimeOffset.Now;
                            trackable.LastUpdatedAt=DateTimeOffset.Now;
                            break;
                        case EntityState.Modified:
                            trackable.LastUpdatedAt=DateTimeOffset.Now;
                            break;
                        case EntityState.Deleted:
                            entry.Property(IsDeletedProperty).CurrentValue = true;
                            entry.State = EntityState.Modified;
                            break;
                    }
                }
                
            }
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSavingData();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }

        //For Concurrency
        public DbSet<CustomerBalance> CustomerBalances { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }

        //End Concurrency

    }
}
