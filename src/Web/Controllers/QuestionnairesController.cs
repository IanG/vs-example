using System.Text.Json;
using System.Text.Json.Serialization;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VsExample.Application.Features.Questionnaire.Dto;
using VsExample.Application.Features.Questionnaire.Queries;
using VsExample.Domain.Entities.Questionaire;
using VsExample.Domain.Entities.Questions;
using VSExample.Web.ViewModels.Questionnaires;

namespace VSExample.Web.Controllers;

public class QuestionnairesController : Controller
{
    private readonly ILogger<QuestionnairesController> _logger;
    private readonly IMediator _mediator;

    public QuestionnairesController(ILogger<QuestionnairesController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery]int pageNumber = 1, int pageSize = 10)
    {
        GetQuestionnaires.Query query = new GetQuestionnaires.Query();
        
        Result<QuestionnaireQueryResult> result = await _mediator.Send(query);
        
        if (result.IsSuccess)
        {
            QuestionnairesIndexViewModel questionnairesIndexViewModel = new()
            {
                Title = "Questionnaire List",
                TotalCount = result.Value.TotalCount,
                Questionnaires = result.Value.Questionnaires,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            
            return View(questionnairesIndexViewModel);
        }
        
        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        GetQuestionnaire.Query query = new GetQuestionnaire.Query(id);
        
        Result<Questionnaire?> result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            QuestionnaireDetailsViewModel questionnaireDetailsViewModel = new()
            {
                Questionnaire = result.Value!
            };
            
            return View(questionnaireDetailsViewModel);
        }
        
        return NotFound();
    }
}