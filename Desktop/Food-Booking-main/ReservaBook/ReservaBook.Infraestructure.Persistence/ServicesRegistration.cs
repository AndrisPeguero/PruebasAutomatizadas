using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservaBook.Core.Domain.Entities;
using ReservaBook.Core.Domain.Interfaces;
using ReservaBook.Infraestructure.Persistence.Contexts;
using ReservaBook.Infraestructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservaBook.Infraestructure.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistencesLayerIOC(this IServiceCollection Service, IConfiguration config) 
        {

            #region context configuration
            if (config.GetValue<bool>("useInMemoryDatabase"))
            {

                Service.AddDbContext<ReservaBookContext>
                    (opt => opt.UseInMemoryDatabase("ReservaDbMemory"));
            }
            else
            {

                var connectionStrings = config.GetConnectionString("DefaultConnection");
                Service.AddDbContext<ReservaBookContext>(
                   (ServiceProvider, opt) =>
                   {

                       opt.EnableSensitiveDataLogging();
                       opt.UseSqlServer(connectionStrings,
                        m => m.MigrationsAssembly(typeof(ReservaBookContext)
                      .Assembly.FullName));

                   },
                    contextLifetime: ServiceLifetime.Scoped,
                    optionsLifetime: ServiceLifetime.Scoped


                );

            }


            #region repositories IOC
            Service.AddScoped<IRestauranteRepository, RestauranteRepository>();
            Service.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            Service.AddScoped<IMesaRepository,MesaRepository>();
            Service.AddScoped<IPlatoRepository, PlatoRepository>();
            Service.AddScoped<IMenuRepository, MenuRepository>();
            Service.AddScoped<IPlatoMenuRepository, PlatoMenuRepository>();
            Service.AddScoped<IPedidoRepository,PedidoRepository>();
            Service.AddScoped<IPedidoPlatoRepository, PedidoPlatoRepository>();
            Service.AddScoped<IReseñaRepository, ReseñaRepository>();
            Service.AddScoped<IReservaResporitory, ReservaRepository>();
            Service.AddScoped<IPagoRepository, PagoRepository>();
            Service.AddScoped<INotificacionRepository, NotificacionRepository>();
            #endregion



        }

        #endregion




    }

}

