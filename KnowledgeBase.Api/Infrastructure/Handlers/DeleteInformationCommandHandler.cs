using KnowledgeBase.Api.Infrastructure.Commands;
using KnowledgeBase.Core.Entities;
using KnowledgeBase.Core.Entitties;
using KnowledgeBase.Core.Infrastructure.Context;
using KnowledgeBase.Core.Infrastructure.Handlers;
using KnowledgeBase.Core.Infrastructure.Repositories;

namespace KnowledgeBase.Api.Infrastructure.Handlers
{
    public class DeleteInformationCommandHandler : ICommandHandler<DeleteInformationCommand, Information>
    {
        private readonly IInformationRepository _repository;
        private readonly IHttpRequest _httpRequest;

        public DeleteInformationCommandHandler(IInformationRepository repository, IHttpRequest httpRequest)
        {
            this._repository = repository;
            this._httpRequest = httpRequest;
        }

        public Information Execute(DeleteInformationCommand command)
        {
            Information information = _repository.GetById(command.Id);

            if (information.UserId != _httpRequest.UserId)
                throw new NotAllowedOperationException();

            _repository.Delete(information);

            return information;
        }
    }
}
