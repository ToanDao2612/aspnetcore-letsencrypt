﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WebApp.Internal.Abstractions;

namespace WebApp.Internal {
    internal class AcmeChallengeListener {
        public AcmeChallengeListener(RequestDelegate next) { }

        public async Task InvokeAsync(HttpContext context, IHttpChallengeResponseStore responseStore) {
            string token = context.GetRouteValue("acmeToken") as string;
            if (!responseStore.TryGetResponse(token, out string response)) {
                await context.Response.WriteAsync("ACME challenge token invalid");
                return;
            }

            context.Response.ContentLength = response.Length;
            context.Response.ContentType = "application/octet-stream";
            await context.Response.WriteAsync(response);
        }
    }
}