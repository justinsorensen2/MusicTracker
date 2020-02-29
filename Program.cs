using System;
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
      //Credit for Console Menu goes to lechu445.
      //Console Menu source code: https://github.com/lechu445/ConsoleMenu
      var menu = new ConsoleMenu(args, level: 0)
        .Add("Add a Band", () => tracker.AddBand())
        .Add("Cancel a Contract", () => tracker.AlterContract(true, "cancel"))
        .Add("Renew a Contract", () => tracker.AlterContract(false, "renew"))
        .Add("Produce an Album", () => tracker.AddAnAlbum())
        .Add("View Albums by Band", () => tracker.ViewAlbumsPerBand())
        .Add("View Albums by Release Date", () => tracker.ViewAlbumsByDate())
        .Add("View Songs by Album", () => tracker.ViewSongsInAlbum())
        .Add("View Signed Bands", () => tracker.DisplayAllSU(true))
        .Add("View Bands with No Active Contract", () => tracker.DisplayAllSU(false))
        .Add("View all Bands", () => tracker.DisplayBands())
        .Add("Close", ConsoleMenu.Close)
        .Configure(config =>
        {
          config.Selector = "=>=> ";
          config.EnableFilter = true;
          config.EnableWriteTitle = true;
          //credit goes to Evan Gilbert for the title config
          config.Title = "*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*\n"
                         + "| Welcome to the Music Tracker! |\n"
                         + "*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*\n";
          config.WriteHeaderAction = () => Console.WriteLine("Use the arrow keys and press enter to make a selection.");
        });

      menu.Show();


    }
  }
}
