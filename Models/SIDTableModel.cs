using System.Data;

namespace SchoolZone.SID.Mvc.Models
{
    public class SIDTableModel
    {
        public DataTable Table { get; set; }
        public string TableID { get; set; }

        public SIDTableModel(DataTable table, string tableID)
        {
            Table = table;
            TableID = tableID;
        }
    }
}