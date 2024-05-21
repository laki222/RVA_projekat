using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Common.Model;

namespace Common
{
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

        bool DeleteRacun();

        [OperationContract]
        bool SearchRacun();



        [OperationContract(IsOneWay = true)]

        void DoubleRacun(Racun racun);

    }
}
