using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;


namespace SecurityToken.ProviderJWT
{
    public static class ConfigStartupTokenExtensions
    {

        public static IServiceCollection RegisterConfigStartupToken(this IServiceCollection services, 
                                                                         string issuer, 
                                                                         string audience,
                                                                         string subject,
                                                                         string secretKey)
        {

            JwtTokenOptions.Values = JwtTokenOptions.Factory(issuer, audience, secretKey, subject);            

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(option =>
               {
                   option.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,

                       ValidIssuer = JwtTokenOptions.Values.Issuer,
                       ValidAudience = JwtTokenOptions.Values.Audience,
                       IssuerSigningKey = JWTSecurityKey.Create(JwtTokenOptions.Values.Secret_Key)
                   };

                   option.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                           return Task.CompletedTask;
                       },
                       OnTokenValidated = context =>
                       {
                           Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                           return Task.CompletedTask;
                       }
                   };
               });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("TotalAccess", policy => policy.RequireClaim("Administrador"));
            });

            return services;
        }
    }
}
