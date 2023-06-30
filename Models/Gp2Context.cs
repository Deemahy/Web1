using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Web1.Models;

public partial class Gp2Context : DbContext
{
    internal readonly object Vaccines;

    public Gp2Context()
    {
    }

    public Gp2Context(DbContextOptions<Gp2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Appontment> Appontments { get; set; }

    public virtual DbSet<AvailabiltyDate> AvailabiltyDates { get; set; }

    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Emergency> Emergencies { get; set; }

    public virtual DbSet<LogIn> LogIns { get; set; }

    public virtual DbSet<Manegeer> Manegeers { get; set; }

    public virtual DbSet<NightShift> NightShifts { get; set; }

    public virtual DbSet<PatentVacc> PatentVaccs { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Reseption> Reseptions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vaccsine> Vaccsines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:gp21.database.windows.net,1433;Initial Catalog=gp2;Persist Security Info=False;User ID=dema;Password=deemah2319+;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appontment>(entity =>
        {
            entity.HasKey(e => e.AppontmentId).IsClustered(false);

            entity.ToTable("appontment");

            entity.Property(e => e.AppontmentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("appontmentId");
            entity.Property(e => e.AvailabiltyDateId).HasColumnName("availabiltyDateId");
            entity.Property(e => e.Ciliid).HasColumnName("ciliid");
            entity.Property(e => e.DocId).HasColumnName("docId");
            entity.Property(e => e.EndTime).HasColumnName("endTime");
            entity.Property(e => e.Medesine).HasColumnName("medesine");
            entity.Property(e => e.MidCase).HasColumnName("midCase");
            entity.Property(e => e.PatId).HasColumnName("patId");
            entity.Property(e => e.StartTime).HasColumnName("startTime");

            entity.HasOne(d => d.AppontmentNavigation).WithOne(p => p.Appontment)
                .HasForeignKey<Appontment>(d => d.AppontmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("availabiltyDateId");

            entity.HasOne(d => d.Appontment1).WithOne(p => p.Appontment)
                .HasForeignKey<Appontment>(d => d.AppontmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ciliid");

            entity.HasOne(d => d.Appontment2).WithOne(p => p.Appontment)
                .HasForeignKey<Appontment>(d => d.AppontmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("docId");

            entity.HasOne(d => d.Appontment3).WithOne(p => p.Appontment)
                .HasForeignKey<Appontment>(d => d.AppontmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patId");
        });

        modelBuilder.Entity<AvailabiltyDate>(entity =>
        {
            entity.HasKey(e => e.AvailabiltyDateId).IsClustered(false);

            entity.ToTable("availabiltyDate");

            entity.Property(e => e.AvailabiltyDateId)
                .ValueGeneratedOnAdd()
                .HasColumnName("availabiltyDateId");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Doctorid).HasColumnName("doctorid");

            entity.HasOne(d => d.AvailabiltyDateNavigation).WithOne(p => p.AvailabiltyDate)
                .HasForeignKey<AvailabiltyDate>(d => d.AvailabiltyDateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctorid");
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.HasKey(e => e.ClinicId).IsClustered(false);

            entity.ToTable("clinic");

            entity.Property(e => e.ClinicId).HasColumnName("clinicId");
            entity.Property(e => e.ClinicAddress)
                .HasMaxLength(50)
                .HasColumnName("clinicAddress");
            entity.Property(e => e.ClinicLocation).HasColumnName("clinicLocation");
            entity.Property(e => e.ClinicName)
                .HasMaxLength(50)
                .HasColumnName("clinicName");
            entity.Property(e => e.ClinicPhone)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("clinicPhone");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).IsClustered(false);

            entity.ToTable("doctor");

            entity.Property(e => e.DoctorId)
                .ValueGeneratedOnAdd()
                .HasColumnName("doctorId");
            entity.Property(e => e.ClinicId).HasColumnName("clinicID");
            entity.Property(e => e.OtherInfo).HasColumnName("otherInfo");
            entity.Property(e => e.Speclized).HasColumnName("speclized");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.DoctorNavigation).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clinicID");

            entity.HasOne(d => d.Doctor1).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userID");
        });

        modelBuilder.Entity<Emergency>(entity =>
        {
            entity.HasKey(e => e.EmergencyId).IsClustered(false);

            entity.ToTable("emergency");

            entity.Property(e => e.EmergencyId)
                .ValueGeneratedOnAdd()
                .HasColumnName("emergencyId");
            entity.Property(e => e.DId).HasColumnName("dId");
            entity.Property(e => e.DoctorResponse).HasColumnName("doctorResponse");
            entity.Property(e => e.PId).HasColumnName("pId");
            entity.Property(e => e.PatientMassages).HasColumnName("patientMassages");

            entity.HasOne(d => d.EmergencyNavigation).WithOne(p => p.Emergency)
                .HasForeignKey<Emergency>(d => d.EmergencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dId");

            entity.HasOne(d => d.Emergency1).WithOne(p => p.Emergency)
                .HasForeignKey<Emergency>(d => d.EmergencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pId");
        });

        modelBuilder.Entity<LogIn>(entity =>
        {
            entity.HasKey(e => e.LogInId).IsClustered(false);

            entity.ToTable("logIn");

            entity.Property(e => e.LogInId)
                .ValueGeneratedOnAdd()
                .HasColumnName("logInId");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.UersId).HasColumnName("uersId");
            entity.Property(e => e.Username)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("username");

            entity.HasOne(d => d.LogInNavigation).WithOne(p => p.LogIn)
                .HasForeignKey<LogIn>(d => d.LogInId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("roleId");

            entity.HasOne(d => d.LogIn1).WithOne(p => p.LogIn)
                .HasForeignKey<LogIn>(d => d.LogInId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("uersId");
        });

        modelBuilder.Entity<Manegeer>(entity =>
        {
            entity.HasKey(e => e.ManegeerId).IsClustered(false);

            entity.ToTable("manegeer");

            entity.Property(e => e.ManegeerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("manegeerId");
            entity.Property(e => e.CliniccId).HasColumnName("cliniccId");
            entity.Property(e => e.Ueserss).HasColumnName("ueserss");

            entity.HasOne(d => d.ManegeerNavigation).WithOne(p => p.Manegeer)
                .HasForeignKey<Manegeer>(d => d.ManegeerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cliniccId");

            entity.HasOne(d => d.Manegeer1).WithOne(p => p.Manegeer)
                .HasForeignKey<Manegeer>(d => d.ManegeerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ueserss");
        });

        modelBuilder.Entity<NightShift>(entity =>
        {
            entity.HasKey(e => e.NightShiftId).IsClustered(false);

            entity.ToTable("nightShift");

            entity.Property(e => e.NightShiftId)
                .ValueGeneratedOnAdd()
                .HasColumnName("nightShiftId");
            entity.Property(e => e.ClinIdd).HasColumnName("clinIdd");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.DocIdd).HasColumnName("docIdd");
            entity.Property(e => e.EndTime).HasColumnName("endTime");
            entity.Property(e => e.StartTime).HasColumnName("startTime");

            entity.HasOne(d => d.NightShiftNavigation).WithOne(p => p.NightShift)
                .HasForeignKey<NightShift>(d => d.NightShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clinIdd");

            entity.HasOne(d => d.NightShift1).WithOne(p => p.NightShift)
                .HasForeignKey<NightShift>(d => d.NightShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("docIdd");
        });

        modelBuilder.Entity<PatentVacc>(entity =>
        {
            entity.HasKey(e => e.PatientVacId).IsClustered(false);

            entity.ToTable("patentVacc");

            entity.Property(e => e.PatientVacId)
                .ValueGeneratedOnAdd()
                .HasColumnName("patientVacId");
            entity.Property(e => e.Headcircumference).HasColumnName("headcircumference");
            entity.Property(e => e.Hight).HasColumnName("hight");
            entity.Property(e => e.PatientId).HasColumnName("patientId");
            entity.Property(e => e.VacDay)
                .HasColumnType("date")
                .HasColumnName("vacDay");
            entity.Property(e => e.VaccId).HasColumnName("vaccId");
            entity.Property(e => e.Width).HasColumnName("width");

            entity.HasOne(d => d.PatientVac).WithOne(p => p.PatentVacc)
                .HasForeignKey<PatentVacc>(d => d.PatientVacId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patientId");

            entity.HasOne(d => d.PatientVacNavigation).WithOne(p => p.PatentVacc)
                .HasForeignKey<PatentVacc>(d => d.PatientVacId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vaccId");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).IsClustered(false);

            entity.ToTable("patient");

            entity.Property(e => e.PatientId)
                .ValueGeneratedOnAdd()
                .HasColumnName("patientId");
            entity.Property(e => e.Alargie).HasColumnName("alargie");
            entity.Property(e => e.BloodType)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("bloodType");
            entity.Property(e => e.Users).HasColumnName("users");

            entity.HasOne(d => d.PatientNavigation).WithOne(p => p.Patient)
                .HasForeignKey<Patient>(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users");
        });

        modelBuilder.Entity<Reseption>(entity =>
        {
            entity.HasKey(e => e.ReseptionId).IsClustered(false);

            entity.ToTable("reseption");

            entity.Property(e => e.ReseptionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("reseptionId");
            entity.Property(e => e.Clinicssid).HasColumnName("clinicssid");
            entity.Property(e => e.Userssid).HasColumnName("userssid");

            entity.HasOne(d => d.ReseptionNavigation).WithOne(p => p.Reseption)
                .HasForeignKey<Reseption>(d => d.ReseptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clinicssid");

            entity.HasOne(d => d.Reseption1).WithOne(p => p.Reseption)
                .HasForeignKey<Reseption>(d => d.ReseptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userssid");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.RoleName).HasColumnName("roleName");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).IsClustered(false);

            entity.ToTable("user");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.DateBearth)
                .HasColumnType("date")
                .HasColumnName("dateBearth");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FullName).HasColumnName("fullName");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Imagpath).HasColumnName("imagpath");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Vaccsine>(entity =>
        {
            entity.HasKey(e => e.VaccsineId).IsClustered(false);

            entity.ToTable("vaccsine");

            entity.Property(e => e.VaccsineId).HasColumnName("vaccsineId");
            entity.Property(e => e.ApprovedBy).HasColumnName("approvedBy");
            entity.Property(e => e.DoseNumber).HasColumnName("doseNumber");
            entity.Property(e => e.Manufacture).HasColumnName("manufacture");
            entity.Property(e => e.RecomendAge).HasColumnName("recomendAge");
            entity.Property(e => e.Symptoms).HasColumnName("symptoms");
            entity.Property(e => e.VName).HasColumnName("vName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
