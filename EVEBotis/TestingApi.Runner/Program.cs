using System;
using System.Linq;
using Core.Common;
using DryIoc;
using EVE.ISXEVE.DataTypes;
using EVE.ISXEVE.Extensions;
using EVE.ISXEVE.TopLevelObjects;
using Gate2Camp.EVEBotLogic.Common;
using InnerSpaceAPI;
using LavishScriptAPI;
using LavishVMAPI;

namespace TestingApi.Runner
{
  public class Program
  {
    private static ILogger _logger;

    private static void Main(string[] args)
    {
      var container = IoCBootstrap.Setup();

      _logger = container.Resolve<ILogger>();

      LavishScript.Events.AttachEventTarget("ISXEVE_OnFrame", ISXEVE_OnFrame);

      Console.ReadLine();
    }

    private static void TestGetLocalGridEntities(Character me, EVE.ISXEVE.TopLevelObjects.EVE eve, ILogger logger)
    {
      //var entities = EntityRepository.GetLocalGridEntities(me, eve);
      eve.RefreshStandings();
      var entities = eve.QueryEntities().Where(x => x.IsPc);

      //var anObj = LavishScript.Objects.GetObject("Local", "Romvex");
      //logger.Log(anObj.ToString());

      eve.get

      var pilot = new Pilot("Romvex ");
      logger.Log(pilot.ToString());

      foreach (var entity in entities)
      {
        logger.Log(entity.Name);
        logger.Log(LavishScriptObject.IsNullOrInvalid(entity.Owner.ToFleetMember).ToString());
        logger.Log(entity.Owner.CharID.ToString());
        logger.Log(entity.Owner.Corp.ID.ToString());
        logger.Log(entity.AllianceID.ToString());
        var standing = me.StandingTo(entity.CharID, entity.Corp.ID, entity.AllianceID);
        logger.Log(standing.AllianceToAlliance.ToString());
        logger.Log(standing.AllianceToCorp.ToString());
        logger.Log(standing.AllianceToPilot.ToString());
        logger.Log(standing.CorpToAlliance.ToString());
        logger.Log(standing.CorpToCorp.ToString());
        logger.Log(standing.CorpToPilot.ToString());
        logger.Log(standing.MeToAlliance.ToString());
        logger.Log(standing.MeToCorp.ToString());
        logger.Log(standing.MeToPilot.ToString());
      }
    }

    /* Pulse method that will execute on our OnFrame, which in turn executes on the lavishscript OnFrame */

    private static void ISXEVE_OnFrame(object sender, LSEventArgs e)
    {
      //using (new FrameLock(true))
      //{
      //  var ext = new Extension();
      //  var eve = ext.EVE();
      //  var me = ext.Me;

      //  TestGetLocalGridEntities(me, eve, logger);
      //  //CombatHelper.ActivateModules(me,eve, new EngageRules() {UseRepairer = false});
      //}

      using (new FrameLock(true))
      {
        var ext = new Extension();
        var eve = ext.EVE();
        var me = ext.Me;

        InnerSpace.Echo("Your character's name is " + me.Name);
        InnerSpace.Echo("Your active ship has " + me.Ship.HighSlots + " high slots.");
        InnerSpace.Echo("Your active ship has " + me.Ship.MediumSlots + " medium slots.");
        InnerSpace.Echo("Your active ship has " + me.Ship.LowSlots + " low slots.");

        TestGetLocalGridEntities(me, eve, _logger);

        LavishScript.Events.DetachEventTarget("ISXEVE_OnFrame", ISXEVE_OnFrame);
      }
    }
  }
}