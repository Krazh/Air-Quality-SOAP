using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirQualityWCF.Model
{
    [DataContract]
    public class Opstilling
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Navn { get; set; }

        [DataMember]
        public Maalested Maalested { get; set; }

    }
}
