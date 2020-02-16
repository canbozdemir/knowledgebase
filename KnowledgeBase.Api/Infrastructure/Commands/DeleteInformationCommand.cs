using KnowledgeBase.Core.Infrastructure.Commands;

namespace KnowledgeBase.Api.Infrastructure.Commands
{
    public class DeleteInformationCommand : ICommand
    {
        public int Id { get; set; }
    }
}
