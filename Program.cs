using System;
using MusicTracker.Models;
using System.Linq;

namespace MusicTracker
{
  class Program
  {

    public static void Main(string[] args)
    {
      Tracker.PopulateDb();
      Console.WriteLine("Welcome to C#");

    }
  }
}
