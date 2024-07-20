using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProgrammersBlog.Shared.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Data.SqlTypes;
using Serilog;
using Microsoft.Extensions.Logging;

namespace ProgrammersBlog.Mvc.Filters
{
    public class MvcExceptionFilter:IExceptionFilter
    {
        private readonly IHostEnvironment _environment;
        private readonly IModelMetadataProvider _metadataProvider;
        private readonly ILogger<MvcExceptionFilter> _logger;
        public MvcExceptionFilter(IHostEnvironment hostEnvironment, IModelMetadataProvider metadataProvider, ILogger<MvcExceptionFilter> logger)
        {
            _environment = hostEnvironment;
            _metadataProvider = metadataProvider;
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            if(_environment.IsDevelopment())
            {
                MvcErrorModel model = new MvcErrorModel()
                {
                    Message = "Üzgünüz, işleminiz sırasında beklenmedik bir hata oluştu lütfen daha sonra tekrar deneyiniz."
                };
                var result = new ViewResult { ViewName = "Errors/Error" };
                context.ExceptionHandled = true;
                switch (context.Exception)
                {
                    case SqlNullValueException:
                        Log.Information(context.Exception,context.Exception.Message);
                        result.ViewName = "Errors/Error500";
                        result.StatusCode = 500;
                        break;
                    case InvalidOperationException:
                        _logger.LogInformation(context.Exception, context.Exception.Message);
                        result.ViewName = "Errors/Error400";
                        result.StatusCode = 400; // Bad Request
                        break;
                    case ArgumentException:
                        _logger.LogInformation(context.Exception, context.Exception.Message);
                        result.ViewName = "Errors/Error400";
                        result.StatusCode = 400; // Bad Request
                        break;
                    case HttpRequestException:
                        _logger.LogInformation(context.Exception, context.Exception.Message);
                        result.ViewName = "Errors/Error500";
                        result.StatusCode = 500; // Internal Server Error
                        break;
                    // Add more cases for popular exceptions as needed
                    default:
                        _logger.LogInformation(context.Exception, context.Exception.Message);
                        // Default handling for other exceptions
                        result.ViewName = "Errors/Error";
                        result.StatusCode = 500; // Internal Server Error
                        break;
                }


                result.ViewData = new ViewDataDictionary(_metadataProvider, context.ModelState);
                result.ViewData.Add("MvcErrorModel", model);
                context.Result = result;
            }
        }
    }
}
