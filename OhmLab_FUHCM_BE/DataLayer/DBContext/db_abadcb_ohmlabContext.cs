using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DataLayer.DBContext
{
    public partial class db_abadcb_ohmlabContext : DbContext
    {
        public db_abadcb_ohmlabContext()
        {
        }

        public db_abadcb_ohmlabContext(DbContextOptions<db_abadcb_ohmlabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accessory> Accessories { get; set; } = null!;
        public virtual DbSet<AccessoryKitTemplate> AccessoryKitTemplates { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<ClassUser> ClassUsers { get; set; } = null!;
        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<EquipmentType> EquipmentTypes { get; set; } = null!;
        public virtual DbSet<EquipmentTypeRoom> EquipmentTypeRooms { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<GradeDescription> GradeDescriptions { get; set; } = null!;
        public virtual DbSet<Kit> Kits { get; set; } = null!;
        public virtual DbSet<KitAccessory> KitAccessories { get; set; } = null!;
        public virtual DbSet<KitTemplate> KitTemplates { get; set; } = null!;
        public virtual DbSet<KitTemplateRoom> KitTemplateRooms { get; set; } = null!;
        public virtual DbSet<Lab> Labs { get; set; } = null!;
        public virtual DbSet<LabEquipmentType> LabEquipmentTypes { get; set; } = null!;
        public virtual DbSet<LabKitTemplate> LabKitTemplates { get; set; } = null!;
        public virtual DbSet<RegistraionSchedule> RegistraionSchedules { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomLab> RoomLabs { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<ScheduleType> ScheduleTypes { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;
        public virtual DbSet<SemesterSubject> SemesterSubjects { get; set; } = null!;
        public virtual DbSet<Slot> Slots { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<TeamEquipment> TeamEquipments { get; set; } = null!;
        public virtual DbSet<TeamKit> TeamKits { get; set; } = null!;
        public virtual DbSet<TeamUser> TeamUsers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        string GetConnectionString()
        {
            IConfiguration builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return builder["ConnectionStrings:hosting"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accessory>(entity =>
            {
                entity.ToTable("Accessory");

                entity.Property(e => e.AccessoryId).HasColumnName("Accessory_id");

                entity.Property(e => e.AccessoryCase)
                    .HasMaxLength(50)
                    .HasColumnName("Accessory_Case");

                entity.Property(e => e.AccessoryCreateDate)
                    .HasColumnType("date")
                    .HasColumnName("Accessory_CreateDate");

                entity.Property(e => e.AccessoryDescription).HasColumnName("Accessory_Description");

                entity.Property(e => e.AccessoryName)
                    .HasMaxLength(50)
                    .HasColumnName("Accessory_Name");

                entity.Property(e => e.AccessoryStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Accessory_Status");

                entity.Property(e => e.AccessoryUrlImg).HasColumnName("Accessory_Url_Img");

                entity.Property(e => e.AccessoryValueCode)
                    .HasMaxLength(50)
                    .HasColumnName("Accessory_ValueCode");
            });

            modelBuilder.Entity<AccessoryKitTemplate>(entity =>
            {
                entity.ToTable("Accessory_KitTemplate");

                entity.Property(e => e.AccessoryKitTemplateId).HasColumnName("Accessory_KitTemplate_id");

                entity.Property(e => e.AccessoryId).HasColumnName("Accessory_id");

                entity.Property(e => e.AccessoryKitTemplateStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Accessory_KitTemplate_Status");

                entity.Property(e => e.AccessoryQuantity).HasColumnName("Accessory_Quantity");

                entity.Property(e => e.KitTemplateId)
                    .HasMaxLength(50)
                    .HasColumnName("KitTemplate_id");

                entity.HasOne(d => d.Accessory)
                    .WithMany(p => p.AccessoryKitTemplates)
                    .HasForeignKey(d => d.AccessoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Accessory__Acces__4222D4EF");

                entity.HasOne(d => d.KitTemplate)
                    .WithMany(p => p.AccessoryKitTemplates)
                    .HasForeignKey(d => d.KitTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Accessory__KitTe__412EB0B6");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.ClassId).HasColumnName("Class_id");

                entity.Property(e => e.ClassDescription).HasColumnName("Class_Description");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .HasColumnName("Class_Name");

                entity.Property(e => e.ClassStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Class_Status");

                entity.Property(e => e.LecturerId).HasColumnName("Lecturer_id");

                entity.Property(e => e.ScheduleTypeId).HasColumnName("ScheduleType_id");

                entity.Property(e => e.SemesterSubjectId).HasColumnName("Semester_Subject_id");

                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.LecturerId)
                    .HasConstraintName("FK__Class__Lecturer___73BA3083");

                entity.HasOne(d => d.ScheduleType)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.ScheduleTypeId)
                    .HasConstraintName("FK__Class__ScheduleT__74AE54BC");

                entity.HasOne(d => d.SemesterSubject)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.SemesterSubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Class__Semester___72C60C4A");
            });

            modelBuilder.Entity<ClassUser>(entity =>
            {
                entity.ToTable("Class_User");

                entity.Property(e => e.ClassUserId).HasColumnName("Class_User_id");

                entity.Property(e => e.ClassId).HasColumnName("Class_id");

                entity.Property(e => e.ClassUserDescription).HasColumnName("Class_User_Description");

                entity.Property(e => e.ClassUserStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Class_User_Status");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassUsers)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Class_Use__Class__778AC167");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ClassUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Class_Use__User___787EE5A0");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.EquipmentId)
                    .HasMaxLength(50)
                    .HasColumnName("Equipment_id");

                entity.Property(e => e.EquipmentCode)
                    .HasMaxLength(50)
                    .HasColumnName("Equipment_Code");

                entity.Property(e => e.EquipmentDescription).HasColumnName("Equipment_Description");

                entity.Property(e => e.EquipmentName)
                    .HasMaxLength(50)
                    .HasColumnName("Equipment_Name");

                entity.Property(e => e.EquipmentNumberSerial)
                    .HasMaxLength(50)
                    .HasColumnName("Equipment_NumberSerial");

                entity.Property(e => e.EquipmentQr).HasColumnName("Equipment_QR");

                entity.Property(e => e.EquipmentStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Equipment_Status");

                entity.Property(e => e.EquipmentTypeId)
                    .HasMaxLength(50)
                    .HasColumnName("EquipmentType_id");

                entity.Property(e => e.EquipmentTypeUrlImg).HasColumnName("EquipmentType_Url_Img");

                entity.Property(e => e.RoomId).HasColumnName("Room_id");

                entity.HasOne(d => d.EquipmentType)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.EquipmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Equipment__Equip__5BE2A6F2");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Equipment__Room___5CD6CB2B");
            });

            modelBuilder.Entity<EquipmentType>(entity =>
            {
                entity.ToTable("EquipmentType");

                entity.Property(e => e.EquipmentTypeId)
                    .HasMaxLength(50)
                    .HasColumnName("EquipmentType_id");

                entity.Property(e => e.EquipmentTypeCode)
                    .HasMaxLength(50)
                    .HasColumnName("EquipmentType_Code");

                entity.Property(e => e.EquipmentTypeCreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("EquipmentType_CreateDate");

                entity.Property(e => e.EquipmentTypeDescription).HasColumnName("EquipmentType_Description");

                entity.Property(e => e.EquipmentTypeName)
                    .HasMaxLength(100)
                    .HasColumnName("EquipmentType_Name");

                entity.Property(e => e.EquipmentTypeQuantity).HasColumnName("EquipmentType_Quantity");

                entity.Property(e => e.EquipmentTypeStatus)
                    .HasMaxLength(50)
                    .HasColumnName("EquipmentType_Status");

                entity.Property(e => e.EquipmentTypeUrlImg).HasColumnName("EquipmentType_Url_Img");
            });

            modelBuilder.Entity<EquipmentTypeRoom>(entity =>
            {
                entity.ToTable("EquipmentType_Room");

                entity.Property(e => e.EquipmentTypeRoomId).HasColumnName("EquipmentType_Room_id");

                entity.Property(e => e.EquipmentTypeId)
                    .HasMaxLength(50)
                    .HasColumnName("EquipmentType_id");

                entity.Property(e => e.EquipmentTypeRoomQuantity).HasColumnName("EquipmentType_Room_Quantity");

                entity.Property(e => e.KitTemplateStatus)
                    .HasMaxLength(50)
                    .HasColumnName("KitTemplate_Status");

                entity.Property(e => e.RoomId).HasColumnName("Room_id");

                entity.HasOne(d => d.EquipmentType)
                    .WithMany(p => p.EquipmentTypeRooms)
                    .HasForeignKey(d => d.EquipmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Equipment__KitTe__5812160E");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.EquipmentTypeRooms)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Equipment__Room___59063A47");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.Property(e => e.GradeId).HasColumnName("Grade_id");

                entity.Property(e => e.GradeDate).HasColumnName("Grade_Date");

                entity.Property(e => e.GradeDescription).HasColumnName("Grade_Description");

                entity.Property(e => e.GradeScore).HasColumnName("Grade_Score");

                entity.Property(e => e.GradeStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Grade_Status");

                entity.Property(e => e.RegistraionScheduleId).HasColumnName("RegistraionSchedule_id");

                entity.Property(e => e.TeacherId).HasColumnName("Teacher_id");

                entity.Property(e => e.TeamId).HasColumnName("Team_id");

                entity.HasOne(d => d.RegistraionSchedule)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.RegistraionScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grade__Registrai__18EBB532");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grade__Grade_Sta__17F790F9");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Grade__Team_id__19DFD96B");
            });

            modelBuilder.Entity<GradeDescription>(entity =>
            {
                entity.ToTable("GradeDescription");

                entity.Property(e => e.GradeDescriptionId).HasColumnName("GradeDescription_id");

                entity.Property(e => e.ClassId).HasColumnName("Class_id");

                entity.Property(e => e.GradeDescriptionDescription).HasColumnName("GradeDescription_Description");

                entity.Property(e => e.GradeDescriptionScore).HasColumnName("GradeDescription_Score");

                entity.Property(e => e.GradeDescriptionStatus)
                    .HasMaxLength(50)
                    .HasColumnName("GradeDescription_Status");

                entity.Property(e => e.GradeId).HasColumnName("Grade_id");

                entity.Property(e => e.LabId).HasColumnName("Lab_id");

                entity.Property(e => e.StudentId).HasColumnName("Student_id");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.GradeDescriptions)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GradeDesc__Class__1EA48E88");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.GradeDescriptions)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GradeDesc__Grade__1CBC4616");

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.GradeDescriptions)
                    .HasForeignKey(d => d.LabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GradeDesc__Lab_i__1F98B2C1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.GradeDescriptions)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GradeDesc__Stude__1DB06A4F");
            });

            modelBuilder.Entity<Kit>(entity =>
            {
                entity.ToTable("Kit");

                entity.Property(e => e.KitId)
                    .HasMaxLength(50)
                    .HasColumnName("Kit_id");

                entity.Property(e => e.KitCreateDate)
                    .HasColumnType("date")
                    .HasColumnName("Kit_CreateDate");

                entity.Property(e => e.KitDescription).HasColumnName("Kit_Description");

                entity.Property(e => e.KitName)
                    .HasMaxLength(50)
                    .HasColumnName("Kit_Name");

                entity.Property(e => e.KitStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Kit_Status");

                entity.Property(e => e.KitTemplateId)
                    .HasMaxLength(50)
                    .HasColumnName("KitTemplate_id");

                entity.Property(e => e.KitUrlImg).HasColumnName("Kit_Url_Img");

                entity.Property(e => e.KitUrlQr).HasColumnName("Kit_Url_QR");

                entity.Property(e => e.RoomId).HasColumnName("Room_id");

                entity.HasOne(d => d.KitTemplate)
                    .WithMany(p => p.Kits)
                    .HasForeignKey(d => d.KitTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Kit__KitTemplate__44FF419A");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Kits)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Kit__Room_id__45F365D3");
            });

            modelBuilder.Entity<KitAccessory>(entity =>
            {
                entity.ToTable("Kit_Accessory");

                entity.Property(e => e.KitAccessoryId).HasColumnName("Kit_Accessory_id");

                entity.Property(e => e.AccessoryId).HasColumnName("Accessory_id");

                entity.Property(e => e.AccessoryQuantity).HasColumnName("Accessory_Quantity");

                entity.Property(e => e.KitAccessoryStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Kit_Accessory_Status");

                entity.Property(e => e.KitId)
                    .HasMaxLength(50)
                    .HasColumnName("Kit_id");

                entity.HasOne(d => d.Accessory)
                    .WithMany(p => p.KitAccessories)
                    .HasForeignKey(d => d.AccessoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Kit_Acces__Acces__49C3F6B7");

                entity.HasOne(d => d.Kit)
                    .WithMany(p => p.KitAccessories)
                    .HasForeignKey(d => d.KitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Kit_Acces__Kit_i__48CFD27E");
            });

            modelBuilder.Entity<KitTemplate>(entity =>
            {
                entity.ToTable("KitTemplate");

                entity.Property(e => e.KitTemplateId)
                    .HasMaxLength(50)
                    .HasColumnName("KitTemplate_id");

                entity.Property(e => e.KitTemplateDescription).HasColumnName("KitTemplate_Description");

                entity.Property(e => e.KitTemplateName)
                    .HasMaxLength(50)
                    .HasColumnName("KitTemplate_Name");

                entity.Property(e => e.KitTemplateQuantity).HasColumnName("KitTemplate_Quantity");

                entity.Property(e => e.KitTemplateStatus)
                    .HasMaxLength(50)
                    .HasColumnName("KitTemplate_Status");

                entity.Property(e => e.KitTemplateUrlImg).HasColumnName("KitTemplate_Url_Img");
            });

            modelBuilder.Entity<KitTemplateRoom>(entity =>
            {
                entity.ToTable("KitTemplate_Room");

                entity.Property(e => e.KitTemplateRoomId).HasColumnName("KitTemplate_Room_id");

                entity.Property(e => e.KitTemplateId)
                    .HasMaxLength(50)
                    .HasColumnName("KitTemplate_id");

                entity.Property(e => e.KitTemplateRoomQuantity).HasColumnName("KitTemplate_Room_Quantity");

                entity.Property(e => e.KitTemplateStatus)
                    .HasMaxLength(50)
                    .HasColumnName("KitTemplate_Status");

                entity.Property(e => e.RoomId).HasColumnName("Room_id");

                entity.HasOne(d => d.KitTemplate)
                    .WithMany(p => p.KitTemplateRooms)
                    .HasForeignKey(d => d.KitTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KitTempla__KitTe__3B75D760");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.KitTemplateRooms)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KitTempla__Room___3C69FB99");
            });

            modelBuilder.Entity<Lab>(entity =>
            {
                entity.ToTable("Lab");

                entity.Property(e => e.LabId).HasColumnName("Lab_id");

                entity.Property(e => e.LabName)
                    .HasMaxLength(50)
                    .HasColumnName("Lab_Name");

                entity.Property(e => e.LabNumberOfPractice).HasColumnName("Lab_NumberOfPractice");

                entity.Property(e => e.LabRequest).HasColumnName("Lab_Request");

                entity.Property(e => e.LabStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Lab_Status");

                entity.Property(e => e.LabTarget).HasColumnName("Lab_Target");

                entity.Property(e => e.SubjectId).HasColumnName("Subject_id");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Labs)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lab__Lab_Status__5FB337D6");
            });

            modelBuilder.Entity<LabEquipmentType>(entity =>
            {
                entity.ToTable("Lab_EquipmentType");

                entity.Property(e => e.LabEquipmentTypeId).HasColumnName("Lab_EquipmentType_id");

                entity.Property(e => e.EquipmentTypeId)
                    .HasMaxLength(50)
                    .HasColumnName("EquipmentType_id");

                entity.Property(e => e.LabEquipmentTypeStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Lab_EquipmentType_Status");

                entity.Property(e => e.LabId).HasColumnName("Lab_id");

                entity.HasOne(d => d.EquipmentType)
                    .WithMany(p => p.LabEquipmentTypes)
                    .HasForeignKey(d => d.EquipmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lab_Equip__Lab_E__6A30C649");

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.LabEquipmentTypes)
                    .HasForeignKey(d => d.LabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lab_Equip__Lab_i__6B24EA82");
            });

            modelBuilder.Entity<LabKitTemplate>(entity =>
            {
                entity.ToTable("Lab_KitTemplate");

                entity.Property(e => e.LabKitTemplateId).HasColumnName("Lab_KitTemplate_id");

                entity.Property(e => e.KitTemplateId)
                    .HasMaxLength(50)
                    .HasColumnName("KitTemplate_id");

                entity.Property(e => e.LabId).HasColumnName("Lab_id");

                entity.Property(e => e.LabKitTemplateStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Lab_KitTemplate_Status");

                entity.HasOne(d => d.KitTemplate)
                    .WithMany(p => p.LabKitTemplates)
                    .HasForeignKey(d => d.KitTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lab_KitTe__Lab_K__66603565");

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.LabKitTemplates)
                    .HasForeignKey(d => d.LabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lab_KitTe__Lab_i__6754599E");
            });

            modelBuilder.Entity<RegistraionSchedule>(entity =>
            {
                entity.ToTable("RegistraionSchedule");

                entity.Property(e => e.RegistraionScheduleId).HasColumnName("RegistraionSchedule_id");

                entity.Property(e => e.ClassId).HasColumnName("Class_id");

                entity.Property(e => e.LabId).HasColumnName("Lab_id");

                entity.Property(e => e.RegistraionScheduleCreateDate)
                    .HasColumnType("datetime2")
                    .HasColumnName("RegistraionSchedule_CreateDate");
                entity.Property(e => e.RegistraionScheduleCheckIn)
                    .HasColumnType("datetime2")
                    .HasColumnName("RegistraionSchedule_CheckIn");
                entity.Property(e => e.RegistraionScheduleCheckOut)
                    .HasColumnType("datetime2")
                    .HasColumnName("RegistraionSchedule_CheckOut");

                entity.Property(e => e.RegistraionScheduleDate)
                .HasColumnType("date")
                .HasColumnName("RegistraionSchedule_Date");

                entity.Property(e => e.RegistraionScheduleDescription).HasColumnName("RegistraionSchedule_Description");

                entity.Property(e => e.RegistraionScheduleName)
                    .HasMaxLength(50)
                    .HasColumnName("RegistraionSchedule_Name");
                entity.Property(e => e.RegistraionSchedule_Url_Img_Checkout)
                    .HasColumnName("RegistraionSchedule_Url_Img_Checkout");

                entity.Property(e => e.RegistraionScheduleNote).HasColumnName("RegistraionSchedule_Note");

                entity.Property(e => e.RegistraionScheduleStatus)
                    .HasMaxLength(50)
                    .HasColumnName("RegistraionSchedule_Status");

                entity.Property(e => e.RoomId).HasColumnName("Room_id");

                entity.Property(e => e.SlotId).HasColumnName("Slot_id");

                entity.Property(e => e.TeaacherId).HasColumnName("Teaacher_id");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.RegistraionSchedules)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Registrai__Class__0D7A0286");

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.RegistraionSchedules)
                    .HasForeignKey(d => d.LabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Registrai__Lab_i__0F624AF8");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RegistraionSchedules)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Registrai__Room___0E6E26BF");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.RegistraionSchedules)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Registrai__Slot___10566F31");

                entity.HasOne(d => d.Teaacher)
                    .WithMany(p => p.RegistraionSchedules)
                    .HasForeignKey(d => d.TeaacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Registrai__Regis__0C85DE4D");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId).HasColumnName("Report_id");

                entity.Property(e => e.EquipmentId)
                    .HasMaxLength(50)
                    .HasColumnName("Equipment_id");

                entity.Property(e => e.RegistraionScheduleId).HasColumnName("RegistraionSchedule_id");

                entity.Property(e => e.ReportCreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Report_CreateDate");

                entity.Property(e => e.ReportDescription).HasColumnName("Report_Description");
                entity.Property(e => e.Url_Img).HasColumnName("Url_Img");

                entity.Property(e => e.ReportNote).HasColumnName("Report_Note");

                entity.Property(e => e.ReportStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Report_Status");

                entity.Property(e => e.ReportTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Report_Title");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__Equipmen__151B244E");

                entity.HasOne(d => d.RegistraionSchedule)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.RegistraionScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__Registra__14270015");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__Report_S__1332DBDC");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomId).HasColumnName("Room_id");

                entity.Property(e => e.RoomName)
                    .HasMaxLength(50)
                    .HasColumnName("Room_Name");

                entity.Property(e => e.RoomStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Room_Status");
            });

            modelBuilder.Entity<RoomLab>(entity =>
            {
                entity.ToTable("Room_Lab");

                entity.Property(e => e.RoomLabId).HasColumnName("Room_Lab_id");

                entity.Property(e => e.LabId).HasColumnName("Lab_id");

                entity.Property(e => e.RoomId).HasColumnName("Room_id");

                entity.Property(e => e.RoomLabIdStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Room_Lab_id_Status");

                entity.HasOne(d => d.Lab)
                    .WithMany(p => p.RoomLabs)
                    .HasForeignKey(d => d.LabId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Room_Lab__Room_L__628FA481");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomLabs)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Room_Lab__Room_i__6383C8BA");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.ScheduleId).HasColumnName("Schedule_id");

                entity.Property(e => e.ClassId).HasColumnName("Class_id");

                entity.Property(e => e.ScheduleDate)
                    .HasColumnType("date")
                    .HasColumnName("Schedule_Date");

                entity.Property(e => e.ScheduleDescription).HasColumnName("Schedule_Description");

                entity.Property(e => e.ScheduleName)
                    .HasMaxLength(50)
                    .HasColumnName("Schedule_Name");

                entity.Property(e => e.ScheduleStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Schedule_Status");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Class___7B5B524B");
            });

            modelBuilder.Entity<ScheduleType>(entity =>
            {
                entity.ToTable("ScheduleType");

                entity.Property(e => e.ScheduleTypeId).HasColumnName("ScheduleType_id");

                entity.Property(e => e.ScheduleTypeDescription).HasColumnName("ScheduleType_Description");

                entity.Property(e => e.ScheduleTypeDow)
                    .HasMaxLength(50)
                    .HasColumnName("ScheduleType_DOW");

                entity.Property(e => e.ScheduleTypeName)
                    .HasMaxLength(50)
                    .HasColumnName("ScheduleType_Name");

                entity.Property(e => e.ScheduleTypeStatus)
                    .HasMaxLength(50)
                    .HasColumnName("ScheduleType_Status");

                entity.Property(e => e.SlotId).HasColumnName("Slot_id");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.ScheduleTypes)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ScheduleT__Slot___6FE99F9F");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("Semester");

                entity.Property(e => e.SemesterId).HasColumnName("Semester_id");

                entity.Property(e => e.SemesterDescription).HasColumnName("Semester_Description");

                entity.Property(e => e.SemesterEndDate)
                    .HasColumnType("date")
                    .HasColumnName("Semester_EndDate");

                entity.Property(e => e.SemesterName)
                    .HasMaxLength(50)
                    .HasColumnName("Semester_Name");

                entity.Property(e => e.SemesterStartDate)
                    .HasColumnType("date")
                    .HasColumnName("Semester_StartDate");

                entity.Property(e => e.SemesterStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Semester_Status");
            });

            modelBuilder.Entity<SemesterSubject>(entity =>
            {
                entity.ToTable("Semester_Subject");

                entity.Property(e => e.SemesterSubjectId).HasColumnName("Semester_Subject_id");

                entity.Property(e => e.SemesterId).HasColumnName("Semester_id");

                entity.Property(e => e.SemesterSubject1)
                    .HasMaxLength(50)
                    .HasColumnName("Semester_Subject");

                entity.Property(e => e.SubjectId).HasColumnName("Subject_id");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.SemesterSubjects)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Semester___Semes__534D60F1");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SemesterSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Semester___Subje__52593CB8");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("Slot");

                entity.Property(e => e.SlotId).HasColumnName("Slot_id");

                entity.Property(e => e.SlotDescription).HasColumnName("Slot_Description");

                entity.Property(e => e.SlotEndTime)
                    .HasMaxLength(50)
                    .HasColumnName("Slot_EndTime");

                entity.Property(e => e.SlotName)
                    .HasMaxLength(50)
                    .HasColumnName("Slot_Name");

                entity.Property(e => e.SlotStartTime)
                    .HasMaxLength(50)
                    .HasColumnName("Slot_StartTime");

                entity.Property(e => e.SlotStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Slot_Status");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject");

                entity.Property(e => e.SubjectId).HasColumnName("Subject_id");

                entity.Property(e => e.SubjectCode)
                    .HasMaxLength(50)
                    .HasColumnName("Subject_Code");

                entity.Property(e => e.SubjectDescription).HasColumnName("Subject_Description");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(100)
                    .HasColumnName("Subject_Name");

                entity.Property(e => e.SubjectStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Subject_Status");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Team");

                entity.Property(e => e.TeamId).HasColumnName("Team_id");

                entity.Property(e => e.ClassId).HasColumnName("Class_id");

                entity.Property(e => e.TeamDescription).HasColumnName("Team_Description");

                entity.Property(e => e.TeamName)
                    .HasMaxLength(50)
                    .HasColumnName("Team_Name");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team__Class_id__7E37BEF6");
            });

            modelBuilder.Entity<TeamEquipment>(entity =>
            {
                entity.ToTable("Team_Equipment");

                entity.Property(e => e.TeamEquipmentId).HasColumnName("Team_Equipment_id");

                entity.Property(e => e.EquipmentId)
                    .HasMaxLength(50)
                    .HasColumnName("Equipment_id");

                entity.Property(e => e.TeamEquipmentDateBorrow)
                .HasColumnType("datetime2")
                .HasColumnName("Team_Equipment_DateBorrow");

                entity.Property(e => e.TeamEquipmentDateGiveBack)
                .HasColumnType("datetime2")
                .HasColumnName("Team_Equipment_DateGiveBack");

                entity.Property(e => e.TeamEquipmentDescription).HasColumnName("Team_Equipment_Description");

                entity.Property(e => e.TeamEquipmentName)
                    .HasMaxLength(50)
                    .HasColumnName("Team_Equipment_Name");

                entity.Property(e => e.TeamEquipmentStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Team_Equipment_Status");

                entity.Property(e => e.TeamId).HasColumnName("Team_id");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.TeamEquipments)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team_Equi__Equip__05D8E0BE");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamEquipments)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team_Equi__Team___04E4BC85");
            });

            modelBuilder.Entity<TeamKit>(entity =>
            {
                entity.ToTable("Team_Kit");

                entity.Property(e => e.TeamKitId).HasColumnName("Team_Kit_id");

                entity.Property(e => e.KitId)
                    .HasMaxLength(50)
                    .HasColumnName("Kit_id");

                entity.Property(e => e.TeamId).HasColumnName("Team_id");

                entity.Property(e => e.TeamKitDateBorrow)
                    .HasColumnType("datetime2")
                    .HasColumnName("Team_Kit_DateBorrow");

                entity.Property(e => e.TeamKitDateGiveBack)
                    .HasColumnType("datetime2")
                    .HasColumnName("Team_Kit_DateGiveBack");

                entity.Property(e => e.TeamKitDescription).HasColumnName("Team_Kit_Description");

                entity.Property(e => e.TeamKitName)
                    .HasMaxLength(50)
                    .HasColumnName("Team_Kit_Name");

                entity.Property(e => e.TeamKitStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Team_Kit_Status");

                entity.HasOne(d => d.Kit)
                    .WithMany(p => p.TeamKits)
                    .HasForeignKey(d => d.KitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team_Kit__Kit_id__09A971A2");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamKits)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team_Kit__Team_i__08B54D69");
            });

            modelBuilder.Entity<TeamUser>(entity =>
            {
                entity.ToTable("Team_User");

                entity.Property(e => e.TeamUserId).HasColumnName("Team_User_id");

                entity.Property(e => e.TeamId).HasColumnName("Team_id");

                entity.Property(e => e.TeamUserStatus)
                    .HasMaxLength(50)
                    .HasColumnName("Team_User_Status");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamUsers)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team_User__Team___01142BA1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TeamUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team_User__User___02084FDA");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("User_id");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(100)
                    .HasColumnName("User_Email");

                entity.Property(e => e.UserFullName)
                    .HasMaxLength(50)
                    .HasColumnName("User_FullName");

                entity.Property(e => e.UserNumberCode)
                    .HasMaxLength(50)
                    .HasColumnName("User_NumberCode");

                entity.Property(e => e.UserRoleName)
                    .HasMaxLength(50)
                    .HasColumnName("User_RoleName");

                entity.Property(e => e.UserRollNumber)
                    .HasMaxLength(50)
                    .HasColumnName("User_RollNumber");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
