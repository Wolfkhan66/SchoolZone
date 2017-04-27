namespace SchoolZone.SID.Mvc.Models
{
    public class SSRSReportsModel
    {
        public string DocumentName { get; set; }
        public string ExtraReportParameters { get; set; }
        public int DocumentID { get; set; }
        public int DocumentTypeID { get; set; }
        public int TopicID { get; set; }
        public int CategoryID { get; set; }
        public int SchoolID { get; set; }

        public string DisplayName { get; set; }
    }
}