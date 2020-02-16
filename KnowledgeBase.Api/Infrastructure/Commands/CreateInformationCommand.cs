
using KnowledgeBase.Core.Infrastructure.Commands;

namespace KnowledgeBase.Api.Infrastructure.Commands
{
    public class CreateInformationCommand : ICommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
