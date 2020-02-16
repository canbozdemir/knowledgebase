using AutoMapper;
using KnowledgeBase.Core.Entitties;

namespace KnowledgeBase.Api.Models
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<InformationCreateModel, Information>();
        }
    }
}
