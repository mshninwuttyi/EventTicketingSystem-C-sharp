using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EventTicketingSystem.CSharp.Database.AppDbContext;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAdmin> TblAdmins { get; set; }

    public virtual DbSet<TblBusinessemail> TblBusinessemails { get; set; }

    public virtual DbSet<TblBusinessowner> TblBusinessowners { get; set; }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblEvent> TblEvents { get; set; }

    public virtual DbSet<TblTicket> TblTickets { get; set; }

    public virtual DbSet<TblTicketprice> TblTicketprices { get; set; }

    public virtual DbSet<TblTickettype> TblTickettypes { get; set; }

    public virtual DbSet<TblTransaction> TblTransactions { get; set; }

    public virtual DbSet<TblTransactionticket> TblTransactiontickets { get; set; }

    public virtual DbSet<TblVenue> TblVenues { get; set; }

    public virtual DbSet<TblVenuetype> TblVenuetypes { get; set; }

    public virtual DbSet<TblVerification> TblVerifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAdmin>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_admin");

            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Usercode)
                .HasColumnType("character varying")
                .HasColumnName("usercode");
            entity.Property(e => e.Userid)
                .HasColumnType("character varying")
                .HasColumnName("userid");
            entity.Property(e => e.Username)
                .HasColumnType("character varying")
                .HasColumnName("username");
        });

        modelBuilder.Entity<TblBusinessemail>(entity =>
        {
            entity.HasKey(e => e.Businessemailid).HasName("tbl_businessemail_pkey");

            entity.ToTable("tbl_businessemail");

            entity.Property(e => e.Businessemailid)
                .HasColumnType("character varying")
                .HasColumnName("businessemailid");
            entity.Property(e => e.Businessemailcode)
                .HasColumnType("character varying")
                .HasColumnName("businessemailcode");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasColumnType("character varying")
                .HasColumnName("fullname");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
        });

        modelBuilder.Entity<TblBusinessowner>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_businessowner");

            entity.Property(e => e.Businessownercode)
                .HasColumnType("character varying")
                .HasColumnName("businessownercode");
            entity.Property(e => e.Businessownerid)
                .HasColumnType("character varying")
                .HasColumnName("businessownerid");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Phonenumber)
                .HasColumnType("character varying")
                .HasColumnName("phonenumber");
        });

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_category");

            entity.Property(e => e.Categorycode)
                .HasColumnType("character varying")
                .HasColumnName("categorycode");
            entity.Property(e => e.Categoryid)
                .HasColumnType("character varying")
                .HasColumnName("categoryid");
            entity.Property(e => e.Categoryname)
                .HasColumnType("character varying")
                .HasColumnName("categoryname");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
        });

        modelBuilder.Entity<TblEvent>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_event");

            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Businessownercode)
                .HasColumnType("character varying")
                .HasColumnName("businessownercode");
            entity.Property(e => e.Categorycode)
                .HasColumnType("character varying")
                .HasColumnName("categorycode");
            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Enddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enddate");
            entity.Property(e => e.Eventcode)
                .HasColumnType("character varying")
                .HasColumnName("eventcode");
            entity.Property(e => e.Eventid)
                .HasColumnType("character varying")
                .HasColumnName("eventid");
            entity.Property(e => e.Eventimage)
                .HasColumnType("character varying")
                .HasColumnName("eventimage");
            entity.Property(e => e.Eventname)
                .HasColumnType("character varying")
                .HasColumnName("eventname");
            entity.Property(e => e.Eventstatus)
                .HasColumnType("character varying")
                .HasColumnName("eventstatus");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
            entity.Property(e => e.Soldoutcount).HasColumnName("soldoutcount");
            entity.Property(e => e.Startdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdate");
            entity.Property(e => e.Totalticketquantity).HasColumnName("totalticketquantity");
        });

        modelBuilder.Entity<TblTicket>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_ticket");

            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Isused).HasColumnName("isused");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
            entity.Property(e => e.Ticketcode)
                .HasColumnType("character varying")
                .HasColumnName("ticketcode");
            entity.Property(e => e.Ticketid)
                .HasColumnType("character varying")
                .HasColumnName("ticketid");
            entity.Property(e => e.Ticketpricecode)
                .HasColumnType("character varying")
                .HasColumnName("ticketpricecode");
        });

        modelBuilder.Entity<TblTicketprice>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_ticketprice");

            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Eventcode)
                .HasColumnType("character varying")
                .HasColumnName("eventcode");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
            entity.Property(e => e.Ticketprice)
                .HasPrecision(20, 2)
                .HasColumnName("ticketprice");
            entity.Property(e => e.Ticketpricecode)
                .HasColumnType("character varying")
                .HasColumnName("ticketpricecode");
            entity.Property(e => e.Ticketpriceid)
                .HasColumnType("character varying")
                .HasColumnName("ticketpriceid");
            entity.Property(e => e.Ticketquantity).HasColumnName("ticketquantity");
            entity.Property(e => e.Tickettypecode)
                .HasColumnType("character varying")
                .HasColumnName("tickettypecode");
        });

        modelBuilder.Entity<TblTickettype>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_tickettype");

            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
            entity.Property(e => e.Tickettypecode)
                .HasColumnType("character varying")
                .HasColumnName("tickettypecode");
            entity.Property(e => e.Tickettypeid)
                .HasColumnType("character varying")
                .HasColumnName("tickettypeid");
            entity.Property(e => e.Tickettypename)
                .HasColumnType("character varying")
                .HasColumnName("tickettypename");
        });

        modelBuilder.Entity<TblTransaction>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_transaction");

            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Eventcode)
                .HasColumnType("character varying")
                .HasColumnName("eventcode");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
            entity.Property(e => e.Paymenttype)
                .HasColumnType("character varying")
                .HasColumnName("paymenttype");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("status");
            entity.Property(e => e.Totalamount)
                .HasPrecision(20, 2)
                .HasColumnName("totalamount");
            entity.Property(e => e.Transactioncode)
                .HasColumnType("character varying")
                .HasColumnName("transactioncode");
            entity.Property(e => e.Transactiondate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("transactiondate");
            entity.Property(e => e.Transactionid)
                .HasColumnType("character varying")
                .HasColumnName("transactionid");
        });

        modelBuilder.Entity<TblTransactionticket>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_transactionticket");

            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
            entity.Property(e => e.Price)
                .HasPrecision(20, 2)
                .HasColumnName("price");
            entity.Property(e => e.Qrstring)
                .HasColumnType("character varying")
                .HasColumnName("qrstring");
            entity.Property(e => e.Ticketcode)
                .HasColumnType("character varying")
                .HasColumnName("ticketcode");
            entity.Property(e => e.Transactioncode)
                .HasColumnType("character varying")
                .HasColumnName("transactioncode");
            entity.Property(e => e.Transactionticketid)
                .HasColumnType("character varying")
                .HasColumnName("transactionticketid");
        });

        modelBuilder.Entity<TblVenue>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_venue");

            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
            entity.Property(e => e.Venueaddons).HasColumnName("venueaddons");
            entity.Property(e => e.Venueaddress)
                .HasColumnType("character varying")
                .HasColumnName("venueaddress");
            entity.Property(e => e.Venuecapacity).HasColumnName("venuecapacity");
            entity.Property(e => e.Venuecode)
                .HasColumnType("character varying")
                .HasColumnName("venuecode");
            entity.Property(e => e.Venuedescription).HasColumnName("venuedescription");
            entity.Property(e => e.Venuedetailcode)
                .HasColumnType("character varying")
                .HasColumnName("venuedetailcode");
            entity.Property(e => e.Venuefacilities).HasColumnName("venuefacilities");
            entity.Property(e => e.Venueid)
                .HasColumnType("character varying")
                .HasColumnName("venueid");
            entity.Property(e => e.Venuename)
                .HasColumnType("character varying")
                .HasColumnName("venuename");
            entity.Property(e => e.Venuetypecode)
                .HasColumnType("character varying")
                .HasColumnName("venuetypecode");
        });

        modelBuilder.Entity<TblVenuetype>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_venuetype");

            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
            entity.Property(e => e.Venuetypecode)
                .HasColumnType("character varying")
                .HasColumnName("venuetypecode");
            entity.Property(e => e.Venuetypeid)
                .HasColumnType("character varying")
                .HasColumnName("venuetypeid");
            entity.Property(e => e.Venuetypename)
                .HasColumnType("character varying")
                .HasColumnName("venuetypename");
        });

        modelBuilder.Entity<TblVerification>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_verification");

            entity.Property(e => e.Createdat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby)
                .HasColumnType("character varying")
                .HasColumnName("createdby");
            entity.Property(e => e.Deleteflag).HasColumnName("deleteflag");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Modifiedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedat");
            entity.Property(e => e.Modifiedby)
                .HasColumnType("character varying")
                .HasColumnName("modifiedby");
            entity.Property(e => e.Verificationcode)
                .HasColumnType("character varying")
                .HasColumnName("verificationcode");
            entity.Property(e => e.Verificationid)
                .HasColumnType("character varying")
                .HasColumnName("verificationid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
