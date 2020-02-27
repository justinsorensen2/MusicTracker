namespace MusicTracker.Models
{
  public class Song
  {
    public int Id { get; set; }
    public string SongTitle { get; set; }
    public string Lyrics { get; set; }
    public string Length { get; set; }
    public string Genre { get; set; }
    //navigation
    public int BandId { get; set; }
    public Band Band { get; set; }

  }
}