using Czone.Core.Data.SID;
using System.Collections.Generic;

namespace Czone.Data.WcfService.DAL.Interfaces
{
    public interface ISSRSService
    {
        IEnumerable<SSRSDto> GetSSRSReportsBySchoolID(int schoolID);
    }
}