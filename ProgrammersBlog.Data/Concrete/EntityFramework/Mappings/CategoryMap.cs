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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Name).HasMaxLength(70);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedTime).IsRequired();
            builder.Property(c => c.ModifiedTime).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(500);
            builder.ToTable("Categories");
            builder.HasData(
                new Category() 
                {
                    Id=1,
                    Name="Python",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedTime = DateTime.Now.ToUniversalTime(),
                    ModifiedByName = "InitialCreate",
                    ModifiedTime = DateTime.Now.ToUniversalTime(),
                    Description = "Python programlama dili ile ilgili güncel bilgiler .",
                    Note = "Python kategorisi"
                },
                new Category()
                {
                    Id = 2,
                    Name = "C#",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedTime = DateTime.Now.ToUniversalTime(),
                    ModifiedByName = "InitialCreate",
                    ModifiedTime = DateTime.Now.ToUniversalTime(),
                    Description = "C# programlama dili ile ilgili güncel bilgiler .",
                    Note = "C# kategorisi"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Nesne Tabanlı Programlama",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "InitialCreate",
                    CreatedTime = DateTime.Now.ToUniversalTime(),
                    ModifiedByName = "InitialCreate",
                    ModifiedTime = DateTime.Now.ToUniversalTime(),
                    Description = "Nesne yönelimli prodramlama ile ilgili güncel bilgiler .",
                    Note = "Nesne yönelimli programlama kategorisi"
                }
                );
        }
    }
}
