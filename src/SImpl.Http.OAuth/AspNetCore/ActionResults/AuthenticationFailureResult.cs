﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SImpl.Http.OAuth.AspNetCore.ActionResults
{
    public class AuthenticationFailureResult : ActionResult
    {
        public HttpRequestMessage RequestMessage { get; }
        public string ErrorMessage { get; }

        public AuthenticationFailureResult(HttpRequestMessage requestMessage, string errorMessage)
        {
            RequestMessage = requestMessage;
            ErrorMessage = errorMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var payload = new Dictionary<string, string>
            {
                {"error", ErrorMessage},
            };

            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = RequestMessage,
                Content = new StringContent(JsonConvert.SerializeObject(payload))
            };

            return Task.FromResult(response);
        }
    }
}