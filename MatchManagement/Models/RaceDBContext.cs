using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MatchManagement.Models
{
    public partial class RaceDBContext : DbContext
    {
        public RaceDBContext()
        {
        }

        public RaceDBContext(DbContextOptions<RaceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<MatchOdds> MatchOdds { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.MatchId).HasColumnName("match_id");

                entity.Property(e => e.MatchDate)
                    .HasColumnName("match_date")
                    .HasColumnType("date");

                entity.Property(e => e.MatchDescription)
                    .IsRequired()
                    .HasColumnName("match_description")
                    .HasMaxLength(250);

                entity.Property(e => e.MatchTime).HasColumnName("match_time");

                entity.Property(e => e.Sport)
                    .IsRequired()
                    .HasColumnName("sport")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TeamA)
                    .IsRequired()
                    .HasColumnName("team_a")
                    .HasMaxLength(250);

                entity.Property(e => e.TeamB)
                    .IsRequired()
                    .HasColumnName("team_b")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<MatchOdds>(entity =>
            {
                entity.Property(e => e.MatchOddsId).HasColumnName("match_odds_id");

                entity.Property(e => e.MatchId).HasColumnName("match_id");

                entity.Property(e => e.Odd)
                    .HasColumnName("odd")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Specifier)
                    .IsRequired()
                    .HasColumnName("specifier")
                    .HasMaxLength(125);

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchOdds)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatchOdds_Match");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
