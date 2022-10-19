using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataTrack.Data;

namespace DataTrack.Models
{
    public class URDetailsModel
    {
        public string RefID { get; set; }
        public string RequestDate { get; set; }
        public string LevelofCare { get; set; }
        public string AuthType { get; set; }
        public string TreatingProvider { get; set; }
        public string TreatingFacility { get; set; }
        public string Diagnosis { get; set; }
        public List<URSupportingDocs> SupportingDocs { get; set; }
        public string RequestedServices { get; set; }
        public string RequestedServiceDescription { get; set; }
        public string RequestedServiceQuantity { get; set; }
        public DateTime RequestedServiceStartDate { get; set; }
        public DateTime RequestedServiceEndDate {get;set;}
        public string MNRReviewRefID { get; set; }
        public string MNRReviewType { get; set; }
        public string MNRReviewStatus { get; set; }
        public DateTime MNRReviewStartDate { get; set; }
        public DateTime MNRReviewCompleteDate { get; set; }
        public DateTime MNRReviewReconsiderationDate { get; set; }
        public string QOCReviewRefID { get; set; }
        public string QOCReviewType { get; set; }
        public string QOCReviewStatus { get; set; }
        public DateTime QOCReviewStartDate { get; set; }
        public DateTime QOCReviewCompleteDate { get; set; }
        public DateTime QOCReviewReconsiderationDate { get; set; }
        public string ActivityDate { get; set; }
        public string ActivityType { get; set; }
        public string ActivityComments { get; set; }
        public string ActivityLetterTitle { get; set; }
        public string ActivityLetterLocation { get; set; }
        public int ActivityID { get; set; }
        public string MemberName { get; set; }
        public string MemberID { get; set; }
        public string MemberDOB { get; set; }
        public string MemberGender { get; set; }
        public string MemberAge { get; set; }
        public string MemberSSN { get; set; }
        public string MemberPhone { get; set; }

        public string MemberCurrentAge { get; set; }
        public int URID { get; set; }
        public List<Activity> Activities { get; set; }
        public string PolicyNumber { get; set; }

    }
}