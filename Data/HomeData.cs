using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DataTrack.Models;

namespace DataTrack.Data
{
    public static class HomeData
    {
        public static DataTable GetMemberData(string lastName = "", string firstName = "", string memberID = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "SelectMemberData";
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    dt.Load(com.ExecuteReader());
                    conn.Close();
                }
            }
            dt.Columns.Add("CurrentAge");
            foreach (DataRow dr in dt.Rows)
            {
                var today = DateTime.Now;
                DateTime birthdate = DateTime.Parse(dr["DOB"].ToString());
                var calculatedAge = today.Year - birthdate.Year;
                if (birthdate.Date > today.AddYears(-calculatedAge))
                    calculatedAge--;
                dr["CurrentAge"] = calculatedAge;
            }

            DataTable filtered;
            DataTable empty = new DataTable();
            if (lastName != "" && firstName != "" && memberID != "")
            {
                if (dt.AsEnumerable().Where(a => a.Field<string>("lastname").ToLower().StartsWith(lastName.ToLower()) && a.Field<string>("firstname").ToLower().StartsWith(firstName.ToLower()) && a.Field<string>("membernumber").ToLower().StartsWith(memberID.ToLower())).Any())
                {
                    filtered = dt.AsEnumerable().Where(a => a.Field<string>("lastname").ToLower().StartsWith(lastName.ToLower()) && a.Field<string>("firstname").ToLower().StartsWith(firstName.ToLower()) && a.Field<string>("membernumber").ToLower().StartsWith(memberID.ToLower())).CopyToDataTable();
                    return filtered;
                }
                else
                    return empty;
                
            }
            else if (lastName != "" && firstName != "")
            {
                if (dt.AsEnumerable().Where(a => a.Field<string>("lastname").ToLower().StartsWith(lastName.ToLower()) && a.Field<string>("firstname").ToLower().StartsWith(firstName.ToLower())).Any())
                {
                    filtered = dt.AsEnumerable().Where(a => a.Field<string>("lastname").ToLower().StartsWith(lastName.ToLower()) && a.Field<string>("firstname").ToLower().StartsWith(firstName.ToLower())).CopyToDataTable();
                    return filtered;
                }
                else
                    return empty;
                
            }
            else if (memberID != "" && lastName != "")
            {
                if (dt.AsEnumerable().Where(a => a.Field<string>("lastname").ToLower().StartsWith(lastName.ToLower()) && a.Field<string>("membernumber").ToLower().StartsWith(memberID.ToLower())).Any())
                {
                    filtered = dt.AsEnumerable().Where(a => a.Field<string>("lastname").ToLower().StartsWith(lastName.ToLower()) && a.Field<string>("membernumber").ToLower().StartsWith(memberID.ToLower())).CopyToDataTable();
                    return filtered;
                }
                else
                    return empty;
                
            }
            else if (memberID != "")
            {
                if (dt.AsEnumerable().Where(a => a.Field<string>("membernumber").ToLower().StartsWith(memberID.ToLower())).Any())
                {
                    filtered = dt.AsEnumerable().Where(a => a.Field<string>("membernumber").ToLower().StartsWith(memberID.ToLower())).CopyToDataTable();
                    return filtered;
                }
                else
                    return empty;
                
            }
            else if (lastName != "")
            {
                if (dt.AsEnumerable().Where(a => a.Field<string>("lastname").ToLower().StartsWith(lastName.ToLower())).Any())
                {
                    filtered = dt.AsEnumerable().Where(a => a.Field<string>("lastname").ToLower().StartsWith(lastName.ToLower())).CopyToDataTable();
                    return filtered;
                }
                else
                    return empty;
            }
            else if (firstName != "")
            {
                if (dt.AsEnumerable().Where(a => a.Field<string>("firstname").ToLower().StartsWith(firstName.ToLower())).Any())
                {
                    filtered = dt.AsEnumerable().Where(a => a.Field<string>("firstname").ToLower().StartsWith(firstName.ToLower())).CopyToDataTable();
                    return filtered;
                }
                else
                    return empty;
                    
                
            }

