using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Service.IntelClient;

namespace EVEIntelStandalone
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var intelService = new IntelClientService();
            intelService.Run();
        }
    }
}
