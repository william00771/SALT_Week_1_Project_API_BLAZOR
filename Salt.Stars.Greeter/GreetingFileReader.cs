using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Salt.Stars.Greeter
{
  public class GreetingFileReader : IGreetingFileReader
  {
    private string _dataDirectory;

    private string getAssemblyDirectory()
    {
      string codeBase = Assembly.GetExecutingAssembly().Location;
      return Path.GetDirectoryName(codeBase);
    }
    public GreetingFileReader(string directoryPath)
    {

      var dataDirectory = Path.Join(getAssemblyDirectory(), directoryPath);
      if (!Directory.Exists(dataDirectory))
      {
        throw new Exception($"Could not find '{dataDirectory}'");
      }

      _dataDirectory = dataDirectory;
    }

    public string[] GetImpoliteGreetings()
    {
      return File.ReadAllLines($"{_dataDirectory}/impoliteGreetings.txt");
    }

    public IEnumerable<string> GetPoliteGreetings()
    {
      return File.ReadAllLines($"{_dataDirectory}/politeGreetings.txt");
    }
  }
}