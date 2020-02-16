using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using KnowledgeBase.Api.Infrastructure.Commands;
using KnowledgeBase.Api.Models;
using KnowledgeBase.Api.Utils;
using KnowledgeBase.Core.Entitties;
using KnowledgeBase.Core.Infrastructure.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Api.Controllers
{
    [Route("api/informations")]
    [ApiController]
    [Authorize]
    public class InformationController : ControllerBase
    {
        private const int PageSize = 10;

        private readonly IInformationService _informationService;
        private readonly IViewCommandExecutor _commandExecutor;
        private readonly IMapper _mapper;

        public InformationController(IInformationService informationService, IViewCommandExecutor commandExecutor, IMapper mapper)
        {
            this._informationService = informationService;
            this._commandExecutor = commandExecutor;
            this._mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<InformationModel>> Get(string text, bool isCurrentUser, int pageStartIndex)
        {
            List<Information> informations = _informationService.All(text, isCurrentUser, pageStartIndex, PageSize).ToList();
            List<InformationModel> informationList = _mapper.Map<List<Information>, List<InformationModel>>(informations);

            return informationList.Count() > 0 ? informationList : null;
        }

        [HttpPost]
        public ActionResult<Information> Post([FromBody] InformationCreateModel informationCreateModel)
        {
            Information information = _commandExecutor.Map<InformationCreateModel, CreateInformationCommand>(informationCreateModel).Execute<Information>();

            return information;
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            _commandExecutor.Execute<DeleteInformationCommand, Information>(new DeleteInformationCommand { Id = id });

            return true;
        }
    }
}
