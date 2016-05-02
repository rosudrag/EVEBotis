using DryIoc;

namespace Core.Common
{
  public static class IoCBootstrap
  {
    public static Container Setup()
    {
      var container = new Container();

      RegisterConfigurations(container);

      return container;
    }

    private static void RegisterConfigurations(Container container)
    {
      container.Register<ILogger, EveDebugLogger>();
    }
  }
}