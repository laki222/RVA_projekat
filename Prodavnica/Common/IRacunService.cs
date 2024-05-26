using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common.Model;

namespace Common
{
    public enum LogInInfo
    {
        WrongUserOrPass,
        AlreadyConnected,
        Sucess
    }


    [ServiceContract]
    public interface IRacunService
    {
        [OperationContract]

        List<Racun> GetAllRacun();

        [OperationContract]
        bool CreateRacun();

        [OperationContract]

        bool EditRacun();

        [OperationContract]

        bool DeleteRacun(string name);

        [OperationContract]
        bool SearchRacun();

        [OperationContract]
        LogInInfo LogIn(string username, string password);


        [OperationContract(IsOneWay = true)]
        void LogOut(string username);



        [OperationContract(IsOneWay = true)]

        void DoubleRacun(Racun racun);

    }
}
