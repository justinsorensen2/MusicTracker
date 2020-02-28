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
          BandId = 1
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
      var signedInput = Console.ReadLine();
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
    public void CancelBandContract()
    {
      //set var to access db
      var db = new DatabaseContext();
      //ask user which band's contract to cancel
      Console.WriteLine("Which band's contract will be cancelled?");
      DisplayAllSigned();
      //ask for ID of band from list
      Console.WriteLine("Please enter the ID of the band whose contract you would like to cancel.");
      //create var for user input then verify
      var bandInput = Console.ReadLine();
      var verifiedBand = NumberVerification(bandInput, "Band ID");
      //parse verified string
      var bandId = int.Parse(verifiedBand);
      FindBand(bandId, true);


    }
    public void RenewBandContract()
    {

    }
    public void FindBand(int bandId, bool signed)
    {
      Console.WriteLine("What band ");
    }
    public void DisplayAllSigned()
    {
      //set var for db access
      var db = new DatabaseContext();


    }
    public void DisplayAllUnsigned()
    {

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
        var verifiedLength = LengthVerification(lengthInput, "song length");
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
        };
        //add song to Album's list
        album.Songs.Add(song);
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

    public void DisplayBands()
    {
      var db = new DatabaseContext();
      var bandList = db.Bands.Where(b => b.BandName != null);
      foreach (var b in bandList)
      {
        Console.WriteLine($"ID:{b.Id} Band:{b.BandName}");
      }
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
        if (input == null)
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
        else if ((!input.Contains("://")) && (!input.Contains(".com") || !input.Contains(".net") || !input.Contains(".io")))
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
    public string LengthVerification(string input, string type)
    {
      var verifying = true;
      var regexPattern = @"^[0-9]\d[0-9]\d{:}[0-9]\d[0-9]$";
      while (verifying)
      {
        //make sure input is not null
        if (input == null)
        {
          Console.WriteLine($"That is not a valid {type}.");
          Console.WriteLine("Please enter the length of the song in the format: mm:ss");
          Console.WriteLine("Where mm is the number of minutes and ss is the number of seconds.");
          Console.WriteLine("Your entry should be 5 characters in length(e.g. 03:15).");
          input = Console.ReadLine();
        }
        //check for match to format
        else if (Regex.IsMatch(input, regexPattern, RegexOptions.IgnorePatternWhitespace)
        {
          verifying = false;
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