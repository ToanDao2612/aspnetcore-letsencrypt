﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.LetsEncrypt.Internal.Extensions {
    internal static class AcmeChallengeMiddlewareExtensions {
        public static IApplicationBuilder UseAcmeChallengeListener(this IApplicationBuilder app)
        {
            var router = new RouteBuilder(app)
                .MapMiddlewareGet("/.well-known/acme-challenge/{*acmeToken}", a => a.UseMiddleware<AcmeChallengeListener>())
                .Build();

            app.UseRouter(router);
            return app;
        }

        public static IServiceCollection AddAcmeChallengeListener(this IServiceCollection services)
        {
            services.AddRouting();
            return services;
        }
    }
}
