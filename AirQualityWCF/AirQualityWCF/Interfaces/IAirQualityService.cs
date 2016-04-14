using System;
using System.Collections.Generic;
using System.ServiceModel;
using AirQualityWCF.Model;

namespace AirQualityWCF.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IAirQualityService
    {
        [OperationContract]
        List<Analyse> HentAnalyser(int month, int maxRows = 0, int enhedId = 0, int stofId = 0, int udstyrId = 0, int opstillingId = 0);

        [OperationContract]
        List<Analyse> HentAnalyserGnsByMonth(int month, int stofId);

        [OperationContract]
        List<Stof> GetAllStof();

        [OperationContract]
        List<Udstyr> GetAllUdstyr();

        [OperationContract]
        List<Enhed> GetAllEnhed();

        [OperationContract]
        List<Opstilling> GetAllOpstilling();

        [OperationContract]
        List<Maalested> GetAllMaalested();
    }

}
