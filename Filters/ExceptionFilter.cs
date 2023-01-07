using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            var response = new ObjectResult(new
            {
                Message = "An internal error has occured."
            })
            {
                StatusCode = (int)System.Net.HttpStatusCode.InternalServerError
            };

            context.Result = response;

            base.OnException(context);
        }
    }
}
