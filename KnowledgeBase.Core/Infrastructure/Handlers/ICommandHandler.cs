using KnowledgeBase.Core.Infrastructure.Commands;

namespace KnowledgeBase.Core.Infrastructure.Handlers
{
    public interface ICommandHandler<in TInput, out TOutput> where TInput : ICommand
    {
        TOutput Execute(TInput command);
    }
}