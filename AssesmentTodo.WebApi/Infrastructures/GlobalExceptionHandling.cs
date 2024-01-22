﻿using AssesmentTodo.Infrastructure;
using System.Net;
using System.Text.Json;
using AssesmentTodo.Application;

namespace AssesmentTodo.WebApi.Infrastructures
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionHandlingAsync(context, ex);
            }
        }

        public class IgnoreResultManipulatorAttribute : Attribute
        {

        }

        public Task ExceptionHandlingAsync(HttpContext context, Exception ex)
        {
            var executeEndpoint = context.GetEndpoint();
            bool hasIgnoreManipulateResult = false;

            if (executeEndpoint != null)
            {
                var att = executeEndpoint.Metadata.OfType<IgnoreResultManipulatorAttribute>();
                if (att.Any())
                    hasIgnoreManipulateResult = true;
            }

            BaseResponse result = new BaseResponse
            {
                IsSuccess = false,
                Path = context.Request.Path.HasValue ? context.Request.Path.Value : "",
                Method = context.Request.Method
            };

            bool errorBecauseOfValidationException = false;
            switch (ex)
            {
                case BadRequestException badRequestException:
                    result.StatusCode = (int)HttpStatusCode.BadRequest;
                    result.Message = badRequestException.Message;
                    break;
                case NotFoundException notFoundException:
                    result.StatusCode = (int)HttpStatusCode.NotFound;
                    result.Message = notFoundException.Message;
                    break;
                default:
                    result.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result.Message = ex.Message;

                    if (ex.InnerException != null)
                    {
                        result.InnerMessage = ex.InnerException.Message;
                    }
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.StatusCode;

            var jsonSetting = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            if (errorBecauseOfValidationException)
                jsonSetting.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;

            if (!hasIgnoreManipulateResult)
                return context.Response.WriteAsync(JsonSerializer.Serialize(result, jsonSetting));
            else
                return context.Response.WriteAsync(JsonSerializer.Serialize(result.Message));
        }
    }
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandling>();
        }
    }
}
