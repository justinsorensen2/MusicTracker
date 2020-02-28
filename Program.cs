using System;
using MusicTracker.Models;
using System.Linq;

namespace MusicTracker
{
  class Program
  {

    public static void Main(string[] args)
    {
      var tracker = new Tracker();
      tracker.PopulateDb();
      Console.WriteLine("Welcome to C#");

    }
  }
}
