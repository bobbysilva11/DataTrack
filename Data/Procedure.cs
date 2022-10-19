using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTrack.Data
{
    public class Procedure
    {
        public string ProcedureCode { get; set; }
        public string ProcedureDescription { get; set; }
        public string ProcedureDischargeDisposition { get; set; }
        public string ProcedureDRGCode { get; set; }
        public string ProcedureDRGDescription { get; set; }
        public string PrimaryProcedure { get; set; }
    }
}