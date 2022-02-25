using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebToolManager.Models
{
    public partial class BannerManagerDbContext : DbContext
    {
        public BannerManagerDbContext()
        {
        }

        public BannerManagerDbContext(DbContextOptions<BannerManagerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BannerData> BannerData { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<TemplateInfo> TemplateInfos { get; set; }
        public virtual DbSet<TemplateInputType> TemplateInputTypes { get; set; }
        public virtual DbSet<UploadedBannerList> UploadedBannerLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=JACKS-DESKTOP;Database=BannerManagerDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Korean_Wansung_CI_AS");

            modelBuilder.Entity<BannerData>(entity =>
            {
                entity.Property(e => e.BannerId).HasColumnName("banner_id");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("data");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("type");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("label");
            });

            modelBuilder.Entity<Page>(entity =>
            {

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Root)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("root");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<TemplateInfo>(entity =>
            {

                entity.ToTable("TemplateInfo");

                entity.Property(e => e.CodeHtml)
                    .HasColumnName("code(html)");

                entity.Property(e => e.CodeCss)
                    .HasColumnName("code(css)");

                entity.Property(e => e.CodeJs)
                    .HasColumnName("code(js)");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("details");

                entity.Property(e => e.FrontImage)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("front_image");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TemplateInputType>(entity =>
            {

                entity.ToTable("TemplateInputType");

                entity.Property(e => e.BannerId).HasColumnName("banner_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("label");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Placeholder)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("placeholder");

                entity.Property(e => e.TemplateId).HasColumnName("template_id");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<UploadedBannerList>(entity =>
            {

                entity.ToTable("UploadedBannerList");

                entity.Property(e => e.BannerName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("banner_name");

                entity.Property(e => e.CodeCss)
                    .HasColumnName("code(css)");

                entity.Property(e => e.CodeHtml)
                    .IsRequired()
                    .HasColumnName("code(html)");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Page).HasColumnName("page");

                entity.Property(e => e.TemplateId)
                    .IsRequired()
                    .HasColumnName("template_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