            return dt;
        }

        public static DataTable GetMNRReviewData(string referenceID = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "SelectMNRReviewData";
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    dt.Load(com.ExecuteReader());
                    conn.Close();
                }
            }

            return dt;
        }

        public static DataTable GetQOCReviewData(string referenceID = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "SelectQOCReviewData";
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    dt.Load(com.ExecuteReader());
                    conn.Close();
                }
            }

            return dt;
        }

        public static List<ReviewDetailsModel> GetMNRMemberDetails(string referenceId, string reviewType)
        {
            List<ReviewDetailsModel> models = new List<ReviewDetailsModel>();


            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "SelectMNRDataByRefNum";
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    com.Parameters.AddWithValue("@RefNum", referenceId);
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ReviewDetailsModel model = new ReviewDetailsModel();
                            model.MemberName = dr["Lastname"].ToString() + ", " + dr["FirstName"].ToString();
                            model.MemberNumber = dr["MemberNumber"].ToString();
                            model.MemberDOB = dr["DOB"].ToString();
                            model.MemberGender = dr["Gender"].ToString();
                            model.MemberSSN = dr["SSN"].ToString();
                            model.MemberPhone = dr["Phone"].ToString();
                            model.ReviewType = reviewType;
                            model.ReferenceID = referenceId;
                            model.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                            model.Status = dr["ReviewStatus"].ToString();
                            model.CompleteDate = dr["CompleteDate"].ToString();
                            model.ReviewTiming = dr["RequestTiming"].ToString();
                            model.TreatmentRequested = dr["RequestedServices"].ToString();
                            model.TreatmentRequestDates = dr["TreatmentRequestDates"].ToString();
                            model.TreatmentRequestQuantity = int.Parse(dr["RequestQuantity"].ToString());
                            model.TreatmentRecommendedDates = dr["RecommendedDates"].ToString();
                            int.TryParse(dr["Quantity"].ToString(), out int result);
                            model.TreatmentRecommendedQuantity = result;
                            model.TreatmentOutcome = dr["Status"].ToString();
                            model.ActivityDate = DateTime.Parse(dr["ActivityDate"].ToString());
                            model.ActivityType = dr["ActivityType"].ToString();
                            model.ActivityComments = dr["Description"].ToString();
                            model.ActivityLetters = dr["LetterTitle"].ToString();
                            model.LetterLocation = dr["LetterLocation"].ToString();
                            model.ActivityId = int.Parse(dr["ActivityID"].ToString());
                            model.LetterTitle = dr["LetterTitle"].ToString();
                            model.URReqDate = dr["RequestDate"].ToString();
                            model.URCount = int.Parse(dr["URCount"].ToString());
                            var today = DateTime.Now;
                            DateTime birthdate = DateTime.Parse(dr["DOB"].ToString());
                            var calculatedAge = today.Year - birthdate.Year;
                            if (birthdate.Date > today.AddYears(-calculatedAge))
                                calculatedAge--;
                            model.MemberCurrentAge = calculatedAge.ToString();
                            model.Activities = GetActivities(model.MemberNumber);
                            models.Add(model);
                        }

                    }
                    conn.Close();
                }
            }


            return models;
        }



        public static List<ReviewDetailsModel> GetQOCMemberDetails(string referenceId, string reviewType)
        {
            List<ReviewDetailsModel> models = new List<ReviewDetailsModel>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "SelectQOCReviewDataByRefNum";
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@RefNum", referenceId);
                    conn.Open();
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ReviewDetailsModel model = new ReviewDetailsModel();
                            model.MemberName = dr["Lastname"].ToString() + ", " + dr["FirstName"].ToString();
                            model.MemberNumber = dr["MemberNumber"].ToString();
                            model.MemberDOB = dr["DOB"].ToString();
                            model.MemberGender = dr["Gender"].ToString();
                            model.MemberSSN = dr["SSN"].ToString();
                            model.MemberPhone = dr["Phone"].ToString();
                            model.ReviewType = reviewType;
                            model.ReferenceID = referenceId;
                            model.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                            model.Status = dr["ReviewStatus"].ToString();
                            if (dr["CompleteDate"] == null)
                                model.CompleteDate = "N/A";
                            else
                            {
                                bool isDate = DateTime.TryParse(dr["CompleteDate"].ToString(), out DateTime result);
                                if (isDate)
                                    model.CompleteDate = result.ToShortDateString();
                                else
                                    model.CompleteDate = "N/A";
                            }

                            model.ReviewTiming = dr["RequestTiming"].ToString();
                            model.TreatmentRequested = dr["RequestedServices"].ToString();
                            model.TreatmentRequestDates = dr["TreatmentRequestDates"].ToString();
                            bool isvalid = int.TryParse(dr["RequestQuantity"].ToString(), out int results);
                            if (isvalid)
                                model.TreatmentRequestQuantity = results;
                            else
                                model.TreatmentRequestQuantity = 0;
                            model.ActivityDate = DateTime.Parse(dr["ActivityDate"].ToString());
                            model.ActivityType = dr["ActivityType"].ToString();
                            model.ActivityComments = dr["Description"].ToString() == null ? "" : dr["Description"].ToString();
                            model.ActivityLetters = dr["LetterTitle"].ToString();
                            model.LetterLocation = dr["LetterLocation"].ToString();
                            model.ActivityId = int.Parse(dr["ActivityID"].ToString());
                            model.LetterTitle = dr["LetterTitle"].ToString() == null ? "" : dr["LetterTitle"].ToString();
                            model.URReqDate = dr["RequestDate"].ToString();
                            model.URCount = int.Parse(dr["URCount"].ToString());
                            var today = DateTime.Now;
                            DateTime birthdate = DateTime.Parse(dr["DOB"].ToString());
                            var calculatedAge = today.Year - birthdate.Year;
                            if (birthdate.Date > today.AddYears(-calculatedAge))
                                calculatedAge--;
                            model.MemberCurrentAge = calculatedAge.ToString();
                            model.Activities = GetActivities(model.MemberNumber);
                            models.Add(model);
                        }
                    }
                    conn.Close();
                }
            }


            return models;
        }

        public static URDetailsModel GetURDetails(string refID)
        {
            URDetailsModel model = new URDetailsModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
                {
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.CommandText = "SelectURDetails";
                        com.Connection = conn;
                        com.CommandType = System.Data.CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@RefNum", refID);
                        conn.Open();
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                model.MemberName = dr["Lastname"].ToString() + ", " + dr["FirstName"].ToString();
                                model.MemberID = dr["MemberNumber"].ToString();
                                model.MemberDOB = dr["DOB"].ToString();
                                model.MemberGender = dr["Gender"].ToString();
                                model.MemberSSN = dr["SSN"].ToString();
                                model.MemberPhone = dr["Phone"].ToString();
                                model.RefID = refID;
                                model.LevelofCare = dr["LevelOfCare"].ToString();
                                model.AuthType = dr["RequestType"].ToString();
                                model.TreatingFacility = dr["TreatingFacility"].ToString();
                                model.Diagnosis = dr["Diagnosis"].ToString();
                                model.RequestedServices = dr["RequestedServices"].ToString();
                                model.RequestedServiceQuantity = dr["RequestQuantity"].ToString();
                                model.RequestedServiceStartDate = DateTime.Parse(dr["RequestServiceStartDate"].ToString());
                                model.RequestedServiceEndDate = DateTime.Parse(dr["RequestServiceEndDate"].ToString());
                                model.MNRReviewRefID = refID;
                                model.MNRReviewType = dr["MNRReviewType"].ToString();
                                model.MNRReviewStatus = dr["MNRReviewStatus"].ToString();
                                model.MNRReviewStartDate = DateTime.Parse(dr["MNRStartDate"].ToString());
                                DateTime.TryParse(dr["MNRCompleteDate"].ToString(), out DateTime result1);
                                model.MNRReviewCompleteDate = result1;
                                model.QOCReviewRefID = refID;
                                model.QOCReviewType = dr["QOCReviewType"].ToString();
                                model.QOCReviewStatus = dr["QOCReviewStatus"].ToString();
                                model.QOCReviewStartDate = DateTime.Parse(dr["QOCStartDate"].ToString());
                                DateTime.TryParse(dr["QOCCompleteDate"].ToString(), out DateTime result2);
                                model.QOCReviewCompleteDate = result2;
                                model.ActivityDate = dr["ActivityDate"].ToString();
                                model.ActivityType = dr["ActivityType"].ToString();
                                model.ActivityComments = dr["Description"].ToString();
                                model.ActivityLetterTitle = dr["LetterTitle"].ToString();
                                model.ActivityLetterLocation = dr["LetterLocation"].ToString();
                                model.ActivityID = int.Parse(dr["ActivityID"].ToString());
                                model.RequestDate = dr["RequestDate"].ToString();
                                var today = DateTime.Now;
                                DateTime birthdate = DateTime.Parse(dr["DOB"].ToString());
                                var calculatedAge = today.Year - birthdate.Year;
                                if (birthdate.Date > today.AddYears(-calculatedAge))
                                    calculatedAge--;
                                model.MemberCurrentAge = calculatedAge.ToString();
                                model.URID = int.Parse(dr["Id"].ToString());
                            }

                            model.SupportingDocs = GetURSupportingDocs(model.URID);
                            model.Activities = GetActivities(model.MemberID);
                        }
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static List<Activity> GetActivities(string memberNumber)
        {
            List<Activity> activities = new List<Activity>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "SelectActivityDocumentsByNumber";
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@MemberNumber", memberNumber);
                    conn.Open();
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Activity activity = new Activity();
                            activity.ActivityID = int.Parse(dr["ActivityID"].ToString());
                            activity.ActivityDate = DateTime.Parse(dr["ActivityDate"].ToString());
                            activity.ActivityType = dr["ActivityType"].ToString();
                            activity.ContractDate = dr["ContactDate"].ToString();
                            activity.LetterLocation = dr["LetterLocation"].ToString();
                            activity.LetterTitle = dr["LetterTitle"].ToString();
                            activity.ReviewRef = int.Parse(dr["ReviewRef"].ToString());
                            activity.URRef = dr["UtilizationReqRef"].ToString();
                            activity.TagList = dr["TagList"].ToString();
                            activity.MemberNumber = dr["MemberNumber"].ToString();
                            activity.UserId = dr["UserId"].ToString();
                            activity.Description = dr["Description"].ToString();
                            activity.SecondsToComplete = dr["SecondsToComplete"].ToString();
                            activities.Add(activity);
                        }
                    }
                }
            }
            return activities;
        }

        private static List<URSupportingDocs> GetURSupportingDocs(int URid)
        {
            List<URSupportingDocs> docs = new List<URSupportingDocs>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "SelectURSupportingDocs";
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@URid", URid);
                    conn.Open();
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            URSupportingDocs doc = new URSupportingDocs();
                            doc.ID = int.Parse(dr["Id"].ToString());
                            if (dr["Location"].ToString() != null && dr["Location"].ToString() != "")
                            {
                                string subbed = dr["Location"].ToString().Substring(dr["Location"].ToString().LastIndexOf('/') + 1);
                                doc.DisplayLocation = subbed;
                                doc.Location = dr["Location"].ToString();
                            }
                            else
                            {
                                doc.DisplayLocation = string.Empty;
                                doc.Location = string.Empty;
                            }

                            doc.URID = int.Parse(dr["URId"].ToString());
                            docs.Add(doc);
                        }
                    }
                }
            }
            return docs;
        }

        public static DataTable GetClaimData(string ClaimNumber)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "GetClaimByClaimNumber";
                    com.Parameters.AddWithValue("@ClaimNumber", ClaimNumber);
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    dt.Load(com.ExecuteReader());
                    conn.Close();
                }
            }

            return dt;
        }

        public static DataTable GetClaimData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "GetAllClaims";
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    dt.Load(com.ExecuteReader());
                    conn.Close();
                }
            }

            return dt;
        }

        public static ClaimDetailsModel GetClaimDetails(string claimNumber)
        {
            ClaimDetailsModel model = null;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "GetClaimsDetails";
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@ClaimNumber", claimNumber);
                    conn.Open();
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            model = new ClaimDetailsModel();
                            model.MemberName = dr["Lastname"].ToString() + ", " + dr["FirstName"].ToString();
                            model.MemberDOB = dr["DOB"].ToString();
                            model.MemberNumber = dr["MemberNumber"].ToString();
                            model.MemberGender = dr["Gender"].ToString();
                            model.MemberSSN = dr["SSN"].ToString();
                            model.MemberPhone = dr["Phone"].ToString();
                            model.ClaimNumber = long.Parse(dr["ClaimNumber"].ToString());
                            model.DataDomain = dr["payerdatadomain"].ToString();
                            model.ELRelationToHolder = dr["relationtoholder"].ToString();
                            model.ELPolicyNumber = dr["policynumber"].ToString();
                            model.ELPayer = dr["payername"].ToString();
                            model.ELCoverageType = dr["coveragetype"].ToString();
                            model.Activities = GetActivities(dr["MemberNumber"].ToString());
                            model.uRDetails = GetURsByMemberNumber(dr["MemberNumber"].ToString());
                            model.Reviews = GetAllReviewsByMemberNumber(dr["MemberNumber"].ToString());
                            var today = DateTime.Now;
                            DateTime birthdate = DateTime.Parse(dr["DOB"].ToString());
                            var calculatedAge = today.Year - birthdate.Year;
                            if (birthdate.Date > today.AddYears(-calculatedAge))
                                calculatedAge--;
                            model.MemberCurrentAge = calculatedAge.ToString();
                        }
                    }
                }
            }

            return model;
        }

        public static List<URDetailsModel> GetURsByMemberNumber(string memberNumber)
        {
            List<URDetailsModel> list = new List<URDetailsModel>();
            URDetailsModel model = null;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "GetURsByMemberNumber";
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@MemberNumber", memberNumber);
                    conn.Open();
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            model = new URDetailsModel();
                            model.RefID = dr["ExternalID"].ToString();
                            model.RequestDate = dr["RequestDate"].ToString();
                            model.PolicyNumber = dr["policynumber"].ToString();

                            list.Add(model);
                        }
                    }
                }
            }

            return list;
        }

        public static List<ReviewDetailsModel> GetAllReviewsByMemberNumber(string memberNumber)
        {
            ReviewDetailsModel model = null;
            List<ReviewDetailsModel> mnrReviews = new List<ReviewDetailsModel>();
            List<ReviewDetailsModel> qocReviews = new List<ReviewDetailsModel>();
            List<ReviewDetailsModel> drgReview = new List<ReviewDetailsModel>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "GetAllMNRReviewsMyMemberNumber";
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@MemberNumber", memberNumber);
                    conn.Open();
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            model = new ReviewDetailsModel();
                            model.ReferenceID = dr["ReferenceNumber"].ToString();
                            model.ReviewType = dr["ReviewType"].ToString();
                            model.Status = dr["ReviewStatus"].ToString();
                            model.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                            model.CompleteDate = dr["CompleteDate"].ToString();
                            mnrReviews.Add(model);
                        }
                    }
                    com.CommandText = "GetAllQOCReviewsMyMemberNumber";
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model = new ReviewDetailsModel();
                            model.ReferenceID = reader["ReferenceNumber"].ToString();
                            model.ReviewType = reader["ReviewType"].ToString();
                            model.Status = reader["ReviewStatus"].ToString();
                            model.StartDate = DateTime.Parse(reader["StartDate"].ToString());
                            model.CompleteDate = reader["CompleteDate"].ToString();
                            qocReviews.Add(model);
                        }
                    }

                    com.CommandText = "GetAllDRGByMemberNumber";
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model = new ReviewDetailsModel();
                            model.ReferenceID = reader["ReferenceNumber"].ToString();
                            model.ReviewType = reader["ReviewType"].ToString();
                            model.Status = reader["ReviewStatus"].ToString();
                            model.StartDate = DateTime.Parse(reader["StartDate"].ToString());
                            model.CompleteDate = reader["CompleteDate"].ToString();
                            drgReview.Add(model);
                        }
                    }
                    conn.Close();
                }
            }

            var combined = qocReviews.Concat(mnrReviews);
            var combined2 = combined.Concat(drgReview);
            return combined2.ToList();
        }

        public static ClaimDetailsModel GetMemberDetails(string memberNumber)
        {
            ClaimDetailsModel model = null;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.CommandText = "GetMemberDetails";
                    com.Connection = conn;
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@MemberNumber", memberNumber);
                    conn.Open();
                    using (SqlDataReader dr = com.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            model = new ClaimDetailsModel();
                            model.MemberName = dr["Lastname"].ToString() + ", " + dr["FirstName"].ToString();
                            model.MemberDOB = dr["DOB"].ToString();
                            model.MemberNumber = dr["MemberNumber"].ToString();
                            model.MemberGender = dr["Gender"].ToString();
                            model.MemberSSN = dr["SSN"].ToString();
                            model.MemberPhone = dr["Phone"].ToString();
                            model.ClaimNumber = long.Parse(dr["ClaimNumber"].ToString());
                            model.DataDomain = dr["payerdatadomain"].ToString();
                            model.ELRelationToHolder = dr["relationtoholder"].ToString();
                            model.ELPolicyNumber = dr["policynumber"].ToString();
                            model.ELPayer = dr["payername"].ToString();
                            model.ELCoverageType = dr["coveragetype"].ToString();
                            model.Activities = GetActivities(dr["MemberNumber"].ToString());
                            model.uRDetails = GetURsByMemberNumber(dr["MemberNumber"].ToString());
                            model.Reviews = GetAllReviewsByMemberNumber(dr["MemberNumber"].ToString());
                            var today = DateTime.Now;
                            DateTime birthdate = DateTime.Parse(dr["DOB"].ToString());
                            var calculatedAge = today.Year - birthdate.Year;
                            if (birthdate.Date > today.AddYears(-calculatedAge))
                                calculatedAge--;
                            model.MemberCurrentAge = calculatedAge.ToString();
                        }
                    }
                }
            }

            return model;
        }

        public static ReviewDetailsModel GetDRGDetails(string refID)
        {
            ReviewDetailsModel mid = new ReviewDetailsModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
                {
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.CommandText = "SelectDRGDetails";
                        com.Connection = conn;
                        com.CommandType = System.Data.CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@RefID", refID);
                        conn.Open();
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                mid = new ReviewDetailsModel();
                                mid.MemberName = dr["LastName"].ToString() + ", " + dr["FirstName"].ToString();
                                mid.MemberNumber = dr["MemberNumber"].ToString();
                                mid.MemberDOB = dr["DOB"].ToString();
                                mid.MemberGender = dr["Gender"].ToString();
                                mid.MemberSSN = dr["SSN"].ToString();
                                mid.MemberPhone = dr["Phone"].ToString();
                                mid.ReferenceID = dr["ReferenceNumber"].ToString();
                                mid.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                                mid.Status = dr["ReviewStatus"].ToString();
                                mid.CompleteDate = dr["CompleteDate"].ToString();
                                mid.ReviewTiming = dr["RequestSource"].ToString();
                                var today = DateTime.Now;
                                DateTime birthdate = DateTime.Parse(dr["DOB"].ToString());
                                var calculatedAge = today.Year - birthdate.Year;
                                if (birthdate.Date > today.AddYears(-calculatedAge))
                                    calculatedAge--;
                                mid.MemberCurrentAge = calculatedAge.ToString();
                                mid.DiagnosisList = GetAllDiagnoses(refID);
                                mid.ProcedureList = GetAllProcedures(refID);
                                //mid.DischargeDisposition = dr["DischargeDisposition"].ToString();
                                mid.DiagnosisRelatedGroupDescription = dr["DRGDescription"].ToString();
                                mid.CurrentDiagnosisRelatedGroupCode = dr["CurrentDRGCode"].ToString();
                                mid.OriginalDiagnosisRelatedGroupCode = dr["OriginalDRGCode"].ToString();
                            }

                        }
                    }
                }
                return mid;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<Diagnosis> GetAllDiagnoses(string refID)
        {
            List<Diagnosis> mid = new List<Diagnosis>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
                {
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.CommandText = "GetDiagnosesByRef";
                        com.Connection = conn;
                        com.CommandType = System.Data.CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@RefID", refID);
                        conn.Open();
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Diagnosis diagnosis = new Diagnosis();
                                diagnosis.Description = dr["Description"].ToString();
                                diagnosis.Code = dr["Code"].ToString();                                  
                                mid.Add(diagnosis);
                            }
                        }
                    }
                }

                return mid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Procedure> GetAllProcedures(string refID)
        {
            List<Procedure> mid = new List<Procedure>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString))
                {
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.CommandText = "GetProceduresByRef";
                        com.Connection = conn;
                        com.CommandType = System.Data.CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@RefID", refID);
                        conn.Open();
                        using (SqlDataReader dr = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Procedure procedure = new Procedure();
                                procedure.ProcedureDescription = dr["Description"].ToString();
                                procedure.ProcedureCode = dr["Code"].ToString();
                                procedure.PrimaryProcedure = dr["PrimaryProcedure"].ToString();
                                mid.Add(procedure);
                            }
                        }
                    }
                }

                return mid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}