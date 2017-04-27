using System.Collections.Generic;
using System.Data;

namespace SchoolZone.SID.Mvc.Models
{
    public class SchoolModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Address { get; set; }
        
        public int? AgeRangeLower { get; set; }
        
        public int? AgeRangeUpper { get; set; }
        
        public string Code { get; set; }
        
        public string CostCentre { get; set; }
        
        public string CourierVisits { get; set; }
        
        public string Fax { get; set; }
        
        public string FullCode { get; set; }
        
        public string Gender { get; set; }
        
        public bool IncludeInSIDExternal { get; set; }
        
        public bool IncludeInSchoolClosures { get; set; }
        
        public bool IncludeInSchoolContacts { get; set; }
        
        public bool IncludeInSchoolDirectory { get; set; }
        
        public string LEACode { get; set; }
        
        public string OfficeEmail { get; set; }
        
        public int PhaseId { get; set; }
        
        public string PhaseName { get; set; }
        
        public int SchoolStatusId { get; set; }
        
        public string SchoolStatusName { get; set; }
        
        public int SchoolTypeId { get; set; }
        
        public string SchoolTypeName { get; set; }
        
        public string SchoolURN { get; set; }
        
        public string Specialism { get; set; }
        
        public string Telephone { get; set; }

        public SIDTableModel Contacts { get; set; }
        public SIDTableModel Clusters { get; set; }
        public string Report { get; set; }
        public List<SSRSReportsModel> SSRSReports { get; set; }
        public string ReportServerURL { get; set; }
        public string Website { get; set; }
    }
}