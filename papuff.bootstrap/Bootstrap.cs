using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using papuff.datainfra.ORM;
using papuff.datainfra.Persistence;
using papuff.datainfra.Transaction;
using papuff.domain.Interfaces.Repositories;
using papuff.domain.Interfaces.Services.Base;
using papuff.domain.Interfaces.Services.Core;
using papuff.domain.Interfaces.Services.Swap;
using papuff.domain.Notifications.Events;
using papuff.services.Services.Base;
using papuff.services.Services.Core.pegabicho.service.Services.Core;
using papuff.services.Services.Swap;

namespace papuff.bootstrap {
    public static class Bootstrap {
        public static void Configure(IServiceCollection services, string conn) {
            #region [ context ]

            // # db context strongly typed
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));

            #endregion

            #region [ kernel ]

            /// <summary>
            /// use this to interact with db transactions
            /// </summary>
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            /// <summary>
            /// use this to inject base events into internal service
            /// </summary>
            services.AddScoped(typeof(IServiceBase), typeof(ServiceBase));
            
            /// <summary>
            /// use this manipule internal entities from db source
            /// </summary>
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            /// <summary>
            /// use this to inject message notifications into internal services application
            /// </summary>
            services.AddScoped(typeof(IEventNotifier), typeof(EventNotifier));
            
            /// <summary>
            /// use this to inject db context for internal services applications
            /// </summary>
            services.AddScoped(typeof(IServiceApp<>), typeof(ServiceApp<>));

            #endregion

            #region [ core ]

            services.AddScoped(typeof(IServiceUser), typeof(ServiceUser));

            #endregion

            #region [ swap ]

            services.AddScoped(typeof(ISwapSiege), typeof(SwapSiege));

            #endregion
        }
    }
}
