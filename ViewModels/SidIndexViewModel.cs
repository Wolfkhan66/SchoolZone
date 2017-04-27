using SchoolZone.SID.Mvc.Models;
using System.Collections.Generic;

namespace SchoolZone.SID.Mvc.ViewModels
{
    public class SidIndexViewModel
    {
        public List<string> A2Z { get; set; }
        public List<SchoolModel> Schools { get; set; }
        public List<SchoolModel> UserSchools { get; set; }
        public int SchoolPhase { get; set; }
        public string Nav { get; set; }
        public string User { get; set; }
        public bool ESCC { get; set; }
        public SidIndexViewModel(List<SchoolModel> schools, List<SchoolModel> userSchools, int schoolPhase, string nav, string user, bool escc)
        {
            Schools = schools;
            A2Z = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "All" }; ;
            UserSchools = userSchools;
            SchoolPhase = schoolPhase;
            Nav = nav;
            User = user;
            ESCC = escc;
        }
    }
}