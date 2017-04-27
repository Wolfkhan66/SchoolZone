using AutoMapper;
using Czone.Core.Data.SID;
using Czone.Data.SidModel;
using Czone.Data.WcfService.DAL.Interfaces;
using Czone.Data.WcfService.Data;
using Czone.Data.WcfService.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Czone.Data.WcfService.DAL
{
    public class SSRSService : ISSRSService
    {
        private IRepository<Category> CategoryRepository { get; set; }
        private IRepository<Document> DocumentRepository { get; set; }
        private IRepository<Link> LinkRepository { get; set; }
        private IRepository<Topic> TopicRepository { get; set; }

        public SSRSService(SchoolInformationEntities context)
        {
            CategoryRepository = new SIDRepository<Category>(context);
            DocumentRepository = new SIDRepository<Document>(context);
            LinkRepository = new SIDRepository<Link>(context);
            TopicRepository = new SIDRepository<Topic>(context);
        }

        public IEnumerable<SSRSDto> GetSSRSReportsBySchoolID(int schoolID)
        {
            var query =
                from x in DocumentRepository.All()
                join y in LinkRepository.All() on x.DocumentID equals y.DocumentID
                where x.DocumentTypeID == 2 && y.SchoolID == schoolID 
                select new SSRSJoin {DocumentID = x.DocumentID, DocumentName = x.DocumentFilename,
                DocumentTypeID = (int)x.DocumentTypeID, ExtraReportParameters = y.ExtraReportParameters, TopicID = y.TopicID, SchoolID = (int)y.SchoolID };

            var data = Mapper.Map<IEnumerable<SSRSJoin>, IEnumerable<SSRSDto>>(query.AsEnumerable());
            return data;
        }
    }
}