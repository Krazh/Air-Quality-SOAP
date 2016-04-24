using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AirQualityWCF.Handlers;
using AirQualityWCF.Interfaces;
using AirQualityWCF.Model;
using AirQualityWCF.Model.Helpers;

namespace AirQualityWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AirQualityService : IAirQualityService
    {
        private Connection _conn;
        private AnalyseHandler _analyseHandler;
        private string _error = "";

        public AirQualityService()
        {
            _conn = new Connection();
            _analyseHandler = new AnalyseHandler(_conn);
            
        }

        public List<Analyse> HentAnalyser(int month, int maxRows, int enhedId, int stofId, int udstyrId, int opstillingId)
        {
            return _analyseHandler.AnalyserByMonth(month, maxRows, enhedId, stofId, udstyrId, opstillingId);
        }

        public List<Analyse> HentAnalyserGnsByMonth(int month, int stofId)
        {
            try
            {
                return _analyseHandler.GetAnalysisByMonth(month, stofId);
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                return null;
            }
        }

        public List<Analyse> GetResultsForDayByCompound(int day, int month, int stofId)
        {
            return _analyseHandler.GetAnalysisByDay(day, month, stofId);
        }

        public List<Stof> GetAllStof()
        {
            return _analyseHandler.GetAllStof();
        }

        public List<Udstyr> GetAllUdstyr()
        {
            return _analyseHandler.GetAllUdstyr();
        }

        public List<Enhed> GetAllEnhed()
        {
            return _analyseHandler.GetAllEnhed();
        }

        public List<Opstilling> GetAllOpstilling()
        {
            return _analyseHandler.GetAllOpstilling();
        }

        public List<Maalested> GetAllMaalested()
        {
            return _analyseHandler.GetAllMaalested();
        }
    }
}
