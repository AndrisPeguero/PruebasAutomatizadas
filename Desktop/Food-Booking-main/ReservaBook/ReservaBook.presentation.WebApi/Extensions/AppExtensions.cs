using Microsoft.AspNetCore.Builder;

namespace ReservaBook.presentation.WebApi.Extensions
{
    public static class AppExtensions
    {


        public static void UseSwaggerExtension(this  IApplicationBuilder app, IEndpointRouteBuilder route)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                var versionDescriptions = route.DescribeApiVersions();
                if (versionDescriptions != null && versionDescriptions.Any()) 
                {
                    foreach (var apiversion in versionDescriptions)
                    {
                        var url = $"/swagger/{apiversion.GroupName}/swagger.json";
                        var name = $"Reserva food Api - {apiversion.GroupName.ToUpperInvariant()}";
                        opt.SwaggerEndpoint(url,name);
                        
                    }
                
                
                }

            });
        
        
        
        
        
        
        
        
        
        
        
        
        }



    }

}
