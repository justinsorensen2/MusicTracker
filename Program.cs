using System;
using MusicTracker.Models;
using System.Linq;
using ConsoleTools;

namespace MusicTracker
{
  class Program
  {

    public static void Main(string[] args)
    {
      var tracker = new Tracker();
      tracker.PopulateDb();
      //create and config console menu
      var menu = new ConsoleMenu(args, level: 0)
        .Add("Add a Band", () => tracker.AddBand())
        .Add("Cancel a Contract", () => tracker.CancelBandContract())
        .Add("Renew a Contract", () => tracker.RenewBandContract())
        .Add("Produce an Album", () => tracker.AddAnAlbum())
        .Add("View Albums by Band", () => tracker.ViewAlbumsPerBand())
        .Add("View Albums by Release Date", () => tracker.ViewAlbumsByDate())
        .Add("View Songs by Album", () => tracker.ViewSongsInAlbum())
        .Add("View Signed Bands", () => tracker.DisplayAllSigned())
        .Add("View Bands with No Active Contract", () => tracker.DisplayAllUnsigned())
        .Add("Close", ConsoleMenu.Close)
        .Configure(config =>
        {
          config.Selector = "=>=> ";
          config.EnableFilter = true;
          config.Title = "Main menu";
          config.EnableWriteTitle = true;
          config.EnableBreadcrumb = true;
        });

      menu.Show();

    }
  }
}
