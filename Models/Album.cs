using System;
using System.Collections.Generic;

namespace MusicTracker.Models
{
  public class Album
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsExplicit { get; set; }
    public DateTime ReleaseDate { get; set; }
    //navigation
    public int BandId { get; set; }
    public Band Band { get; set; }

    public List<Song> Songs { get; set; } = new List<Song>();
  }
}