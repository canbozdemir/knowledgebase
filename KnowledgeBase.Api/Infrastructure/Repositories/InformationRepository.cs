using KnowledgeBase.Core.Data;
using KnowledgeBase.Core.Entitties;
using KnowledgeBase.Core.Infrastructure.Repositories;

namespace KnowledgeBase.Api.Infrastructure.Repositories
{
    public class InformationRepository : BaseRepository<Information>, IInformationRepository
    {
        public InformationRepository(KnowledgeBaseDbContext dbContext) : base(dbContext)
        {
        }
    }
}
