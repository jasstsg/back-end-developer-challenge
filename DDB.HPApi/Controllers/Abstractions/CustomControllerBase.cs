using Microsoft.AspNetCore.Mvc;

namespace DDB.HPApi.Controllers.Abstractions
{
    public class CustomControllerBase : ControllerBase
    {
        protected ILogger _logger;

        public CustomControllerBase(ILogger logger)
        {
            _logger = logger;
        }

        protected ObjectResult OkResult(object? value, string message)
        {
            _logger.LogDebug($"HTTP 200 - Ok: ${message}");
            return Ok(value);
        }

        protected ObjectResult BadRequestResponse(string reason)
        {
            _logger.LogError($"HTTP 400 - Bad Request: {reason}");
            return BadRequest(reason);
        }

        protected ObjectResult InternalServerErrorResponse(Exception ex)
        {
            _logger.LogError($"HTTP 500 - Internal Service Error:\n\t{ex}");
            return Problem("Internal Server Error", statusCode: 500);
        }
    }
}
