using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Covid19Testing.Models
{
    public partial class Covid19TestingContext : DbContext
    {
        public Covid19TestingContext()
        {
        }

        public Covid19TestingContext(DbContextOptions<Covid19TestingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblBiodata> TblBiodata { get; set; }
        public virtual DbSet<TblLabTests> TblLabTests { get; set; }
        public virtual DbSet<TblLabTestsIndicatorsValues> TblLabTestsIndicatorsValues { get; set; }
        public virtual DbSet<TblLabTestsSpecimen> TblLabTestsSpecimen { get; set; }
        public virtual DbSet<TblRoles> TblRoles { get; set; }
        public virtual DbSet<TblUsers> TblUsers { get; set; }
        public virtual DbSet<TblUsersProfiles> TblUsersProfiles { get; set; }
        public virtual DbSet<TlkpGenders> TlkpGenders { get; set; }
        public virtual DbSet<TlkpResults> TlkpResults { get; set; }
        public virtual DbSet<TlkpSpecimen> TlkpSpecimen { get; set; }
        public virtual DbSet<TlkpStaticInfo> TlkpStaticInfo { get; set; }
        public virtual DbSet<TlkpTestIndicators> TlkpTestIndicators { get; set; }
        public virtual DbSet<TlkpTestMethods> TlkpTestMethods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=mdgw10dtm\\mssqlserver16;Database=Covid19Testing;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblBiodata>(entity =>
            {
                entity.ToTable("tblBiodata");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dateofbirth)
                    .HasColumnName("dateofbirth")
                    .HasColumnType("date");

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
                    //.IsRequired()
                    .HasColumnName("home_phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocalPhone)
                    .IsRequired()
                    .HasColumnName("local_phone")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsertBy)
                    .IsRequired()
                    .HasColumnName("insert_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.LegalGardianName)
                    .HasColumnName("Legal_gardian_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ResidentialAddress)
                    .IsRequired()
                    .HasColumnName("residential_address")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasColumnName("update_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Biodata).HasColumnName("biodata");

                entity.Property(e => e.InsertBy)
                    .IsRequired()
                    .HasColumnName("insert_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("datetime");

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

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasColumnName("update_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Approved)
                    .HasColumnName("approved")
                    .HasColumnType("bit");

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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Indicator).HasColumnName("indicator");

                entity.Property(e => e.IndicatorValue).HasColumnName("indicator_value");

                entity.Property(e => e.InsertBy)
                    .IsRequired()
                    .HasColumnName("insert_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Labtest).HasColumnName("labtest");

                entity.Property(e => e.Method).HasColumnName("method");

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasColumnName("update_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InsertBy)
                    .IsRequired()
                    .HasColumnName("insert_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Checked)
                    .HasColumnName("checked")
                    .HasColumnType("bit");

                entity.Property(e => e.Labtest).HasColumnName("labtest");

                entity.Property(e => e.Specimen).HasColumnName("specimen");

                entity.Property(e => e.SpecimenOther)
                    .HasColumnName("specimen_other")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasColumnName("update_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

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

            modelBuilder.Entity<TblRoles>(entity =>
            {
                entity.ToTable("tblRoles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Rolename)
                    .HasColumnName("rolename")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUsers>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("tblUsers");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastunlockTime)
                    .HasColumnName("lastunlock_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastunlockedBy)
                    .IsRequired()
                    .HasColumnName("lastunlocked_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LockTime)
                    .HasColumnName("lock_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.LockedBy)
                    .HasColumnName("locked_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Userrole).HasColumnName("userrole");

                entity.HasOne(d => d.UserroleNavigation)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.Userrole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUsers_tblRoles");
            });

            modelBuilder.Entity<TblUsersProfiles>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("tblUsersProfiles");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithOne(p => p.TblUsersProfiles)
                    .HasForeignKey<TblUsersProfiles>(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblUsersProfiles_tblUsers");
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

            modelBuilder.Entity<TlkpResults>(entity =>
            {
                entity.ToTable("tlkpResults");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasColumnName("result")
                    .HasMaxLength(20)
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

            modelBuilder.Entity<TlkpStaticInfo>(entity =>
            {
                entity.ToTable("tlkpStaticInfo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsertBy)
                    .IsRequired()
                    .HasColumnName("insert_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.LabSupervisor)
                    .HasColumnName("Lab_supervisor")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LabSupervisorSignature).HasColumnName("Lab_supervisor_signature");

                entity.Property(e => e.Miscellaneous)
                    .HasColumnName("miscellaneous")
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Organization)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateBy)
                    .IsRequired()
                    .HasColumnName("update_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Website)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TlkpTestIndicators>(entity =>
            {
                entity.ToTable("tlkpTestIndicators");

                entity.HasIndex(e => new { e.Id, e.Method })
                    .HasName("IX_tlkpTestIndicators")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IndicatorName)
                    .IsRequired()
                    .HasColumnName("indicator_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsertBy)
                    .IsRequired()
                    .HasColumnName("insert_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("datetime");

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

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.InsertBy)
                    .IsRequired()
                    .HasColumnName("insert_by")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsertTime)
                    .HasColumnName("insert_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Methodname)
                    .IsRequired()
                    .HasColumnName("methodname")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
