
using Advanced_Querying_LAB.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Advanced_Querying_LAB.Data;

public partial class MusicHubContext : DbContext
{
    public MusicHubContext()
    {
    }

    public MusicHubContext(DbContextOptions<MusicHubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; } = null!;
    public virtual DbSet<Performer> Performers { get; set; } = null!;
    public virtual DbSet<Producer> Producers { get; set; } = null!;
    public virtual DbSet<Song> Songs { get; set; } = null!;
    public virtual DbSet<Writer> Writers { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer("Server=DESKTOP-6ADIQGK\\SQLEXPRESS;Database=MusicHub;Trusted_Connection=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasIndex(e => e.ProducerId, "IX_Albums_ProducerId");

            entity.Property(e => e.Name).HasMaxLength(40);

            entity.Property(e => e.ReleaseDate).HasColumnType("date");

            entity.HasOne(d => d.Producer)
                .WithMany(p => p.Albums)
                .HasForeignKey(d => d.ProducerId);
        });

        modelBuilder.Entity<Performer>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(20);

            entity.Property(e => e.LastName).HasMaxLength(20);

            entity.Property(e => e.NetWorth).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasIndex(e => e.AlbumId, "IX_Songs_AlbumId");

            entity.HasIndex(e => e.WriterId, "IX_Songs_WriterId");

            entity.Property(e => e.CreatedOn).HasColumnType("date");

            entity.Property(e => e.Name).HasMaxLength(20);

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Album)
                .WithMany(p => p.Songs)
                .HasForeignKey(d => d.AlbumId);

            entity.HasOne(d => d.Writer)
                .WithMany(p => p.Songs)
                .HasForeignKey(d => d.WriterId);

            entity.HasMany(d => d.Performers)
                .WithMany(p => p.Songs)
                .UsingEntity<Dictionary<string, object>>(
                    "SongsPerformer",
                    l => l.HasOne<Performer>().WithMany().HasForeignKey("PerformerId"),
                    r => r.HasOne<Song>().WithMany().HasForeignKey("SongId"),
                    j =>
                    {
                        j.HasKey("SongId", "PerformerId");

                        j.ToTable("SongsPerformers");

                        j.HasIndex(new[] { "PerformerId" }, "IX_SongsPerformers_PerformerId");
                    });
        });

        modelBuilder.Entity<Writer>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
