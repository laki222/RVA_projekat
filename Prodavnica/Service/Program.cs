using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(BillService));
            host.AddServiceEndpoint(typeof(IBillService), new NetTcpBinding(), "net.tcp://localhost:9000");
            host.Open();

           

            Console.WriteLine("Racun services are now online. Waiting for requests...");
            Console.ReadLine();

        }
    }
}
