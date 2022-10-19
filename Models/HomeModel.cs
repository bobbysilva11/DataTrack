using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace DataTrack.Models
{
    public class HomeModel
    {
        public List<dynamic> memberDt { get; set; }
        public WebGrid webGrid { get; set; }
        public string SearchType { get; set; }
    }
}