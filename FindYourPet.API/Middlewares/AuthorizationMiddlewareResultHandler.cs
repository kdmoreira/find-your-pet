using FindYourPet.Domain.Enums;
using FindYourPet.Domain.Extensions;
using FindYourPet.Domain.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace FindYourPet.API.Middlewares
{
    public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly Microsoft.AspNetCore.Authorization.Policy.AuthorizationMiddlewareResultHandler
            _defaultHandler = new();

        public async Task HandleAsync(
            RequestDelegate requestDelegate,
            HttpContext httpContext,
            AuthorizationPolicy authorizationPolicy,
            PolicyAuthorizationResult policyAuthorizationResult)
        {
            var jsonOptions = (IOptions<JsonOptions>)httpContext.RequestServices.GetService(typeof(IOptions<JsonOptions>));

            if (!policyAuthorizationResult.Succeeded)
            {
                await WriteJsonResponseAsync<DefaultErrorResponse>(httpContext, jsonOptions?.Value ?? new JsonOptions(), HttpStatusCode.Unauthorized,
                    new DefaultErrorResponse(
                        "Expired or invalid Token. Please authenticate again if this problem persists.",
                        Enum.GetName(ErrorCodeEnum.Unauthorized)?.ToSnakeCase().ToLower()));
                return;
            }

            await _defaultHandler.HandleAsync(requestDelegate, httpContext, authorizationPolicy, policyAuthorizationResult);
        }

        public async Task WriteJsonResponseAsync<T>(HttpContext context, JsonOptions options, HttpStatusCode statusCode, object jsonObject)
        {
            await using MemoryStream stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, jsonObject, typeof(T), options.JsonSerializerOptions);
            ReadOnlyMemory<byte> readOnlyMemory = new ReadOnlyMemory<byte>(stream.ToArray());

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json; charset=utf-8";

            await context.Response.Body.WriteAsync(readOnlyMemory);
            await context.Response.Body.FlushAsync();
        }
    }
}
