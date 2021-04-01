using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Covid19TestingServer.Models
{
    public partial class Covid19TestingSrvContext : DbContext
    {
        public Covid19TestingSrvContext()
        {
        }

        public Covid19TestingSrvContext(DbContextOptions<Covid19TestingSrvContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblBiodata> TblBiodata { get; set; }
        public virtual DbSet<TblLabTests> TblLabTests { get; set; }
        public virtual DbSet<TblLabTestsIndicatorsValues> TblLabTestsIndicatorsValues { get; set; }
        public virtual DbSet<TblLabTestsSpecimen> TblLabTestsSpecimen { get; set; }
        public virtual DbSet<TlkpGenders> TlkpGenders { get; set; }
        public virtual DbSet<TlkpSpecimen> TlkpSpecimen { get; set; }
        public virtual DbSet<TlkpTestIndicators> TlkpTestIndicators { get; set; }
        public virtual DbSet<TlkpTestMethods> TlkpTestMethods { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
////            if (!optionsBuilder.IsConfigured)
////            {
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
////                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));//"Server=mdgw10dtm\\mssqlserver16;Database=Covid19TestingSrv;Trusted_Connection=True;");
////                //Configuration.GetConnectionString("DefaultConnection")
////            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblBiodata>(entity =>
            {
                entity.ToTable("tblBiodata");

                entity.HasIndex(e => e.EpidNo)
                    .HasName("IX_tblBiodata")
                    .IsUnique();

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();

                entity.Property(e => e.Dateofbirth)
                    .HasColumnName("dateofbirth")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EpidNo)
                    .HasColumnName("epid_no")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.HomePhone)
                    .HasColumnName("home_phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);


                entity.Property(e => e.LegalGardianName)
                    .HasColumnName("Legal_gardian_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LocalPhone)
                    .HasColumnName("local_phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ResidentialAddress)
                    .IsRequired()
                    .HasColumnName("residential_address")
                    .HasMaxLength(250)
                    .IsUnicode(false);

            entity.Property(e => e.TransferTime)
                    .HasColumnName("transfer_time")
                    .HasColumnType("datetime");



                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.TblBiodata)
                    .HasForeignKey(d => d.Gender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblBiodata_tlkpGenders");
            });

         

            modelBuilder.Entity<TblLabTests>(entity =>
            {
                entity.ToTable("tblLabTests");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();

                entity.Property(e => e.Interpretation).HasColumnName("interpretation");

                entity.Property(e => e.Method).HasColumnName("method");

                entity.Property(e => e.ReportingDate)
                    .HasColumnName("reporting_date")
                    .HasColumnType("date");

                entity.Property(e => e.ReportingTime)
                    .HasColumnName("reporting_time")
                    .HasColumnType("time(0)");

                entity.Property(e => e.TestingDate)
                    .HasColumnName("testing_date")
                    .HasColumnType("date");

                entity.Property(e => e.TestingTime)
                    .HasColumnName("testing_time")
                    .HasColumnType("time(0)");

                entity.Property(e => e.TransferTime)
                      .HasColumnName("transfer_time")
                      .HasColumnType("datetime");

                entity.HasOne(d => d.BiodataNavigation)
                    .WithMany(p => p.TblLabTests)
                    .HasForeignKey(d => d.Biodata)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLabTests_tblBiodata");

                entity.HasOne(d => d.MethodNavigation)
                    .WithMany(p => p.TblLabTests)
                    .HasForeignKey(d => d.Method)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLabTests_tlkpTestMethods");
            });

            modelBuilder.Entity<TblLabTestsIndicatorsValues>(entity =>
            {
                entity.ToTable("tblLabTestsIndicatorsValues");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();

                entity.Property(e => e.Indicator).HasColumnName("indicator");

                entity.Property(e => e.IndicatorValue)
                .HasColumnName("indicator_value")
                .HasColumnType("decimal(18,2)");

                entity.Property(e => e.Labtest).HasColumnName("labtest");

                entity.Property(e => e.Method).HasColumnName("method");

                entity.HasOne(d => d.LabtestNavigation)
                    .WithMany(p => p.TblLabTestsIndicatorsValues)
                    .HasForeignKey(d => d.Labtest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLabTestsIndicatorsValues_tblLabTests");

                entity.HasOne(d => d.TlkpTestIndicators)
                    .WithMany(p => p.TblLabTestsIndicatorsValues)
                    .HasPrincipalKey(p => new { p.Id, p.Method })
                    .HasForeignKey(d => new { d.Indicator, d.Method })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLabTestsIndicatorsValues_tlkpTestIndicators");
            });

            modelBuilder.Entity<TblLabTestsSpecimen>(entity =>
            {
                entity.ToTable("tblLabTestsSpecimen");

                entity.HasIndex(e => new { e.Labtest, e.Specimen })
                    .HasName("IX_tblLabTestsSpecimen")
                    .IsUnique();

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();

                entity.Property(e => e.Checked).HasColumnName("checked");

                entity.Property(e => e.Labtest).HasColumnName("labtest");

                entity.Property(e => e.Specimen).HasColumnName("specimen");

                entity.Property(e => e.SpecimenOther)
                    .HasColumnName("specimen_other")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.LabtestNavigation)
                    .WithMany(p => p.TblLabTestsSpecimen)
                    .HasForeignKey(d => d.Labtest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLabTestsSpecimen_tblLabTests");

                entity.HasOne(d => d.SpecimenNavigation)
                    .WithMany(p => p.TblLabTestsSpecimen)
                    .HasForeignKey(d => d.Specimen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLabTestsSpecimen_tlkpSpecimen");
            });

          
            modelBuilder.Entity<TlkpGenders>(entity =>
            {
                entity.ToTable("tlkpGenders");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

           

            modelBuilder.Entity<TlkpSpecimen>(entity =>
            {
                entity.ToTable("tlkpSpecimen");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

           

            modelBuilder.Entity<TlkpTestIndicators>(entity =>
            {
                entity.ToTable("tlkpTestIndicators");

                entity.HasIndex(e => new { e.Id, e.Method })
                    .HasName("IX_tlkpTestIndicators")
                    .IsUnique();

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();

                entity.Property(e => e.IndicatorName)
                    .IsRequired()
                    .HasColumnName("indicator_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Method).HasColumnName("method");

                entity.HasOne(d => d.MethodNavigation)
                    .WithMany(p => p.TlkpTestIndicators)
                    .HasForeignKey(d => d.Method)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tlkpTestIndicators_tlkpTestMethods");
            });

            modelBuilder.Entity<TlkpTestMethods>(entity =>
            {
                entity.ToTable("tlkpTestMethods");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();

                entity.Property(e => e.Methodname)
                    .IsRequired()
                    .HasColumnName("methodname")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
