using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MusicTracker.Models
{
  public partial class DatabaseContext : DbContext
  {

    // Add Database tables here
    public DbSet<Song> Songs { get; set; }
    public DbSet<Band> Bands { get; set; }
    public DbSet<Album> Albums { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseNpgsql("server=localhost;database=MusicTrackerDatabase;User Id=postgres;Password=dev");
      }
    }
  }
}
