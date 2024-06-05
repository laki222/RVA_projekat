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
    public interface IBillService
    {
        [OperationContract]

        List<Bill> GetAllBills();

        [OperationContract]
        Bill CreateBill();

        [OperationContract]

        bool EditBill(string creator,int id);

        [OperationContract]
        void AddProductToBill(string name, string manufacturer);


        [OperationContract]

        bool DeleteBill(int billid);

        [OperationContract]
        Bill SearchBill(int id);

        [OperationContract]
        LogInInfo LogIn(string username, string password);


        [OperationContract(IsOneWay = true)]
        void LogOut(string username);


        [OperationContract]
        RegisteredCustomer GetUserInfo(string username);

        [OperationContract]
        void EditUserInfo(string username, string firstName, string lastName);

        [OperationContract]
        bool CreateUser(string username, string password, string firstName, string lastName);



        [OperationContract(IsOneWay = true)]

        void DoubleBill(Bill racun);

    }
}
