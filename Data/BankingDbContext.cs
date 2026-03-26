using System;
using System.Collections.Generic;
using BankingService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingService.API.Data;

public partial class BankingDbContext : DbContext
{
    public BankingDbContext()
    {
    }

    public BankingDbContext(DbContextOptions<BankingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=3VMVDB3;Database=BankingDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA5A6C8B8E5CF");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6B90FFA764");

            entity.Property(e => e.TransactionDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Account).WithMany(p => p.Transactions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Accounts");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
