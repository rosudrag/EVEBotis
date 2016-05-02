using System;
using InnerSpaceAPI;

namespace Core.Common
{
  public class EveDebugLogger : ILogger
  {
    public void Log(string message)
    {
      Console.WriteLine(message);
      InnerSpace.Echo(message);
    }
  }
}