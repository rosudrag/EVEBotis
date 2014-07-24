using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVE.ISXEVE;
using LavishVMAPI;

namespace SimpleRandomISXEVETests
{
    class Program
    {
        public static void Main(string[] args)
        {
            Frame.Wait(true);

            var eve = new EVE.ISXEVE.EVE();

            var myeve = new Me();

            Console.WriteLine(myeve.Name);

            Frame.Unlock();

            Console.ReadLine();
        }
    }
}
