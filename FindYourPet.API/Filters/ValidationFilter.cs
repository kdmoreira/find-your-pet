using FindYourPet.Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace FindYourPet.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        private readonly ILogger<ValidationFilter> _logger;

        public ValidationFilter(ILogger<ValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Where(v => v.Value.Errors.Count > 0)
                    .SelectMany(v =>
                        v.Value.Errors.Select(e => new ValidationError() { Field = v.Key.TrimStart('$', '.'), Message = e.ErrorMessage }))
                    .ToList();

                var responseData = new DefaultErrorResponse()
                {
                    Errors = errors,
                    Message = "Validation Error"
                };

                _logger.LogError(new Exception(JsonConvert.SerializeObject(errors)), $"Validation error in {context.ActionDescriptor.DisplayName}");

                context.Result = new UnprocessableEntityObjectResult(responseData);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
