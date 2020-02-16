using KnowledgeBase.Core.Entitties;
using System.Collections.Generic;

namespace KnowledgeBase.Core.Infrastructure.Queries
{
    public interface IInformationService
    {
        IEnumerable<Information> All(string text, bool isCurrentUser, int pageStartIndex, int pageSize);
    }
}