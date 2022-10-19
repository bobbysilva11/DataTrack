using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataTrack.Data;

namespace DataTrack.Models
{
    public class ReviewDetailsModel
    {
        public string ReviewType { get; set; }
        public string ReferenceID { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public string CompleteDate { get; set; }
        public string ReviewTiming { get; set; }
        public string MemberName { get; set; }
        public string MemberNumber { get; set; }
        public string MemberDOB { get; set; }
        public string MemberGender { get; set; }
        public string MemberCurrentAge { get; set; }
        public string MemberSSN { get; set; }
        public string MemberPhone { get; set; }
        public string TreatmentRequested { get; set; }
        public string TreatmentRequestDates { get; set; }
        public int TreatmentRequestQuantity { get; set; }
        public string TreatmentRecommendedDates { get; set; }
        public int TreatmentRecommendedQuantity { get; set; }
        public string TreatmentOutcome { get; set; }
        public DateTime ActivityDate { get; set; }
        public string ActivityType { get; set; }
        public string ActivityComments { get; set; }
        public string ActivityLetters { get; set; }
        public string LetterLocation { get; set; }
        public int ActivityId { get; set; }
        public string LetterTitle { get; set; }
        public string URReqDate { get; set; }
        public int URCount { get; set; }
        public List<Activity> Activities { get; set; }
        public List<Procedure> ProcedureList { get; set; }
        public List<Diagnosis> DiagnosisList { get; set; }
        public string DischargeDisposition { get; set; }
        public string DiagnosisRelatedGroupDescription { get; set; }
        public string CurrentDiagnosisRelatedGroupCode { get; set; }
        public string OriginalDiagnosisRelatedGroupCode { get; set; }
    }

    public class ClaimDetailsModel
    {
        public string MemberName { get; set; }
        public string MemberNumber { get; set; }
        public string MemberDOB { get; set; }
        public string MemberGender { get; set; }
        public string MemberCurrentAge { get; set; }
        public string MemberSSN { get; set; }
        public string MemberPhone { get; set; }
        public long ClaimNumber { get; set; }
        public string DataDomain { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<Activity> Activities { get; set; }
        public string ELCoverageType { get; set; }
        public string ELStartDate { get; set; }
        public string ELEndDate { get; set; }
        public string ELRelationToHolder { get; set; }
        public string ELPolicyNumber { get; set; }
        public string ELGroupNumber { get; set; }
        public string ELStateofIssue { get; set; }
        public string ELPayer { get; set; }
        public string ELPlanName { get; set; }
        public List<URDetailsModel> uRDetails { get; set; }
        public List<ReviewDetailsModel> Reviews { get; set; }

    }

    public class MemberDetailsModel
    {
        public string MemberName { get; set; }
        public string MemberNumber { get; set; }
        public string MemberDOB { get; set; }
        public string MemberGender { get; set; }
        public string MemberCurrentAge { get; set; }
        public string MemberSSN { get; set; }
        public string MemberPhone { get; set; }
        public string MemberEmail { get; set; }
        public string MemberAddress { get; set; }
     }
}