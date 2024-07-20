using Microsoft.AspNetCore.Identity;
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
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);
            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            builder.Property(u => u.Picture).IsRequired();
            builder.Property(u => u.Picture).HasMaxLength(250);

            builder.Property(u => u.LinkedInProfile).IsRequired(false);
            builder.Property(u => u.LinkedInProfile).HasMaxLength(256);

            builder.Property(u => u.GitHubProfile).IsRequired(false);
            builder.Property(u => u.GitHubProfile).HasMaxLength(256);
            User admin = new User
            {
                Id = 1,
                UserName = "batuhan",
                NormalizedUserName = "BATUHAN",
                Email = "batuhandogan@petalmail.com",
                NormalizedEmail = "batuhandogan@petalmail.com".ToUpper(),
                PhoneNumber = "+905462636750",
                Picture = "AdminProfile.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            admin.PasswordHash = CreatePasswordHash(admin, "adminUser220658");
            User editor = new User
            {
                Id = 2,
                UserName = "editor",
                NormalizedUserName = "editor".ToUpper(),
                Email = "editor@petalmail.com",
                NormalizedEmail = "editor@petalmail.com".ToUpper(),
                PhoneNumber = "+905442656951",
                Picture = "EditorProfile.png",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            editor.PasswordHash = CreatePasswordHash(editor, "editorUser220658");
            builder.HasData(admin,editor);
        }
        private string CreatePasswordHash(User user,string password)
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
