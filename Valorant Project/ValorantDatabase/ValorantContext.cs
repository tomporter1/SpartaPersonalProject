using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ValorantDatabase
{
    public partial class ValorantContext : DbContext
    {
        public ValorantContext()
        {
        }

        public ValorantContext(DbContextOptions<ValorantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AgentType> AgentType { get; set; }
        public virtual DbSet<Agents> Agents { get; set; }
        public virtual DbSet<GameLogs> GameLogs { get; set; }
        public virtual DbSet<Maps> Maps { get; set; }
        public virtual DbSet<GameModes> GameModes { get; set; }
        public virtual DbSet<Ranks> Ranks { get; set; }
        public virtual DbSet<RankAdjustments> RankAdjustments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Valorant;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgentType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK_TypeID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
                
                entity.Property(e => e.ImagePath)
                    .HasMaxLength(35)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ranks>(entity =>
            {
                entity.HasKey(e => e.RankID)
                    .HasName("PK_RankID");

                entity.Property(e => e.RankID).HasColumnName("RankID");

                entity.Property(e => e.RankName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(35)
                    .IsUnicode(false);
            });
            
            modelBuilder.Entity<RankAdjustments>(entity =>
            {
                entity.HasKey(e => e.AdjustmentID)
                    .HasName("PK_AdjustmentID");

                entity.Property(e => e.AdjustmentID).HasColumnName("AdjustmentID");

                entity.Property(e => e.AdjustmentName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(35)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Agents>(entity =>
            {
                entity.HasKey(e => e.AgentId)
                    .HasName("PK_AgentID");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.AbilityOneDiscription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AbilityOneName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.AbilityTwoDiscription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AbilityTwoName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.AgentName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AgentTypeId).HasColumnName("AgentTypeID");

                entity.Property(e => e.Bio)
                    .HasMaxLength(700)
                    .IsUnicode(false);
                
                entity.Property(e => e.ImagePath)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.SignatureAbilityDiscription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SignatureAbilityName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UltamateAbilityDiscription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UltamateAbilityName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.AgentType)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.AgentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgentType");
            });

            modelBuilder.Entity<GameLogs>(entity =>
            {
                entity.HasKey(e => e.GameId);

                entity.Property(e => e.GameId).HasColumnName("GameID");

                entity.Property(e => e.Adr).HasColumnName("ADR");

                entity.Property(e => e.AgentId).HasColumnName("AgentID");

                entity.Property(e => e.DateLogged).HasColumnType("datetime");

                entity.Property(e => e.MapId).HasColumnName("MapID");

                entity.Property(e => e.RankID).HasColumnName("RankID");

                entity.Property(e => e.RankAdjustmentID).HasColumnName("RankAdjustmentID");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.GameLogs)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agent");
                
                entity.HasOne(d => d.GameMode)
                    .WithMany(p => p.GameLogs)
                    .HasForeignKey(d => d.ModeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GameMode");

                entity.HasOne(d => d.Map)
                    .WithMany(p => p.GameLogs)
                    .HasForeignKey(d => d.MapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Map");
                
                entity.HasOne(d => d.Rank)
                    .WithMany(p => p.GameLogs)
                    .HasForeignKey(d => d.RankID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rank");
                
                entity.HasOne(d => d.RankAdjustment)
                    .WithMany(p => p.GameLogs)
                    .HasForeignKey(d => d.RankAdjustmentID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RankAdjustment");
            });

            modelBuilder.Entity<Maps>(entity =>
            {
                entity.HasKey(e => e.MapId)
                    .HasName("PK_MapID");

                entity.Property(e => e.MapId).HasColumnName("MapID");

                entity.Property(e => e.MapName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
                
                entity.Property(e => e.ImagePath)
                    .HasMaxLength(35)
                    .IsUnicode(false);
                
                entity.Property(e => e.LayoutImagePath)
                    .HasMaxLength(35)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GameModes>(entity =>
            {
                entity.HasKey(e => e.ModeID)
                    .HasName("PK_ModeID");

                entity.Property(e => e.ModeID).HasColumnName("ModeID");

                entity.Property(e => e.ModeName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModeDiscription)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}