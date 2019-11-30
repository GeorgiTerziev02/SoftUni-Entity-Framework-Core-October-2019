namespace MusicHub.Data
{
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data.Models;

    public class MusicHubDbContext : DbContext
    {
        public MusicHubDbContext()
        {
        }

        public MusicHubDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Performer> Performers { get; set; }

        public DbSet<Producer> Producers { get; set; }

        public DbSet<Song> Songs { get; set; }

        public DbSet<SongPerformer> SongsPerformers { get; set; }

        public DbSet<Writer> Writers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SongPerformer>(b =>
            {
                b.HasKey(k => new { k.SongId, k.PerformerId });

                b.HasOne(s => s.Performer)
                .WithMany(p => p.PerformerSongs)
                .HasForeignKey(s => s.PerformerId);
                
                b.HasOne(sg => sg.Song)
                .WithMany(s => s.SongPerformers)
                .HasForeignKey(sg => sg.SongId);
            });

            builder.Entity<Album>(b =>
            {
                b.HasOne(a => a.Producer)
                .WithMany(p => p.Albums)
                .HasForeignKey(a => a.ProducerId);
            });
            
            builder.Entity<Song>(b =>
            {
                b.HasOne(s => s.Album)
                .WithMany(a=>a.Songs)
                .HasForeignKey(s => s.AlbumId);

                b.HasOne(s => s.Writer)
                .WithMany(w => w.Songs)
                .HasForeignKey(s => s.WriterId);
            });
        }
    }
}
