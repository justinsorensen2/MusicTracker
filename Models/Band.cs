using System.Collections.Generic;

namespace MusicTracker.Models
{
  public class Band
  {
    public int Id { get; set; }
    public string BandName { get; set; }
    public string Country { get; set; }
    public string NumberOfMembers { get; set; }
    public string Website { get; set; }
    public string Style { get; set; }
    public bool IsSigned { get; set; }
    public string Contact { get; set; }
    public string ContactPhoneNumber { get; set; }
    //navigation
    public List<Album> Albums { get; set; } = new List<Album>();
  }
}