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

    // [HttpGet]
    // public async Task<IActionResult> Details(int questionSetId)
    [HttpPost]
    public async Task<IActionResult> Details(int questionSetId, Dictionary<string, List<string>>? dataPoints)
    {
        // QuestionSet questionSet = QuestionSetRepository.GetQuestionSetById(1);
        //
        // string json = JsonSerializer.Serialize(questionSet, new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull});
        //
        // QuestionSet questionSetCopy = JsonSerializer.Deserialize<QuestionSet>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        GetQuestionSet.Query query = new GetQuestionSet.Query(questionSetId);
        
        Result<QuestionSet?> result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            QuestionSetDetailsViewModel questionSetDetailsViewModel = new()
            {
                QuestionSet = result.Value!,
                DataPoints = dataPoints
            };
            
            return View("Details", questionSetDetailsViewModel);
        }
        
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Submit(IFormCollection form)
    {
        // Process the natural form elements
        Dictionary<string, List<string?>> dataPoints = form
            .ToDictionary(
                kvp => kvp.Key, 
                kvp => kvp.Value.ToList() // Convert StringValues to List<string>
            );
        
        // Process the files
        List<CapturedDataPointsViewModel.FileContent> files = new List<CapturedDataPointsViewModel.FileContent>();
        if (form.Files.Count > 0)
        {
            foreach (var file in form.Files)
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                
                files.Add(new CapturedDataPointsViewModel.FileContent
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    FileData = memoryStream.ToArray() // Store file contents as byte array
                });
            }
        }
        
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("{dataPointCount} data points found", dataPoints.Count(x => !x.Key.StartsWith("__")));
            _logger.LogDebug("{filesCount} files found", files.Count());
            _logger.LogDebug("{otherFormElements} other form elements found", dataPoints.Count(x => x.Key.StartsWith("__")));
        }
        
        int questionSetId = int.Parse(form["__QuestionSetId"]);
        
        GetQuestionSet.Query query = new GetQuestionSet.Query(questionSetId);
        
        Result<QuestionSet?> result = await _mediator.Send(query);

        CapturedDataPointsViewModel capturedDataPoints = new() { QuestionSet = result.Value, DataPoints = dataPoints, Files = files };
        
        return View("CapturedDataPoints", capturedDataPoints);
    }
}