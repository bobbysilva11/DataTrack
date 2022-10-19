using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataTrack.Data
{
    public class Activity
    {
        public int ActivityID { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityType { get; set; }
        public string ContractDate { get; set; }
        public string LetterLocation { get; set; }
        public string LetterTitle { get; set; }
        public int ReviewRef { get; set; }
        public string URRef { get; set; }
        public string TagList { get; set; }
        public string MemberNumber { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string SecondsToComplete { get; set; }
    }
}