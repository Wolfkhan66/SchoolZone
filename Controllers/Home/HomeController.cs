using log4net;
using SchoolZone.SID.Mvc.Core.Helpers;
using SchoolZone.SID.Mvc.Models;
using SchoolZone.SID.Mvc.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SchoolZone.SID.Mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ISidServiceHelper _sidServiceHelper;

        public HomeController(ISidServiceHelper sidServiceHelper)
        {
            _sidServiceHelper = sidServiceHelper;
        }

        [Route(Name = "home")]
        public ActionResult Index()
        {
            return GetSchools("A", 0);
        }

        [Route("getSchools/{A2Z}/{Phase}", Name = "getSchools")]
        public ActionResult GetSchools(string A2Z, int Phase)
        {
            SidIndexViewModel model = new SidIndexViewModel(null, new List<SchoolModel>(), Phase, A2Z, "", false);

            var user = ClaimsPrincipal.Current;
            model.User = user.Identity.GetLoginWithDomain();
            model.ESCC = IsUserESCC();

            // Get a list of schools filtered by navigation and phase
            model.Schools = _sidServiceHelper.ReadAllSchoolInfo(A2Z, Phase);

            // Get a list of the schools the user has access to.
            var UserSchools = model.ESCC ? model.Schools : SidHelper.FindSchoolForUser(_sidServiceHelper);
            foreach (var item in UserSchools)
            {
                // Get each schools details.
                model.UserSchools.Add(_sidServiceHelper.ReadSchoolBySchoolId(item.Id));
            }
            model.UserSchools = GetUserSchoolsInfo(model.UserSchools);

            // Remove schools from the general list that are in the users schools list.
            model.Schools = model.Schools.Except(model.UserSchools).ToList();
            model.Schools = GetGeneralSchoolsInfo(model.Schools);

            return View("Index", model);
        }

        private bool IsUserESCC()
        {
            var claims = ClaimsPrincipal.Current.Claims;
            var ESCCGroups = ConfigurationManager.AppSettings["EsccADGroups"].Split(',').ToList();
            foreach (var group in ESCCGroups)
            {
                foreach (var item in claims)
                {
                    if (item.Value.Contains(group)) return true;
                }
            }
            return false;
        }

        private List<SchoolModel> GetGeneralSchoolsInfo(List<SchoolModel> model)
        {
            for (int index = 0; index < model.Count; index++)
            {
                var schoolID = model[index].Id;
                model[index].Contacts = GetContacts(schoolID);
                model[index].Website = (_sidServiceHelper.ReadSchoolUrlBySchoolId(schoolID) == null) ? "None Found" : _sidServiceHelper.ReadSchoolUrlBySchoolId(schoolID).LinkUrl;
            }
            return model;
        }

        private List<SchoolModel> GetUserSchoolsInfo(List<SchoolModel> model)
        {
            for (int index = 0; index < model.Count; index++)
            {
                var schoolID = model[index].Id;
                model[index].Contacts = GetContacts(schoolID);
                model[index].Clusters = GetClusters(schoolID);
                model[index].Report = String.Format("https://eastsussex.sharepoint.com/sites/czone/{0}/Forms/AllItems.aspx", model[index].Code);
                model[index].Website = (_sidServiceHelper.ReadSchoolUrlBySchoolId(schoolID) == null) ? "None Found" : _sidServiceHelper.ReadSchoolUrlBySchoolId(schoolID).LinkUrl;
                model[index].SSRSReports = _sidServiceHelper.GetSSRSReportsBySchoolID(schoolID);

                for (int i = 0; i < model[index].SSRSReports.Count; i++)
                {
                    giveDisplayName(model[index].SSRSReports[i]);
                }
                model[index].SSRSReports = model[index].SSRSReports.OrderBy(x => x.DisplayName).ToList();
                model[index].ReportServerURL = ConfigurationManager.AppSettings["SidReportServerUrl"];
            }
            return model;
        }

        private void giveDisplayName(SSRSReportsModel model)
        {
            switch (model.DocumentName)
            {
                case "Teaching staff - Individual - Table":
                    model.DisplayName = "Table - Teaching staff";
                    break;
                case "Clusters - Individual - Table":
                    model.DisplayName = "Table - Organisational clusters";
                    break;
                case "Clusters - All schools - Table":
                    model.DisplayName = "Table - Organisational clusters - All schools";
                    break;
                case "Absence - Individual - Table":
                    model.DisplayName = "Table - Pupil absences";
                    break;
                case "SEN - Individual - Table":
                    model.DisplayName = "Table - Statemented and non-statemented children";
                    break;
                case "FSM - Individual - Table":
                    model.DisplayName = "Table - Free school meal eligibility";
                    break;
                case "Ethnicity - Individual - Table":
                    model.DisplayName = "Table - Pupil ethnicity";
                    break;
                case "Absence - Individual - Chart":
                    model.DisplayName = "Chart - Pupil absence percentages";
                    break;
                case "NOR - Individual - Table":
                    model.DisplayName = "Table - Pupil numbers on roll";
                    break;
                case "School Census - Number on roll - Chart":
                    model.DisplayName = "Chart - Pupil numbers on roll";
                    break;
                case "School Census - All schools - Summary":
                    model.DisplayName = "~ Summary for all schools";
                    break;
                case "School Census - Free school meals - Chart":
                    model.DisplayName = "Chart - Free school meal eligibility";
                    break;
                case "School Census - Ethnicity - Chart":
                    model.DisplayName = "Chart - Ethnicity";
                    break;
                case "School Census - SEN type - Chart":
                    model.DisplayName = "Chart - Special educational needs";
                    break;
                case "School Census - This school - Summary":
                    model.DisplayName = "~ Summary for this school";
                    break;
            }
        }

        private SIDTableModel GetContacts(int SchoolID)
        {
            DataTable contacts = new DataTable();
            contacts.Columns.Add("Name", typeof(string));
            contacts.Columns.Add("Role", typeof(string));
            contacts.Columns.Add("Start Date", typeof(string));
            contacts.Columns.Add("Email", typeof(HtmlString));

            // Query Sid Service for contacts, and filter by contacts with an email.
            var contactslist = _sidServiceHelper.ReadSchoolContacts(SchoolID, true).Where(contact => contact.Email != "").ToList();

            // for each Contact in the list , add as a new row to the contacts datatable
            for (int y = 0; y < contactslist.Count; y++)
            {
                HtmlString Email = new HtmlString("<a href=mailto:" + contactslist[y].Email + ">" + contactslist[y].Email + "</a>");
                contacts.Rows.Add(contactslist[y].FullName, contactslist[y].PersonRoleName, contactslist[y].StartDate.ToShortDateString(), Email);
            }

            return new SIDTableModel(contacts, ""); ;
        }

        private SIDTableModel GetClusters(int SchoolID)
        {
            DataTable clusters = new DataTable();
            clusters.Columns.Add("Cluster Type", typeof(string));
            clusters.Columns.Add("Cluster Name", typeof(string));

            // Query Sid Service for Clusters information and Cluster Areas by school id
            var clusterAreas = _sidServiceHelper.ReadClusterAreasBySchool(SchoolID);
            var clustersList = _sidServiceHelper.ReadAllClusters();

            // for each cluster in the list, check the id and add the corresponding row to the clusters table
            foreach (var item in clusterAreas)
            {
                clusters.Rows.Add(clustersList.SingleOrDefault(x => x.ClusterId == item.ClusterId).ClusterName, item.Name);
            }
            return new SIDTableModel(clusters, ""); ;
        }
    }
}