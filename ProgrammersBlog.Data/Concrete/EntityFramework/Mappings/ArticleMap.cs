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
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Title).HasMaxLength(100);
            builder.Property(a => a.Title).IsRequired(true);
            builder.Property(a => a.Content).IsRequired();
            builder.Property(a => a.Content).HasColumnType("text"); 
            builder.Property(a => a.Date).IsRequired();
            builder.Property(a => a.SeoAuthor).IsRequired();
            builder.Property(a => a.SeoAuthor).HasMaxLength(50);
            builder.Property(a => a.SeoDescription).HasMaxLength(150);
            builder.Property(a => a.SeoDescription).IsRequired();
            builder.Property(a => a.SeoTags).IsRequired();
            builder.Property(a => a.SeoTags).HasMaxLength(70);
            builder.Property(a => a.ViewsCount).IsRequired();
            builder.Property(a => a.CommentCount).IsRequired();
            builder.Property(a => a.Thumbnail).IsRequired();
            builder.Property(a => a.Thumbnail).HasMaxLength(250);

            builder.Property(a => a.CreatedByName).IsRequired();
            builder.Property(a => a.CreatedByName).HasMaxLength(50);
            builder.Property(a => a.ModifiedByName).IsRequired();
            builder.Property(a => a.ModifiedByName).HasMaxLength(50);
            builder.Property(a => a.CreatedTime).IsRequired();
            builder.Property(a => a.ModifiedTime).IsRequired();
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.IsDeleted).IsRequired();
            builder.Property(a => a.Note).HasMaxLength(500);

            builder.HasOne<Category>(a => a.Category).WithMany(c=>c.Articles).HasForeignKey(a => a.CategoryId);
            builder.HasOne<User>(a => a.User).WithMany(u => u.Articles).HasForeignKey(a => a.UserId);
            builder.ToTable("Articles");
            builder.HasData(new Article()
            {
                Id = 1,
                UserId = 1,
                CategoryId = 3,
                Title = "Nesne nedir?",
                Thumbnail = "https://codingnomads.co/wp-content/uploads/2020/12/OOP-graphic-blog-oop-concepts-in-java-what-is-object-oriented-programming.png",
                Content = "Yazılımda, bir nesne, nesne yönelimli programlama (OOP) paradigmalarından biri olan ve gerçek dünyadaki nesnelerin modellemesi için kullanılan temel bir bileşendir. Bir nesne, " +
                "verileri (değişkenler veya özellikler) ve bu verilere uygulanan işlevselliği (metodlar) içeren birimdir." +
                " Örneğin, bir \"Araba\" nesnesi, rengi, hızı ve modeli gibi verileri içerebilir ve aynı zamanda \"Gaz Ver\" ve \"Fren Yap\" gibi işlevselliği sağlayan metodları içerebilir. Bu şekilde," +
                " yazılım nesneleri, gerçek dünyadaki nesnelerin özelliklerini ve davranışlarını yansıtarak, programların daha modüler," +
                " anlaşılır ve sürdürülebilir olmasına yardımcı olur.",
                SeoAuthor = "Batuhan Doğan",
                SeoDescription = "Yazılımda nesne nedir?",
                SeoTags = "Nesne",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedTime = DateTime.Now.ToUniversalTime(),
                ModifiedByName = "InitialCreate",
                ModifiedTime = DateTime.Now.ToUniversalTime(),
                ViewsCount = 288,
                CommentCount = 15,
                Note = "Yazılımda nesne nedir?"
            },
            new Article()
            {
                Id = 2,
                UserId = 1,
                CategoryId = 3,
                Title = "Nesne Tabanlı programlama (OOP) nedir?",
                Thumbnail = "https://codingnomads.co/wp-content/uploads/2020/12/OOP-graphic-blog-oop-concepts-in-java-what-is-object-oriented-programming.png",
                Content = "Nesne Yönelimli Programlama (OOP), yazılım geliştirmenin bir programlama paradigması veya yaklaşımıdır ve yazılım tasarımını ve kod organizasyonunu şekillendirmek için kullanılır. OOP," +
                " programların daha anlaşılır, modüler ve sürdürülebilir olmasına yardımcı olmak için kullanılan bir dizi kavram ve prensibe dayanır. İşte OOP'nin temel açıklamalarından bazıları:\r\n\r\nNesneler:" +
                " OOP, gerçek dünyadaki nesneleri programlamada kullanır. Bu nesneler, verileri ve bu verileri işleyen metodları içerir. Örneğin, bir \"Araba\" nesnesi, rengi, hızı ve modeli gibi özellikleri içerebilir " +
                "ve gaz verme veya fren yapma gibi işlevleri gerçekleştirebilir.\r\n\r\nSınıflar: Nesnelerin tasarım şablonlarıdır. Sınıflar, belirli bir nesne türünün özelliklerini ve davranışlarını tanımlar. Örneğin," +
                " \"Araba\" sınıfı, araba nesnelerinin nasıl oluşturulacağını ve hangi özelliklere sahip olacağını belirtir.\r\n\r\nSoyutlama: Soyutlama, karmaşıklığı yönetmek ve gereksiz ayrıntıları gizlemek için kullanılır. " +
                "Nesneler, gerçek dünyadaki nesnelerin önemli özelliklerini ve davranışlarını temsil ederken gereksiz detayları gizler.\r\n\r\nKapsülleme: Kapsülleme, verileri ve işlevleri bir araya getirerek bir \"kapsül\" oluşturur." +
                " Bu, verilere dışarıdan sadece belirli metodlar aracılığıyla erişilmesini ve bu verilerin güvenliğini sağlar",
                Note = "OOP nedir?",
                SeoAuthor = "Batuhan Doğan",
                SeoDescription = "Yazılımda nesne nedir?",
                SeoTags = " OOP",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "InitialCreate",
                CreatedTime = DateTime.Now.ToUniversalTime(),
                ModifiedByName = "InitialCreate",
                ModifiedTime = DateTime.Now.ToUniversalTime(),
                ViewsCount = 218,
                CommentCount = 4,

            }

            );
        }
    }
}
