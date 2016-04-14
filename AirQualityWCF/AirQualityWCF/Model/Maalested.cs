using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirQualityWCF.Model
{
    [DataContract]
    public class Maalested
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Navn { get; set; }

        [DataMember]
        public string Easting_32 { get; set; }

        [DataMember]
        public string Northing_32 { get; set; }
    }
}
