using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VsExample.Application.Features.QuestionSets.Dto;
using VsExample.Application.Features.QuestionSets.Queries;
using VsExample.Domain.Entities.Questions;
using VSExample.Web.ViewModels.QuestionSets;

namespace VSExample.Web.Controllers;

public class QuestionSetsController: Controller
{
    private readonly ILogger<QuestionSetsController> _logger;
    private readonly IMediator _mediator;

    public QuestionSetsController(ILogger<QuestionSetsController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index([FromQuery]int pageNumber = 1, int pageSize = 10)
    {
        GetQuestionSets.Query query = new GetQuestionSets.Query();
        
        Result<QuestionSetsQueryResult> result = await _mediator.Send(query);
        
        if (result.IsSuccess)
        {
            QuestionSetsIndexViewModel questionnairesIndexViewModel = new()
            {
                Title = "Questionnaire List",
                TotalCount = result.Value.TotalCount,
                QuestionSets = result.Value.QuestionSets,
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
        // QuestionSet questionSet = QuestionSetRepository.GetQuestionSetById(1);
        //
        // string json = JsonSerializer.Serialize(questionSet, new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull});
        //
        // QuestionSet questionSetCopy = JsonSerializer.Deserialize<QuestionSet>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        GetQuestionSet.Query query = new GetQuestionSet.Query(id);
        
        Result<QuestionSet?> result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            QuestionSetDetailsViewModel questionSetDetailsViewModel = new()
            {
                QuestionSet = result.Value!
            };
            
            return View(questionSetDetailsViewModel);
        }
        
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> SubmitAnswers(IFormCollection form)
    {
        return RedirectToAction("Index");
    }
}