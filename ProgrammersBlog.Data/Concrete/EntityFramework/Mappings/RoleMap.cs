using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("AspNetRoles");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<RoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
            var superAdmin=new Role 
            {
                Id = 1,
                Name="admin",
                NormalizedName = "ADMIN",   
                ConcurrencyStamp= Guid.NewGuid().ToString()
            };
            var editor = new Role
            {
                Id = 2,
                Name = "editor",
                NormalizedName = "EDITOR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var user = new Role
            {
                Id = 3,
                Name = "user",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var userRead = new Role
            {
                Id = 3,
                Name = "user.read",
                NormalizedName = "USER.READ",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var userUpdate = new Role
            {
                Id = 3,
                Name = "user.update",
                NormalizedName = "USER.UPDATE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var userDelete = new Role
            {
                Id = 3,
                Name = "user.delete",
                NormalizedName = "USER.DELETE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var userCreate = new Role
            {
                Id = 3,
                Name = "user.create",
                NormalizedName = "USER.CREATE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };


            var commentRead = new Role
            {
                Id = 3,
                Name = "comment.read",
                NormalizedName = "COMMENT.READ",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var commentUpdate = new Role
            {
                Id = 3,
                Name = "comment.update",
                NormalizedName = "COMMENT.UPDATE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var commentDelete = new Role
            {
                Id = 3,
                Name = "comment.delete",
                NormalizedName = "COMMENT.DELETE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var commentCreate = new Role
            {
                Id = 3,
                Name = "comment.create",
                NormalizedName = "COMMENT.CREATE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var categoryRead = new Role
            {
                Id = 3,
                Name = "category.read",
                NormalizedName = "CATEGORY.READ",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var categoryUpdate = new Role
            {
                Id = 3,
                Name = "category.update",
                NormalizedName = "CATEGORY.UPDATE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var categoryDelete = new Role
            {
                Id = 3,
                Name = "category.delete",
                NormalizedName = "CATEGORY.DELETE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var categoryCreate = new Role
            {
                Id = 3,
                Name = "category.create",
                NormalizedName = "CATEGORY.CREATE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var articleRead = new Role
            {
                Id = 3,
                Name = "article.read",
                NormalizedName = "ARTICLE.READ",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var articleUpdate = new Role
            {
                Id = 3,
                Name = "article.update",
                NormalizedName = "ARTICLE.UPDATE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var articleDelete = new Role
            {
                Id = 3,
                Name = "article.delete",
                NormalizedName = "ARTICLE.DELETE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var articleCreate = new Role
            {
                Id = 3,
                Name = "article.create",
                NormalizedName = "ARTICLE.CREATE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            builder.HasData(superAdmin, user,editor);
        }
    }
}
