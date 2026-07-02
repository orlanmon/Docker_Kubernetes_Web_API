
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.Extensions.Options;





namespace OAuth2.Web.API
{
    public class Program
    {
        public static void Main(string[] args)
        {


            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            // Support for Minimal APIs
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddOpenApi();


            // Add the following to Conifgure CORS Settings

            
            builder.Services.AddCors(options =>
            {
                // Configure SpecificOriginsPolicy
                var specificOriginsSection = builder.Configuration.GetSection("CorsSettings:SpecificOriginsPolicy");
                if (specificOriginsSection.Exists())
                {
                    var allowedOrigins = specificOriginsSection["AllowedOrigins"]?.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    var allowedHeaders = specificOriginsSection["AllowedHeaders"]?.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    var allowedMethods = specificOriginsSection["AllowedMethods"]?.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    var allowCredentials = specificOriginsSection.GetValue<bool>("AllowCredentials");

                    options.AddPolicy("SpecificOriginsPolicy", policy =>
                    {
                        if (allowedOrigins != null && allowedOrigins.Length > 0)
                        {
                            policy.WithOrigins(allowedOrigins);
                        }
                        if (allowedHeaders != null && allowedHeaders.Length > 0)
                        {
                            policy.WithHeaders(allowedHeaders);
                        }
                        if (allowedMethods != null && allowedMethods.Length > 0)
                        {
                            policy.WithMethods(allowedMethods);
                        }
                        if (allowCredentials)
                        {
                            policy.AllowCredentials();
                        }
                    });
                };
            });

            var app = builder.Build();

            // Use CORS Policy
            app.UseCors("SpecificOriginsPolicy");

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();


            //}


            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();  // Maps attribute-routed controllers

            app.Run();
        }
    }
}
