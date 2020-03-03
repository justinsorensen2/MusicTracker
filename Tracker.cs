using System;
using MusicTracker.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace MusicTracker
{
  public class Tracker
  {
    public void PopulateDb()
    {
      var db = new DatabaseContext();
      if (!db.Bands.Any())
      {
        var band = new Band()
        {
          BandName = "Dream Theater",
          Country = "USA",
          NumberOfMembers = "5",
          Website = "http://dreamtheater.net/",
          Style = "Progressive Metal",
          IsSigned = true,
          Contact = "John Petrucci",
          ContactPhoneNumber = "555-416-7127"
        };
        db.Bands.Add(band);
        db.SaveChanges();
      }
      if (!db.Albums.Any())
      {
        var album = new Album()
        {
          Title = "A Change of Seasons",
          IsExplicit = false,
          ReleaseDate = new DateTime(1995, 9, 19),
          BandId = 1
        };
        db.Albums.Add(album);
        db.SaveChanges();
      }
      if (!db.Songs.Any())
      {
        var song = new Song()
        {
          SongTitle = "A Change of Seasons",
          Lyrics = "So far, or so it seems, all is lost with nothing fulfilled.",
          Length = "23:09",
          Genre = "Progressive Metal",
          BandId = 1,
          AlbumId = 1
        };
        db.Songs.Add(song);
        db.SaveChanges();
      }
    }
    public void AddBand()
    {
      //ask user for all inputs needed to create a band and store as variables
      Console.WriteLine("Which band would you like to add?");
      Console.WriteLine("Please enter the band name.");
      var input = Console.ReadLine();
      //call input verification method
      var verifiedBand = InputVerification(input, "band name");
      Console.WriteLine("What is the band's country of origin?");
      Console.WriteLine("Please enter a country.");
      var countryInput = Console.ReadLine();
      var verifiedCountry = InputVerification(countryInput, "country");
      Console.WriteLine("How many members are in the band?");
      Console.WriteLine("Please enter a number.");
      var numInput = Console.ReadLine();
      var verifiedNum = NumberVerification(numInput, "number");
      Console.WriteLine("What is the URL of the band's website?");
      Console.WriteLine("Please enter in the format: http(s)://sampleurl.com");
      var webInput = Console.ReadLine();
      var verifiedWeb = UrlVerification(webInput, "URL");
      Console.WriteLine("What is the band's style?");
      Console.WriteLine("Please enter the type of music this band is known for playing.");
      var styleInput = Console.ReadLine();
      var verifiedStyle = InputVerification(styleInput, "style");
      Console.WriteLine("Is this band signed?");
      Console.WriteLine("Please enter yes(Y) or no(N).");
      var signedInput = Console.ReadLine().ToUpper();
      var verifiedSigned = BoolVerification(signedInput, "entry");
      Console.WriteLine("Who is the Contact for this band?");
      Console.WriteLine("Please enter a name.");
      var contactInput = Console.ReadLine();
      var verifiedContact = InputVerification(contactInput, "contact name");
      Console.WriteLine("What is the best phone number for this contact?");
      Console.WriteLine("Please enter area code and number.");
      var phoneInput = Console.ReadLine();
      var verifiedPhone = InputVerification(phoneInput, "phone number");
      //call method to write band to db
      WriteBandToDb(verifiedBand, verifiedCountry, verifiedNum, verifiedWeb, verifiedStyle, verifiedSigned, verifiedContact, verifiedPhone);

    }
    public void AddAnAlbum()
    {
      //ask which band this album will be for
      Console.WriteLine("Which band would you like to update with a new album?");
      DisplayBands();
      //ask for ID of band from list
      Console.WriteLine("Please enter the ID of the band you would like to add an album to.");
      //create var for user input then verify
      var bandInput = Console.ReadLine();
      var verifiedBand = NumberVerification(bandInput, "Band ID");
      //once input has been verified parse back to a number
      var bandId = int.Parse(verifiedBand);
      //ask user for remaining details of album
      Console.WriteLine("Please enter the title of the album.");
      //set var for input them verify input
      var albumInput = Console.ReadLine();
      var verifiedAlbum = InputVerification(albumInput, "album title");
      Console.WriteLine("Does this album contain explicit lyrics?");
      Console.WriteLine("Valid entries are yes(Y) or no(N).");
      //create var for input then verify input
      var explicitInput = Console.ReadLine().ToUpper();
      var verifiedExplicit = BoolVerification(explicitInput, "entry");
      Console.WriteLine("Please enter the realease date in the format: yyyy, mm, dd ");
      var releaseInput = Console.ReadLine();
      var verifiedRelease = DateVerification(releaseInput, "date");
      //create var for database access
      var db = new DatabaseContext();
      //create var for new album and set properties based on previous user inputs
      var album = new Album()
      {
        Title = verifiedAlbum,
        IsExplicit = verifiedExplicit,
        ReleaseDate = verifiedRelease,
        BandId = bandId
      };
      //save album and commit
      db.Albums.Add(album);
      db.SaveChanges();
      //call method to offer to add songs to album
      AddSongsToAlbum(bandId, album);
    }
    public void AlterContract(bool signed, string type)
    {
      //set var to access db
      var db = new DatabaseContext();
      //ask user which band's contract to cancel
      Console.WriteLine($"Which band's contract will be {type}ed?");
      DisplayAllSU(signed);
      //ask for ID of band from list
      Console.WriteLine($"Please enter the ID of the band whose contract you would like to {type}.");
      //create var for user input then verify
      var bandInput = Console.ReadLine();
      var verifiedBand = NumberVerification(bandInput, "Band ID");
      //parse verified string
      var bandId = int.Parse(verifiedBand);
      //call method to select the band you want to sign
      UpdateSigned(bandId, signed, type);
    }
    public void UpdateSigned(int bandId, bool signed, string type)
    {
      //set var to access db
      var db = new DatabaseContext();
      //use band id to locate the band
      var band = db.Bands.First(b => b.Id == bandId);
      //decide what to do based on bool passed in
      if (signed == true)
      {
        band.IsSigned = false;
      }
      else
      {
        band.IsSigned = true;
      }
      Console.WriteLine($"{band.BandName}'s contract has been {type}ed.");
      db.SaveChanges();
      Console.WriteLine($"Press Enter to return to the main menu.");
      Console.ReadKey();
    }
    public void DisplayAllSU(bool signed)
    {
      //set var for db access
      var db = new DatabaseContext();
      //set var for new list pulled from db showing bands based on bool passed in
      var signedList = db.Bands.Where(b => b.IsSigned == signed);
      foreach (var b in signedList)
      {
        Console.WriteLine($"ID: {b.Id} Band Name: {b.BandName} Contract is Current: {b.IsSigned}");
      }
      Console.WriteLine($"Press Enter to continue.");
      Console.ReadKey();
    }

    public void AddSongsToAlbum(int bandId, Album album)
    {
      //ask if user would like to add songs to the album
      Console.WriteLine("Would you like to add a song to this album?");
      Console.WriteLine("Valid entries are yes(Y) or no(N).");
      //set var for input then run verification
      var addSongInput = Console.ReadLine().ToUpper();
      var verifiedAdd = BoolVerification(addSongInput, "entry");
      //if user selected yes, start while loop
      while (verifiedAdd)
      {
        //ask for song title
        Console.WriteLine("Please enter the song's title.");
        //set var based on input then run verification
        var songTitleInput = Console.ReadLine();
        var verifiedSongTitle = InputVerification(songTitleInput, "song name");
        //ask for a selection of lyrics
        Console.WriteLine("Please enter a sample of the song's lyrics.");
        //set var based on input then run verification
        var lyricsInput = Console.ReadLine();
        var verifiedLyrics = InputVerification(lyricsInput, "lyric");
        //ask for song length
        Console.WriteLine("Please enter the length of the song in the format: mm:ss");
        Console.WriteLine("Where mm is the number of minutes and ss is the number of seconds.");
        Console.WriteLine("Your entry should be 5 characters in length(e.g. 03:15).");
        //set var based on input then run verification
        var lengthInput = Console.ReadLine();
        var verifiedLength = SongLengthVerification(lengthInput, "song length");
        //ask for genre
        Console.WriteLine("Please enter the genre of the song.");
        //set var based on input then run verification
        var genreInput = Console.ReadLine();
        var verifiedGenre = InputVerification(genreInput, "genre");
        //set var for db access
        var db = new DatabaseContext();
        //set var for new song and set properties
        var song = new Song()
        {
          SongTitle = verifiedSongTitle,
          Lyrics = verifiedLyrics,
          Length = verifiedLength,
          Genre = verifiedGenre,
          BandId = bandId,
          AlbumId = album.Id
        };
        //add song to Album's list
        album.Songs.Add(song);
        db.SaveChanges();
        //add song to db and commit
        db.Songs.Add(song);
        db.SaveChanges();
        //offer to add another song
        Console.WriteLine("Would you like to add another song to this album?");
        Console.WriteLine("Valid entries are yes(Y) or no(N).");
        var addAnother = Console.ReadLine().ToUpper();
        verifiedAdd = BoolVerification(addAnother, "entry");

      }

    }
    public void WriteBandToDb(string name, string country, string numberOfMembers, string website, string style, bool isSigned, string contact, string phone)
    {
      var db = new DatabaseContext();
      var band = new Band()
      {
        BandName = name,
        Country = country,
        NumberOfMembers = numberOfMembers,
        Website = website,
        Style = style,
        IsSigned = isSigned,
        Contact = contact,
        ContactPhoneNumber = phone
      };
      db.Bands.Add(band);
      db.SaveChanges();
    }
    public void ViewAlbumsPerBand()
    {
      //ask which
      Console.WriteLine("Which band's albums would you like to view?");
      //display all
      DisplayBands();
      //request user enter the ID
      Console.WriteLine("Please enter the ID of the band whose albums you wish to view.");
      //create var for user input then verify
      var bandInput = Console.ReadLine();
      var verifiedBand = NumberVerification(bandInput, "Band ID");
      //parse verified string
      var bandId = int.Parse(verifiedBand);
      //create var for db access
      var db = new DatabaseContext();
      foreach (var a in db.Albums.Where(a => a.BandId == bandId))
      {
        Console.WriteLine($"ID: {a.Id} Album Title: {a.Title} Contains Explicit Lyrics: {a.IsExplicit}");
        Console.WriteLine($"Release Date: {a.ReleaseDate}");
      }
      Console.WriteLine($"Press Enter to return to the main menu.");
      Console.ReadKey();
    }
    public void ViewAlbumsByDate()
    {
      //create var for db access
      var db = new DatabaseContext();
      foreach (var a in db.Albums.OrderBy(Album => Album.ReleaseDate))
      {
        Console.WriteLine($"ID: {a.Id} Album Title: {a.Title} Contains Explicit Lyrics: {a.IsExplicit}");
        Console.WriteLine($"Release Date: {a.ReleaseDate}");
      }
      Console.WriteLine($"Press Enter to return to the main menu.");
      Console.ReadKey();
    }
    public void ViewSongsInAlbum()
    {
      //create var for db access
      var db = new DatabaseContext();
      //create var for album list
      var albumList = db.Albums.Where(a => a.ReleaseDate != null);
      //display each item in list
      foreach (var a in albumList)
      {
        Console.WriteLine($"ID: {a.Id} Album Title: {a.Title}");
      }
      //ask user which album they want to view the song list for
      Console.WriteLine($"Please enter the ID of the album for which you wish to view the song list.");
      //create var from input then run verification
      var albumInput = Console.ReadLine();
      var verifiedAlbum = NumberVerification(albumInput, "album id");
      //parse verified input
      var albumId = int.Parse(verifiedAlbum);
      //use album id to locate album
      var album = db.Albums.First(a => a.Id == albumId);
      //create song list from album.songs
      var songList = album.Songs;
      //display each item in list
      foreach (var s in songList)
      {
        Console.WriteLine($"Album Title: {album.Title} Band: {s.Band.BandName} Song Title: {s.SongTitle}");
        Console.WriteLine($"Song Length: {s.Length} Sample Lyrics: {s.Lyrics}");
      }
      Console.WriteLine($"Press Enter to continue.");
      Console.ReadKey();
    }
    public void DisplayBands()
    {
      //set var for db access
      var db = new DatabaseContext();
      foreach (var b in db.Bands)
      {
        Console.WriteLine($"ID:{b.Id} Band:{b.BandName} Contract is Current: {b.IsSigned}");
        Console.WriteLine($"Website: {b.Website} # of Members: {b.NumberOfMembers} Style: {b.Style}");
      }
      Console.WriteLine($"Press Enter to continue.");
      Console.ReadKey();
    }
    public DateTime DateVerification(string input, string type)
    {
      var verifying = true;
      var test = DateTime.Now;
      var dateBool = true;
      while (verifying)
      {
        if (input == null)
        {
          Console.WriteLine($"That is not a valid {type}.");
          Console.WriteLine($"Please enter the {type}.");
          input = Console.ReadLine();
        }
        dateBool = DateTime.TryParse(input, out test);
        if (dateBool == false)
        {
          Console.WriteLine($"That is not a valid {type}.");
          Console.WriteLine($"Please enter the {type}.");
          input = Console.ReadLine();
        }
        else
        {
          verifying = false;
        }
      }
      return test;
    }
    public string InputVerification(string input, string type)
    {
      var verifying = true;
      while (verifying)
      {
        //make sure input is not null
        if (input == "")
        {
          Console.WriteLine($"That is not a valid {type}.");
          Console.WriteLine($"Please enter the {type}.");
          input = Console.ReadLine().ToUpper();
        }
        else
        {
          verifying = false;
        }
      }
      return input;
    }
    public string NumberVerification(string input, string type)
    {
      var verifying = true;
      var test = 0;
      var intBool = true;
      while (verifying)
      {
        if (input == null)
        {
          Console.WriteLine($"That is not a valid {type}.");
          Console.WriteLine($"Please enter the {type}.");
          input = Console.ReadLine();
        }
        intBool = int.TryParse(input, out test);
        if (intBool == false)
        {
          Console.WriteLine($"That is not a valid {type}.");
          Console.WriteLine($"Please enter the {type}.");
          input = Console.ReadLine();
        }
        else
        {
          verifying = false;
        }
      }
      return input;
    }
    public string UrlVerification(string input, string type)
    {
      var verifying = true;
      while (verifying)
      {
        //make sure input is not null
        if (input == null)
        {
          Console.WriteLine($"That is not a valid {type}.");
          Console.WriteLine($"Please enter the {type}.");
          input = Console.ReadLine();
        }
        //check for common features of a URL
        else if (Regex.IsMatch(input, @"(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z
            0-9\-\._\?\,\'/\\\+&%\$#\=~])*", RegexOptions.IgnorePatternWhitespace) == true)
        {
          verifying = false;
        }
        else
        {
          Console.WriteLine($"That is not a valid {type}.");
          Console.WriteLine($"Please enter the {type}.");
          input = Console.ReadLine();
        }
      }
      return input;
    }
    public bool BoolVerification(string input, string type)
    {
      var isSigned = false;
      var verifying = true;
      while (verifying)
      {
        //make sure input is not null
        if (input == null)
        {
          Console.WriteLine($"That is not a valid {type}.");
          Console.WriteLine($"Please try again.");
          input = Console.ReadLine().ToUpper();
        }
        //make sure input is one of the required entries
        else if (input != "Y" && input != "N")
        {
          Console.WriteLine($"That is not a valid {type}.");
          Console.WriteLine($"Please try again.");
          input = Console.ReadLine().ToUpper();
        }
        else if (input == "Y")
        {
          isSigned = true;
          verifying = false;
        }
        else if (input == "N")
        {
          isSigned = false;
          verifying = false;
        }
      }
      return isSigned;
    }
    public string SongLengthVerification(string input, string type)
    {
      var verifying = true;
      while (verifying)
      {
        //check for match to format
        if (Regex.IsMatch(input, @"^([0-9][0-9]|[2][0-3]):([0-5][0-9])$", RegexOptions.IgnorePatternWhitespace) == true)
        {
          verifying = false;
        }
        //make sure input is not null
        else if (input == null)
        {
          Console.WriteLine($"That is not a valid {type}.");
          Console.WriteLine("Please enter the length of the song in the format: mm:ss");
          Console.WriteLine("Where mm is the number of minutes and ss is the number of seconds.");
          Console.WriteLine("Your entry should be 5 characters in length(e.g. 03:15).");
          input = Console.ReadLine();
        }
        else
        {
          Console.WriteLine($"That is not a valid {type}.");
          Console.WriteLine("Please enter the length of the song in the format: mm:ss");
          Console.WriteLine("Where mm is the number of minutes and ss is the number of seconds.");
          Console.WriteLine("Your entry should be 5 characters in length(e.g. 03:15).");
          input = Console.ReadLine();
        }
      }
      return input;
    }
  }

}