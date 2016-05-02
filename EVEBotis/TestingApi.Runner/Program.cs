using System;
using System.Linq;
using Core.Common;
using DryIoc;
using EVE.ISXEVE.DataTypes;
using EVE.ISXEVE.Extensions;
using Gate2Camp.EVEBotLogic.BusinessLogic;
using Gate2Camp.EVEBotLogic.Common;
using InnerSpaceAPI;
using LavishVMAPI;

namespace TestingApi.Runner
{
  public class Program
  {
    private static void Main(string[] args)
    {
      var container = IoCBootstrap.Setup();

      var logger = container.Resolve<ILogger>();

      Frame.Wait(true);

      var ext = new Extension();
      var eve = ext.EVE();
      var me = ext.Me;


      //TestGetLocalGridEntities(me, eve, logger);
      //CombatHelper.ActivateModules(me,eve, new EngageRules() {UseRepairer = false});

      Frame.Unlock();

      Console.ReadLine();
    }

    private static void TestGetLocalGridEntities(Character me, EVE.ISXEVE.TopLevelObjects.EVE eve, ILogger logger)
    {
      var entities = EntityRepository.GetLocalGridEntities(me, eve);

      foreach (var entity in entities)
      {
        logger.Log(entity.ToString());
      }
    }
  }
}