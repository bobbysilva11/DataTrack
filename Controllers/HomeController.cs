using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DataTrack.Data;
using DataTrack.Models;


namespace DataTrack.Controllers
{
    public class HomeController : Controller
    {

        private static bool UserVaild()
        {
            string[] users = ConfigurationManager.AppSettings["Users"].Split(';');
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append(userName);
            sb.Append("  ---  ");
            sb.Append(DateTime.Now);
            if(Directory.Exists(ConfigurationManager.AppSettings["Logs"]))
                System.IO.File.AppendAllText(ConfigurationManager.AppSettings["Logs"] + "log.txt", sb.ToString());
            string user = userName.Substring(userName.IndexOf('\\') + 1);
            if (users.Contains(user.ToLower()))
                return true;
            else
                return false;

        }
        public ActionResult MemberSearch()
        {            
            bool isValid = UserVaild();
            if (isValid)
            {
                HomeModel model = new HomeModel();
                var result = new List<dynamic>();
                DataTable dt = HomeData.GetMemberData();

                foreach (DataRow dr in dt.Rows)
                {
                    var obj = (IDictionary<string, object>)new ExpandoObject();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        obj.Add(dc.ColumnName, dr[dc.ColumnName]);
                    }
                    result.Add(obj);
                }
                model.memberDt = result;
                model.webGrid = new WebGrid(result);
                return View(model);
            }
            else
            {
                return View("PermissionDenied");
            }

        }
        [HttpPost]
        public ActionResult MemberSearch(string LastName = "", string FirstName = "", string MemberNumber = "")
        {
            bool isValid = UserVaild();
            if (isValid)
            {
                HomeModel model = new HomeModel();
                var result = new List<dynamic>();
                DataTable dt = HomeData.GetMemberData(LastName, FirstName, MemberNumber);

                foreach (DataRow dr in dt.Rows)
                {
                    var obj = (IDictionary<string, object>)new ExpandoObject();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        obj.Add(dc.ColumnName, dr[dc.ColumnName]);
                    }
                    result.Add(obj);
                }
                model.memberDt = result;
                model.webGrid = new WebGrid(result);
                model.SearchType = "Member";
                return View(model);
            }
            else
            {
                return View("PermissionDenied");
            }

        }

        [HttpPost]
        public ActionResult ReviewSearch(string ReferenceID)
        {
            bool isValid = UserVaild();
            if (isValid)
            {
                HomeModel model = new HomeModel();
                var result = new List<dynamic>();
                DataTable dt = HomeData.GetMNRReviewData(ReferenceID);
                DataTable dt2 = HomeData.GetQOCReviewData(ReferenceID);
                dt.Merge(dt2);
                DataTable filtered;
                if (ReferenceID != "")
                {
                    if (dt.AsEnumerable().Where(a => a.Field<string>("ReferenceNumber").ToLower().StartsWith(ReferenceID.ToLower())).Any())
                    {
                        filtered = dt.AsEnumerable().Where(a => a.Field<string>("ReferenceNumber").ToLower().StartsWith(ReferenceID.ToLower())).CopyToDataTable();
                        foreach (DataRow dr in filtered.Rows)
                        {
                            var obj = (IDictionary<string, object>)new ExpandoObject();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                obj.Add(dc.ColumnName, dr[dc.ColumnName]);
                            }
                            result.Add(obj);
                        }
                    }
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var obj = (IDictionary<string, object>)new ExpandoObject();
                        foreach (DataColumn dc in dt.Columns)
                        {
                            obj.Add(dc.ColumnName, dr[dc.ColumnName]);
                        }
                        result.Add(obj);
                    }
                }


                model.memberDt = result;
                model.webGrid = new WebGrid(result);
                model.SearchType = "Review";
                return View(model);
            }
            else
            {
                return View("PermissionDenied");
            }

        }

        public ActionResult ReviewSearch()
        {
            bool isValid = UserVaild();
            if (isValid)
            {
                HomeModel model = new HomeModel();
                var result = new List<dynamic>();
                DataTable dt = HomeData.GetMNRReviewData();
                DataTable dt2 = HomeData.GetQOCReviewData();
                dt.Merge(dt2);
                DataView dv = dt.DefaultView;
                dv.Sort = "ReferenceNumber asc";
                DataTable sorted = dv.ToTable();
                foreach (DataRow dr in sorted.Rows)
                {
                    var obj = (IDictionary<string, object>)new ExpandoObject();
                    foreach (DataColumn dc in sorted.Columns)
                    {
                        obj.Add(dc.ColumnName, dr[dc.ColumnName]);
                    }
                    result.Add(obj);
                }
                model.memberDt = result;
                model.webGrid = new WebGrid(result);
                model.SearchType = "Review";
                return View(model);
            }
            else
            {
                return View("PermissionDenied");
            }

        }

        public ActionResult ReviewDetails(string id, string revType)
        {
            bool isValid = UserVaild();
            if (isValid)
            {
                List<ReviewDetailsModel> model;
                if (revType == "Medical Necessity")
                    model = HomeData.GetMNRMemberDetails(id, revType);
                else
                    model = HomeData.GetQOCMemberDetails(id, revType);
                return View(model);
            }
            else
            {
                return View("PermissionDenied");
            }

        }

        public ActionResult URDetails(string id)
        {
            bool isValid = UserVaild();
            if (isValid)
            {
                URDetailsModel model = HomeData.GetURDetails(id);

                return View(model);
            }
            else
            {
                return View("PermissionDenied");
            }

        }

        public ActionResult DRGDetails(string id)
        {
            bool isValid = UserVaild();
            if (isValid)
            {
                ReviewDetailsModel model = HomeData.GetDRGDetails(id);

                return View(model);
            }
            else
            {
                return View("PermissionDenied");
            }

        }

        public ActionResult ClaimSearch()
        {
            bool isValid = UserVaild();
            if (isValid)
            {
                HomeModel model = new HomeModel();
                var result = new List<dynamic>();
                DataTable dt = HomeData.GetClaimData();

                foreach (DataRow dr in dt.Rows)
                {
                    var obj = (IDictionary<string, object>)new ExpandoObject();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        obj.Add(dc.ColumnName, dr[dc.ColumnName]);
                    }
                    result.Add(obj);
                }
                model.memberDt = result;
                model.webGrid = new WebGrid(result);
                return View(model);
            }
            else
            {
                return View("PermissionDenied");
            }

        }
        [HttpPost]
        public ActionResult ClaimSearch(string ClaimNumber)
        {
            bool isValid = UserVaild();
            if (isValid)
            {
                HomeModel model = new HomeModel();
                var result = new List<dynamic>();
                DataTable dt = HomeData.GetClaimData();
                DataTable filtered = new DataTable();
                if (ClaimNumber != "")
                {
                    if (dt.AsEnumerable().Where(a => a.Field<Int64>("ClaimNumber").ToString().ToLower().StartsWith(ClaimNumber.ToLower())).Any())
                    {
                        filtered = dt.AsEnumerable().Where(a => a.Field<Int64>("ClaimNumber").ToString().ToLower().StartsWith(ClaimNumber.ToLower())).CopyToDataTable();

                        foreach (DataRow dr in filtered.Rows)
                        {
                            var obj = (IDictionary<string, object>)new ExpandoObject();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                obj.Add(dc.ColumnName, dr[dc.ColumnName]);
                            }
                            result.Add(obj);
                        }
                    }

                }
                else
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        var obj = (IDictionary<string, object>)new ExpandoObject();
                        foreach (DataColumn dc in dt.Columns)
                        {
                            obj.Add(dc.ColumnName, dr[dc.ColumnName]);
                        }
                        result.Add(obj);
                    }
                }

                model.memberDt = result;
                model.webGrid = new WebGrid(result);
                return View(model);
            }
            else
            {
                return View("PermissionDenied");
            }

        }

        public ActionResult ClaimDetails(string ClaimNumber)
        {
            bool isValid = UserVaild();
            if (isValid)
            {
                ClaimDetailsModel model = HomeData.GetClaimDetails(ClaimNumber);

                return View(model);
            }
            else
            {
                return View("PermissionDenied");
            }

        }

        public ActionResult MemberDetails(string memberNumber)
        {
            bool isValid = UserVaild();
            if (isValid)
            {
                ClaimDetailsModel model = HomeData.GetMemberDetails(memberNumber);
                return View(model);
            }
            else
            {
                return View("PermissionDenied");
            }

        }
    }
}