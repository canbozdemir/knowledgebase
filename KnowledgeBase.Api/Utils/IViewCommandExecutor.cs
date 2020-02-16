using KnowledgeBase.Core.Infrastructure.Commands;

namespace KnowledgeBase.Api.Utils
{
    public interface IViewCommandExecutor
    {
        ICommandExecutor Map<TSource, TDestination>(TSource viewModel) where TDestination : ICommand;
        TCommandOutput Execute<TCommand, TCommandOutput>(TCommand command) where TCommand : ICommand;
    }

    public interface ICommandExecutor
    {
        ICommandOutput Execute<ICommandOutput>();
    }
}
