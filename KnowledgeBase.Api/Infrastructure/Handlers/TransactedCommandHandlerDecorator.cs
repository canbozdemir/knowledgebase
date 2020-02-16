using KnowledgeBase.Core.Data;
using KnowledgeBase.Core.Infrastructure.Commands;
using KnowledgeBase.Core.Infrastructure.Handlers;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBase.Api.Infrastructure.Handlers
{
    public class TransactedCommandHandlerDecorator<TCommand, TCommandOutput> : ICommandHandler<TCommand, TCommandOutput> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand, TCommandOutput> _commandHandler;
        private readonly DbContext _dbContext;

        public TransactedCommandHandlerDecorator(ICommandHandler<TCommand, TCommandOutput> commandHandler, KnowledgeBaseDbContext dbContext)
        {
            this._commandHandler = commandHandler;
            this._dbContext = dbContext;
        }

        public TCommandOutput Execute(TCommand command)
        {
            TCommandOutput output = _commandHandler.Execute(command);

            _dbContext.SaveChanges();

            return output;
        }
    }
}
