using Microsoft.Extensions.DependencyInjection;
using ReservaBook.Core.Aplication.Interfaces;
using ReservaBook.Core.Aplication.Services;
using System.Reflection;

namespace ReservaBook.Core.Aplication
{
    public static class ServiceRegistration
    {

        public static void AddServicesLayerIOC(this IServiceCollection services)
        {

            #region generalConfiguration
            services.AddAutoMapper(opt => { }, Assembly.GetExecutingAssembly());
            #endregion



            #region services Registration IOC
            services.AddScoped(typeof(IGenericService<,,,>),typeof(GenericService<,,,>));
            services.AddScoped<IRestauranteServices, RestauranteService>();
            services.AddScoped<IMesaService, MesaService>();
            services.AddScoped<IPlatoService, PlatoService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPlatoMenuServices, PlatoMenuService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IPedidoPlatoService, PedidoPlatoService>();
            services.AddScoped<IReseñaService, ReseñaService>();
            services.AddScoped<IReservaRestauranteService, ReservaService>();
            services.AddScoped<IPagoService, PagoService>();
            services.AddScoped<INotificacionService, NotificacionService>();
            #endregion
                

        }
        


    }
}
