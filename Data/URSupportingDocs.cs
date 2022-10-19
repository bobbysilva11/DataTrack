using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTrack.Data
{
    public class URSupportingDocs
    {
        public int ID { get; set; }
        public int URID { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string Location { get; set; }
        public string DisplayLocation { get; set; }
    }
}