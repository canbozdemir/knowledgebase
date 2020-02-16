using KnowledgeBase.Api.Infrastructure.Commands;
using KnowledgeBase.Core.Entitties;
using KnowledgeBase.Core.Infrastructure.Context;
using KnowledgeBase.Core.Infrastructure.Handlers;
using KnowledgeBase.Core.Infrastructure.Repositories;

namespace KnowledgeBase.Api.Infrastructure.Handlers
{
    public class CreateInformationCommandHandler : ICommandHandler<CreateInformationCommand, Information>
    {
        private readonly IInformationRepository _repository;
        private readonly IHttpRequest _httpRequest;

        public CreateInformationCommandHandler(IInformationRepository repository, IHttpRequest httpRequest)
        {
            this._repository = repository;
            this._httpRequest = httpRequest;
        }

        public Information Execute(CreateInformationCommand command)
        {
            Information information = new Information
            {
                Title = command.Title,
                Description = command.Description,
                UserId = _httpRequest.UserId
            };

            _repository.Create(information);

            return information;
        }
    }
}
