using KnowledgeBase.Core.Entitties;
using KnowledgeBase.Core.Infrastructure.Context;
using KnowledgeBase.Core.Infrastructure.Queries;
using KnowledgeBase.Core.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeBase.Api.Infrastructure.Services
{
    public class InformationService : IInformationService
    {
        private readonly IInformationRepository _informationRepository;
        private readonly IHttpRequest _httpRequest;

        public InformationService(IInformationRepository informationRepository, IHttpRequest httpRequest)
        {
            this._informationRepository = informationRepository;
            this._httpRequest = httpRequest;
        }

        public IEnumerable<Information> All(string text, bool isCurrentUser, int pageStartIndex, int pageSize)
        {
            IQueryable<Information> query = _informationRepository.All();

            if (isCurrentUser)
                query = query.Where(x => x.UserId == _httpRequest.UserId);

            if (!string.IsNullOrEmpty(text))
            {
                query = query.Where(x => x.Title.Contains(text.ToLower()) || x.Description.Contains(text.ToLower()));
            }

            return query.OrderByDescending(x => x.CreatedDate).Skip(pageStartIndex * pageSize).Take(pageSize);
        }
    }
}