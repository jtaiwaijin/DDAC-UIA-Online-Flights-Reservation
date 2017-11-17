using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using uiaflights.Data;

namespace uiaflights.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20171101182751_passengerRequired")]
    partial class passengerRequired
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("uiaflights.Models.Airport", b =>
                {
                    b.Property<int>("AirportID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("IATA");

                    b.Property<string>("Name");

                    b.Property<double>("Timezone");

                    b.HasKey("AirportID");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("uiaflights.Models.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("uiaflights.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApplicationUser");
                });

            modelBuilder.Entity("uiaflights.Models.Booking", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CustomerId");

                    b.Property<string>("Status");

                    b.Property<decimal?>("Total")
                        .HasColumnType("money");

                    b.HasKey("ID");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("uiaflights.Models.Flight", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ArrivalDateTime");

                    b.Property<DateTime>("DepartureDateTime");

                    b.Property<int>("DestAirportID");

                    b.Property<string>("FlightNo");

                    b.Property<int>("OriginAirportID");

                    b.Property<int>("PlaneID");

                    b.HasKey("ID");

                    b.HasIndex("DestAirportID");

                    b.HasIndex("OriginAirportID");

                    b.HasIndex("PlaneID");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("uiaflights.Models.Passenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DOB");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Nationality")
                        .IsRequired();

                    b.Property<string>("PassportNo")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.HasKey("Id");

                    b.ToTable("Passengers");
                });

            modelBuilder.Entity("uiaflights.Models.Payment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<int>("BookingID");

                    b.HasKey("ID");

                    b.HasIndex("BookingID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("uiaflights.Models.Plane", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BusCap");

                    b.Property<int>("EconCap");

                    b.Property<int>("FirstCap");

                    b.Property<string>("Make");

                    b.Property<string>("Model");

                    b.Property<string>("TailNo");

                    b.HasKey("ID");

                    b.ToTable("Planes");
                });

            modelBuilder.Entity("uiaflights.Models.Tariff", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AdultFare")
                        .HasColumnType("money");

                    b.Property<string>("CabinCl");

                    b.Property<int>("FlightID");

                    b.HasKey("ID");

                    b.HasIndex("FlightID");

                    b.ToTable("Tariffs");
                });

            modelBuilder.Entity("uiaflights.Models.Ticket", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BookingID");

                    b.Property<string>("CabinCl");

                    b.Property<int>("FlightID");

                    b.Property<int>("PassengerId");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("ID");

                    b.HasIndex("BookingID");

                    b.HasIndex("FlightID");

                    b.HasIndex("PassengerId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("uiaflights.Models.Customer", b =>
                {
                    b.HasBaseType("uiaflights.Models.ApplicationUser");

                    b.Property<string>("AddLine1");

                    b.Property<string>("AddLine2");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime>("DOB");

                    b.Property<int>("Postcode");

                    b.Property<string>("Region");

                    b.ToTable("Customer");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("uiaflights.Models.ApplicationRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("uiaflights.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("uiaflights.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("uiaflights.Models.ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("uiaflights.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("uiaflights.Models.Booking", b =>
                {
                    b.HasOne("uiaflights.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("uiaflights.Models.Flight", b =>
                {
                    b.HasOne("uiaflights.Models.Airport", "Dest")
                        .WithMany("Arriving")
                        .HasForeignKey("DestAirportID");

                    b.HasOne("uiaflights.Models.Airport", "Origin")
                        .WithMany("Departing")
                        .HasForeignKey("OriginAirportID");

                    b.HasOne("uiaflights.Models.Plane", "Plane")
                        .WithMany()
                        .HasForeignKey("PlaneID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("uiaflights.Models.Payment", b =>
                {
                    b.HasOne("uiaflights.Models.Booking", "Booking")
                        .WithMany()
                        .HasForeignKey("BookingID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("uiaflights.Models.Tariff", b =>
                {
                    b.HasOne("uiaflights.Models.Flight", "Flight")
                        .WithMany("Tariff")
                        .HasForeignKey("FlightID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("uiaflights.Models.Ticket", b =>
                {
                    b.HasOne("uiaflights.Models.Booking", "Booking")
                        .WithMany("Tickets")
                        .HasForeignKey("BookingID");

                    b.HasOne("uiaflights.Models.Flight", "Flight")
                        .WithMany("Tickets")
                        .HasForeignKey("FlightID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("uiaflights.Models.Passenger", "Passenger")
                        .WithMany("Tickets")
                        .HasForeignKey("PassengerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
