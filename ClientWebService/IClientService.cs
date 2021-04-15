using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ClientWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        string InsertClientDetails(ClientDetails eDatils);
        [OperationContract]
        DataSet GetClientDetails(ClientDetails eDatils);
        [OperationContract]
        DataSet FetchUpdatedRecords(ClientDetails eDatils);
        [OperationContract]
        string UpdateClientDetails(ClientDetails eDatils);
        [OperationContract]
        bool DeleteClientDetails(ClientDetails eDatils);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class ClientDetails
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string Name { get; set; }  = string.Empty;
        [DataMember]
        public string Surname { get; set; }  = string.Empty;
        [DataMember]
        public string Gender { get; set; }  = string.Empty;
        [DataMember]
        public string ResAddress { get; set; }  = string.Empty;
        [DataMember]
        public string WorkAddress { get; set; } = string.Empty;
        [DataMember]
        public string PostalAddress { get; set; } = string.Empty;
        [DataMember]
        public string CellNumber { get; set; } = string.Empty;
        [DataMember]
        public string WorkNumber { get; set; } = string.Empty;        
    }
}
