using AutoMapper;
using KnowledgeBase.Core.Entities;
using KnowledgeBase.Core.Infrastructure.Commands;
using KnowledgeBase.Core.Infrastructure.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace KnowledgeBase.Api.Utils
{
    public class ViewCommandExecutor : IViewCommandExecutor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;

        public ViewCommandExecutor(IServiceProvider serviceProvider, IMapper mapper)
        {
            this._serviceProvider = serviceProvider;
            this._mapper = mapper;
        }

        public ICommandExecutor Map<TViewModel, TCommand>(TViewModel viewModel) where TCommand : ICommand
        {
            TCommand command = _mapper.Map<TViewModel, TCommand>(viewModel);

            return new CommandExecutor<TCommand>(command, _serviceProvider);
        }

        public TCommandOutput Execute<TCommand, TCommandOutput>(TCommand command) where TCommand : ICommand
        {
            return new CommandExecutor<TCommand>(command, _serviceProvider).Execute<TCommandOutput>();
        }
    }

    public class CommandExecutor<TCommand> : ICommandExecutor where TCommand : ICommand
    {
        private readonly TCommand _command;
        private readonly IServiceProvider _serviceProvider;

        public CommandExecutor(TCommand command, IServiceProvider serviceProvider)
        {
            this._command = command;
            this._serviceProvider = serviceProvider;
        }

        public TCommandOutput Execute<TCommandOutput>()
        {
            return Execute<TCommand, TCommandOutput>(_command);
        }

        private TOutput Execute<TInput, TOutput>(TInput inputCommand) where TInput : ICommand
        {
            ICommandHandler<TInput, TOutput> commandHandler = _serviceProvider.GetService<ICommandHandler<TInput, TOutput>>();

            if (commandHandler == null)
                throw new UnknownCommandException();

            return commandHandler.Execute(inputCommand);
        }
    }
}
