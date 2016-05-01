using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using EVE.ISXEVE;
using EVE.ISXEVE.DataTypes;
using EVE.ISXEVE.Extensions;
using InnerSpaceAPI;
using LavishVMAPI;

namespace TestingApi.Scanners
{
    class Program
    {
        static void Main(string[] args)
        {
            //SysScan();
            ObjectScan();
        }

        private static void ObjectScan()
        {
            Frame.Wait(true);

            var ext = new Extension();
            var eve = ext.EVE();

            var entities = eve.QueryEntities();

            foreach (var entity in entities)
            {
                if (entity.Name.ToLower().Contains("corp"))
                {
                    InnerSpace.Echo(entity.ID + " " + entity.Name);
                }
            }

            Frame.Unlock();
        }

        private static void SysScan()
        {
            Frame.Wait(true);

            var ext = new EVE.ISXEVE.Extension();
            var eve = ext.EVE();
            var me = ext.Me;

            var sysScanner = me.Ship.Scanners.System;

            var sigs = sysScanner.GetSignatures();

            foreach (var sig in sigs)
            {
                InnerSpace.Echo(sig.Name + " " + sig.IsWarpable + " " + sig.Difficulty + " " + sig.Deviation);
            }

            var anoms = sysScanner.GetAnomalies();

            foreach (var anom in anoms)
            {
                InnerSpace.Echo(anom.Name);
            }

            eve.Execute(ExecuteCommand.CmdToggleMap);

            Frame.Unlock();
        }
    }
}
