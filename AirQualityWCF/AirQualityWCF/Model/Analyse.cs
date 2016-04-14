using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirQualityWCF.Model
{
    [DataContract]
    public class Analyse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Resultat { get; set; }

        [DataMember]
        public DateTime Datomaerke { get; set; }

        [DataMember]
        public Enhed Enhed { get; set; }

        [DataMember]
        public Udstyr Udstyr { get; set; }

        [DataMember]
        public Stof Stof { get; set; }

        [DataMember]
        public Opstilling Opstilling { get; set; }
    }
}
