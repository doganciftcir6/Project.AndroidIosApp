﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.AndroidIosApp.DataAccess.Contexts.EntityFramework;

namespace Project.AndroidIosApp.DataAccess.Migrations
{
    [DbContext(typeof(AndroidIosContext))]
    [Migration("20230218113619_RemoveBlogRelationsFromCommentTable")]
    partial class RemoveBlogRelationsFromCommentTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project.AndroidIosApp.Entities.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Subtitle")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectUserId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("ProjectUserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(3000)
                        .HasColumnType("nvarchar(3000)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Skype")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CPU")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("DeviceTypeId")
                        .HasColumnType("int");

                    b.Property<int>("GPU")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MEM")
                        .HasColumnType("int");

                    b.Property<int>("OSId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("ReleaseYear")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("TotalScore")
                        .HasColumnType("int");

                    b.Property<int>("UX")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeviceTypeId");

                    b.HasIndex("OSId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.DeviceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("DeviceTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Definition = "Phone",
                            Status = true
                        },
                        new
                        {
                            Id = 2,
                            Definition = "Tablet",
                            Status = true
                        });
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Genders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Definition = "Erkek",
                            Status = true
                        },
                        new
                        {
                            Id = 2,
                            Definition = "Kadın",
                            Status = true
                        },
                        new
                        {
                            Id = 3,
                            Definition = "Belirtmek İstemiyorum",
                            Status = true
                        });
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.OS", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("GetOS");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Definition = "Ios",
                            Status = true
                        },
                        new
                        {
                            Id = 2,
                            Definition = "Android",
                            Status = true
                        });
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.ProjectRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("ProjectRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Definition = "Admin",
                            Status = true
                        },
                        new
                        {
                            Id = 2,
                            Definition = "Member",
                            Status = true
                        },
                        new
                        {
                            Id = 3,
                            Definition = "SupportUser",
                            Status = true
                        });
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.ProjectUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordVerify")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.ToTable("ProjectUsers");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.ProjectUserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProjectRoleId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectUserId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ProjectRoleId");

                    b.HasIndex("ProjectUserId", "ProjectRoleId")
                        .IsUnique();

                    b.ToTable("ProjectUserRoles");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.SocialMedia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("SocialMedias");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.Support", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectUserId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("ProjectUserId");

                    b.ToTable("Supports");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.SupportUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("SupportEmail")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("SupportImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupportLastname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SupportName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SupportPhone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("SupportUsers");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.SupportUserSupport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("SupportId")
                        .HasColumnType("int");

                    b.Property<int>("SupportUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupportUserId");

                    b.HasIndex("SupportId", "SupportUserId")
                        .IsUnique();

                    b.ToTable("SupportUserSupports");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.Comment", b =>
                {
                    b.HasOne("Project.AndroidIosApp.Entities.Device", "Device")
                        .WithMany("Comments")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.AndroidIosApp.Entities.ProjectUser", "ProjectUser")
                        .WithMany("Comments")
                        .HasForeignKey("ProjectUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("ProjectUser");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.Device", b =>
                {
                    b.HasOne("Project.AndroidIosApp.Entities.DeviceType", "DeviceType")
                        .WithMany("Devices")
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.AndroidIosApp.Entities.OS", "OS")
                        .WithMany("Devices")
                        .HasForeignKey("OSId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeviceType");

                    b.Navigation("OS");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.ProjectUser", b =>
                {
                    b.HasOne("Project.AndroidIosApp.Entities.Gender", "Gender")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.ProjectUserRole", b =>
                {
                    b.HasOne("Project.AndroidIosApp.Entities.ProjectRole", "ProjectRole")
                        .WithMany("ProjectUserRoles")
                        .HasForeignKey("ProjectRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.AndroidIosApp.Entities.ProjectUser", "ProjectUser")
                        .WithMany("ProjectUserRoles")
                        .HasForeignKey("ProjectUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectRole");

                    b.Navigation("ProjectUser");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.Support", b =>
                {
                    b.HasOne("Project.AndroidIosApp.Entities.Device", "Device")
                        .WithMany("Supports")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.AndroidIosApp.Entities.ProjectUser", "ProjectUser")
                        .WithMany("Supports")
                        .HasForeignKey("ProjectUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("ProjectUser");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.SupportUserSupport", b =>
                {
                    b.HasOne("Project.AndroidIosApp.Entities.Support", "Support")
                        .WithMany("SupportUserSupports")
                        .HasForeignKey("SupportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.AndroidIosApp.Entities.SupportUser", "SupportUser")
                        .WithMany("SupportUserSupports")
                        .HasForeignKey("SupportUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Support");

                    b.Navigation("SupportUser");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.Device", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Supports");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.DeviceType", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.Gender", b =>
                {
                    b.Navigation("ProjectUsers");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.OS", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.ProjectRole", b =>
                {
                    b.Navigation("ProjectUserRoles");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.ProjectUser", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("ProjectUserRoles");

                    b.Navigation("Supports");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.Support", b =>
                {
                    b.Navigation("SupportUserSupports");
                });

            modelBuilder.Entity("Project.AndroidIosApp.Entities.SupportUser", b =>
                {
                    b.Navigation("SupportUserSupports");
                });
#pragma warning restore 612, 618
        }
    }
}
