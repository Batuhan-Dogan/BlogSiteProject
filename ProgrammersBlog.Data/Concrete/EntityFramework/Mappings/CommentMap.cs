﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.Text).HasMaxLength(1000);

            builder.Property(c => c.CreatedByName).IsRequired();
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModifiedByName).IsRequired();
            builder.Property(c => c.ModifiedByName).HasMaxLength(50);
            builder.Property(c => c.CreatedTime).IsRequired();
            builder.Property(c => c.ModifiedTime).IsRequired();
            builder.Property(c => c.IsActive).IsRequired();
            builder.Property(c => c.IsDeleted).IsRequired();
            builder.Property(c => c.Note).HasMaxLength(500);

            builder.HasOne<Article>(c => c.Article).WithMany(a=>a.Comments).HasForeignKey(c=>c.ArticleId);
            builder.ToTable("Comments");

            builder.HasData(new Comment()
            {
                Id = 1,
                Text = "Çok teşşekür ederim çok faydası dokundu",
                ArticleId = 1,
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedTime = DateTime.Now.ToUniversalTime(),
                ModifiedByName = "InitialCreate",
                ModifiedTime = DateTime.Now.ToUniversalTime(),
                Note = "Nesne yönelimli programlama yorumu",

            });
        }
    }
}
