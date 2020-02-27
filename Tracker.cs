using System;
using MusicTracker.Models;
using System.Linq;

namespace MusicTracker
{
  public class Tracker
  {
    public static void PopulateDb()
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
    public static void AddBand()
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
    public static void WriteBandToDb(string name, string country, string numberOfMembers, string website, string style, bool isSigned, string contact, string phone)
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
    public static string InputVerification(string input, string type)
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
    public static string NumberVerification(string input, string type)
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
    public static string UrlVerification(string input, string type)
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
    public static bool BoolVerification(string input, string type)
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
    public static void ProduceAnAlbum()
    {

    }
  }

}