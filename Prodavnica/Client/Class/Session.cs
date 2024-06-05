using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client.Class
{
    public class Session
    {
        private static Session current;

        public IBillService BillProxy { get; private set; }
        public string LoggedInUser { get; set; }



        public static Session Current
        {
            get
            {
                if (current == null)
                    current = new Session();

                return current;
            }
        }

        public Session()
        {
            ChannelFactory<IBillService> cf = new ChannelFactory<IBillService>(new NetTcpBinding(), "net.tcp://localhost:9000");
            BillProxy = cf.CreateChannel();

            LoggedInUser = string.Empty;
        }

        public void Abandon()
        {
            if (!LoggedInUser.Equals(string.Empty))
                BillProxy.LogOut(LoggedInUser);

            current = null;
        }
    }
}
